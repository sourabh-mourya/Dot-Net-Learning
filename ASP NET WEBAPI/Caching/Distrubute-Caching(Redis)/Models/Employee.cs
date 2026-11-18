using System;
using System.Collections.Generic;

namespace Distrubute_Caching_Redis_.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Designation { get; set; } = null!;

    public string Department { get; set; } = null!;

    public decimal Salary { get; set; }

    public int WorkingYears { get; set; }

    public DateTime JoiningDate { get; set; }

    public bool IsActive { get; set; }
}
