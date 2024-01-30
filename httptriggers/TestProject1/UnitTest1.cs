using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using http_pratice.Domain;
using http_pratice.DAL;
using http_pratice;
using Microsoft.Azure.Cosmos;
using http_pratice.CommonUtilities.Modles;
using Microsoft.Extensions.Logging;
using http_pratice.CommonUtilities;
using System.Reflection;
using System.Net;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;

namespace TestProject1
{
    public class UnitTest1
    {

        public class StudentHttpTriggersTests
        {
            private readonly Mock<CosmosClient> _cosmosClientMock;
            private readonly Mock<IStudentDomain> _studentDomainMock;
            private readonly Mock<IStudentDal> _studentDalMock;
            private readonly StudentHttpTriggers _studentHttpTriggers;
            private readonly Mock<ILogger> _loggerMock;
            private readonly Mock<IEnumerable<Student>> _docmock;
            private readonly Mock<StudentCosmologic> _cosmologic;
            private readonly Mock<Microsoft.Azure.Cosmos.Container> _documentContainerMock;

            public StudentHttpTriggersTests()
            {
                _cosmosClientMock = new Mock<CosmosClient>();
                _studentDomainMock = new Mock<IStudentDomain>();
                _studentDalMock = new Mock<IStudentDal>();
                _loggerMock = new Mock<ILogger>();
                _cosmologic = new Mock<StudentCosmologic>();
                _docmock = new Mock<IEnumerable<Student>>();
                _documentContainerMock = new Mock<Container>();
                _studentHttpTriggers = new StudentHttpTriggers(_cosmosClientMock.Object, _studentDomainMock.Object);
            }




            [Fact]
            public async Task GetStudentDetalis_Return200OkStatus()
            {
                var httpRequestMessageMock = new Mock<HttpRequestMessage>();
                IEnumerable<Student> documents = new List<Student>
                {
                    new Student
                    {
                        id = "45",
                        Name = "swathi",
                        Age = 71,
                        Phone = "9494523304",
                        Email = "swathi@gmail.com"
                    }
                };
                var studentDomain = new StudentDomain(_studentDalMock.Object);
                //_studentDomainMock.Setup(x => x.ReadItem(It.IsAny<HttpRequest>(), It.IsAny<IEnumerable<dynamic>>())).Returns(Task.FromResult(It.IsAny<IActionResult>()));
                _studentDalMock.Setup(x => x.ReadItem(It.IsAny<IEnumerable<dynamic>>())).Returns(Task.FromResult(It.IsAny<IActionResult>()));
                var student = new StudentHttpTriggers(_cosmosClientMock.Object, studentDomain);
                dynamic response = await student.GetAllStudentInfo(httpRequestMessageMock.Object, documents, _loggerMock.Object);
                Assert.Equal(StatusCodes.Status200OK, response.StatusCode);

            }

            //[Fact]
            //public async Task GetStudentDetalis_ReturnBadRequestStatus()
            //{
            //    var httpRequestMock = new Mock<HttpRequest>();
            //    IEnumerable<Student> documents = new List<Student>
            //        {
            //            new Student
            //            {
            //                id = "45",
            //                Name = "swathi",
            //                Age = 71,
            //                Phone = "9494523304",
            //                Email = "swathi@gmail.com"
            //            }
            //        };

            //    var studentDomain = new StudentDomain(_studentDalMock.Object);
            //    _studentDomainMock.Setup(x => x.ReadItem(It.IsAny<HttpRequest>(), It.IsAny<IEnumerable<dynamic>>())).Returns(Task.FromResult(It.IsAny<IActionResult>()));
            //    _studentDalMock.Setup(x => x.ReadItem(It.IsAny<HttpRequest>(), It.IsAny<IEnumerable<dynamic>>())).Returns(Task.FromResult(It.IsAny<IActionResult>()));
            //    var student = new StudentHttpTriggers(_cosmosClientMock.Object, studentDomain);
            //    dynamic response = await student.GetAllStudentInfo(httpRequestMock.Object, documents, _loggerMock.Object);

            //    Assert.Equal(StatusCodes.Status400BadRequest, response.StatusCode);
            //    // Add more assertions if necessary
            //}
            //[Fact]
            //public async Task GetStudentById_ReturnsOkObject()
            //{
            //    var httpRequestMock = new Mock<HttpRequest>();
            //    var mock_id = "45";
            //    var expectedStudent = new Student
            //    {
            //        id = "45",
            //        Name = "swathi",
            //        Age = 71,
            //        Phone = "9494523304",
            //        Email = "swathi@gmail.com"

            //    };

            //    var itemResponseMock = new Mock<ItemResponse<Student>>();
            //    var expectedActionResult = new OkObjectResult(expectedStudent);

            //    itemResponseMock.SetupGet(r => r.Resource).Returns(expectedStudent);
            //    itemResponseMock.SetupGet(r => r.StatusCode).Returns(HttpStatusCode.OK);

            //    var studentDomain = new StudentDomain(_studentDalMock.Object);
            //    _documentContainerMock.Setup(c => c.ReadItemAsync<Student>(It.IsAny<String>(), It.IsAny<PartitionKey>(), default, default))
            //    .ReturnsAsync(itemResponseMock.Object);

            //    //_studentDomainMock.Setup(x => x.ReadItemById(It.IsAny<HttpRequest>(),mock_id, _documentContainerMock.Object)).Returns(Task.FromResult(It.IsAny<IActionResult>()));
            //    _studentDalMock.Setup(x => x.ReadItemById(It.IsAny<HttpRequest>(), mock_id, _documentContainerMock.Object)).Returns(Task.FromResult(It.IsAny<IActionResult>()));
            //    var student = new StudentHttpTriggers(_cosmosClientMock.Object, studentDomain);
            //    dynamic response = await student.GetStudentById(httpRequestMock.Object, _loggerMock.Object, mock_id);
            //    Assert.IsType<OkObjectResult>(response);
            //    var okObjectResult = response as OkObjectResult;
            //    Assert.Equal(expectedActionResult.Value, okObjectResult?.Value);

            //}


        }
    }
}
