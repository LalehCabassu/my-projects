// Fig. 10.9: EmployeeTest.cs
// Composition demonstration.
using System;

public class EmployeeTest
{
    public static void Main(string[] args)
    {
        Date birth = new Date(7, 24, 1949);
        Date hire = new Date(3, 12, 1988);
        Employee employee = new Employee("Bob", "Blue", birth, hire);
        
        Console.WriteLine(employee);
    }
}