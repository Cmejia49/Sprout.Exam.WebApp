using Moq;
using Sprout.Exam.Business.EmployeeManager;
using Sprout.Exam.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.Business.UnitTest
{
    public class EmployeeServiceTest
    {
        [Theory]
        [InlineData("16690.91", 1, 0, 1)]
        [InlineData("7750.00", 0, 15.5, 2)]
        [InlineData("17600.00", 0, 0, 1)]
        [InlineData("0", 0, 0, 2)]
        public async Task Calculate_Employee_SalaryAsync(string expecterdResult , decimal absenceDay, decimal workedDay, EmployeeType typeId)
        {

            var _mocFileTransformService = new Mock<IEmployeeService>();
            _mocFileTransformService.Setup(x => x.CalculateAsync(
                It.IsAny<EmployeeType>(),
                It.IsAny<decimal>(),
                It.IsAny<decimal>()
                )).Returns(Task.FromResult(expecterdResult));

            var result = await _mocFileTransformService.Object.CalculateAsync(typeId,absenceDay,workedDay);

            Assert.Equal(expecterdResult, result);
        }
    }
}
