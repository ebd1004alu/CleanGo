using CleanGo.Domain.Entities;

namespace CleanGo.Tests.Domain
{
    public class UserTests
    {
        [Fact]

        public void Constructor_Should_Set_Properties_Correctly()
        {
            var dateOfBirth = DateTime.UtcNow.AddYears(-25);
            var createdAt = DateTime.UtcNow;
            var user = new User
                (
                "Test",
                "User Domain",
                "123456789",
                "testuser@example.com",
                "Address 1",
                "hashedPassword",
                dateOfBirth,
                createdAt
                );

            Assert.Equal("Test", user.FirstName);
            Assert.Equal("User Domain", user.LastName);            
            Assert.Equal(dateOfBirth, user.DateOfBirth);
            Assert.Equal(createdAt, user.CreatedAt);    
        }
    }
}
