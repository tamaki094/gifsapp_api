using System;
using System.Collections.Generic;

namespace GifsApp.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Contrasena { get; set; }

    public int? Rol { get; set; }

    public virtual Role? RolNavigation { get; set; }
}
