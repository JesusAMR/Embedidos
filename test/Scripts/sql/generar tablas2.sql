create table registrosAcceso(
	registroID int identity primary key,
	dtFecha datetime null,
	bValido bit null,
	bEliminado bit null
)