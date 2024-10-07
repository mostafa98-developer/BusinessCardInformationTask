using BusinessCardInformation.Controllers;
using BusinessCardInformation.Core.Entities;
using BusinessCardInformation.Core.Entities.FilterEntities;
using BusinessCardInformation.Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Text;

namespace BusinessCardInformation.Test
{
    public class UnitTest
    {
        public class BusinessCardControllerTests
        {
            private readonly Mock<IBusinessCardService> _mockService;
            private readonly BusinessCardController _controller;

            public BusinessCardControllerTests()
            {
                _mockService = new Mock<IBusinessCardService>();
                _controller = new BusinessCardController(_mockService.Object);
            }

            [Fact]
            public async Task CreateNewBusinessCard_ShouldReturnBadRequest_WhenCardIsNull()
            {
                // Act
                var result = await _controller.CreateNewBusinessCard(null);

                // Assert
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                Assert.Equal("Invalid input.", badRequestResult.Value);
            }

            [Fact]
            public async Task CreateNewBusinessCard_ShouldReturnBadRequest_WhenPhotoSizeExceedsLimit()
            {
                // Arrange
                var card = new BusinessCard
                {
                    Name = "Mostafa Almomani",
                    Gender = "Male",
                    DateOfBirth = new DateTime(1998, 1, 4),
                    Email = "mostafa2112@example.com",
                    Phone = "123-456-7890",
                    Address = "irbide, jordan",
                    PhotoBase64 = GenerateBase64Image(3) // 3MB photo
                };
                // Act
                var result = await _controller.CreateNewBusinessCard(card);

                // Assert
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                Assert.Equal("Photo size exceeds 1MB.", badRequestResult.Value);
            }

            [Fact]
            public async Task CreateNewBusinessCard_ShouldReturnOk_WhenCardIsValid()
            {
                // Arrange
                var card = new BusinessCard
                {
                    Name = "Mostafa Almomani",
                    Gender = "Male",
                    DateOfBirth = new DateTime(1998, 1, 4),
                    Email = "mostafa11@example.com",
                    Phone = "123-456-7890",
                    Address = "irbide, jordan",
                    PhotoBase64 = GenerateBase64Image(1) // 1MB photo
                };
                var serviceResult = new ServiceResult<BusinessCard>(card);
                _mockService.Setup(s => s.AddAsync(card)).ReturnsAsync(serviceResult);

                // Act
                var result = await _controller.CreateNewBusinessCard(card);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Equal(card, okResult.Value);
            }

            [Fact]
            public async Task GetAllBusinessCards_ShouldReturnOk_WhenServiceReturnsData()
            {
                var businessCard = new BusinessCardFilter() { Email = "mostafa2424@example.com" };

                // Arrange
                var cards = new List<BusinessCard> { new BusinessCard
                        {
                            Name = "Mostafa Almomani",
                            Gender = "Male",
                            DateOfBirth = new DateTime(1998, 1, 4),
                            Email = "mostafa4564@example.com",
                            Phone = "123-456-7890",
                            Address = "irbide, jordan",
                            PhotoBase64 = GenerateBase64Image(1) // 1MB photo
                        } };
                var serviceResult = new ServiceResult<IEnumerable<BusinessCard>>(cards);
                _mockService.Setup(s => s.GetAllAsync(businessCard)).ReturnsAsync(serviceResult);

                // Act
                var result = await _controller.GetAllBusinessCards(businessCard);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Equal(cards, okResult.Value);
            }

            [Fact]
            public async Task UpdateBusinessCards_ShouldReturnBadRequest_WhenCardIsNull()
            {
                // Act
                var result = await _controller.UpdateBusinessCards(null);

                // Assert
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                Assert.Equal("Invalid input.", badRequestResult.Value);
            }

            [Fact]
            public async Task DeleteBusinessCards_ShouldReturnBadRequest_WhenCardIdIsNull()
            {
                var result = await _controller.DeleteBusinessCards(0);

                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                Assert.Equal("Invalid input.", badRequestResult.Value);
            }

