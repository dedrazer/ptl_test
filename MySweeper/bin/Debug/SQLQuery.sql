use master;

if exists(select 1 from master..sysdatabases where Name = 'ptl_test')
begin
	drop database ptl_test;
end

create database ptl_test;

use ptl_test

create table minefield
(
	id int primary key identity(1,1),
	n int not null,
	m int not null,
	minefield text,
	check (n > 0),
	check (m > 0),
	check (n <= 100),
	check (m <= 100)
)