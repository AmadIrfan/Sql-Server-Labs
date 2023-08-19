--Q1
-- crass join 
  SELECT DISTINCT(CONCAT(Employees.FirstName,Employees.LastName)) 
From Employees
CROSS JOIN Orders;

SELECT *
FROM Orders
CROSS JOIN Products;  

--Natural JOIN
--Syntex or natural join is given blow but Natural join can't Works in MS SQL SERVER 
  SELECT *
FROM Orders
Natural JOIN Products;  

--Equi Join
  SELECT ContactName,ProductName
FROM Suppliers
JOIN Products
ON Suppliers.SupplierID=Products.SupplierID;
  

--INNER JOIN
  SELECT ContactName,ProductName
FROM Suppliers
 INNER JOIN Products
ON Suppliers.SupplierID=Products.SupplierID;
  

--LEFT OUTER JOIN
  SELECT ContactName,ProductName
FROM Suppliers
LEFT OUTER JOIN Products
ON Suppliers.SupplierID=Products.SupplierID;
  

--RIGHT OUTER JOIN
  SELECT ContactName,ProductName
FROM Suppliers
RIGHT OUTER JOIN Products
ON Suppliers.SupplierID=Products.SupplierID
  

--FULL OUTER JOIN
  SELECT ContactName,ProductName
FROM Suppliers
FULL OUTER JOIN Products
ON Suppliers.SupplierID=Products.SupplierID  

--Q2
--Difference between self or cross JOIN
SELECT S.ContactName
FROM Suppliers S
CROSS JOIN Suppliers E;

SELECT S.ContactName
FROM Suppliers S, Suppliers E;  
--Q3
  

SELECT Customers.CustomerID, Customers.CompanyName, Orders.OrderID, Orders.OrderDate 
FROM Customers 
INNER JOIN Orders  
ON Customers.CustomerID = OrderID;
  
--Q4

SELECT Customers.CustomerID,Orders.OrderID,Orders.OrderDate
FROM Customers
LEFT OUTER JOIN Orders
On Orders.CustomerID=Customers.CustomerID;


--Q5
(SELECT *
FROM Customers C
JOIN Orders O
On O.CustomerID<>C.CustomerID)
EXCEPT 
(
SELECT C.CustomerID,O.OrderID,O.OrderDate
FROM Orders O
JOIN Customers C
On O.CustomerID=C.CustomerID
WHERE O.ShippedDate is  null
);


--Q6

SELECT C.CustomerID,OrderID,OrderDate
From Customers C
JOIN ORDERS O
ON C.CustomerID=O.CustomerID
WHERE year(OrderDate)='1997' AND MONTH(OrderDate)='07'
 

--Q7
  
SELECT C.CustomerID,COUNT(C.CustomerID) as totalOrder
FROM Customers C 
JOIN Orders O
On C.CustomerID=O.CustomerID
WHERE OrderDate is not null
Group BY C.CustomerID;
 
--Q8

(SELECT * 
FROM Employees)
UNION ALL
SELECT * 
FROM Employees
UNION ALL
(SELECT * 
FROM Employees)
UNION ALL
SELECT * 
FROM Employees
UNION ALL
SELECT * 
FROM Employees;


--Q9
  
SELECT EmployeeID ,Employees.HireDate as Date
FROM Employees
WHERE Employees.HireDate > 1996-07-04 AND Employees.HireDate < 1097-08-04 ;
	  	
--Q10
  SELECT  Customers.CustomerID,COUNT(Customers.CustomerID) as TotalOrders,COUNT(Orders.CustomerID) as totalquantity
FROM Customers
JOIN Orders
ON Customers.CustomerID=Orders.CustomerID
WHERE Customers.Country ='USA'
Group By Customers.CustomerID;
  
--Q11
  
SELECT C.CustomerID,CompanyName,OrderID,OrderDate
FROM Customers C
JOIN  Orders O
ON C.CustomerID=O.CustomerID
WHERE O.OrderDate ='1997-07-04';

  

--Q12
  
SELECT COUNT(*) [Total Employee older than their managers]                
FROM Employees ,(SELECT BirthDate
							FROM Employees
							WHERE Title like '%Manager%')A
WHERE Employees.BirthDate < A.BirthDate; 

  
--Q13
  

SELECT Concat(Employees.FirstName,' ', Employees.LastName) [EmployeeName],year(GETDATE()) - year(Employees.BirthDate) AS Age ,YEAR(GETDATE()) -YEAR(A.BirthDate) [Manager Age]                
FROM Employees ,(SELECT BirthDate
							FROM Employees
							WHERE Title like '%Manager%')A
WHERE Employees.BirthDate < A.BirthDate; 

  
--Q14

  
SELECT p.ProductName,Orders.OrderDate
FROM Products p
Join [Order Details] Od
ON p.ProductID=Od.ProductID
JOIN Orders 
ON Orders.OrderID= Od.OrderID
WHERE Orders.OrderDate ='1997-08-08';
  

--2nd last
  SELECT *
  FROM Suppliers
  WHERE ContactName like '%Anne%'  

--Q16 
  
SELECT DISTINCT(ShipCountry)
FROM PROducts 
JOIN (SELECT CategoryID
		FROM Categories
		WHERE  CategoryName='beverages')A
On Products.CategoryID=A.CategoryID
JOIN [Order Details] Od
On Products.ProductID=Od.ProductID
join Orders
ON Orders.OrderID=Od.OrderID;
  