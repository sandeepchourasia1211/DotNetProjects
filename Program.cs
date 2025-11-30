using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


using var db = new OrganizationDb();

// Ensures DB + Tables are created
db.Database.EnsureCreated();

Console.WriteLine(" Database & Tables Created Successfully!");

OrganizationDb organizationDb=new OrganizationDb();
List<Department> dept = organizationDb.Departments.ToList();

foreach(var D in dept)
{
    Console.WriteLine($"Did : {D.Did}   DName: {D.Dname}   Description: {D.Description} ");
}

#region DepartmentTable/Design Entities
[Table("Department")]
public class Department
{
    [Key]
    public int Did { get; set; }

    public String? Dname { get; set; }
    public String? Description { get; set; }

    public List<Employee>? Employees { get; set; }
}
#endregion
#region EmployeeTable

[Table("Employee")]
public class Employee
{
    [Key]
    public int Eid { get; set; }
    public String? EName { get; set; }
    public String? Gender { get; set; }
    public String? Email { get; set; }
    public String? Salary { get; set; }
   
    [ForeignKey("Department")]
    public int? Did { get; set; }

    public Department? Department { get; set; }


}
#endregion
[Table("Student")]
public class Student
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Sid { get; set; }
    public String? Name { get; set; }
    public String? Gender { get; set; }

}
#region EFCoreDbContext
public class OrganizationDb : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Server=localhost;Database=OrgEFDb;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Student> Students { get; set; }

}
#endregion


