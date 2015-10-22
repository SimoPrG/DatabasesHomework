--1.
--Create a database with two tables:
--  Persons(Id(PK), FirstName, LastName, SSN)
--  Accounts(Id(PK), PersonId(FK), Balance).
--Insert few records for testing.
--Write a stored procedure that selects the full names of all persons.

USE master
GO

CREATE DATABASE SouthBreeze
GO

USE SouthBreeze
GO

CREATE TABLE Persons(
Id int IDENTITY PRIMARY KEY,
FirstName nvarchar(50) NOT NULL,
LastName nvarchar(50) NOT NULL,
Ssn char(12) UNIQUE NOT NULL);

CREATE TABLE Accounts(
Id int IDENTITY PRIMARY KEY,
PersonId int FOREIGN KEY REFERENCES Persons(Id),
Balance money NOT NULL);

INSERT INTO Persons (FirstName, LastName, Ssn)
VALUES
('Michael', 'Jordan', '123-12-1234'),
('Michael', 'Jackson', '123-45-6789'),
('Misho', 'Mihov', '123-21-3455');

INSERT INTO Accounts (PersonId, Balance)
VALUES
(1, 9999999.99),
(2, 99999999.99),
(3, 111.11);

GO

CREATE PROC dbo.usp_SelectFullNameOfAllPersons AS
  SELECT CONCAT(FirstName, ' ', LastName) AS FullName
  FROM Persons;
GO

EXEC usp_SelectFullNameOfAllPersons;
GO

--2.
--Create a stored procedure that accepts a number as a parameter
--and returns all persons who have more money in their accounts
--than the supplied number.
CREATE PROC dbo.usp_SelectPersonsWithBalanceGreaterThan(
@balance money) AS
  SELECT FirstName, LastName FROM Persons
  INNER JOIN Accounts ON Persons.Id = Accounts.PersonId
  WHERE @balance < Balance;
GO

EXEC usp_SelectPersonsWithBalanceGreaterThan 1000
GO

--3.
--Create a function that accepts as parameters
--  sum, yearly interest rate and number of months.
--It should calculate and return the new sum.
--Write a SELECT to test whether the function works as expected.
CREATE FUNCTION dbo.ufn_CalculateCompoundInterest(
	@sum money, @yearlyInterestRate float, @numberOfMonths tinyint)
	RETURNS money
AS
	BEGIN
		RETURN @sum * @yearlyInterestRate * @numberOfMonths / 1200 + @sum
	END
GO

SELECT Balance, dbo.ufn_CalculateCompoundInterest(Balance, 1, 1) as [Bonus]
FROM Accounts

DECLARE @sum money;
EXEC @sum = ufn_CalculateCompoundInterest 1000, 3, 1
SELECT @sum AS [Acumulated sum];
GO

--4.
--Create a stored procedure that uses the function from the previous example
--to give an interest to a person's account for one month.
--It should take the AccountId and the interest rate as parameters.
CREATE PROC usp_CalculateCompoundInterestForOneMonth(
@accountId int, @yearlyInterestRate money) AS
  DECLARE @balance money;
  SELECT @balance = Balance FROM Accounts WHERE Id = @accountId;

  DECLARE @newBalance money = dbo.ufn_CalculateCompoundInterest(@balance, @yearlyInterestRate, 1);

  UPDATE Accounts
  SET Balance = @newBalance
  WHERE Id = @accountId;
GO

--5.
--Add two more stored procedures WithdrawMoney(AccountId, money)
--and DepositMoney(AccountId, money) that operate in transactions.
CREATE PROC usp_WithdrawMoney(
@accountId int, @sum money) AS
  BEGIN TRAN;
  IF @sum <= 0
  BEGIN;
    ROLLBACK TRAN;
  END;
  ELSE
  BEGIN;
    DECLARE @balance money;
    SELECT @balance = Balance FROM Accounts WHERE Id = @accountId;
	IF @balance IS NULL
	BEGIN;
	  ROLLBACK TRAN;
	END;
	ELSE
	BEGIN;
      DECLARE @newBalance money = @balance - @sum;
	  IF @newBalance < 0
	  BEGIN;
	    ROLLBACK TRAN;
	  END;
	  ELSE
	  BEGIN;
        UPDATE Accounts
        SET Balance = @newBalance
        WHERE Id = @accountId;
	  END;
	END;
  END;
  COMMIT TRAN;
GO

CREATE PROC usp_DepositMoney(
@accountId int, @sum money) AS
  BEGIN TRAN;
  IF @sum <= 0
  BEGIN;
    ROLLBACK TRAN;
  END;
  ELSE
  BEGIN;
    DECLARE @balance money;
    SELECT @balance = Balance FROM Accounts WHERE Id = @accountId;
	IF @balance IS NULL
	BEGIN;
	  ROLLBACK TRAN;
	END;
	ELSE
	BEGIN;
      DECLARE @newBalance money = @balance + @sum;
      UPDATE Accounts
      SET Balance = @newBalance
      WHERE Id = @accountId;
	END;
  END;
  COMMIT TRAN;
GO

--6.
--Create another table – Logs(LogID, AccountID, OldSum, NewSum).
--Add a trigger to the Accounts table that enters a new entry into the Logs table
--every time the sum on an account changes.
CREATE TABLE Logs(
LogId int PRIMARY KEY IDENTITY,
AccountID int FOREIGN KEY REFERENCES Accounts(Id),
OldSum money NOT NULL,
NewSum money NOT NULL);
GO

CREATE TRIGGER tr_UpdateAccountBalance ON Accounts FOR UPDATE
AS
    DECLARE @oldSum money;
    SELECT @oldSum = Balance FROM deleted;

    INSERT INTO Logs(AccountID, OldSum, NewSum)
        SELECT ID, @oldSum, Balance
        FROM inserted
