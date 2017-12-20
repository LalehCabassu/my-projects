// Fig. 10.8: Employee.cs
// Employee class with references to other objects.
public class Employee
{
   public string FirstName { get; private set; }
   public string LastName { get; private set; }
   public Date BirthDate { get; private set; }
   public Date HireDate { get; private set; }

   // constructor to initialize name, birth date and hire date
   public Employee( string first, string last,
      Date dateOfBirth, Date dateOfHire )
   {
      FirstName = first;
      LastName = last;
      BirthDate = dateOfBirth;
      HireDate = dateOfHire;
   }

   // convert Employee to string format
   public override string ToString()
   {
      return string.Format( "{0}, {1}  Hired: {2}  Birthday: {3}",
         LastName, FirstName, HireDate, BirthDate );
   }
} 