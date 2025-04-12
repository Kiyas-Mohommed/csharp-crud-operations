USE students_crud;

CREATE TABLE registration (
	id INT PRIMARY KEY IDENTITY,
	reg_no INT UNIQUE NOT NULL,
	first_name VARCHAR(15) NOT NULL,
	last_name VARCHAR(15) NOT NULL,
	date_of_birth VARCHAR(50) NOT NULL,
	gender VARCHAR(6) NOT NULL,
	address VARCHAR(50) NOT NULL,
	email VARCHAR(25) UNIQUE NOT NULL,
	home_number VARCHAR(10) NULL,
	mobile_number VARCHAR(10) NOT NULL,
	parent_name VARCHAR(25) NOT NULL,
	nic_number VARCHAR(12) NOT NULL,
	parent_number VARCHAR(10) NULL
);

SELECT * FROM registration;