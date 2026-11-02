using System;
using System.Collections.Generic;

namespace Database_First_Approach_of_Entity_Core_Framework.Models;

public partial class Efmigrationshistory
{
    public string MigrationId { get; set; } = null!;

    public string ProductVersion { get; set; } = null!;
}
