
Requirements:

-    In C# create a single class that when subclassed allows this sample test code to run using only the file system for storage, no pre-built database allowed; use files. 
-    Create all classes required to compile and pass the test case; you may not modify the test. The test is not wrong, it's not a trick.
-    The Id, Save, Delete, and Find methods must be in the super class only; subclasses must not implement these methods themselves. 

[TestCase]
public void ProgrammerTest() {
    var address = new Address("56 Main St", "Mesa", "AZ", "38574");
    var customer = new Customer("John", "Doe", address);
    var company = new Company("Google", address);

    Assert.IsNullOrEmpty(customer.Id);
    customer.Save();
    Assert.IsNotNullOrEmpty(customer.Id);

    Assert.IsNullOrEmpty(company.Id);
    company.Save();
    Assert.IsNotNullOrEmpty(company.Id);

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
    Assert.IsNullOrEmpty(customer.Id);
    Assert.IsNull(Customer.Find(customer.Id));

    company.Delete();
    Assert.IsNullOrEmpty(company.Id);
    Assert.IsNull(Company.Find(company.Id));
}

