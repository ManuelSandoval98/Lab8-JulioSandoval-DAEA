-- Crear base de datos
CREATE DATABASE LINQExample;

-- Cambiar a la base de datos recién creada
\c LINQExample;

-- Crear tabla de clientes
CREATE TABLE Clients (
    ClientId SERIAL PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL
);

-- Crear tabla de productos
CREATE TABLE Products (
    ProductId SERIAL PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Description VARCHAR(100) NULL,
    Price DECIMAL(10, 2) NOT NULL
);

-- Crear tabla de pedidos (Orders)
CREATE TABLE Orders (
    OrderId SERIAL PRIMARY KEY,
    ClientId INT NOT NULL,
    OrderDate TIMESTAMP NOT NULL,
    FOREIGN KEY (ClientId) REFERENCES Clients(ClientId)
);

-- Crear tabla de detalles de la orden (OrderDetails)
CREATE TABLE OrderDetails (
    OrderDetailId SERIAL PRIMARY KEY,
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

-- Insertar clientes
INSERT INTO Clients (Name, Email) VALUES 
('Juan Pérez', 'juan.perez@example.com'),
('Ana Gómez', 'ana.gomez@example.com'),
('Carlos Díaz', 'carlos.diaz@example.com'),
('Lucía Martínez', 'lucia.martinez@example.com');

-- Insertar productos
INSERT INTO Products (Name, Price, Description) VALUES
('Producto A', 10.50,"This product is cheap"),
('Producto B', 25.00,"This product is pretty"),
('Producto C', 15.75,"This product is awesome"),
('Producto D', 30.20,NULL);

-- Insertar pedidos
INSERT INTO Orders (ClientId, OrderDate) VALUES
(1, '2025-05-01'),
(1, '2025-05-02'),
(2, '2025-05-03'),
(2, '2025-05-04'),
(3, '2025-05-05'),
(3, '2025-05-06'),
(4, '2025-05-07');

-- Insertar detalles de la orden (OrderDetails)
INSERT INTO OrderDetails (OrderId, ProductId, Quantity) VALUES
(1, 2, 2),  -- Orden 1: 2 Productos B
(1, 1, 1),  -- Orden 1: 1 Producto A
(2, 3, 1),  -- Orden 2: 1 Producto C
(2, 4, 1),  -- Orden 2: 1 Producto D
(3, 1, 3),  -- Orden 3: 3 Productos A
(3, 2, 2),  -- Orden 3: 2 Productos B
(4, 3, 1);  -- Orden 4: 1 Producto C