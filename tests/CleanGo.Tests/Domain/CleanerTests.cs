
using CleanGo.Domain.Entities;
using System.Net;

namespace CleanGo.Tests.Domain
{
    public class CleanerTests
    {
        public void constructor_Should_Set_Properties_Correctly()
        {
            var dateOfBirth = DateTime.UtcNow.AddYears(-25);
            var hireDate = DateTime.UtcNow.AddYears(-1);
            var createdAt = DateTime.UtcNow;
            var cleaner = new Cleaner
                (
                "Test", 
                "Cleaner Domain", 
                "123456789", 
                "cleaner@example.com", 
                "Address 2", 
                "hashedPassword", 
                dateOfBirth, 
                hireDate, 
                20, 
                createdAt
                );
            Assert.Equal("Test", cleaner.FirstName);
            Assert.Equal("Cleaner Domain", cleaner.LastName);
            Assert.Equal(dateOfBirth, cleaner.DateOfBirth);
            Assert.Equal(hireDate, cleaner.HireDate);
            Assert.Equal(20, cleaner.Salary);
            Assert.Equal(createdAt, cleaner.CreatedAt);
        }
    }
}
