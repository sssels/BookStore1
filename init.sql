-- Books tablosunu oluştur
CREATE TABLE Bookz (
    Id INTEGER NOT NULL PRIMARY KEY,
    Title TEXT NOT NULL,
    Author TEXT NOT NULL,
    Price DECIMAL NOT NULL,
    InStock INTEGER NOT NULL,
    AddedDate TEXT NOT NULL,
    UpdatedDate TEXT NOT NULL
);

-- 15 kitap ekle
INSERT INTO Bookz (Id, Title, Author, Price, InStock, AddedDate, UpdatedDate) VALUES
(101, 'The Great Gatsby', 'F. Scott Fitzgerald', 25.99, 1, '2024-01-10', '2024-01-10'),
(202, 'To Kill a Mockingbird', 'Harper Lee', 30.50, 0, '2024-02-15', '2024-02-15'),
(303, '1984', 'George Orwell', 45.00, 1, '2024-03-20', '2024-03-20'),
(404, 'Pride and Prejudice', 'Jane Austen', 35.75, 1, '2024-04-05', '2024-04-05'),
(505, 'The Catcher in the Rye', 'J.D. Salinger', 28.90, 1, '2024-05-10', '2024-05-10'),
(606, 'The Hobbit', 'J.R.R. Tolkien', 40.00, 1, '2024-06-14', '2024-06-14'),
(707, 'Fahrenheit 451', 'Ray Bradbury', 33.60, 0, '2024-01-20', '2024-01-20'),
(808, 'Jane Eyre', 'Charlotte Brontë', 29.99, 1, '2024-07-01', '2024-07-01'),
(909, 'Brave New World', 'Aldous Huxley', 38.50, 1, '2024-03-25', '2024-03-25'),
(1010, 'Wuthering Heights', 'Emily Brontë', 32.99, 1, '2024-06-30', '2024-06-30'),
(1111, 'The Lion, the Witch and the Wardrobe', 'C.S. Lewis', 27.50, 0, '2024-02-11', '2024-02-11'),
(1212, 'Animal Farm', 'George Orwell', 24.99, 1, '2024-04-14', '2024-04-14'),
(1313, 'The Chronicles of Narnia', 'C.S. Lewis', 35.00, 1, '2024-05-01', '2024-05-01'),
(1414, 'Moby Dick', 'Herman Melville', 44.00, 1, '2024-01-30', '2024-01-30'),
(1515, 'War and Peace', 'Leo Tolstoy', 50.99, 0, '2024-06-25', '2024-06-25');

SELECT * FROM Bookz;