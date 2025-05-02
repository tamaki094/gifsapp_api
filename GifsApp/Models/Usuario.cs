using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GifsApp.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Contrasena { get; set; }

    public int? Rol { get; set; }

    [JsonIgnore]
    public virtual Role? IdNavigation { get; set; }
}
