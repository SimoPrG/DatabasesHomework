USE TelerikAcademy
GO

--1.
--Write a SQL query to find the names and salaries of the employees
--that take the minimal salary in the company.
--Use a nested SELECT statement.
SELECT FirstName + ' ' + LastName AS Name, Salary FROM Employees
WHERE Salary = (SELECT MIN(Salary) FROM Employees);

--2.
--Write a SQL query to find the names and salaries of the employees
--that have a salary that is up to 10% higher than the minimal salary for the company.
SELECT FirstName + ' ' + LastName AS Name, Salary FROM Employees
WHERE Salary < (SELECT MIN(Salary) FROM Employees) * 1.1;

--3.
--Write a SQL query to find the full name, salary and department of the employees
--that take the minimal salary in their department.
--Use a nested SELECT statement.
SELECT FirstName + ' ' + MiddleName + ' ' + LastName AS [Full Name],
       Salary,
	   Name AS Department FROM Employees
INNER JOIN Departments ON Employees.DepartmentID = Departments.DepartmentID
WHERE Salary = (SELECT MIN(Salary) FROM Employees);

--4.
--Write a SQL query to find the average salary in the department #1.
SELECT AVG(Salary) AS [Average Salary in Department #1] FROM Employees
WHERE DepartmentID = 1;

--5.
--Write a SQL query to find the average salary in the "Sales" department.
SELECT AVG(Salary) AS [Average Salary in Sales Department] FROM Employees
INNER JOIN Departments ON Employees.DepartmentID = Departments.DepartmentID
WHERE Name = 'Sales';

--6.
--Write a SQL query to find the number of employees in the "Sales" department.
SELECT COUNT(*) AS [# of Employees in Sales] FROM Employees
INNER JOIN Departments ON Employees.DepartmentID = Departments.DepartmentID
WHERE Name = 'Sales';

--7.
--Write a SQL query to find the number of all employees that have manager.
SELECT COUNT(*) AS [# of Employees with Manager] FROM Employees
WHERE ManagerID IS NOT NULL;

--8.
--Write a SQL query to find the number of all employees that have no manager.
SELECT COUNT(*) AS [# of Employees with Manager] FROM Employees
WHERE ManagerID IS NULL;

--9.
--Write a SQL query to find all departments and the average salary for each of them.
SELECT Departments.Name AS Department, AVG(Employees.Salary) FROM Employees
INNER JOIN Departments ON Employees.DepartmentID = Departments.DepartmentID
GROUP BY Departments.Name;

--10.
--Write a SQL query to find the count of all employees in each department and for each town.
SELECT Departments.Name AS Department,
       Towns.Name AS Town,
	   COUNT(*) AS [# of Employees]
FROM Employees
INNER JOIN Departments ON Employees.DepartmentID = Departments.DepartmentID
INNER JOIN Addresses ON Employees.AddressID = Addresses.AddressID
INNER JOIN Towns ON Addresses.TownID = Towns.TownID
GROUP BY Departments.Name, Towns.Name;

--11.
--Write a SQL query to find all managers that have exactly 5 employees.
--Display their first name and last name.
SELECT FirstName + ' ' + LastName AS [Managers with 5 Employees] FROM Employees
WHERE EmployeeID IN (SELECT m.EmployeeID FROM Employees m
                     INNER JOIN Employees e ON m.EmployeeID = e.ManagerID
					 GROUP BY m.EmployeeID
					 HAVING COUNT(*) = 5);

--12.
--Write a SQL query to find all employees along with their managers.
--For employees that do not have manager display the value "(no manager)".
SELECT CONCAT(e.FirstName, ' ', e.LastName) AS Employee,
       ISNULL(m.FirstName + ' ' + m.LastName, '(no manager)') AS Manager
FROM Employees e
LEFT OUTER JOIN Employees m ON e.ManagerID = m.EmployeeID;

--13.
--Write a SQL query to find the names of all employees whose last name
--is exactly 5 characters long. Use the built-in LEN(str) function.
SELECT FirstName, LastName FROM Employees
WHERE LEN(LastName) = 5;

--14.
--Write a SQL query to display the current date and time in the following format
--"day.month.year hour:minutes:seconds:milliseconds".
SELECT FORMAT(GETDATE(), 'dd.MM.yyyy HH:mm:ss:fff');

--15.
--Write a SQL statement to create a table Users.
--Users should have username, password, full name and last login time.
--Choose appropriate data types for the table fields. Define a primary key column with a primary key constraint.
--Define the primary key column as identity to facilitate inserting records.
--Define unique constraint to avoid repeating usernames.
--Define a check constraint to ensure the password is at least 5 characters long.
CREATE TABLE Users (
UserId int IDENTITY PRIMARY KEY,
UserName nvarchar(50) NOT NULL UNIQUE,
Pass nvarchar(50) NOT NULL CHECK(LEN(Pass) >= 5),
FullName nvarchar(100) NOT NULL,
LastLogin smalldatetime);

--Adding some rows
INSERT INTO Users (UserName, Pass, FullName, LastLogin)
VALUES
('pesho', '12345', 'Peter Petrov', GETDATE()), --pesho logged in today
('jhon', '54321', 'Jhon Doe', DATEADD(DAY, -1, GETDATE())), --jhon logged in yesterday
('gosho', '15243', 'Georgi Ivanov', NULL); --gosho didnt log in

--16.
--Write a SQL statement to create a view that displays the users from the Users table
--that have been in the system today.
CREATE VIEW vwUsersWhoHaveLoggedInToday
AS
SELECT UserName FROM Users
WHERE 0 = DATEDIFF(DAY, LastLogin, GETDATE());

SELECT * FROM vwUsersWhoHaveLoggedInToday;

--17.
--Write a SQL statement to create a table Groups.
--Groups should have unique name (use unique constraint).
--Define primary key and identity column.
CREATE TABLE Groups (
GroupId int IDENTITY PRIMARY KEY,
Name NVARCHAR(50) UNIQUE);

--18.
--Write a SQL statement to add a column GroupID to the table Users.
ALTER TABLE Users
ADD GroupID int;

--Fill some data in this new column and as well in the `Groups table.
INSERT INTO Groups (Name)
VALUES
('Web'),
('Mobile'),
('QA'),
('Front End');

UPDATE Users
SET GroupID = 1
WHERE UserName = 'pesho';

UPDATE Users
SET GroupID = 2
WHERE UserName = 'jhon';

UPDATE Users
SET GroupID = 3
WHERE UserName = 'gosho';

--Write a SQL statement to add a foreign key constraint between tables Users and Groups tables.
ALTER TABLE Users
ADD CONSTRAINT FK_Groups_GroupId FOREIGN KEY (GroupID)
REFERENCES Groups(GroupId);

--19.
--Write SQL statements to insert several records in the Users and Groups tables.
INSERT INTO Users (UserName, Pass, FullName, LastLogin, GroupID)
VALUES
('ivan', '51423', 'Ivan Ivanov', GETDATE(), 5);

INSERT INTO Groups (Name)
VALUES
('Ninja');

SELECT * FROM Groups;

--20.
--Write SQL statements to update some of the records in the Users and Groups tables.
UPDATE Users
SET GroupID = 5
WHERE FullName = 'Ivan Ivanov';

UPDATE Groups
SET Name = 'NinjaNot'
WHERE GroupId = 5;

--21.
--Write SQL statements to delete some of the records from the Users and Groups tables.
DELETE FROM Users
WHERE UserName = 'ivan';

DELETE FROM Groups
WHERE Name = 'NinjaNot';

--22.
--Write SQL statements to insert in the Users table
--the names of all employees from the Employees table.
--Combine the first and last names as a full name.
--For username use the first letter of the first name + the last name (in lowercase).
--Use the same for the password, and NULL for last login time.

INSERT INTO Users (UserName, Pass, FullName)
SELECT
       LOWER(FirstName + '.' + LastName), -- Violating the problem condition due UserName duplication
       LOWER(LEFT(FirstName, 1) + LastName) + REPLICATE('0' ,5 - LEN(LEFT(LEFT(FirstName, 1) + LastName, 5))), -- Pad short names due Pass constrain
       FirstName + ' ' + LastName
FROM Employees;

--23.
--Write a SQL statement that changes the password to NULL
--for all users that have not been in the system since 10.03.2010.

--Firstly, create a user who didnt log in for a long time
INSERT INTO Users (UserName, Pass, FullName, LastLogin)
VALUES
('stamat2', 'stamat123', 'Stamat Stamatov', CONVERT(smalldatetime, '10.02.2010'));

--Secondly, Alter Pass NULL Constrain
ALTER TABLE Users ALTER COLUMN Pass nvarchar(50);

UPDATE Users
SET Pass = NULL
WHERE LastLogin <= CONVERT(smalldatetime, '10.03.2010');

--24.
--Write a SQL statement that deletes all users without passwords (NULL password).
DELETE FROM Users
WHERE Pass IS NULL;

--25.
--Write a SQL query to display the average employee salary by department and job title.
SELECT AVG(Salary) AS [Average Salary], JobTitle, Name AS Department FROM Employees
INNER JOIN Departments ON Employees.DepartmentID = Departments.DepartmentID
GROUP BY Name, JobTitle;

--26.
--Write a SQL query to display the minimal employee salary by department and job title
--along with the name of some of the employees that take it.

SELECT ROUND(MIN(e.Salary), 2) AS [MinSalary], 
        d.Name AS [Department],
        e.JobTitle, 
        MIN(CONCAT(e.FirstName, ' ', e.LastName)) AS [Employee]
    FROM Employees e 
    JOIN Departments d
        ON e.DepartmentID = d.DepartmentID
    GROUP BY d.Name, e.JobTitle
    ORDER BY d.Name;

--27.
--Write a SQL query to display the town where maximal number of employees work.
SELECT TOP 1 t.Name AS [Town], COUNT (*) AS [Employees count] FROM Employees e
INNER JOIN Addresses a ON e.AddressID = a.AddressID
INNER JOIN Towns t ON a.TownID = t.TownID
GROUP BY t.Name
ORDER BY [Employees count] DESC;

--28.
--Write a SQL query to display the number of managers from each town.
SELECT t.Name AS [Town], COUNT (*) AS [Managers count] FROM Employees m
INNER JOIN Addresses a ON m.AddressID = a.AddressID
INNER JOIN Towns t ON a.TownID = t.TownID
WHERE m.EmployeeID IN
(
    SELECT m.EmployeeID FROM Employees m
    INNER JOIN Employees e ON e.ManagerID = m.EmployeeID
	GROUP BY m.EmployeeID
)
GROUP BY t.Name;

--29.
--Write a SQL to create table WorkHours to store
--work reports for each employee (employee id, date, task, hours, comments).
--Don't forget to define identity, primary key and appropriate foreign key.
CREATE TABLE WorkHours (
WorkHoursID int IDENTITY PRIMARY KEY,
EmployeeID int FOREIGN KEY REFERENCES Employees(EmployeeID),
WorkDate date NOT NULL,
Task nvarchar(200),
WorkTime real NOT NULL,
Comments nvarchar(1000)
);

--Issue few SQL statements to insert, update and delete of some data in the table.
INSERT INTO WorkHours (EmployeeID, WorkDate, Task, WorkTime)
VALUES
(1, GETDATE(), 'Do the dishes', 2.5),
(20, GETDATE(), 'UnDo the dishes', 2.5),
(85, GETDATE(), 'ReDo the dishes', 2.5),
(90, GETDATE(), 'Do manager''s wife', 8);

UPDATE WorkHours
SET EmployeeID = 69
WHERE EmployeeID = 90;

DELETE FROM WorkHours
WHERE EmployeeID = 69;

--Define a table WorkHoursLogs to track all changes in the WorkHours table with triggers.
--For each change keep the old record data, the new record data
--and the command (insert / update / delete).
CREATE TABLE WorkHourLogs (
	[ChangeID] int IDENTITY,
	[OldEmployeeID] int FOREIGN KEY REFERENCES Employees(EmployeeID),
	[OldTask] nvarchar(200),
	[OldWorkDate] date,	
	[OldWorkTime] real,
	[OldComments] nvarchar(1000),
	[NewEmployeeID] int FOREIGN KEY REFERENCES Employees(EmployeeID),
	[NewTask] nvarchar(200),
	[NewWorkDate] date,	
	[NewWorkTime] real,
	[NewComments] nvarchar(1000),
	Command nvarchar(20)
	CONSTRAINT PK_WorkHourLogs PRIMARY KEY([ChangeID]),
);

CREATE TRIGGER tr_InsertTrigger ON WorkHours FOR INSERT
AS
	INSERT INTO WorkHourLogs
	SELECT NULL, NULL, NULL, NULL, NULL, 
	i.EmployeeID, i.Task, i.WorkDate, i.WorkTime, i.Comments, 'INSERT'
	FROM inserted i;

CREATE TRIGGER tr_UpdateTrigger ON WorkHours FOR UPDATE
AS
	INSERT INTO WorkHourLogs
	SELECT d.EmployeeID, d.Task, d.WorkDate, d.WorkTime, d.Comments, 
	i.EmployeeID, i.Task, i.WorkDate, i.WorkTime, i.Comments, 'UPDATE'
	FROM inserted i, deleted d;

CREATE TRIGGER tr_DELETETrigger ON WorkHours FOR DELETE
AS
	INSERT INTO WorkHourLogs
	SELECT d.EmployeeID, d.Task, d.WorkDate, d.WorkTime, d.Comments, 
	NULL, NULL, NULL, NULL, NULL, 'DELETE'
	FROM deleted d;

--30.
--Start a database transaction, delete all employees from the 'Sales' department along with all dependent 
--records from the pother tables. At the end rollback the transaction.
DECLARE @TransactionDelete varchar(20) = 'DeleteSalesEmployees';

BEGIN TRAN @TransactionDelete
	DELETE FROM Employees 
	WHERE DepartmentID = 3;
ROLLBACK TRAN @TransactionDelete;

--31.
--Start a database transaction and drop the table EmployeesProjects.
--Now how you could restore back the lost table data?
BEGIN TRAN 
	DROP TABLE EmployeesProjects
ROLLBACK TRAN;

--32.
--Find how to use temporary tables in SQL Server.
--Using temporary tables backup all records from EmployeesProjects 
--and restore them back after dropping and re-creating the table.
SELECT * 
	INTO #Temp FROM EmployeesProjects
	DROP TABLE EmployeesProjects
		SELECT * INTO EmployeeProjects FROM #Temp
		DROP TABLE #Temp;