            [Fact]
            public async Task DeleteBusinessCards_ShouldReturnOk_WhenServiceDeletesSuccessfully()
            {
                int cardId = 1;
                var serviceResult = new ServiceResult<BusinessCard>(new BusinessCard());
                _mockService.Setup(s => s.DeleteAsync(cardId)).ReturnsAsync(serviceResult);

                var result = await _controller.DeleteBusinessCards(cardId);

                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Equal(serviceResult.Data, okResult.Value);
            }

            [Fact]
            public void ImportBusinessCards_ShouldReturnBadRequest_WhenFileIsMissing()
            {
                IFormFile file = null;

                var result = _controller.ImportBusinessCards(file);

                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                Assert.Equal("File is missing.", badRequestResult.Value);
            }

            [Fact]
            public void ImportBusinessCards_ShouldReturnBadRequest_WhenFileIsEmpty()
            {
                var fileMock = new Mock<IFormFile>();
                fileMock.Setup(f => f.Length).Returns(0);
                fileMock.Setup(f => f.FileName).Returns("test.csv");

                var result = _controller.ImportBusinessCards(fileMock.Object);

                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                Assert.Equal("File is missing.", badRequestResult.Value);
            }

            [Fact]
            public void ImportBusinessCards_ShouldReturnOk_WhenCsvFileIsValid()
            {
                // Arrange
                var csvContent = "Name,Gender,DateOfBirth,Email,Phone,Address,PhotoBase64\n" +
                                 "Mostafa Almomani,Male,1998-01-04,mostafa@example.com,123-456-7890,irbide, jordan," +
                                 GenerateBase64Image(1); // 1MB photo

                var fileMock = new Mock<IFormFile>();
                var fileName = "test.csv";
                var stream = new MemoryStream(Encoding.UTF8.GetBytes(csvContent));

                fileMock.Setup(f => f.Length).Returns(stream.Length);
                fileMock.Setup(f => f.FileName).Returns(fileName);
                fileMock.Setup(f => f.OpenReadStream()).Returns(stream);

                // Act
                var result = _controller.ImportBusinessCards(fileMock.Object);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Equal("1 business cards imported successfully.", okResult.Value);
            }

            [Fact]
            public void ImportBusinessCards_ShouldReturnOk_WhenXmlFileIsValid()
            {
                // Arrange
                var xmlContent = "<ArrayOfBusinessCard xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">" +
                                 "<BusinessCard><Name>Mostafa Almomani</Name><Gender>Male</Gender><DateOfBirth>1998-01-04</DateOfBirth>" +
                                 "<Email>mostafa@example.com</Email><Phone>123-456-7890</Phone><Address>irbide, jordan</Address>" +
                                 "<PhotoBase64>" + GenerateBase64Image(1) + "</PhotoBase64></BusinessCard></ArrayOfBusinessCard>";

                var fileMock = new Mock<IFormFile>();
                var fileName = "test.xml";
                var stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlContent));

                fileMock.Setup(f => f.Length).Returns(stream.Length);
                fileMock.Setup(f => f.FileName).Returns(fileName);
                fileMock.Setup(f => f.OpenReadStream()).Returns(stream);

                // Act
                var result = _controller.ImportBusinessCards(fileMock.Object);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Equal("1 business cards imported successfully.", okResult.Value);
            }

            [Fact]
            public void ImportBusinessCards_ShouldReturnBadRequest_WhenFileFormatIsUnsupported()
            {
                // Arrange
                var fileMock = new Mock<IFormFile>();
                var stream = new MemoryStream(Encoding.UTF8.GetBytes("Some content here"));
                fileMock.Setup(f => f.Length).Returns(stream.Length);
                fileMock.Setup(f => f.FileName).Returns("test.txt"); // Unsupported format
                fileMock.Setup(f => f.OpenReadStream()).Returns(stream);

                // Act
                var result = _controller.ImportBusinessCards(fileMock.Object);

                // Assert
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                Assert.Equal("Unsupported file format.", badRequestResult.Value);
            }


