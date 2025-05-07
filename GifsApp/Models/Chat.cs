using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GifsApp.Models;

public partial class Chat
{
    [JsonIgnore]
    public int Id { get; set; }

    public string Mensaje { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public int? IdUsuario { get; set; }

    public int? ConUsuario { get; set; }

    [JsonIgnore]
    public virtual Usuario? ConUsuarioNavigation { get; set; }
    [JsonIgnore]
    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
