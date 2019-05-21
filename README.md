# Binary-Client-Server
## Overwiew
This project implements simple binary queries protocol between client and server. Client asks for answer of math operation, which are calculating and sends back.
## Description
Data is send via TCP protocol. Binary protocol, which is inside look like:
- operation (3 bits)
- status field (4 bits)
- pointer to the data lenght (32 bits)
- data field (up to 2^32 bits)
