using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
//////using Microsoft.VisualBasic.FileIO;

namespace TechnoBrainApplication
{
    public class Employees
    {
        readonly Dictionary<string, List<string>> _lstSubOrdinates = new Dictionary<string, List<string>>();
        private List<EmployeeObject> _lstEmployees = new List<EmployeeObject>();

        public List<EmployeeObject> LstEmployees => _lstEmployees;

        public Employees(String[] data)
        {
            ProcessEmployees(data);
        }


        public void ProcessEmployees(String[] data)
        {

            int totalceo = 0;//keep count of ceos

            //string[] data = GetData(path);


            foreach (var li in data)
            {
                try
                {
                    var parts = li.Split(',');
                    var temp = new EmployeeObject();
                    temp.Id = parts[0];
                    if (parts[1].Equals(""))
                    {
                        temp.ManagerId = "";
                        totalceo++;
                        //Managers are more than one throws Exception
                        if (totalceo > 1)
                        {
                           // throw new ManagerMoreThanOne("Managers are more than one... Exiting");
                            Console.WriteLine("Failed due to More than 1 CEO");
                            return;
                        }
                    }
                    else
                    {
                        temp.ManagerId = parts[1];
                    }


                    long salary;
                    var isvalid = Int64.TryParse(parts[2], out salary);
                    //is salary a valid number?
                    if (isvalid)
                    {
                        //valid salary should be greater than 0
                        if (salary > 0)
                        {
                            temp.Salary = salary;
                        }
                        else
                        {
                            
                            Console.WriteLine("Salary is a Negative");
                        }

                    }
                    else
                    {
                        //throw new SalaryInvalid("Salary is not valid");
                        Console.WriteLine("Salary is not valid");
                    }

                    _lstEmployees.Add(temp);
                }
                catch (Exception ex)
                {
                    //Data is not formed well. clear list of employees and exit
                    _lstEmployees.Clear();
                    Console.WriteLine(ex.Message);
                    return;
                }
                
            }

            foreach (var emp in _lstEmployees)
            {
                Add(emp.ManagerId, emp.Id);
            }

            //Verify That their is manager
            if (totalceo != 1)
            {
                Console.WriteLine("Failed due to no CEO Found ");
                _lstEmployees.Clear();
                return;
            }
        }

        public void Add(string boss, string employeeId)
        {
            Add(boss);
            Add(employeeId);
            _lstSubOrdinates[boss].Add(employeeId);
        }
        public void Add(string employeeId)
        {
            //if Employee ID exists do nothing
            if (_lstSubOrdinates.ContainsKey(employeeId))
            {
                return;
            }

            _lstSubOrdinates.Add(employeeId, new List<string>());
        }
        public long getSalaryBudget(String root)
        {
            long salary = 0;
            HashSet<String> visited = new HashSet<String>();
            Stack<String> stack = new Stack<String>();
            stack.Push(root);
            while (stack.Count != 0)
            {
                String empId = stack.Pop();
                if (!visited.Contains(empId))
                {
                    visited.Add(empId);
                    foreach (String v in GetSubordinates(empId))
                    {
                        stack.Push(v);
                    }
                }
            }

            if (visited.Count == 0) return salary;
            foreach (var id in visited)
            {
                salary += LookUp(id).Salary;
            }

            return salary;
        }

        public List<String> GetSubordinates(String empId)
        {
            return _lstSubOrdinates[empId];
        }

        public EmployeeObject LookUp(string id)
        {
            foreach (EmployeeObject emp in _lstEmployees)
            {
                if (emp.Id.Equals(id))
                {
                    return emp;
                }
            }

            return null;
        }

        public string[] GetData(String path)
        {

            return File.ReadAllLines(path);
        }

    }
}
