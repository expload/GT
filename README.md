# XGold
Expload XGold Pravda Program

## Whitelisting of the known addresses

There is the secret called "whitelist" in the CI Drone. That secret contains
a list of known addresses. It is just a string with addresses delimited by ";".

If you made changes in the whitelist, you should create and push a new tag to reflect
these changes in Krivda.

## Deploy program

To deploy the program in the Krivda you should create and push a new tag.
If the program was not deployed in Krivda before (for instance, if Krivda was wiped),
it will be `deployed`, otherwise - `updated`. In any case, all known addresses
will be whitelisted.
