-- SQL setup scripts go here

CREATE TABLE Users (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Email VARCHAR(255) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL
);

CREATE TABLE Clients (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Description VARCHAR(500) NOT NULL,
    IsArchived BOOLEAN DEFAULT FALSE,
    UserId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);

CREATE TABLE PhoneNumbers (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    ClientId INT NOT NULL,
    Phone VARCHAR(20) NOT NULL,
    FOREIGN KEY (ClientId) REFERENCES Clients(Id) ON DELETE CASCADE
);

CREATE TABLE Products (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Description VARCHAR(500),
    Price DECIMAL(10,2) NOT NULL
);

CREATE TABLE Orders (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    ClientId INT NOT NULL,
    ProductId INT NOT NULL,
    UserId INT NOT NULL,
    OrderDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    TotalAmount DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (ClientId) REFERENCES Clients(Id) ON DELETE CASCADE,
    FOREIGN KEY (ProductId) REFERENCES Products(Id) ON DELETE CASCADE
);

-- Inserted data to get the application started for user test@testing.com w/ password test

INSERT INTO Users (Email, PasswordHash) 
VALUES ('test@testing.com', '$2a$11$6krE09MM2wKjKIaifY2HPu/Zo9BB5fb1UTOT0JEFDTU1U6r4iwvQ2');

INSERT INTO Clients (Name, Description, IsArchived, UserId) 
VALUES 
('Paul Atreides', 'A great potential client and world leader.', 0, 1),
('Dr. Who', 'Time traveler I think', 1, 1),
('Mr. P', 'Penguino who is hard to contact', 0, 1),
('Jessie', 'Hopefully a lead here!', 0, 1),
('Brock', 'Friendly, but I suspect the phone number is a fake', 0, 1),
('Gray', 'Teleports', 0, 1),
('Penny', 'A good friend', 0, 1),
('Luna', 'Mysterious and only responds at night.', 0, 1),
('Sam Bridges', 'Delivery guy, always on the move.', 0, 1),
('Walter White', 'Said he was in the chemistry business.', 0, 1),
('Tony Stark', 'Seems wealthy, might be a good investor.', 0, 1),
('Ada Lovelace', 'Legend in computing, would be great to work with.', 0, 1);

INSERT INTO PhoneNumbers (ClientId, Phone) 
VALUES 
(1, '+1-555-123-4567'), 
(1, '+1-555-987-6543'),
(2, '+44-7700-900123'), 
(3, '+81-90-1234-5678'), 
(4, '+1-222-333-4444'), 
(4, '+1-333-444-5555'),
(5, '+1-555-999-0000'), 
(6, '+49-160-9876543'), 
(6, '+49-160-1112223'),
(7, '+33-6-12-34-56-78'), 
(8, '+1-999-222-3333'), 
(9, '+1-777-555-1212'), 
(9, '+1-888-666-1313'),
(10, '+1-505-555-0199'), 
(10, '+1-505-555-0200'),
(11, '+1-310-555-8080'), 
(12, '+44-20-7946-0958'),
(12, '+44-20-7946-1234');
