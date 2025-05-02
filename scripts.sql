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
	CONSTRAINT FK_Usuarios_Roles FOREIGN KEY (ID) REFERENCES Roles(ID)
);


--dotnet ef dbcontext scaffold "Server=localhost\\SQLEXPRESS;Database=GifsApp;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models --context AppDbContext