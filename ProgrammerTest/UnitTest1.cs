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

        private static readonly Type Type = typeof(T);

        private static readonly string DbLocation = Path.Combine(_dbRoot, $"{Type.Name}.db");

        private static readonly PropertyInfo[] Properties = Type.GetProperties().Where(prop => prop.Name != "Id").OrderBy(prop => prop.Name).ToArray();

        public static T Find(string id)
        {
            var dbRecord = !File.Exists(DbLocation) ? null : File.ReadLines(DbLocation).FirstOrDefault(l => l.StartsWith($"{id}|"));
            if (dbRecord == null)
            {
                return null;
            }

            var dbProps = dbRecord.Split('|');
            var t = new T
            {
                Id = dbProps[0]
            };
            dbProps = dbProps.Skip(1).ToArray();
            foreach (var property in Properties.Select((p, i) => new { Index = i, PropertyInfo = p }))
            {
                if (property.PropertyInfo.PropertyType.Equals(dbProps[property.Index].GetType()))
                {
                    property.PropertyInfo.SetValue(t, dbProps[property.Index], null);
                }
                else if (IsExtendedType(property.PropertyInfo))
                {
                    var findMethod = property.PropertyInfo.PropertyType.BaseType.GetMethod("Find");
                    var dbAddress = findMethod.Invoke(null, new[] { dbProps[property.Index] });

                    property.PropertyInfo.SetValue(t, dbAddress, null);
                }
            }

            return t;
        }

        public void Save()
        {
            if (!string.IsNullOrWhiteSpace(Id))
            {
                return;
            }

            Id = GetNextId();

            using (var writer = new StreamWriter(DbLocation, true))
            {
                writer.Write($"{Id}|");
                foreach (var property in Properties)
                {
                    if (IsExtendedType(property))
                    {
                        var address = property.GetValue(this);
                        var saveMethod = property.PropertyType.GetMethod("Save");
                        saveMethod.Invoke(address, new object[0]);
                        writer.Write($"{address.GetType().GetProperty("Id").GetValue(address)}|");
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
            if (File.Exists(DbLocation))
            {
                File.WriteAllLines(DbLocation, File.ReadLines(DbLocation).Where(l => !l.StartsWith($"{Id}|")).ToList());
                Id = null;
            }
            else
            {
                throw new Exception();
            }
        }

        public override bool Equals(object obj)
        {
            foreach (var property in GetType().GetProperties())
            {
                if (property.PropertyType.Namespace != Type.Namespace)
                {
                    if (!property.GetValue(this).Equals(property.GetValue(obj)))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private string GetNextId()
        {
            return (File.Exists(DbLocation) ? File.ReadLines(DbLocation).Count() + 1 : 1).ToString();
        }

        private static bool IsExtendedType(PropertyInfo property)
        {
            return property.PropertyType.Namespace == Type.Namespace
                && property.PropertyType == property.PropertyType.BaseType.GenericTypeArguments[0];
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