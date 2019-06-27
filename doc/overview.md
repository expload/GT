# Overview

XGold is a virtual currency lives inside [Expload Platform](https://developers.expload.com/documentation/).
It similar to Ethereum's ERC-20 token by the nature. But the main purpose is
providing in-app purchasing in games integrated with Expload Platform.
A user can buy XGold for the real currency like Euro and spend in any game on the Expload Platform.

Since XGold program is a regular [Pravda](https://developers.expload.com/documentation/pravda/) program,
thus it can be compiled and deployed and called as any other Pravda program.

Source code of the program can be found on Github: `https://github.com/expload/XGold`.

Documentation about how to compile and deploy program
can be found [here](https://developers.expload.com/documentation/pravda/) in section *Compiling the Program*.

## Terms

Term         | Description
-------------| -----------
Sender       | Account who signed the transaction that contains calls program methods.
Whitelisting | Is a process of adding address into the whitelist of the XGold program. The calling of some methods is permitted only for addresses which are in the whitelist. Thus, before calling such methods you must add all needed addresses in the whitelist. But keep in mind that adding/removing can be done only by XGold program's owner.

## Public methods

The methods below are public and can be called directly or from another program.

### MyBalance

Returns XGold balance of the Sender's acount.

### Give

Gives the recipient the given amount of XGold.

*Note. This method can be executed by only XGold program's owner.*

### Burn

Withdraws amount of XGold from the recipient's balance.

*Note. This method can be executed by only XGold program's owner.*

### Spend

Transfers the given amount of XGold from the Sender's address to the recipient address.
The recipient address should be in the whitelist of the XGold program.

### Refund

Transfers the given amount of XGold from one account (Sender) to another (Recipient).
The Sender's account should be presented in the whitelist of the XGold program.

### WhitelistAdd

Adds the given address into the whitelist of the XGold program.

*Note. This method can be executed by only XGold program's owner.*

### WhitelistRemove

Removes the given address from the whitelist of the XGold program.

*Note. This method can be executed by only XGold program's owner.*

## Public events

There are some events which emitted by corresponding public methods:

- Spend
- Give
- Burn
- Refund

All events are emitted only in a successful case when the action is done.

Documentation about how to get the events can be found [here](https://developers.expload.com/documentation/pravda/integration/node-api/)
