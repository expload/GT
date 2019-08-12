namespace Expload {

    using Pravda;
    using System;

    [Program]
    public class XGold {
        public static void Main() { }

        private Mapping<Bytes, Int64> Balance =
            new Mapping<Bytes, Int64>();

        private Mapping<Bytes, sbyte> WhiteList =
            new Mapping<Bytes, sbyte>();

        // Gives amount of XGold to recipient.
        public void Give(Bytes recipient, Int64 amount) {
            assertIsOwner();
            Require(amount > 0, "Amount must be positive");

            Int64 lastBalance = Balance.GetOrDefault(recipient, 0);
            Int64 newBalance = lastBalance + amount;
            Balance[recipient] = newBalance;
            Log.Event("Give", new EventData(recipient, amount));
        }

        // Remove amount of XGold from balance of address.
        public void Burn(Bytes address, Int64 amount) {
            assertIsOwner();
            Require(amount > 0, "Amount must be positive");
            Int64 balance = Balance.GetOrDefault(address, 0);
            if (balance >= amount) {
                Balance[address] = balance - amount;
                Log.Event("Burn", new EventData(address, amount));
            } else {
                Error.Throw("Not enough funds for Burn");
            }
        }

        // Add address to white list
        public void WhiteListAdd(Bytes address) {
            assertIsOwner();
            WhiteList[address] = 1;
        }

        // Remove address from white list.
        public void WhiteListRemove(Bytes address) {
            assertIsOwner();
            WhiteList[address] = 0;
        }

        public Int64 MyBalance()
        {
            Bytes sender = Info.Sender();
            Int64 senderBalance = Balance.GetOrDefault(sender, 0);
            return senderBalance;
        }

        // Send XGold from transaction Sender to recipient.
        // Recipient should be present in the Game Developers white list.
        public void Spend(Bytes recipient, Int64 amount) {
            if (WhiteListCheck(recipient)) {
                Require(amount > 0, "Amount must be positive");
                Bytes sender = Info.Sender();
                Int64 senderBalance = Balance.GetOrDefault(sender, 0);
                Int64 recipientBalance = Balance.GetOrDefault(recipient, 0);
                if (senderBalance >= amount) {
                    Balance[sender] = senderBalance - amount;
                    Balance[recipient] = recipientBalance + amount;
                    Log.Event("Spend", new EventData(recipient, amount));
                } else {
                    Error.Throw("XGoldError: Not enough funds for Spend operation");
                }
            } else Error.Throw("Operation denied");
        }

        // Check if XGold program was called from another program
        private bool IsCalledFrom(Bytes address) {
            if(Info.Callers().Length < 2) return false;
            if(Info.Callers()[Info.Callers().Length-2] != address) return false;
            return true;
        }

        // Send XGolds from sender to recipient.
        // Sender should be presented in the Game Developers white list.
        // Can only be called from the program presented in the Game Developers white list or by such program.
        public void Refund(Bytes sender, Bytes recipient, Int64 amount) {
            if (WhiteListCheck(sender) && (Info.Sender() == sender || IsCalledFrom(sender))) {
                Require(amount > 0, "Amount must be positive");
                Int64 senderBalance = Balance.GetOrDefault(sender, 0);
                Int64 recipientBalance = Balance.GetOrDefault(recipient, 0);
                if (senderBalance >= amount) {
                    Balance[sender] = senderBalance - amount;
                    Balance[recipient] = recipientBalance + amount;
                    Log.Event("Refund", new RefundEventData(sender, recipient, amount));
                } else {
                    Error.Throw("XGoldError: Not enough funds for Refund operation");
                }
            } else Error.Throw("Operation denied");
        }

        //// Private methods

        // Check address is white listed/
        private bool WhiteListCheck(Bytes address)
        {
            return WhiteList.GetOrDefault(address, 0) == 1;
        }

        private void assertIsOwner()
        {
            if (Info.Sender() != Info.ProgramAddress())
            {
                Error.Throw("Only owner of the program can do that.");
            }
        }

        private void Require(Boolean condition, String message)
        {
            if (!condition)
            {
                Error.Throw(message);
            }
        }
    }

    class EventData {
        public EventData(Bytes recipient, Int64 amount) {
            this.recipient = recipient;
            this.amount = amount;
        }
        public Int64 amount;
        public Bytes recipient;
    }

    class RefundEventData {
        public RefundEventData(Bytes sender, Bytes recipient, Int64 amount) {
            this.sender = sender;
            this.recipient = recipient;
            this.amount = amount;
        }
        public Bytes sender;
        public Bytes recipient;
        public Int64 amount;
    }
}
