﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models;
[Table("tb_m_accounts")]

public class Account
{
    [Key, Column("employee_nik", TypeName = "char(5)")]
    public string EmployeeNIK { get; set; }
    [Column("password")]
    public string Password { get; set; }

    //Cardinality
    [JsonIgnore]
    public ICollection<AccountRole>? AccountRoles { get; set; }
    [JsonIgnore]
    public Employee? Employees { get; set; }

}
