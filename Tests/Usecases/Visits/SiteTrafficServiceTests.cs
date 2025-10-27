
using Application.UseCases.SiteTraffic;
using Domain;
using Infrastructure.SqlServer.Repository.SiteTraffic;
using Moq;

namespace Tests.Usecases.Visits
{
    public class SiteTrafficServiceTests
    {
        [Fact]
        public void RegisterVisit_ShouldReturnSiteVisitDto_WithValidIp()
        {
            // Arrange
            var mockRepo = new Mock<ISiteTrafficRepository>();
            mockRepo.Setup(r => r.Create(It.IsAny<SiteVisit>())).Returns(new SiteVisit { IpAddress = "127.0.0.1" });

            var service = new SiteTrafficService(mockRepo.Object);

            // Act
            var result = service.RegisterVisit("127.0.0.1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("127.0.0.1", result.IpAddress);
        }
    }
}
