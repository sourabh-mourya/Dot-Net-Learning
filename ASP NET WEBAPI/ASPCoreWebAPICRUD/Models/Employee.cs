using System;
using System.Collections.Generic;

namespace ASPCoreWebAPICRUD.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public int Age { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Department { get; set; } = null!;

    public string Designation { get; set; } = null!;

    public decimal Salary { get; set; }

    public DateOnly JoiningDate { get; set; }

    public string Phone { get; set; } = null!;

    public string City { get; set; } = null!;
}
