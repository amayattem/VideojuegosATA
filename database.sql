CREATE DATABASE IF NOT EXISTS catalogo_videojuegos;
USE catalogo_videojuegos;

CREATE TABLE IF NOT EXISTS Usuarios (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre_usuario VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(100) NOT NULL
);

INSERT INTO Usuarios (nombre_usuario, password) VALUES 
('admin', '1234'),
('usuario1', 'abcd');