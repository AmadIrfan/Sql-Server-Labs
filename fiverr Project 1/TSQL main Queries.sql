
CREATE TABLE UserTbl (
    UId      INT          IDENTITY (1, 1) NOT NULL,
    UName    VARCHAR (50) NOT NULL,
    UDob     DATE         NOT NULL,
    UPhone   INT          NOT NULL,
    UPass    VARCHAR (50) NOT NULL,
    UAddress VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([UId] ASC)
);

CREATE TABLE [dbo].[IncomeTbl] (
    [IncId]   INT           IDENTITY (1, 1) NOT NULL,
    [IncName] VARCHAR (50)  NOT NULL,
    [IncAmt]  INT           NOT NULL,
    [IncCat]  VARCHAR (50)  NOT NULL,
    [IncDate] DATE          NOT NULL,
    [IncDesc] VARCHAR (100) NOT NULL,
    [IncUser] VARCHAR (50)  NOT NULL,
    PRIMARY KEY CLUSTERED ([IncId] ASC)
);


CREATE TABLE [dbo].[ExpenseTbl] (
    [ExpId]   INT           IDENTITY (1, 1) NOT NULL,
    [ExpName] VARCHAR (50)  NOT NULL,
    [ExpAmt]  INT           NOT NULL,
    [ExpCat]  VARCHAR (50)  NOT NULL,
    [ExpDate] DATE          NOT NULL,
    [ExpDesc] VARCHAR (100) NOT NULL,
    [ExpUser] VARCHAR (20)  NOT NULL,
    PRIMARY KEY CLUSTERED ([ExpId] ASC)
);


-- Insert dummy data into ExpenseTbl
INSERT INTO ExpenseTbl (ExpName, ExpAmt, ExpCat, ExpDate, ExpDesc, ExpUser)
VALUES 
    ('Groceries', 100, 'Food', '2023-01-15', 'Monthly grocery shopping', 'User1'),
    ('Internet Bill', 50, 'Utilities', '2023-01-20', 'Monthly internet bill', 'User2'),
    ('Dinner Out', 30, 'Entertainment', '2023-01-25', 'Dinner with friends', 'User3'),
    ('Gasoline', 40, 'Transportation', '2023-01-28', 'Fuel for the car', 'User1');

	-- Insert dummy data into IncomeTbl
INSERT INTO IncomeTbl (IncName, IncAmt, IncCat, IncDate, IncDesc, IncUser)
VALUES 
    ('Salary', 3000, 'Monthly Income', '2023-01-15', 'Monthly salary', 'User1'),
    ('Freelance Payment', 500, 'Freelance', '2023-01-20', 'Payment for freelance work', 'User2'),
    ('Bonus', 200, 'Extra Income', '2023-01-25', 'Year-end bonus', 'User3'),
    ('Investment Dividends', 100, 'Investments', '2023-01-28', 'Dividends from investments', 'User1');

-- Insert dummy data into UserTb
INSERT INTO UserTbl (UName, UDob, UPhone, UPass, UAddress)
VALUES 
    ('John Doe', '1990-01-15', 123, 'password123', '123 Main St'),
    ('Alice Smith', '1985-05-22', 987, 'securepass', '456 Oak Ave'),
    ('Bob Johnson', '1995-08-10', 555, 'pass123', '789 Pine St'),
    ('Eva Davis', '1988-11-28', 333, 'password567', '321 Elm St');
