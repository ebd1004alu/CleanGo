using CleanGo.Domain.Entities;

namespace CleanGo.Tests.Domain
{
    public class ServiceTests
    {
        [Fact]
        public void Constructor_Should_Set_Properties_Correctly()
        {
            var service = new Service
                (
                "Service 1",
                "Service description",
                10,
                new TimeSpan(3, 2, 12)
                );
            Assert.Equal("Service 1", service.Name);
            Assert.Equal("Service description", service.Description);
            Assert.Equal(10, service.Price);
            Assert.Equal(new TimeSpan(3, 2, 12), service.Duration);
        }
    }
}
