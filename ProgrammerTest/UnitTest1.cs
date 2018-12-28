using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Tests
{
    public class Base<T> where T : Base<T>, new()
    {
        public string Id { get; set; }

        private static readonly string _dbRoot = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        //private static string DbName => $"{typeof(T).Name}.db";

        public static T Find(string id)
        {
            var tType = typeof(T);
            var fileLocation = Path.Combine(_dbRoot, $"{tType.Name}.db");
            var dbRecord = !File.Exists(fileLocation) ? null : File.ReadLines(fileLocation).FirstOrDefault(l => l.StartsWith($"{id}|"));
            if (dbRecord == null)
            {
                return null;
            }

            var dbProps = dbRecord.Split('|');
            var index = 0;
            var t = new T();
            t.Id = dbProps[0];
            dbProps = dbProps.Skip(1).ToArray();
            foreach (var property in tType.GetProperties().Where(prop => prop.Name != "Id").OrderBy(prop => prop.Name))
            {
                if (property.PropertyType.Equals(dbProps[index].GetType()))
                {
                    property.SetValue(t, dbProps[index], null);
                }
                else if (property.PropertyType.Namespace == typeof(T).Namespace)
                {
                    property.SetValue(t, Address.Find(dbProps[index]), null);
                }
                index++;
            }

            return t;
        }

        public void Save()
        {
            if (!string.IsNullOrWhiteSpace(Id))
            {
                return;
            }

            var tType = typeof(T);
            var fileLocation = Path.Combine(_dbRoot, $"{tType.Name}.db");
            Id = (File.Exists(fileLocation) ? File.ReadLines(fileLocation).Count() + 1 : 1).ToString();

            using (var writer = new StreamWriter(fileLocation, true))
            {
                writer.Write($"{Id}|");
                foreach (var property in tType.GetProperties().Where(prop => prop.Name != "Id").OrderBy(prop => prop.Name))
                {
                    if (property.PropertyType.Namespace == typeof(T).Namespace)
                    {
                        var address = property.GetValue(this) as Address;
                        address.Save();
                        writer.Write($"{address.Id}|");
                    }
                    else
                    {
                        writer.Write($"{property.GetValue(this)}|");
                    }
                }

                writer.WriteLine();
            }
        }

        public void Delete()
        {
            var tType = typeof(T);
            var fileLocation = Path.Combine(_dbRoot, $"{tType.Name}.db");

            if (File.Exists(fileLocation))
            {
                File.WriteAllLines(fileLocation, File.ReadLines(fileLocation).Where(l => l.StartsWith($"{Id}|")).ToList());
                Id = null;
            }
            else
            {
                throw new Exception();
            }
        }

        public override bool Equals(object obj)
        {
            foreach (var property in GetType().GetProperties().OrderBy(prop => prop.Name))
            {
                if (property.PropertyType.Namespace != typeof(T).Namespace)
                {
                    if (!property.GetValue(this).Equals(property.GetValue(obj)))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }

    public class Address : Base<Address>
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public Address() {}

        public Address(string street, string city, string state, string zip)
        {
            Street = street;
            City = city;
            State = state;
            Zip = zip;
        }
    }

    public class Customer : Base<Customer>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }

        public Customer() {}

        public Customer(string firstName, string lastName, Address address)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
        }
    }

    public class Company : Base<Company>
    {
        public string Name { get; set; }
        public Address Address { get; set; }

        public Company() {}

        public Company(string name, Address address)
        {
            Name = name;
            Address = address;
        }
    }

    public class Tests
    {
        [Test]
        public void Test1()
        {
            var address = new Address("56 Main St", "Mesa", "AZ", "38574");
            var customer = new Customer("John", "Doe", address);
            var company = new Company("Google", address);

            Assert.That(customer.Id, Is.Null.Or.Empty);
            customer.Save();
            Assert.That(customer.Id, Is.Not.Null.Or.Empty);

            Assert.That(company.Id, Is.Null.Or.Empty);
            company.Save();
            Assert.That(company.Id, Is.Not.Null.Or.Empty);

            Customer savedCustomer = Customer.Find(customer.Id);
            Assert.IsNotNull(savedCustomer);
            Assert.AreSame(customer.Address, address);
            Assert.AreEqual(savedCustomer.Address, address);
            Assert.AreEqual(customer.Id, savedCustomer.Id);
            Assert.AreEqual(customer.FirstName, savedCustomer.FirstName);
            Assert.AreEqual(customer.LastName, savedCustomer.LastName);
            Assert.AreEqual(customer, savedCustomer);
            Assert.AreNotSame(customer, savedCustomer);

            Company savedCompany = Company.Find(company.Id);
            Assert.IsNotNull(savedCompany);
            Assert.AreSame(company.Address, address);
            Assert.AreEqual(savedCompany.Address, address);
            Assert.AreEqual(company.Id, savedCompany.Id);
            Assert.AreEqual(company.Name, savedCompany.Name);
            Assert.AreEqual(company, savedCompany);
            Assert.AreNotSame(company, savedCompany);

            customer.Delete();
            Assert.That(customer.Id, Is.Null.Or.Empty);
            Assert.IsNull(Customer.Find(customer.Id));

            company.Delete();
            Assert.That(company.Id, Is.Null.Or.Empty);
            Assert.IsNull(Company.Find(company.Id));
        }
    }
}