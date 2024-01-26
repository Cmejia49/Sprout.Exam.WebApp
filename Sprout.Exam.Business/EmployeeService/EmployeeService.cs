using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.EmployeeService;
using Sprout.Exam.Common;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.DataAccess;
using Sprout.Exam.DataAccess.Repository;
using Sprout.Exam.DataAccess.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.EmployeeManager
{
    public class EmployeeService : CommonService, IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;
        public EmployeeService(IUnitOfWork<SproutExamDbContext> unitOfWork) : base(unitOfWork)
        {
            _employeeRepository = unitOfWork.GetEntityRepository<Employee>();
        }

        public async Task<string> CalculateAsync(EmployeeType typeId, decimal absentDays, decimal workedDays)
        {
   
            var strategy = EmployeeComputationFactory.CreateComputationStrategy(typeId);

            if (strategy == null)
            {
                throw new ArgumentNullException("type in not valid!");
            }

            decimal computedValue = strategy.Calculate(absentDays, workedDays);
            return computedValue.ToString("0.00");

        }

        public async Task<int> CreateAsync(CreateEmployeeDto request)
        {
            var emp = new Employee()
            {
                Tin = request.Tin,
                FullName = request.FullName,
                Birthdate = request.Birthdate,
                EmployeeTypeId = request.EmployeeTypeId
            };
            await _employeeRepository.CreateAsync(emp);
            await UnitOfWork.SaveAsync();
            return emp.Id;
        }
        //soft delete
        public async Task<bool> DeleteByIdAsync(int id)
        {
            var emp = await _employeeRepository.GetByIdAsync(id);
            emp.IsDeleted = true;
            await _employeeRepository.UpdateAsync(emp,id);
            return await UnitOfWork.SaveAsync() > 0;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAsync()
        {
           var result = await _employeeRepository.Get().Where(x => !x.IsDeleted).Select(x => new EmployeeDto
           {
               Birthdate = x.Birthdate.ToString("yyyy-MM-dd"),
               FullName = x.FullName,
               Tin = x.Tin,
               EmployeeTypeId = x.EmployeeTypeId,
               Id = x.Id,
           }).ToListAsync();

            return(result);
        }

        public async Task<EmployeeDto> GetByIdAsync(int id)
        {
            var result = await _employeeRepository.GetByIdAsync(id);
            var empDto = new EmployeeDto()
            {
                FullName = result.FullName,
                Birthdate = result.Birthdate.ToString("yyyy-MM-dd"),
                Tin = result.Tin,
                EmployeeTypeId = result.EmployeeTypeId,
                Id = result.Id,
            };
            return empDto;
        }

        public async Task<bool> UpdateAsync(EditEmployeeDto request)
        {
            try
            {

                await _employeeRepository.UpdateAsync(request, request.Id);
                return await UnitOfWork.SaveAsync() > 0;
            }
            catch (Exception ex)
            {

            }
            return false;
        }
    }
}