GO

--7.
--Define a function in the database TelerikAcademy
--that returns all Employee's names (first or middle or last name)
--and all town's names that are comprised of given set of letters.
--Example: 'oistmiahf' will return 'Sofia', 'Smith', … but not 'Rob' and 'Guy'.
USE TelerikAcademy
GO

CREATE FUNCTION ufn_CheckName (@nameToCheck nvarchar(50), @letters nvarchar(26)) RETURNS int
AS
BEGIN
  DECLARE @i int = 1
  DECLARE @currentChar nchar(1)
  WHILE (@i <= LEN(@nameToCheck))
  BEGIN
	SET @currentChar = SUBSTRING(@nameToCheck, @i, 1)
	IF (CHARINDEX(@currentChar, @letters) <= 0) 
	BEGIN  
	  RETURN 0
	END
	SET @i = @i + 1
  END
  RETURN 1
END
GO

CREATE FUNCTION ufn_CheckIfNameConsistsOfLetters (@searchString nvarchar(26)) 
RETURNS @T TABLE (Name nvarchar(50)) AS
BEGIN
  DECLARE employeeCursor CURSOR READ_ONLY FOR
	(SELECT e.FirstName, e.LastName, t.Name FROM Employees e
	JOIN Addresses a ON e.AddressID = a.AddressID
	JOIN Towns t ON a.TownID=t.TownID)
  OPEN employeeCursor
  DECLARE @firstName nvarchar(50), @lastName nvarchar(50), @town nvarchar(50)
  DECLARE @tempTable TABLE (Name nvarchar(50))
  FETCH NEXT FROM employeeCursor INTO @firstName, @lastName, @town
  WHILE @@FETCH_STATUS = 0
  BEGIN
    DECLARE @i INT = 1
	DECLARE @match nvarchar(50) = NULL
	DECLARE @currentChar nvarchar(1)
	IF (dbo.ufn_CheckName(@firstName, @searchString) = 1)
	BEGIN
	  SET @match = @firstName
	END
	IF (dbo.ufn_CheckName(@lastName, @searchString) = 1)
	BEGIN
	  SET @match = @lastName
	END
	IF (dbo.ufn_CheckName(@town, @searchString) = 1)
	BEGIN
	  SET @match = @town
	END
	IF @match IS NOT NULL
	  INSERT INTO @tempTable
	VALUES (@match)
	FETCH NEXT FROM employeeCursor INTO @firstName, @lastName, @town
  END
  CLOSE employeeCursor
  DEALLOCATE employeeCursor
  INSERT INTO @T
  SELECT DISTINCT Name FROM @tempTable
  RETURN
END
GO

SELECT * FROM ufn_CheckIfNameConsistsOfLetters('oistmiahf')

--8.
--Using database cursor write a T-SQL script that scans all employees and their addresses
--and prints all pairs of employees that live in the same town.
DECLARE employeeCursor CURSOR READ_ONLY FOR
SELECT 
  emp1.FirstName + ' ' + emp1.LastName AS [First Employee], 
  t1.Name AS Town, 
  emp2.FirstName + ' ' + emp2.LastName AS [Second Employee]
FROM Employees emp1, Employees emp2, Addresses a1, Towns t1, Addresses a2, Towns t2
WHERE 
  emp1.AddressID = a1.AddressID AND 
  a1.TownID = t1.TownID AND 
  emp2.AddressID = a2.AddressID AND 
  a2.TownID = t2.TownID AND 
  t1.TownID = t2.TownID AND 
  emp1.EmployeeID != emp2.EmployeeID
ORDER BY [First Employee], [Second Employee]
OPEN employeeCursor
DECLARE @firstEmployee nvarchar(50), @secondEmployee nvarchar(50), @townName nvarchar(50)
FETCH NEXT FROM employeeCursor INTO @firstEmployee, @townName, @secondEmployee
WHILE @@FETCH_STATUS = 0
BEGIN
  PRINT @firstEmployee + ' and ' + @secondEmployee + ' both live in ' + @townName;
  FETCH NEXT FROM employeeCursor 
  INTO @firstEmployee, @townName, @secondEmployee
END
CLOSE employeeCursor
DEALLOCATE employeeCursor

--9 *.
--Write a T-SQL script that shows for each town a list of all employees that live in it.
--Sample output:
-- Sofia -> Svetlin Nakov, Martin Kulov, George Denchev
-- Ottawa -> Jose Saraiva
CREATE TABLE #UsersTowns (ID INT IDENTITY, FullName NVARCHAR(50), TownName NVARCHAR(50))
INSERT INTO #UsersTowns
SELECT e.FirstName + ' ' + e.LastName, t.Name FROM Employees e
INNER JOIN Addresses a ON a.AddressID = e.AddressID
INNER JOIN Towns t ON t.TownID = a.TownID
GROUP BY t.Name, e.FirstName, e.LastName
DECLARE @name NVARCHAR(50)
DECLARE @town NVARCHAR(50)
DECLARE employeeCursor CURSOR READ_ONLY FOR
SELECT DISTINCT ut.TownName FROM #UsersTowns ut
OPEN employeeCursor
FETCH NEXT FROM employeeCursor INTO @town
WHILE @@FETCH_STATUS = 0
BEGIN
  DECLARE @empName nvarchar(MAX);
  SET @empName = N'';
  SELECT @empName += ut.FullName + N', '
  FROM #UsersTowns ut
  WHERE ut.TownName = @town
  PRINT @town + ' -> ' + LEFT(@empName,LEN(@empName)-1);
  FETCH NEXT FROM employeeCursor INTO @town
END
CLOSE employeeCursor
DEALLOCATE employeeCursor
DROP TABLE #UsersTowns