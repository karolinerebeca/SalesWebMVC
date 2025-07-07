namespace SalesWebMVC.Models;

public class Seller
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; } 
    public DateTime BirthDay { get; set; }
    public double BaseSalary { get; set; }

    public int? DepartmentId { get; set; }
    public Department? Department { get; set; }
    public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();


    public Seller() 
    {
    }

    public Seller(int id, string? name, string? email, DateTime birthDay, double baseSalary, Department? department)
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
