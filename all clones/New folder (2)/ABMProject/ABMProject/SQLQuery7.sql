
SELECT CONCAT(U.firstName,' ',U.lastName), U.email,U.address,U.phoneNumbers,A.dateOpened,A.Amounts,A.accountNumber,U.dateOfBirth,ATT.accountTypeName
FROM Users U 
JOIN Customers C ON C.uid= U.userId 
JOIN Accounts A ON A.customerId= C.CustomerId 
JOIN AccountType ATT ON A.accountTypeId=ATT.accountType
where  U.userId= 7
