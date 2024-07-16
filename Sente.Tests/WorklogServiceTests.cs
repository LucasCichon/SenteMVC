using Moq;
using Sente.Domain.Interfaces;
using Sente.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sente.Tests
{
    [TestFixture]
    public class WorklogServiceTests
    {
        [Test]
        public void FilterWorklogsByAuthorAndEmployees_Should_Return_Filtered_Worklogs()
        {
            // Arrange
            var mockRepository = new Mock<IWorklogRepository>();
            mockRepository.Setup(repo => repo.GetAllWorklogs())
                .Returns(new List<WorklogEntry>
                {
                new Worklog { Author = "John Doe", Qualification = "RP", TimeSpent = 5 },
                new Worklog { Author = "Jane Smith", Qualification = "HD", TimeSpent = 3 }
                });

            var mockConfigService = new Mock<IConfigurationService>();
            mockConfigService.Setup(cs => cs.GetEmployees())
                .Returns(new List<EmployeeConfig>
                {
                new EmployeeConfig { Name = "John Doe", Qualifications = new List<string> { "RP", "R" } }
                });

            var worklogService = new WorklogService(mockConfigService.Object, mockRepository.Object);

            // Act
            var filteredWorklogs = worklogService.FilterWorklogsByAuthorAndEmployees("John Doe");

            // Assert
            filteredWorklogs.Should().HaveCount(1);
            filteredWorklogs.Should().ContainSingle(w => w.Author == "John Doe");
        }
    }
}
