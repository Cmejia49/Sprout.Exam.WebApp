using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.EmployeeService
{
    public interface IEmployeeSalaryCalculation
    {
        decimal Calculate(decimal absentDays, decimal workedDays);
    }
}