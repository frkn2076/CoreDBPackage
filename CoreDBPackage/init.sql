create database if not exists db; 

use db;

create table Login (
  id int(11) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
  email varchar(50) NOT NULL,       
  password varchar(50)  NOT NULL
);