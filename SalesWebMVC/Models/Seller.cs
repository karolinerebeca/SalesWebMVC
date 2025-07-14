using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMVC.Models;

public class Seller
{
    public int Id { get; set; }

    [Required(ErrorMessage ="{0} required")]
    [StringLength(60, MinimumLength =3, ErrorMessage ="{0} size should be between {2} and {1}")]
    public string Name { get; set; }


    [Required(ErrorMessage = "{0} required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required(ErrorMessage = "{0} required")]
    [Display(Name = "Birth Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime BirthDay { get; set; }


    [Required(ErrorMessage = "{0} required")]
    [Range(100.0, 50000.0, ErrorMessage = "{0} must be between {1} and {2}")]
    [Display(Name = "Base Salary")]
    [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
    public double BaseSalary { get; set; }

    public Department? Department { get; set; }
    public int DepartmentId { get; set; }
    

    public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();


    public Seller() { }

    public Seller(int id, string name, string email, DateTime birthDay, double baseSalary, Department? department)
    {
        Id = id;
        Name = name;
        Email = email;
        BirthDay = birthDay;
        BaseSalary = baseSalary;
        Department = department;
    }

    public void AddSales(SalesRecord sr)
    {
        Sales.Add(sr);
    }

    public void RemoveSales(SalesRecord sr)
    { 
        Sales.Remove(sr); 
    }

    public double TotalSales(DateTime Initial, DateTime Final) 
    {
        return Sales.Where(sr => sr.Date >= Initial && sr.Date <= Final) .Sum(sr => sr.Amount);
    }

}
