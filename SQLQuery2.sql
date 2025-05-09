-- Tabelul Utilizatori
CREATE TABLE Utilizatori (
    Id INT PRIMARY KEY IDENTITY,
    Nume NVARCHAR(50),
    Prenume NVARCHAR(50),
    Email NVARCHAR(100) UNIQUE,
    Parola NVARCHAR(64),
    Telefon NVARCHAR(20),
    Adresa NVARCHAR(200),
    TipUtilizator NVARCHAR(20) -- 'client' sau 'angajat'
);
GO

-- Procedura de înregistrare
CREATE PROCEDURE RegisterUser
    @Nume NVARCHAR(50),
    @Prenume NVARCHAR(50),
    @Email NVARCHAR(100),
    @Parola NVARCHAR(64),
    @Telefon NVARCHAR(20),
    @Adresa NVARCHAR(200),
    @TipUtilizator NVARCHAR(20)
AS
BEGIN
    INSERT INTO Utilizatori (Nume, Prenume, Email, Parola, Telefon, Adresa, TipUtilizator)
    VALUES (@Nume, @Prenume, @Email, @Parola, @Telefon, @Adresa, @TipUtilizator);
END;
GO

-- Procedura de login
CREATE PROCEDURE LoginUser
    @Email NVARCHAR(100),
    @Parola NVARCHAR(64)
AS
BEGIN
    SELECT * FROM Utilizatori
    WHERE Email = @Email AND Parola = @Parola;
END;
GO
CREATE PROCEDURE RegisterUser
    @Nume NVARCHAR(50),
    @Prenume NVARCHAR(50),
    @Email NVARCHAR(100),
    @Parola NVARCHAR(64),
    @Telefon NVARCHAR(20),
    @Adresa NVARCHAR(200),
    @TipUtilizator NVARCHAR(20)
AS
BEGIN
    INSERT INTO Utilizatori (Nume, Prenume, Email, Parola, Telefon, Adresa, TipUtilizator)
    VALUES (@Nume, @Prenume, @Email, @Parola, @Telefon, @Adresa, @TipUtilizator);
END;
GO
