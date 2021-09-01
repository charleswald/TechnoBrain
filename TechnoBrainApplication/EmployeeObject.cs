using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechnoBrainApplication
{
    public class EmployeeObject
    {
        private string _id, _managerId = "";
        private long _salary = 0;

        /// <summary>
        /// sets and gets employee's id
        /// </summary>
        public string Id
        {
            get => _id;
            set => _id = value;
        }

        /// <summary>
        /// sets and get the id of the personell who this employee reports to
        /// </summary>
        public string ManagerId
        {
            get => _managerId;
            set => _managerId = value;
        }
        /// <summary>
        /// sets and gets employees Salary
        /// </summary>

        public long Salary
        {
            get => _salary;
            set => _salary = value;
        }

        public override bool Equals(object obj)
        {
            EmployeeObject emp1 = (EmployeeObject)obj;
            return (emp1.Id.ToUpper().Equals(Id.ToUpper()));
        }
    }
}
