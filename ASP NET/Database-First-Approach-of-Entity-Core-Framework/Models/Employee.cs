using System;
using System.Collections.Generic;

namespace Database_First_Approach_of_Entity_Core_Framework.Models;

public partial class Employee
{
    public int EmpId { get; set; }

    public string FirstName { get; set; } = null!;

    public int EmpAge { get; set; }

    public int EmpSalary { get; set; }

    public string EmpDesignation { get; set; } = null!;
}
