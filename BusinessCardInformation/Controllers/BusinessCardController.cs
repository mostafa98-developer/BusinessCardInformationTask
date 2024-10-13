using BusinessCardInformation.Core.Common.Enums;
using BusinessCardInformation.Core.Common;
using BusinessCardInformation.Core.Entities;
using BusinessCardInformation.Core.Entities.FilterEntities;
using BusinessCardInformation.Core.IServices;
using BusinessCardInformation.Infrastructure.Services;
using BusinessCardInformation.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using System.Xml.Serialization;
using BusinessCardInformation.Common.Utilities;

namespace BusinessCardInformation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessCardController : Controller
    {
        private readonly IBusinessCardService _businessCardService;
        private readonly IValidator<BusinessCard> _validator;


        public BusinessCardController(IBusinessCardService businessCardService, IValidator<BusinessCard> validator)
        {
            _businessCardService = businessCardService;
            _validator = validator;
        }



        [HttpPost]
        public async Task<IActionResult> CreateNewBusinessCard([FromBody] BusinessCard card)
        {
            if (card == null)
                return BadRequest(new ServiceResult { Status = ResultCode.BadRequest, Errors = new List<Error> { new Error("Invalid input.") } });

            var validateResult = await _validator.ValidateAsync(card);
            if (validateResult != null && !validateResult.IsValid)
                return BadRequest(new ServiceResult { Status = ResultCode.BadRequest, Errors = validateResult.Errors.Select(e => new Error(e.ErrorMessage)).ToList() });

            if (!string.IsNullOrEmpty(card.PhotoBase64) && FileManager.GetBase64Size(card.PhotoBase64) > 1 * 1024 * 1024)
                return BadRequest(new ServiceResult { Status = ResultCode.BadRequest, Errors = new List<Error> { new Error("Photo size exceeds 1MB.") } });

            var result = await _businessCardService.AddAsync(card);
            if (!result.IsSucceed)
                return BadRequest(result);

            return Ok(result);
        }

        // GET: api/BusinessCard
        [HttpGet]
        public async Task<IActionResult> GetAllBusinessCards([FromQuery] BusinessCardFilter filter)
        {
            var result = await _businessCardService.GetAllAsync(filter);
            if (!result.IsSucceed)
                return BadRequest(result);

            return Ok(result);
        }

        // GET: api/BusinessCard/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCardById(int id)
        {
            if (id == 0)
                return BadRequest(new ServiceResult { Status = ResultCode.BadRequest, Errors = new List<Error> { new Error("Invalid input.") } });

            var result = await _businessCardService.GetByIdAsync(id);
            if (!result.IsSucceed)
                return BadRequest(result);

            return Ok(result);
        }


        // PUT: api/BusinessCard
        [HttpPut]
        public async Task<IActionResult> UpdateBusinessCard([FromBody] BusinessCard card)
        {
            if (card == null)
                return BadRequest(new ServiceResult { Status = ResultCode.BadRequest, Errors = new List<Error> { new Error("Invalid input.") } });

            var validateResult = await _validator.ValidateAsync(card);
            if (validateResult != null && !validateResult.IsValid)
                return BadRequest(new ServiceResult { Status = ResultCode.BadRequest, Errors = validateResult.Errors.Select(e => new Error(e.ErrorMessage)).ToList() });

            if (!string.IsNullOrEmpty(card.PhotoBase64) && FileManager.GetBase64Size(card.PhotoBase64) > 1 * 1024 * 1024)
                return BadRequest(new ServiceResult { Status = ResultCode.BadRequest, Errors = new List<Error> { new Error("Photo size exceeds 1MB.") } });

            var result = await _businessCardService.UpdateAsync(card);
            if (!result.IsSucceed)
                return BadRequest(result);

            return Ok(result);
        }

        // DELETE: api/BusinessCard/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBusinessCard(int id)
        {
            if (id == 0)
                return BadRequest(new ServiceResult { Status = ResultCode.BadRequest, Errors = new List<Error> { new Error("Invalid input.") } });

            var result = await _businessCardService.DeleteAsync(id);
            if (!result.IsSucceed)
                return BadRequest(result);

            return Ok(result);
        }

        // POST: api/BusinessCard/bulk
        [HttpPost("bulk")]
        public async Task<IActionResult> ImportBulk([FromBody] IEnumerable<BusinessCard> cards)
        {
            var validationErrors = await ValidateTheCards(cards);
            if (validationErrors.Any())
            {
                var serviceResult = new ServiceResult { Status = ResultCode.BadRequest };
                serviceResult.AddErrors(validationErrors.Select(e => e.ErrorMessage).ToArray());
                return BadRequest(serviceResult);
            }

            var result = await _businessCardService.AddBulkAsync(cards);
            if (!result.IsSucceed)
                return BadRequest(result);

            return Ok(result);
        }

        private async Task<List<FluentValidation.Results.ValidationFailure>> ValidateTheCards(IEnumerable<BusinessCard> cards)
        {
            var validationErrors = new List<FluentValidation.Results.ValidationFailure>();

            foreach (var card in cards)
            {
                var validationResult = await _validator.ValidateAsync(card);
                if(validationResult != null)
                {
                    if (!validationResult.IsValid)
                    {
                        validationErrors.AddRange(validationResult.Errors);
                    }
                }
                
            }

            
            return validationErrors;
            
        }

        [HttpPost("import")]
        public async Task<ServiceResult<IEnumerable<BusinessCard>>> ImportBusinessCards(IFormFile file)
        {
            List<BusinessCard> businessCards;
            var serviceResult = new ServiceResult<IEnumerable<BusinessCard>> { Status = ResultCode.BadRequest, Errors = new List<Error> { new Error("File is missing.") } };

            if (file == null || file.Length == 0)
            {
                return serviceResult;
            }

            


            string fileExtension = Path.GetExtension(file.FileName);
            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                if (fileExtension.Equals(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    businessCards = FileManager.ReadCsv(stream);
                }
                else if (fileExtension.Equals(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    businessCards = FileManager.ReadXml(stream);
                }
                else
                {
                    serviceResult.Errors = new List<Error> { new Error("Unsupported file format.") };
                    return serviceResult;
                }
            }
            serviceResult.Data = businessCards;
            serviceResult.Status = ResultCode.Ok;
            return serviceResult;
        }

        [HttpGet("export/xml")]
        public async Task<IActionResult> ExportToXml()
        {
            var result = await _businessCardService.GetAllAsync(null);
            if (!result.IsSucceed)
                return BadRequest(result);

            List<BusinessCard> updatedCards = result.Data.Select(card => new BusinessCard
            {
                Name = card.Name,
                Gender = card.Gender,
                DateOfBirth = card.DateOfBirth,
                Email = card.Email,
                Phone = card.Phone,
                Address = card.Address,
                PhotoBase64 = card.PhotoBase64

                // Do not copy the Email property
            }).ToList();
            var xmlSerializer = new XmlSerializer(typeof(List<BusinessCard>));
            using var stringWriter = new StringWriter();
            xmlSerializer.Serialize(stringWriter, updatedCards);

            var xmlResult = Encoding.UTF8.GetBytes(stringWriter.ToString());
            return File(xmlResult, "application/xml", "business_cards.xml");
        }

        // Export to CSV
        [HttpGet("export/csv")]
        public async Task<IActionResult> ExportToCsv()
        {
            var result = await _businessCardService.GetAllAsync(null);
            if (!result.IsSucceed)
                return BadRequest(result);

            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("Name,Gender,DateOfBirth,Email,Phone,Address");

            foreach (var card in result.Data)
            {
                csvBuilder.AppendLine($"{card.Name},{card.Gender},{card.DateOfBirth},{card.Email},{card.Phone},{card.Address},{card.PhotoBase64}");
            }

            var csvResult = Encoding.UTF8.GetBytes(csvBuilder.ToString());
            return File(csvResult, "text/csv", "business_cards.csv");
        }

        [HttpPost("QRCodeReader")]
        public async Task<ServiceResult<BusinessCard>> QRCodeReader([FromForm] IFormFile file)
        {
            var serviceResult = new ServiceResult<BusinessCard> { Status = ResultCode.BadRequest, Errors = new List<Error> { new Error("File is missing.") } };

            if (file == null || file.Length == 0)
            {
                return serviceResult;
            }
           
            var businessCard = await FileManager.ReadQRCodeFromFileAsync(file);

            if (businessCard == null) {
                serviceResult.Errors = (new List<Error> { new Error("File has error.") });
                return serviceResult;
            }
            serviceResult.Data = businessCard;
            serviceResult.Status = ResultCode.Ok;
            return serviceResult;

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [Route("/")]
        public IActionResult Get()
        {
            return Ok("Hello, World!");
        }

        
    }
}
