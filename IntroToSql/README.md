## Structured Query Language (SQL)
### _Homework_

1.	What is SQL? What is DML? What is DDL? Recite the most important SQL commands.
  * Structured Query Language (SQL)
    Declarative language for query and manipulation of relational data
  * Data Manipulation Language (DML)
    SELECT, INSERT, UPDATE, DELETE
  * Data Definition Language (DDL)
    CREATE, DROP, ALTER
    GRANT, REVOKE
1.	What is Transact-SQL (T-SQL)?
  * T-SQL(Transact SQL) is an extension to the standard SQL language
    T-SQL is the standard language used in MS SQL Server
    Supports if statements, loops, exceptions
    Constructions used in the high-level procedural programming languages
    T-SQL is used for writing stored procedures, functions, triggers, etc.
1.	Start SQL Management Studio and connect to the database TelerikAcademy. Examine the major tables in the "TelerikAcademy" database.
1.	Write a SQL query to find all information about all departments (use "TelerikAcademy" database).
  * `SELECT * FROM Departments`
1.	Write a SQL query to find all department names.
  * `SELECT Name FROM Departments;`
1.	Write a SQL query to find the salary of each employee.
  * `SELECT FirstName, Salary FROM Employees;`
1.	Write a SQL to find the full name of each employee.
  * `SELECT FirstName + ' ' + MiddleName + ' ' + LastName AS [Full Name] FROM Employees;`
1.	Write a SQL query to find the email addresses of each employee (by his first and last name). Consider that the mail domain is telerik.com. Emails should look like “John.Doe@telerik.com". The produced column should be named "Full Email Addresses".
  * `SELECT FirstName + '.' + LastName + '@telerik.com' AS [Full Email Addresses] FROM Employees;`
1.	Write a SQL query to find all different employee salaries.
  * `SELECT Salary FROM Employees GROUP BY Salary;`
1.	Write a SQL query to find all information about the employees whose job title is “Sales Representative“.
  * `SELECT * FROM Employees WHERE JobTitle = 'Sales Representative';`
1.	Write a SQL query to find the names of all employees whose first name starts with "SA".
  * `SELECT FirstName, LastName FROM Employees WHERE FirstName LIKE 'SA%';`
1.	Write a SQL query to find the names of all employees whose last name contains "ei".
  * `SELECT FirstName, LastName FROM Employees WHERE LastName LIKE '%ei%';`
1.	Write a SQL query to find the salary of all employees whose salary is in the range [20000…30000].
  * `SELECT FirstName, LastName, Salary FROM Employees WHERE Salary BETWEEN 20000 AND 30000;`
1.	Write a SQL query to find the names of all employees whose salary is 25000, 14000, 12500 or 23600.
  * `SELECT FirstName, LastName, Salary FROM Employees WHERE Salary IN (25000, 14000, 12500, 23600);`
1.	Write a SQL query to find all employees that do not have manager.
  * `SELECT FirstName, LastName FROM Employees WHERE ManagerID IS NULL;`
1.	Write a SQL query to find all employees that have salary more than 50000. Order them in decreasing order by salary.
  * `SELECT FirstName, LastName, Salary FROM Employees WHERE Salary > 50000 ORDER BY Salary DESC;`
1.	Write a SQL query to find the top 5 best paid employees.
  * `SELECT TOP 5 FirstName, LastName, Salary FROM Employees ORDER BY Salary DESC;`
1.	Write a SQL query to find all employees along with their address. Use inner join with `ON` clause.
  * ```

    SELECT Employees.FirstName, Employees.LastName,
    Addresses.AddressText AS Address, Towns.Name AS Town FROM Employees
    INNER JOIN Addresses ON Employees.AddressID = Addresses.AddressID
    INNER JOIN Towns ON Addresses.TownID = Towns.TownID;

    ```

1.	Write a SQL query to find all employees and their address. Use equijoins (conditions in the `WHERE` clause).

  * ```

    SELECT Employees.FirstName, Employees.LastName,
       Addresses.AddressText AS Address,
	   Towns.Name AS Town
    FROM Employees, Addresses, Towns
    WHERE Employees.AddressID = Addresses.AddressID
      AND Addresses.TownID = Towns.TownID;

    ```

1.	Write a SQL query to find all employees along with their manager.

  * ```
  
    SELECT e.FirstName + ' ' + e.LastName AS Employee, m.FirstName + ' ' + m.LastName AS Manager
    FROM Employees e
    INNER JOIN Employees m ON (e.ManagerID = m.EmployeeID);

    ```

1.	Write a SQL query to find all employees, along with their manager and their address. Join the 3 tables: `Employees e`, `Employees m` and `Addresses a`.

  * ```
  
    SELECT e.FirstName + ' ' + e.LastName AS Employee,
       a.AddressText AS Address,
	   t.Name AS Town,
       m.FirstName + ' ' + m.LastName AS Manager
    FROM Employees e
    INNER JOIN Employees m ON e.ManagerID = m.EmployeeID
    INNER JOIN Addresses a ON e.AddressID = a.AddressID
    INNER JOIN Towns t ON a.TownID = t.TownID;

    ```

1.	Write a SQL query to find all departments and all town names as a single list. Use `UNION`.

  * ```
  
    SELECT Name FROM Departments
    UNION
    SELECT Name FROM Towns;

    ```

1.	Write a SQL query to find all the employees and the manager for each of them along with the employees that do not have manager. Use right outer join. Rewrite the query to use left outer join.

  * ```
  
    SELECT e.FirstName + ' ' + e.LastName AS Employee, m.FirstName + ' ' + m.LastName AS Manager
    FROM Employees m
    RIGHT OUTER JOIN Employees e ON e.ManagerID = m.EmployeeID;

    SELECT e.FirstName + ' ' + e.LastName AS Employee, m.FirstName + ' ' + m.LastName AS Manager
    FROM Employees e
    LEFT OUTER JOIN Employees m ON e.ManagerID = m.EmployeeID;
    ```

1.	Write a SQL query to find the names of all employees from the departments "Sales" and "Finance" whose hire year is between 1995 and 2005.

  * ```
  
    SELECT Employees.FirstName + ' ' + Employees.LastName AS Employee,
       Departments.Name AS Department,
       Employees.HireDate
    FROM Employees
    INNER JOIN Departments ON Employees.DepartmentID = Departments.DepartmentID
    WHERE (YEAR(Employees.HireDate) BETWEEN 1995 AND 2005) AND
      (Departments.Name = 'Sales' OR Departments.Name = 'Finance');

    ```
