CREATE DATABASE TEMPFINALPRECTICE;

CReate TABLE EMPLOYEE(
 id int not null
)
Alter table EMPLOYEE

add CONSTRAINT CHk_EMPLOYEE_id
check(id >2)

add CONSTRAINT PK_EMPLOYEE_id
Primary key (id);


SELECT  LastName,Count(LastName)
FROM Employees
Where city ='London'
GROUP BY LastName
having Count(LastName) >=1
Order by LastName DESC
--DESC


-- OFFSET AND FETCH WORK WITH ONLY ORDER BY

SELECT *
FROM Employees
Order by LastName
OFFSET 2 ROWS FETCH NEXT 3 ROWs ONLY;

-- NOT WORK AS ALONE
/* SELECT *
FROM Employees
OFFSET 2 ROWS FETCH NEXT 3 ROWs ONLY;
*/
/*
SELECT  LastName ,TitleOfCourtesy , 
CASE 
WHEN TitleOfCourtesy ='Dr.' THEN 'AALA'
WHEN TitleOfCourtesy ='Mr.' THEN 'WALA'
WHEN TitleOfCourtesy ='Ms.' THEN 'NOT SO WALA'
WHEN TitleOfCourtesy ='Mrs.' THEN 'WALi'
END as newTitle
FROM Employees
*/

/*
SELECT  SUBSTRING(LastName,1,4)
FROM Employees
*/
SELECT  LastName
FROM Employees

-- get right substring of 4 char

SELECT  Right(LastName,4)
FROM Employees

-- get left substring of 4 char

SELECT  Left(LastName,4)
FROM Employees

-- find length of sstring
SELECT  Len(LastName)
FROM Employees

--Replace e to '-'
SELECT  REPLACE(LastName,'e','-')
FROM Employees

SELECT  UPPER(LastName)
FROM Employees

SELECT  LOWER(LastName)
FROM Employees

/*
SELECT  String_Split(LastName,'e',[])
FROM Employees
*/

--SELECT GETDATE()
SELECT *
FROM Employees E , (SELECT EmployeeID, CONCAT(FirstName,' ',LastName) as name FROM Employees WHERE City ='London') A
WHERE A.EmployeeID = E.EmployeeID;

-----------------------------------------------views--------------------------------------------------------

CREATE VIEW myView1
AS 
SELECT FirstName,LastName
FROM Employees

Select * FROM myView1

Alter view myView1
AS
SELECT EmployeeID,FirstName,LastName
FROM Employees

