-- Active: 1713932005808@@bvbkpuxgbrboeucgwftx-mysql.services.clever-cloud.com@3306@bvbkpuxgbrboeucgwftx

table Duvan database 

CREATE TABLE Usuarios (
    Id INT AUTO_INCREMENT PRIMARY KEY,
	DocumentoIdentidad VARCHAR(255)
);

CREATE TABLE Turnos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UsuariosId INT,
    NameTurno varchar(45),
    FechaHoraInicio DATETIME,
    typeServicio Varchar(45),
	FechaHoraFin DATETIME null,
    Estado VARCHAR(20),
    Modulo Varchar(45)
);

CREATE TABLE AsesoresRecepcion (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Correo VARCHAR(255) UNIQUE,
    Contrasena VARCHAR (255),
    Modulo Varchar(45)
);


/*Agregamos columna*/
alter table AsesoresRecepcion add Roles Json;

CREATE TABLE AsignacionTurnos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    TurnoId INT,
    AsesorId INT,
    FOREIGN KEY (AsesorId) REFERENCES AsesoresRecepcion(Id),
    FOREIGN KEY (TurnoId) REFERENCES Turnos(Id)
);

#select
select * from Turnos;
select * from Usuarios;
select * from Servicios;


#drop table

Drop Table AsesoresRecepcion;
Drop table Turnos;
Drop table Servicios;
Drop table AsignacionTurnos;


#INSERT INTO 

INSERT INTO Turnos(FechaHoraInicio,UsuariosId,typeServicio,Estado,NameTurno)values('2024-8-2',1,"Medicamento","En espera","MDI-01");

insert into Turnos(FechaHoraInicio,UsuariosId,typeServicio,Estado,NameTurno)values('2024-9-12',1,"medicamentos","En espera","M-12");

insert into Turnos(FechaHoraInicio, UsuariosId, typeServicio, Estado, NameTurno) values
('2024-01-15 08:30:00', 5, "Medicamentos", "En espera", "C-01"),
('2024-02-28 10:15:00', 3, "General", "En espera", "E-02"),
('2024-03-10 14:45:00', 7, "General", "En espera", "O-03"),
('2024-04-05 11:00:00', 2, "Medicamentos", "En espera", "V-04"),
('2024-05-20 09:30:00', 6, "terapia física", "En espera", "T-05"),
('2024-06-08 13:00:00', 4, "Pagos", "En espera", "R-06"),
('2024-07-17 15:45:00', 8, "Medicamentos", "En espera", "C-07"),
('2024-08-22 16:20:00', 9, "Pagos", "En espera", "A-08"),
('2024-09-09 10:00:00', 10, "Pagos", "En espera", "P-09"),
('2024-10-30 08:00:00', 11, "General", "En espera", "CV-10");

insert into Turnos(FechaHoraInicio, UsuariosId, typeServicio, Estado, NameTurno) values(('2024-01-15 08:30:00', 5, "Pagos", "En espera", "C-01"));
INSERT INTO AsesoresRecepcion(Correo,Contrasena,Modulo,Roles)
values
("Duvan@lamisericordia.com","123","1",'["Asesor"]'),
("Mateo@lamisericordia.com","123","2",'["Asesor"]'),
("Fernando@lamisericordia.com","123","3",'["Asesor"]'),
("Machado@lamisericordia.com","123","4",'["Asesor"]'),
("Robinson@lamisericordia.com","123","5",'["Asesor"]'),
("Juan@adminlamisericordia.com", "juan123", "N/A", '["Admin"]');
truncate table Turnos;


truncate table AsesoresRecepcion;
#foreign key
# FOREIGN KEY (UsuariosId) REFERENCES Usuarios(Id);