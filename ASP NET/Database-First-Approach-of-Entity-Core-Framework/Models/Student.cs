using System;
using System.Collections.Generic;

namespace Database_First_Approach_of_Entity_Core_Framework.Models;

public partial class Student
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public int Age { get; set; }

    public string Gender { get; set; } = null!;

    public int Standard { get; set; }
}
