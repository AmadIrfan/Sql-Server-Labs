--Q1
SELECT Customers.CustomerID, A.OrderID,A.OrderDate
FROM Customers
LEFT OUTER JOIN (SELECT * FROM Orders
				 )A
On A.CustomerID=Customers.CustomerID;

--Q2

SELECT Customers.CustomerID,A.OrderID, A.OrderDate
FROM Customers
LEFT OUTER JOIN (SELECT * FROM Orders)A
ON Customers.CustomerID = A.CustomerID
WHERE A.OrderID IS NULL;

--Q3

SELECT C.CustomerID,OrderID,OrderDate
From Customers C
JOIN (SELECT * 
	  FROM  ORDERS O
	  WHERE year(O.OrderDate)='1997' AND MONTH(O.OrderDate)='07'
		)Od
ON C.CustomerID=Od.CustomerID

--Q4

SELECT C.CustomerID,COUNT(C.CustomerID) as totalOrder
FROM Customers C 
JOIN (SELECT * 
	  FROM Orders O
	  WHERE O.OrderDate is not null)A
On C.CustomerID=A.CustomerID
Group BY C.CustomerID;

--Q5
--Q6
SELECT E.EmployeeID ,E.HireDate as Date
FROM Employees E,(SELECT * FROM Employees 
				WHERE Employees.HireDate > 1996-07-04 AND Employees.HireDate < 1097-08-04 )A
WHERE E.EmployeeID=A.EmployeeID;


--Q7
 SELECT  A.CustomerID,COUNT(A.CustomerID) as TotalOrders,COUNT(Orders.CustomerID) as totalquantity
FROM  Orders
JOIN  (SELECT * FROM Customers
WHERE Customers.Country ='USA')A
ON A.CustomerID=Orders.CustomerID
Group By A.CustomerID;

--Q8

SELECT C.CustomerID,CompanyName,OrderID,OrderDate
FROM Customers C
JOIN  (SELECT * FROM  Orders O 
	   WHERE O.OrderDate ='1997-07-04')A
ON C.CustomerID=A.CustomerID;

--Q9

SELECT COUNT(*) [Total Employee older than their managers]                
FROM Employees ,(SELECT BirthDate
							FROM Employees
							WHERE Title like '%Manager%')A
WHERE Employees.BirthDate < A.BirthDate; 

  
--Q10

SELECT Concat(Employees.FirstName,' ', Employees.LastName) [EmployeeName],year(GETDATE()) - year(Employees.BirthDate) AS Age ,YEAR(GETDATE()) -YEAR(A.BirthDate) [Manager Age]                
FROM Employees ,(SELECT BirthDate
							FROM Employees
							WHERE Title like '%Manager%')A
WHERE Employees.BirthDate < A.BirthDate; 

--Q11
SELECT p.ProductName,S.OrderDate
FROM Products p
Join (SELECT Orders.OrderID,Od.ProductID,Orders.OrderDate
	  FROM [Order Details] Od
	  JOIN Orders 
	  ON Orders.OrderID= Od.OrderID
	  WHERE Orders.OrderDate ='1997-08-08')S
ON p.ProductID=S.ProductID;


--Q12
--Q13
SELECT DISTINCT(ShipCountry)
FROM PROducts 
JOIN   (SELECT CategoryID
		FROM Categories
		WHERE  CategoryName='beverages')A
On Products.CategoryID=A.CategoryID
JOIN (SELECT *
	  FROM  [Order Details])B
On Products.ProductID=B.ProductID
join (SELECT * 
	  FROM  Orders)C
ON C.OrderID=B.OrderID;
  

