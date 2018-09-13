create table dbo.imagenes(
	iCodImagen int primary key clustered identity,
	vbImagen varbinary(max) null
);

create table dbo.usuarios(
	iCodUsuario int primary key clustered identity,
	vchNombre varchar(100) null,
	iCodImagenAsociada int null foreign key references imagenes(iCodImagen)
);

create table dbo.registros(
	iCodRegistro int primary key clustered identity,
	iCodImagen int null foreign key references imagenes(iCodImagen),
	iCodUsuario int null foreign key references usuarios(iCodUsuario),
	bValido binary null,
	dtFecha datetime null
);
/*
if OBJECT_ID('dbo.registros', 'U') is not null 
begin
	drop table dbo.registros 
end

if OBJECT_ID('dbo.usuarios', 'U') is not null 
begin
	drop table dbo.usuarios 
end

if OBJECT_ID('dbo.imagenes', 'U') is not null 
begin
	drop table dbo.imagenes 
end
*/