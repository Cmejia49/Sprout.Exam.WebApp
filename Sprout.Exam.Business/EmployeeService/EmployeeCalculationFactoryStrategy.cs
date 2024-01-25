using Sprout.Exam.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.EmployeeService
{
    public class RegularEmployeeComputationStrategy : IEmployeeSalaryCalculation
    {
        public decimal Calculate(decimal absentDays, decimal workedDays)
        {
            return 25000.00m;
        }
    }

    public class ContractualEmployeeComputationStrategy : IEmployeeSalaryCalculation
    {
        public decimal Calculate(decimal absentDays, decimal workedDays)
        {
            return 25.25m;
        }
    }

    public class EmployeeComputationFactory
    {
        public static IEmployeeSalaryCalculation CreateComputationStrategy(EmployeeType employeeType)
        {
            switch (employeeType)
            {
                case EmployeeType.Regular:
                    return new RegularEmployeeComputationStrategy();
                case EmployeeType.Contractual:
                    return new ContractualEmployeeComputationStrategy();
                default:
                    return null; // Or throw an exception if needed
            }
        }
    }

}
