Create Table Roles (
	ID int identity not null,
	Rol varchar(100) not null,
	CONSTRAINT PK_Roles PRIMARY KEY (ID)
);

Create Table Usuarios (
	ID int identity not null,
	Nombre varchar(100) not null,
	Contrasena varchar(100),
	Rol int,
	CONSTRAINT PK_Usuarios PRIMARY KEY (ID),
	CONSTRAINT FK_Usuarios_Roles FOREIGN KEY (Rol) REFERENCES Roles(ID)
);

use GifsApp;
insert into Roles(Rol) VALUES('ROOT');
SELECT * FROM Roles;
select * from Usuarios;

drop table Usuarios;


Create Table Chat (
	ID int identity not null,
	Mensaje varchar(100) not null,
	Fecha datetime not null,
	IdUsuario int,
	CONSTRAINT FK_Chat_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuarios(ID)
);


ALTER TABLE Chat
ADD ConUsuario INT;


ALTER TABLE Chat
ADD CONSTRAINT FK_Usuario_Chat FOREIGN KEY (ConUsuario) REFERENCES Usuarios(Id);


--dotnet ef dbcontext scaffold "Server=localhost\SQLEXPRESS;Database=GifsApp;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models --context AppDbContext