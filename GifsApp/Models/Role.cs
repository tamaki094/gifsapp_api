using System;
using System.Collections.Generic;

namespace GifsApp.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Rol { get; set; } = null!;

    public virtual Usuario? Usuario { get; set; }
}
