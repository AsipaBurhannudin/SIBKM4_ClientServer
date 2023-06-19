using Client.Models;
using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels;

public class RegisterVM
{
    [MaxLength(5),MinLength(5,ErrorMessage ="{0} harus terdapat minimal {1} kombinasi Huruf Dan/Atau Angka")]
    public string NIK { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public Enum Gender { get; set; }
    public DateTime HiringDate { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [Phone]
    public string PhoneNumber { get; set; }
    public string Major { get; set; }
    public string Degree { get; set; }
    [Range(0,4, ErrorMessage = "{0} Gk Boleh Kurang Dari {1} dan Lebih dari {2}")]
    public string GPA { get; set; }
    public string UniversityName { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Password Harus Sama BosQ")]
    public string ConfirmPassword { get; set; }
}
