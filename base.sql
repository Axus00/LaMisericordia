-- Active: 1713932005808@@bvbkpuxgbrboeucgwftx-mysql.services.clever-cloud.com@3306@bvbkpuxgbrboeucgwftx
CREATE TABLE Usuarios (
    Id INT AUTO_INCREMENT PRIMARY KEY,
	DocumentoIdentidad VARCHAR(255)
);

CREATE TABLE Turnos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UsuariosId INT,
    ServicioId INT,
    FechaHoraInicio DATETIME,
	FechaHoraFin DATETIME,
    Estado VARCHAR(20), 
    FOREIGN KEY (UsuariosId) REFERENCES Usuarios(Id),
    FOREIGN KEY (ServicioId) REFERENCES Servicios(Id)
);

CREATE TABLE AsesoresRecepcion (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Correo VARCHAR(255) UNIQUE,
    Contrasena VARCHAR (255)
);

ALTER TABLE `AsesoresRecepcion` ADD Roles JSON;

TRUNCATE TABLE `AsesoresRecepcion`;

/*Insertar información*/
INSERT INTO AsesoresRecepcion (Correo, Contrasena) VALUES ('ejemplo@correo.com', 'contrasena123');
INSERT INTO `AsesoresRecepcion` (Correo, Contrasena, `Roles`) VALUES ('juan@lamisericordia.com', 'juan123', '["Asesor"]');


CREATE TABLE AsignacionTurnos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    TurnoId INT,
    AsesorId INT,
    FOREIGN KEY (AsesorId) REFERENCES AsesoresRecepcion(Id),
    FOREIGN KEY (TurnoId) REFERENCES Turnos(Id)
);
