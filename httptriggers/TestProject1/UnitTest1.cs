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
<<<<<<< HEAD
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Formatting;
using Microsoft.AspNetCore.Mvc.WebApiCompatShim;
using Microsoft.Extensions.Options;
using System.Reflection.Metadata;
using System.Collections;
using System.Net;
using System.Reflection;
using Azure;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;


=======
using System.Reflection;
using System.Net;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;

>>>>>>> 645198d923440a218678b613b60545179a158961
namespace TestProject1
{
    public class UnitTest1
    {

        public class StudentHttpTriggersTests
        {
<<<<<<< HEAD
            private Mock<CosmosClient> _cosmosClientMock;
            private Mock<IStudentDomain> _studentDomainMock;
            private Mock<IStudentDal> _studentDalMock;
            private StudentHttpTriggers _studentHttpTriggers;
            private Mock<ILogger> _loggerMock;
            private Mock<IEnumerable<Student>> _docmock;
            private Mock<StudentCosmologic> _cosmologic;
            private Mock<Microsoft.Azure.Cosmos.Container> _documentContainerMock;


            public void TestSetup()
            {
                this._cosmosClientMock = new Mock<CosmosClient>();
                this._studentDomainMock = new Mock<IStudentDomain>();
                this._studentDalMock = new Mock<IStudentDal>();
                this._loggerMock = new Mock<ILogger>();
                this._cosmologic = new Mock<StudentCosmologic>();
                this._docmock = new Mock<IEnumerable<Student>>();
                this._documentContainerMock = new Mock<Container>();
                this._studentHttpTriggers = new StudentHttpTriggers( _studentDomainMock.Object);
            }
            [Fact]
            public async Task GetStudentDetalis_Return200OkStatus()
            {
                TestSetup();
=======
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
>>>>>>> 645198d923440a218678b613b60545179a158961
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
<<<<<<< HEAD
                //_studentDalMock.Setup(x => x.ReadItem(It.IsAny<IEnumerable<dynamic>>())).Returns(Task.FromResult(It.IsAny<IActionResult>()));
                _studentDalMock.Setup(x => x.ReadItem())
         .Returns(Task.FromResult<IActionResult>(new OkResult()));

                var student = new StudentHttpTriggers( studentDomain);
                dynamic response = await student.GetAllStudentInfo(httpRequestMessageMock.Object, _loggerMock.Object);
                Assert.Equal(StatusCodes.Status200OK, response.StatusCode);

            }
            [Theory]
            [ClassData(typeof(StudentSuccessData))]
            public async Task GetStudentById_ReturnsOkObject(Student Mockdata)
            {
                var req = CreateMockRequest(Mockdata);
                TestSetup();
                var mock_id = "45";
                var studentDomain = new StudentDomain(_studentDalMock.Object);
                _studentDalMock.Setup(x => x.ReadItemById(It.IsAny<string>()))
                    .Returns(Task.FromResult<IActionResult>(new OkObjectResult(Mockdata)
                    { StatusCode = StatusCodes.Status200OK }));

                dynamic response = await studentDomain.ReadItemById(mock_id);
                Assert.IsType<OkObjectResult>(response);
                var okObjectResult = response as OkObjectResult;
                Assert.Equal(Mockdata, okObjectResult?.Value);
                Assert.Equal(StatusCodes.Status200OK, okObjectResult?.StatusCode);

            }
            [Theory]
            [ClassData(typeof(StudentEmptyId))]
            public async Task GetStudentById_ReturnsBadObject(Student Mockdata)
            {
                var req = CreateMockRequest(Mockdata);
                TestSetup();
                var mock_id = "45";
                var studentDomain = new StudentDomain(_studentDalMock.Object);
                _studentDalMock.Setup(x => x.ReadItemById(It.IsAny<string>()))
                    .Returns(Task.FromResult<IActionResult>(new OkObjectResult(Mockdata)
                    { StatusCode = StatusCodes.Status400BadRequest }));

                dynamic response = await studentDomain.ReadItemById(mock_id);
                Assert.IsType<OkObjectResult>(response);
                var okObjectResult = response as OkObjectResult;
                Assert.Equal(Mockdata, okObjectResult?.Value);
                Assert.Equal(StatusCodes.Status400BadRequest, okObjectResult?.StatusCode);


            }

            [Theory]
            [ClassData(typeof(StudentSuccessData))]
            public async Task PostStudentAsync_ReturnsOkObjectResult(Student student)
            {
                TestSetup();
                var studentDomain = new StudentDomain(_studentDalMock.Object);
                _studentDalMock.Setup(x => x.CreateItem(It.IsAny<Student>()))
              .Returns(Task.FromResult<IActionResult>(new OkObjectResult(student)));

                dynamic result = await studentDomain.CreateItem(student);

                // Assert
                Assert.IsType<OkObjectResult>(result);
                var okObjectResult = result as OkObjectResult;
                Assert.Equal(StatusCodes.Status200OK, okObjectResult?.StatusCode);

            }

