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
      isbn varchar(250) not null
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
