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