            [Theory]
            [ClassData(typeof(StudentInvalidData))]
            public async Task PostStudentAsync_ReturnsBadObjectResult(Student student)
            {
                TestSetup();
                var studentDomain = new StudentDomain(_studentDalMock.Object);
                _studentDalMock.Setup(x => x.CreateItem(It.IsAny<Student>()))
              .Returns(Task.FromResult<IActionResult>(new BadRequestResult()));

                dynamic result = await studentDomain.CreateItem(student);

                // Assert
                Assert.IsType<BadRequestObjectResult>(result);
                var badRequestObjectResult = result as BadRequestObjectResult;
                Assert.Equal(StatusCodes.Status400BadRequest, badRequestObjectResult?.StatusCode);
                Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);

            }

            [Theory]
            [ClassData(typeof(StudentSuccessData))]
            public async Task DeleteStudentAsync_ReturnsOkObjectResult(Student Mockdata)
            {
                var req = CreateMockRequest(Mockdata);
                TestSetup();
                var mock_id = "1";
                var studentDomain = new StudentDomain(_studentDalMock.Object);
                _studentDalMock.Setup(x => x.DeleteItem(It.IsAny<string>()))
               .Returns(Task.FromResult<IActionResult>(new OkObjectResult(Mockdata)
               { StatusCode = StatusCodes.Status200OK }));

                dynamic response = await studentDomain.DeleteItem(mock_id);
                Assert.IsType<OkObjectResult>(response);
                var okObjectResult = response as OkObjectResult;
                Assert.Equal(Mockdata, okObjectResult?.Value);
                Assert.Equal(StatusCodes.Status200OK, okObjectResult?.StatusCode);

            }
            [Theory]
            [ClassData(typeof(StudentEmptyId))]
            public async Task DeleteStudentAsync_ReturnsBadObjectResult(Student Mockdata)
            {
                
                var req = CreateMockRequest(Mockdata);
                TestSetup();
                var mock_id = "1";
                var studentDomain = new StudentDomain(_studentDalMock.Object);
                _studentDalMock.Setup(x => x.DeleteItem(It.IsAny<string>()))
               .Returns(Task.FromResult<IActionResult>(new OkObjectResult(Mockdata)
               { StatusCode = StatusCodes.Status400BadRequest }));

                dynamic response = await studentDomain.DeleteItem(mock_id);
                Assert.IsType<OkObjectResult>(response);
                var okObjectResult = response as OkObjectResult;
                Assert.Equal(Mockdata, okObjectResult?.Value);
                Assert.Equal(StatusCodes.Status400BadRequest, okObjectResult?.StatusCode);

            }