            [Fact]
            public async Task ExportToXml_ShouldSaveFileAndReturnFileResult()
            {
                var faker = new Bogus.Faker<BusinessCard>()
                    .RuleFor(b => b.Id, f => f.IndexFaker + 1)
                    .RuleFor(b => b.Name, f => f.Name.FullName())
                    .RuleFor(b => b.Gender, f => f.PickRandom(new[] { "Male", "Female" }))
                    .RuleFor(b => b.DateOfBirth, f => f.Date.Past(30, DateTime.Now.AddYears(-18)))
                    .RuleFor(b => b.Email, (f, b) => f.Internet.Email(b.Name.ToLower().Replace(" ", "")))
                    .RuleFor(b => b.Phone, f => f.Phone.PhoneNumber())
                    .RuleFor(b => b.Address, f => f.Address.FullAddress());

                var cards = faker.Generate(10);
                var serviceResult = new ServiceResult<IEnumerable<BusinessCard>>(cards);
                _mockService.Setup(s => s.GetAllAsync(null)).ReturnsAsync(serviceResult);

                var projectRoot = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName;
                var xmlFilePath = Path.Combine(projectRoot, "business_cards.xml");

                if (System.IO.File.Exists(xmlFilePath))
                {
                    System.IO.File.Delete(xmlFilePath); 
                }

                var result = await _controller.ExportToXml();  

                var fileResult = Assert.IsType<FileContentResult>(result);

                Assert.Equal("application/xml", fileResult.ContentType);

                var xmlContent = Encoding.UTF8.GetString(fileResult.FileContents);
                Assert.NotEmpty(xmlContent); 

                await System.IO.File.WriteAllBytesAsync(xmlFilePath, fileResult.FileContents);

                Assert.True(System.IO.File.Exists(xmlFilePath), $"The file {xmlFilePath} was not saved correctly.");

                _mockService.Verify(s => s.GetAllAsync(null), Times.Once);
            }

            [Fact]
            public async Task ExportToCsv_ShouldSaveFileAndReturnFileResult()
            {
                var faker = new Bogus.Faker<BusinessCard>()
                           .RuleFor(b => b.Id, f => f.IndexFaker + 1)
                           .RuleFor(b => b.Name, f => f.Name.FullName())
                           .RuleFor(b => b.Gender, f => f.PickRandom(new[] { "Male", "Female" }))
                           .RuleFor(b => b.DateOfBirth, f => f.Date.Past(30, DateTime.Now.AddYears(-18)))
                           .RuleFor(b => b.Email, (f, b) => f.Internet.Email(b.Name.ToLower().Replace(" ", "")))
                           .RuleFor(b => b.Phone, f => f.Phone.PhoneNumber())
                           .RuleFor(b => b.Address, f => f.Address.FullAddress());

                var cards = faker.Generate(10);
                var serviceResult = new ServiceResult<IEnumerable<BusinessCard>>(cards);
                _mockService.Setup(s => s.GetAllAsync(null)).ReturnsAsync(serviceResult);

                var projectRoot = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName;
                var csvFilePath = Path.Combine(projectRoot, "business_cards.csv");

                if (System.IO.File.Exists(csvFilePath))
                {
                    System.IO.File.Delete(csvFilePath); 
                }

                var result = await _controller.ExportToCsv();

                var fileResult = Assert.IsType<FileContentResult>(result);

                Assert.Equal("text/csv", fileResult.ContentType);

                var csvContent = Encoding.UTF8.GetString(fileResult.FileContents);

            
                await System.IO.File.WriteAllBytesAsync(csvFilePath, fileResult.FileContents);

                Assert.True(System.IO.File.Exists(csvFilePath), $"The file {csvFilePath} was not saved correctly.");
                
                _mockService.Verify(s => s.GetAllAsync(null), Times.Once);
            }



            private static string GenerateBase64Image(int sizeInMB)
            {

                int base64Size = (int)(sizeInMB * 1_398_000);

                string base64ImageContent = new string('A', base64Size);

                return $"data:image/jpeg;base64,{base64ImageContent}";
            }

        }
    }
}