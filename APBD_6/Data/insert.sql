-- Insert Countries
SET IDENTITY_INSERT Country ON;
INSERT INTO Country (IdCountry, Name) VALUES (1, 'Poland');
INSERT INTO Country (IdCountry, Name) VALUES (2, 'Germany');
SET IDENTITY_INSERT Country OFF;

-- Insert Clients
SET IDENTITY_INSERT Client ON;
INSERT INTO Client (IdClient, FirstName, LastName, Email, Telephone, Pesel) VALUES
                                                                                (1, 'John', 'Smith', 'john@smith.com', '111222333', '98020212345'),
                                                                                (2, 'Jake', 'Doe', 'jake@doe.com', '666555444', '20020212345'),
                                                                                (3, 'Remove', 'Me', 'Remove@Me.com', '1337133733', '1337133733');
SET IDENTITY_INSERT Client OFF;

-- Insert Trips
SET IDENTITY_INSERT Trip ON;
INSERT INTO Trip (IdTrip, Name, Description, DateFrom, DateTo, MaxPeople) VALUES
                                                                              (1, 'ABC', 'Lorem ipsum...', '2025-06-30', '2025-07-05', 20),
                                                                              (2, 'XYZ', 'Lorem ipsum...', '2025-07-02', '2025-07-05', 0),
                                                                              (3, 'Testowa Wycieczka', 'Lorem ipsum...', '2025-07-05', '2025-07-15', 0);
SET IDENTITY_INSERT Trip OFF;

-- Insert Country_Trip
INSERT INTO Country_Trip (IdCountry, IdTrip) VALUES (1, 1);
INSERT INTO Country_Trip (IdCountry, IdTrip) VALUES (2, 1);

-- Insert Client_Trip
INSERT INTO Client_Trip (IdClient, IdTrip, RegisteredAt, PaymentDate) VALUES
                                                                          (1, 1, '2025-06-29', NULL),
                                                                          (2, 1, '2025-06-30', '2025-05-30');
