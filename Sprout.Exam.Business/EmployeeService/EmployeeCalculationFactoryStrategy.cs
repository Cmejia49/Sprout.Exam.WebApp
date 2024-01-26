using Sprout.Exam.Common.Constant;
using Sprout.Exam.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.EmployeeService
{
    public class RegularEmployeeComputationStrategy : IEmployeeSalaryCalculation
    {
        private const decimal taxRate = 0.12m;
        private const decimal regularWorkday = 23.00m;
        public decimal Calculate(decimal absentDays, decimal workedDays)
        {

            if(absentDays <= 0) {
                return SalaryRate.RegularRate - (SalaryRate.RegularRate * taxRate);
            }
            var workedDay = regularWorkday - absentDays;
            return SalaryRate.RegularRate - (SalaryRate.RegularRate / workedDay) - (SalaryRate.RegularRate * taxRate);
        }
    }

    public class ContractualEmployeeComputationStrategy : IEmployeeSalaryCalculation
    {
        public decimal Calculate(decimal absentDays, decimal workedDays)
        {
            if(workedDays <= 0) { return 0; }
            return SalaryRate.ContractualRate * workedDays;
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
