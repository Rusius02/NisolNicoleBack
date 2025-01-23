/*Creation of the database tables*/
IF EXISTS (SELECT * FROM sysobjects WHERE name='users' and xtype='U')
    DROP TABLE users;


CREATE TABLE users(
      idUser int IDENTITY PRIMARY KEY,
      last_name varchar(60) not null,
      first_name varchar(60) not null,
      sexe varchar(20) not null,
      birthdate date not null,
      mail varchar(60) not null,
      pseudo varchar (60) not null,
      password varchar(500) not null,
      role varchar(20) default 'user',
      imageProfil varchar (500)
);     
IF EXISTS (SELECT * FROM sysobjects WHERE name='book' and xtype='U')
    DROP TABLE book;


CREATE TABLE book(
      idBook int IDENTITY PRIMARY KEY,
      name varchar(60) not null,
      description varchar(5000) not null,
      price DECIMAL(18, 2) not null,
      isbn varchar(250) not null,
      CoverImagePath varchar(255) not null
);   

IF EXISTS (SELECT * FROM sysobjects WHERE name='writing_event' and xtype='U')
    DROP TABLE writing_event;


CREATE TABLE writing_event(
      idWritingEvent int IDENTITY PRIMARY KEY,
      name varchar(60) not null,
      description varchar(5000) not null,
      theme varchar(50),
      start_date datetime not null,
      end_date datetime
);   

IF EXISTS (SELECT * FROM sysobjects WHERE name='order_books' AND xtype='U')
    DROP TABLE order_books;

-- Supprimer la table orders si elle existe
IF EXISTS (SELECT * FROM sysobjects WHERE name='orders' AND xtype='U')
    DROP TABLE orders;

CREATE TABLE orders (
    orderId INT IDENTITY PRIMARY KEY,
    userId INT NOT NULL,
    amount DECIMAL(10, 2) NOT NULL,
    paymentStatus NVARCHAR(50) NOT NULL,
    createdAt DATETIME NOT NULL DEFAULT GETDATE()
);

-- Création de la table order_books
CREATE TABLE order_books (
    orderBookId INT IDENTITY PRIMARY KEY,
    orderId INT NOT NULL,
    bookId INT NOT NULL,
    FOREIGN KEY (orderId) REFERENCES orders(orderId) ON DELETE CASCADE,
    FOREIGN KEY (bookId) REFERENCES books(idBook) ON DELETE CASCADE
);