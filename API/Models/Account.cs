﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;
[Table("tb_m_accounts")]

public class Account
{
    [Key, Column("employee_nik", TypeName ="char(5)")]
    public string EmployeeNIK { get; set; }
    [Column("password")]
    public string password { get; set; }

    //Cardinality
    public ICollection<AccountRole> AccountRoles { get; set; }
    public Employee Employee { get; set; }


}
