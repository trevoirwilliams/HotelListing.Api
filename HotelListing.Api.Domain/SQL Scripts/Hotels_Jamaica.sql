-- Adds sample Hotels for Jamaica (JM)
SET NOCOUNT ON;

DECLARE @JamaicaId int;
SELECT @JamaicaId = CountryId FROM Countries WHERE ShortName = 'JM' OR Name = 'Jamaica';

IF @JamaicaId IS NULL
BEGIN
    RAISERROR('Country ''Jamaica'' (JM) not found in Countries table.', 16, 1);
    RETURN;
END

BEGIN TRANSACTION;

-- Insert hotels if they do not already exist for Jamaica
INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Half Moon', 'Rose Hall, Montego Bay', 4.7, 450.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Half Moon' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'The Jamaica Pegasus', '81 Knutsford Blvd, Kingston', 4.2, 180.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'The Jamaica Pegasus' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Moon Palace Jamaica', 'Main Street, Ocho Rios', 4.5, 300.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Moon Palace Jamaica' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Iberostar Grand Rose Hall', 'Rose Hall, Montego Bay', 4.8, 520.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Iberostar Grand Rose Hall' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Sandals Montego Bay', 'Kent Ave, Montego Bay', 4.6, 400.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Sandals Montego Bay' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'S Hotel Jamaica', '7 Jimmy Cliff Blvd, Montego Bay', 4.4, 220.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'S Hotel Jamaica' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Secrets St. James Montego Bay', 'Lot 59A Freeport, Montego Bay', 4.6, 350.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Secrets St. James Montego Bay' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Riu Palace Jamaica', 'Mahoe Bay, Montego Bay', 4.3, 230.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Riu Palace Jamaica' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'The Cliff Hotel', 'West End Road, Negril', 4.6, 280.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'The Cliff Hotel' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Hyatt Ziva Rose Hall', 'Rose Hall Road, Montego Bay', 4.6, 370.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Hyatt Ziva Rose Hall' AND CountryId = @JamaicaId);

-- Additional Hotels
INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Jamaica Inn', 'Main Street, Ocho Rios', 4.7, 320.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Jamaica Inn' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'GoldenEye', 'Oracabessa, St. Mary', 4.8, 650.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'GoldenEye' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Round Hill Hotel and Villas', 'John Pringle Drive, Montego Bay', 4.7, 580.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Round Hill Hotel and Villas' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Tryall Club', 'Sandy Bay, Hanover', 4.6, 600.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Tryall Club' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'The Caves', 'West End Road, Negril', 4.7, 420.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'The Caves' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Couples Tower Isle', 'A3, Ocho Rios', 4.5, 310.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Couples Tower Isle' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Couples Swept Away', 'Norman Manley Blvd, Negril', 4.6, 330.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Couples Swept Away' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Rockhouse Hotel', 'West End Road, Negril', 4.6, 270.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Rockhouse Hotel' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Geejam', 'San San, Port Antonio', 4.6, 500.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Geejam' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Trident Hotel', 'Anchovy, Port Antonio', 4.7, 700.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Trident Hotel' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Spanish Court Hotel', '1 St Lucia Ave, Kingston', 4.4, 190.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Spanish Court Hotel' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Courtyard Kingston', '1 Park Close, Kingston 5', 4.3, 170.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Courtyard Kingston' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'AC Hotel Kingston', '38-42 Lady Musgrave Rd, Kingston', 4.4, 200.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'AC Hotel Kingston' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Terra Nova All Suite Hotel', '17 Waterloo Rd, Kingston', 4.4, 160.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Terra Nova All Suite Hotel' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Jewel Paradise Cove', 'Runaway Bay, St. Ann', 4.3, 220.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Jewel Paradise Cove' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Bahia Principe Grand', 'Runaway Bay, St. Ann', 4.2, 210.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Bahia Principe Grand' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Grand Palladium Jamaica Resort & Spa', 'Point, Lucea', 4.3, 240.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Grand Palladium Jamaica Resort & Spa' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Azul Beach Resort Negril', 'Norman Manley Blvd, Negril', 4.4, 260.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Azul Beach Resort Negril' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Royalton Blue Waters', 'Highway A1, Falmouth', 4.5, 290.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Royalton Blue Waters' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Royalton White Sands', 'Highway A1, Falmouth', 4.3, 270.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Royalton White Sands' AND CountryId = @JamaicaId);

-- Airbnb-style listings (stored in Hotels table)
INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Airbnb Cozy Studio Kingston 6', 'Barbican, Kingston 6', 4.5, 65.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Airbnb Cozy Studio Kingston 6' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Airbnb Modern Loft New Kingston', 'New Kingston, Kingston', 4.6, 90.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Airbnb Modern Loft New Kingston' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Airbnb Seaside Cottage Port Antonio', 'San San, Port Antonio', 4.7, 120.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Airbnb Seaside Cottage Port Antonio' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Airbnb Ocean View Apt Montego Bay', 'Freeport, Montego Bay', 4.5, 110.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Airbnb Ocean View Apt Montego Bay' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Airbnb Garden Villa Ocho Rios', 'Content Gardens, Ocho Rios', 4.6, 150.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Airbnb Garden Villa Ocho Rios' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Airbnb Hilltop Retreat Mandeville', 'Ingleside, Mandeville', 4.4, 85.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Airbnb Hilltop Retreat Mandeville' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Airbnb Beachfront Bungalow Negril', 'Seven Mile Beach, Negril', 4.7, 180.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Airbnb Beachfront Bungalow Negril' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Airbnb City Center Studio Montego Bay', 'Gloucester Ave, Montego Bay', 4.4, 75.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Airbnb City Center Studio Montego Bay' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Airbnb Country Escape St. Ann', 'Mammee Bay, St. Ann', 4.3, 70.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Airbnb Country Escape St. Ann' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Airbnb Luxe Penthouse Kingston', 'Trafalgar Rd, Kingston', 4.8, 200.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Airbnb Luxe Penthouse Kingston' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Airbnb Rustic Cabin Blue Mountains', 'Irish Town, St. Andrew', 4.6, 140.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Airbnb Rustic Cabin Blue Mountains' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Airbnb Marina Condo Port Royal', 'Port Royal, Kingston', 4.2, 95.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Airbnb Marina Condo Port Royal' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Airbnb Colonial Home Falmouth', 'Water Square, Falmouth', 4.5, 130.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Airbnb Colonial Home Falmouth' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Airbnb Riverside Cottage Black River', 'Black River, St. Elizabeth', 4.4, 100.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Airbnb Riverside Cottage Black River' AND CountryId = @JamaicaId);

INSERT INTO Hotels (Name, Address, Rating, PerNightRate, CountryId)
SELECT 'Airbnb Treasure Beach Villa', 'Calabash Bay, Treasure Beach', 4.7, 210.00, @JamaicaId
WHERE NOT EXISTS (SELECT 1 FROM Hotels WHERE Name = 'Airbnb Treasure Beach Villa' AND CountryId = @JamaicaId);

COMMIT TRANSACTION;
