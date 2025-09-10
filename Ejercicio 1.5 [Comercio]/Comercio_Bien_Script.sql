Create Database [Comercio_Bien]
GO
USE [Comercio_Bien]
GO
/****** Object:  Table [dbo].[Articulos]    Script Date: 05/09/2025 17:02:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articulos](
	[cod_articulo] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[pre_unitario] [decimal](18, 0) NULL,
 CONSTRAINT [pk_articulo] PRIMARY KEY CLUSTERED 
(
	[cod_articulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetallesFacturas]    Script Date: 05/09/2025 17:02:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetallesFacturas](
	[cod_detalleFactura] [int] NOT NULL,
	[nro_factura] [int] NULL,
	[cod_articulo] [int] NULL,
	[cantidad] [int] NULL,
 CONSTRAINT [pk_detalleFactura] PRIMARY KEY CLUSTERED 
(
	[cod_detalleFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Facturas]    Script Date: 05/09/2025 17:02:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Facturas](
	[nro_factura] [int] IDENTITY(1,1) NOT NULL,
	[fecha] [datetime] NULL,
	[cod_formaPago] [int] NULL,
	[cliente] [varchar](50) NULL,
 CONSTRAINT [pk_factura] PRIMARY KEY CLUSTERED 
(
	[nro_factura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FormasPago]    Script Date: 05/09/2025 17:02:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FormasPago](
	[cod_formaPago] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NULL,
 CONSTRAINT [pk_FormaPago] PRIMARY KEY CLUSTERED 
(
	[cod_formaPago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DetallesFacturas]  WITH CHECK ADD  CONSTRAINT [fk_articulo] FOREIGN KEY([cod_articulo])
REFERENCES [dbo].[Articulos] ([cod_articulo])
GO
ALTER TABLE [dbo].[DetallesFacturas] CHECK CONSTRAINT [fk_articulo]
GO
ALTER TABLE [dbo].[DetallesFacturas]  WITH CHECK ADD  CONSTRAINT [fk_factura] FOREIGN KEY([nro_factura])
REFERENCES [dbo].[Facturas] ([nro_factura])
GO
ALTER TABLE [dbo].[DetallesFacturas] CHECK CONSTRAINT [fk_factura]
GO
ALTER TABLE [dbo].[Facturas]  WITH CHECK ADD  CONSTRAINT [fk_formaPago] FOREIGN KEY([cod_formaPago])
REFERENCES [dbo].[FormasPago] ([cod_formaPago])
GO
ALTER TABLE [dbo].[Facturas] CHECK CONSTRAINT [fk_formaPago]
GO

--CRUD DE ARTICULOS
CREATE PROCEDURE SP_GUARDAR_ARTICULO
@codigo int,
@nombre Varchar(50),
@precio float
as
	begin
	IF @codigo = 0
		BEGIN
			insert into Articulos(nombre,pre_unitario) 
			values (@nombre,@precio)	
		END
	ELSE
		BEGIN
			update Articulos	set nombre= @nombre, pre_unitario= @precio
			where cod_articulo=@codigo
		END
END
GO

CREATE PROCEDURE SP_RECUPERAR_ARTICULOS
as	
begin
	Select * from Articulos
end
go

CREATE PROCEDURE SP_RECUPERAR_ARTICULO_POR_CODIGO
@codigo int
as
begin
	select * from Articulos where cod_articulo=@codigo
end
go

CREATE PROCEDURE SP_ELIMINAR_ARTICULO
@codigo int
as
begin	
	Delete from Articulos where cod_articulo=@codigo
end
go
--CRUD DE FACTURAS
CREATE PROCEDURE SP_RECUPERAR_FACTURAS
as
begin	
		select * from Facturas
end
go

CREATE PROCEDURE SP_RECUPERAR_FACTURA_POR_CODIGO
@codigo int
AS
begin
	select * from Facturas where nro_factura=@codigo
end
go

CREATE PROCEDURE SP_GUARDAR_FACTURA
	@cliente varchar(50),
	@fecha datetime,
	@id int output,
	@formaPago int
AS
begin	
	INSERT into Facturas (cliente, fecha, cod_formaPago) values (@cliente, GETDATE(), @formaPago);
	set @id = SCOPE_IDENTITY();
end
go
CREATE PROCEDURE SP_ACTUALIZAR_FACTURA
@codigo int,
@cliente varchar(50)
as
begin
	update Facturas set cliente=@cliente
	where nro_factura=@codigo
end

go
--CRUD DE DETALLE
CREATE PROCEDURE SP_INSERTAR_DETALLE
@id_detalle int,
@cod_articulo int,
@cantidad int,
@nro_factura int
as
begin
	INSERT into DetallesFacturas(cod_detalleFactura,cod_articulo,cantidad,nro_factura) values (@id_detalle,@cod_articulo,@cantidad,@nro_factura)
end
go
INSERT INTO FormasPago (descripcion) VALUES ('Efectivo');
INSERT INTO FormasPago (descripcion) VALUES ('Tarjeta de Cr dito');
INSERT INTO FormasPago (descripcion) VALUES ('Tarjeta de D bito');
INSERT INTO FormasPago (descripcion) VALUES ('Transferencia');
INSERT INTO FormasPago (descripcion) VALUES ('MercadoPago');

INSERT INTO Articulos (Nombre, pre_unitario) VALUES ('Coca-Cola 1.5L', 1200.00);
INSERT INTO Articulos (Nombre, pre_unitario) VALUES ('Yerba Mate 1Kg', 2500.00);
INSERT INTO Articulos (Nombre, pre_unitario) VALUES ('Pan Lactal', 1500.00);
INSERT INTO Articulos(Nombre, pre_unitario) VALUES ('Arroz 1Kg', 1800.00);
INSERT INTO Articulos(Nombre, pre_unitario) VALUES ('Aceite Girasol 1L', 3500.00);

INSERT INTO Facturas (Fecha, cod_formaPago, Cliente) VALUES ('2025-08-01', 1, 'Juan P rez');
INSERT INTO Facturas (Fecha, cod_formaPago, Cliente) VALUES ('2025-08-02', 2, 'Mar a L pez');
INSERT INTO Facturas (Fecha, cod_formaPago, Cliente) VALUES ('2025-08-03', 3, 'Carlos G mez');
INSERT INTO Facturas (Fecha, cod_formaPago, Cliente) VALUES ('2025-08-04', 4, 'Ana Rodr guez');
INSERT INTO Facturas (Fecha, cod_formaPago, Cliente) VALUES ('2025-08-05', 5, 'Pedro S nchez');

INSERT INTO DetallesFacturas (cod_detalleFactura,nro_factura,cod_articulo,cantidad) VALUES  (1,1,1,5);
INSERT INTO DetallesFacturas (cod_detalleFactura,nro_factura,cod_articulo,cantidad) VALUES	(2,2,2,20);
INSERT INTO DetallesFacturas (cod_detalleFactura,nro_factura,cod_articulo,cantidad) VALUES	(3,3,3,40);
INSERT INTO DetallesFacturas (cod_detalleFactura,nro_factura,cod_articulo,cantidad) VALUES	(4,4,4,70);
INSERT INTO DetallesFacturas (cod_detalleFactura,nro_factura,cod_articulo,cantidad) VALUES	(5,5,5,10);