            [Theory]
            [ClassData(typeof(StudentSuccessData))]
            public async Task UpdateStudentAsync_Returns200OkResult(Student student)
            {


                // Arrange
                TestSetup();
                var mock_id = "1";
                var studentDomain = new StudentDomain(_studentDalMock.Object);
                //            var itemResponseMock = new Mock<ItemResponse<Student>>();
                //            itemResponseMock.SetupGet(r => r.Resource).Returns(student);
                //            itemResponseMock.SetupGet(r => r.StatusCode).Returns(HttpStatusCode.OK);

                //            _documentContainerMock.Setup(c => c.ReadItemAsync<Student>(It.IsAny<string>(), It.IsAny<PartitionKey>(), It.IsAny<ItemRequestOptions>(),
                //    default))
                //.Returns(Task.FromResult(itemResponseMock.Object));

                _studentDalMock.Setup(x => x.UpdateItem(It.IsAny<Student>(), It.IsAny<string>()))
               .Returns(Task.FromResult<IActionResult>(new OkObjectResult(student)));


                // Act
                dynamic result = await studentDomain.UpdateItem(student, mock_id);

                // Assert
                Assert.IsType<OkObjectResult>(result);
                var okObjectResult = result as OkObjectResult;
                Assert.Equal(StatusCodes.Status200OK, okObjectResult?.StatusCode);
            }
            [Theory]
            [ClassData(typeof(StudentInvalidData))]
            public async Task UpadteStudentAsync_Returns400BadRequestInvalidData(Student student)
            {
                TestSetup();
                var studentDomain = new StudentDomain(_studentDalMock.Object);
                _studentDalMock.Setup(x => x.UpdateItem(It.IsAny<Student>(), It.IsAny<string>()))
              .Returns(Task.FromResult<IActionResult>(new BadRequestResult()));

                dynamic result = await studentDomain.CreateItem(student);

                Assert.IsType<BadRequestObjectResult>(result);
                var badRequestObjectResult = result as BadRequestObjectResult;
                Assert.Equal(StatusCodes.Status400BadRequest, badRequestObjectResult?.StatusCode);
                Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            }
            [Theory]
            [ClassData(typeof(StudentEmptyId))]
            public async Task UpadteStudentAsync_Returns400BadRequestIdEmpty(Student student)
            {
                TestSetup();
                var studentDomain = new StudentDomain(_studentDalMock.Object);
                _studentDalMock.Setup(x => x.UpdateItem(It.IsAny<Student>(), It.IsAny<string>()))
              .Returns(Task.FromResult<IActionResult>(new BadRequestResult()));

                dynamic result = await studentDomain.CreateItem(student);

                Assert.IsType<BadRequestObjectResult>(result);
                var badRequestObjectResult = result as BadRequestObjectResult;
                Assert.Equal(StatusCodes.Status400BadRequest, badRequestObjectResult?.StatusCode);
                Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);


            }
        }
        public class StudentSuccessData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new Student { id = "1", Name = "Swathi", Age = 71, Email = "swathi@gmail.com", Phone = "9494523304" } };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class StudentInvalidData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new Student { id = string.Empty, Name = string.Empty, Age = 0, Email = string.Empty, Phone = string.Empty } };
                yield return new object[] { new Student { id = "1", Name = string.Empty, Age = 21, Email = "Swathi@gmail.com", Phone = "9494523304" } };
                yield return new object[] { new Student { id = "1", Name = "Sw", Age = 21, Email = "Swathi@gmail.com", Phone = "9494523304" } };
                yield return new object[] { new Student { id = "1", Name = "Sw123", Age = 21, Email = "Swathi@gmail.com", Phone = "9494523304" } };
                yield return new object[] { new Student { id = "1", Name = "Swathi", Age = 0, Email = "Swathi@gmail.com", Phone = "9494523304" } };
                yield return new object[] { new Student { id = "1", Name = "Swathi", Age = -22, Email = "Swathi@gmail.com", Phone = "9494523304" } };
                yield return new object[] { new Student { id = "1", Name = "Swathi", Age = 20, Email = "Swathi@gmail.com", Phone = string.Empty } };
                yield return new object[] { new Student { id = "1", Name = "Swathi", Age = 23, Email = "Swathi@gmail.com", Phone = "9494" } };
                yield return new object[] { new Student { id = "1", Name = "Swathi", Age = 20, Email = "Swathi@gmail.com", Phone = "949452330467" } };
                yield return new object[] { new Student { id = "1", Name = "Swathi", Age = 30, Email = string.Empty, Phone = "9494523304" } };
                yield return new object[] { new Student { id = "1", Name = "swathi", Age = 20, Email = "swathigmail", Phone = "9494523304" } };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class StudentEmptyId : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new Student { id = string.Empty } };

            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
        public static HttpRequestMessage CreateMockRequest(object body)
        {
            var req = new HttpRequestMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"),
            };
            req = SetupHttp(req);
            return req;
        }
        public static HttpRequestMessage SetupHttp(HttpRequestMessage requestMessage)
        {
            var services = new Mock<IServiceProvider>(MockBehavior.Strict);
            var formatter = new XmlMediaTypeFormatter();
            var context = new DefaultHttpContext();

            var contentNegotiator = new Mock<IContentNegotiator>();
            contentNegotiator
                .Setup(c => c.Negotiate(It.IsAny<Type>(), It.IsAny<HttpRequestMessage>(), It.IsAny<IEnumerable<MediaTypeFormatter>>()))
                .Returns(new ContentNegotiationResult(formatter, mediaType: null));

            var options = new WebApiCompatShimOptions();

            if (formatter == null)
            {
                options.Formatters.AddRange(new MediaTypeFormatterCollection());
            }
            else
            {
                options.Formatters.Add(formatter);
            }

            var optionsAccessor = new Mock<IOptions<WebApiCompatShimOptions>>();
            optionsAccessor.SetupGet(o => o.Value).Returns(options);

            services.Setup(s => s.GetService(typeof(IOptions<WebApiCompatShimOptions>))).Returns(optionsAccessor.Object);

            if (contentNegotiator != null)
            {
                services.Setup(s => s.GetService(typeof(IContentNegotiator))).Returns(contentNegotiator);
            }

            context.RequestServices = CreateServices(contentNegotiator.Object, formatter);
            requestMessage.Options.TryAdd(nameof(HttpContext), context);
            return requestMessage;
        }

        private static IServiceProvider CreateServices(IContentNegotiator contentNegotiator = null, MediaTypeFormatter formatter = null)
        {
            var options = new WebApiCompatShimOptions();

            if (formatter == null)
            {
                options.Formatters.AddRange(new MediaTypeFormatterCollection());
            }

            else
            {
                options.Formatters.Add(formatter);
            }

            var optionsAccessor = new Mock<IOptions<WebApiCompatShimOptions>>();
            optionsAccessor.SetupGet(o => o.Value).Returns(options);

            var services = new Mock<IServiceProvider>(MockBehavior.Strict);
            services.Setup(s => s.GetService(typeof(IOptions<WebApiCompatShimOptions>))).Returns(optionsAccessor.Object);

            if (contentNegotiator != null)
            {
                services.Setup(s => s.GetService(typeof(IContentNegotiator))).Returns(contentNegotiator);
            }

            return services.Object;
        }

    }
}
=======
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
>>>>>>> 645198d923440a218678b613b60545179a158961
