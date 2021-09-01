using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TechnoBrainApplication.EmployeeTest
{
    [TestClass()]
    public class EmployeesTest
    {

        private Employees _employees;

        

        [TestInitialize]
        public void TestInitiliaze()
        {
            var data = ProcessCsv("test1.csv");
            _employees = new Employees(data);
        }

        [TestMethod()]
        public void AddTest()
        {

            Assert.IsTrue(_employees.LstEmployees.Contains(new EmployeeObject
            { Id = "Employee2", ManagerId = "Employee1", Salary = 800 }));
            Assert.IsTrue(_employees.LstEmployees.Contains(new EmployeeObject
            { Id = "Employee4", ManagerId = "Employee2", Salary = 500 }));
        }

        [TestMethod]
        public void SubOrdinate_Not_NULL()
        {
            var subordinates = _employees.GetSubordinates("Employee2");
            Assert.AreEqual(2, subordinates.Count);
        }

        [TestMethod]
        public void Employee5_has_No_SubOrdinates_Test()
        {
            var subordinates = _employees.GetSubordinates("Employee5");
            Assert.AreEqual(0, subordinates.Count);
        }

        [TestMethod]
        public void LookUpTest()
        {
            EmployeeObject emp = _employees.LookUp("Employee1");
            Assert.IsNotNull(emp);
        }

        [TestMethod]
        public void Lookup_Wrong_emp_id_Test()
        {
            EmployeeObject emp = _employees.LookUp("Employee10");
            Assert.IsNull(emp);
        }

        [TestMethod]
        public void GetBudgetTest()
        {
            Assert.AreEqual(1800, _employees.getSalaryBudget("Employee2"));
            Assert.AreEqual(500, _employees.getSalaryBudget("Employee3"));
            Assert.AreEqual(3800, _employees.getSalaryBudget("Employee1"));
        }

        [TestMethod]
        public void Test_Invalid_Salary_Not_Added()
        {
            Employees h2 = new Employees(ProcessCsv("test2.csv"));
            Assert.IsFalse(h2.LstEmployees.Contains(new EmployeeObject
            { Id = "Employee5" }));
            Assert.IsFalse(h2.LstEmployees.Contains(new EmployeeObject
            { Id = "Employee2" }));

            Assert.AreEqual(0, h2.LstEmployees.Count);

        }

        [TestMethod]
        public void Test_Manager_More_Than_Three()
        {
            Employees h = new Employees(ProcessCsv("test3.csv"));
            Assert.IsFalse(h.LstEmployees.Contains(new EmployeeObject
            { Id = "Employee5" }));
            Assert.IsFalse(h.LstEmployees.Contains(new EmployeeObject
            { Id = "Employee1" }));
            Assert.AreEqual(0, h.LstEmployees.Count);

        }

        [TestMethod]
        public void Test_Negative_Salary_Check()
        {
            Employees h = new Employees(ProcessCsv("test4.csv"));
            Assert.IsFalse(h.LstEmployees.Contains(new EmployeeObject
            { Id = "Employee5" }));
            Assert.AreEqual(0, h.LstEmployees.Count);
        }

        [TestMethod]
        public void No_Manager_Record()
        {
            Employees h = new Employees(ProcessCsv("test5.csv"));
            Assert.AreEqual(0, h.LstEmployees.Count);
        }

        public string[] ProcessCsv(String path)
        {

            return File.ReadAllLines(path);
        }
    }

}
