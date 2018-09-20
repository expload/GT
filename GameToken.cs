namespace Expload {

    using Pravda;
    using System;

    [Program]
    public class GameToken {

        public Mapping<Bytes, double> Balance = 
            new Mapping<Bytes, double>();

        public Mapping<Bytes, int> WhiteList =
            new Mapping<Bytes, int>();
        
        private void assertIsOwner() {
            if (Info.Sender() != Info.Owner(Info.ProgramAddress())) {
              Error.Throw("Only owner of the program can do that.");
            }
        }

        // Gives amount of GameTokens to recipient.
        public void Give(Bytes recipient, double amount) {
            assertIsOwner();
            double lastBalance = Balance.getDefault(recipient, 0);
            double newBalance = lastBalance + amount;
            Balance.put(recipient, newBalance);
        }

        // Remove amount of GameTokens from balance of address.
        public void Burn(Bytes address, double amount) {
            assertIsOwner();
            double balance = Balance.getDefault(address, 0);
            if (balance >= amount) {
                Balance.put(address, balance - amount);
            } else {
                Error.Throw("Not enough funds");
            }
        }

        // Add address to white list
        public void WhiteListAdd(Bytes address) {
            assertIsOwner();
            WhiteList.put(address, 1);
        }

        // Remove address from white list.
        public void WhiteListRemove(Bytes address) {
            assertIsOwner();
            WhiteList.put(address, 0);
        }

        // Check address is white listed/
        public bool WhiteListCheck(Bytes address) {
            return WhiteList.getDefault(address, 0) == 1;
        }

        // Send GameTokens from transaction Sender to recipient.
        // Recipient should be present in the Game Developers white list.
        public void Spend(Bytes recipient, double amount) {
            if (WhiteListCheck(recipient)) {
                Bytes sender = Info.Sender();
                double senderBalance = Balance.getDefault(sender, 0);
                double recipientBalance = Balance.getDefault(recipient, 0);
                if (senderBalance >= amount) {
                    Balance.put(sender, senderBalance - amount);
                    Balance.put(recipient, recipientBalance + amount);
                } else {
                    Error.Throw("Not enough funds");
                }
            }
        }
    }
}
