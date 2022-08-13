using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEntityFramework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new AdventureWorkContext()) 
            {
                //Create and save a new Customer
                Console.Write("Create and save a new Customer. " + Environment.NewLine +  "Enter the first name for a new Customer: ");
                var firstName = Console.ReadLine();
                
                Console.WriteLine(" Thank you. " + Environment.NewLine +  "Enter its last name : ");
                var lastName = Console.ReadLine();

                Console.WriteLine(" Thank you. " + Environment.NewLine + "You have saved : " + firstName + " " + lastName);

                var customer = new Customer {
                    LastName = lastName,
                    FirstName = firstName,
                    PasswordHash = "-",
                    PasswordSalt = "-",
                    ModifiedDate = DateTime.Now,
                    rowguid = Guid.NewGuid()                    
                };
                db.Customers.Add(customer);

                try
                {
                    _ = db.SaveChanges();
                }
                catch(DbEntityValidationException e)
                {
                    Console.WriteLine(e.Message);
                }

                // Display the 10 last Customers from the database 
                var query = (from c in db.Customers
                            orderby c.CustomerID descending
                            select c).Take(10).ToList();

                Console.WriteLine("The 10 most recent names:");
                foreach (var person in query)
                {
                    Console.WriteLine(person.FirstName + " " + person.LastName);
                }

                Console.WriteLine("Thank you. Press any key to exit.");
                Console.ReadKey();
            }
        }
    }
}
