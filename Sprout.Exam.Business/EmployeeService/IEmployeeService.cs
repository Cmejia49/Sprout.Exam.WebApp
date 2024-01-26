using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.EmployeeManager
{
    public interface IEmployeeService
    {
         Task<int> CreateAsync(CreateEmployeeDto request);
         Task<IEnumerable<EmployeeDto>> GetAsync();
         Task<EmployeeDto> GetByIdAsync(int id);
         Task<bool> UpdateAsync(EditEmployeeDto request);
         Task<bool> DeleteByIdAsync(int id);
         Task<string> CalculateAsync(EmployeeType typeId, decimal absentDays, decimal workedDays);
    }
}
