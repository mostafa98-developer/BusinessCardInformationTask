using BusinessCardInformation.Core.Entities;
using BusinessCardInformation.Infrastructure.Services;
using BusinessCardInformation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml.Serialization;

namespace BusinessCardInformation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessCardController : Controller
    {
        private readonly ILogger<BusinessCardController> _logger;
        private readonly BusinessCardService _businessCardService;


        public BusinessCardController(ILogger<BusinessCardController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult CreateNewBusinessCard([FromBody] BusinessCard card)
        {
            if (card == null)
                return BadRequest("Invalid input.");

            // Check if the photo exceeds the size limit (1MB)
            if (!string.IsNullOrEmpty(card.PhotoBase64) && GetBase64Size(card.PhotoBase64) > 1 * 1024 * 1024)
                return BadRequest("Photo size exceeds 1MB.");

            // Save the business card to the database here
            return Ok($"Business card created for {card.Name}");
        }

        // GET: api/BusinessCard
        [HttpGet]
        public async Task<IActionResult> GetAllBusinessCards()
        {
            var cards = await _businessCardService.GetAllAsync();
            return Ok(cards);
        }


        [HttpPost("import")]
        public IActionResult ImportBusinessCards(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is missing.");

            List<BusinessCard> businessCards;

            string fileExtension = Path.GetExtension(file.FileName);
            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                if (fileExtension.Equals(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    businessCards = ReadCsv(stream);
                }
                else if (fileExtension.Equals(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    businessCards = ReadXml(stream);
                }
                else
                {
                    return BadRequest("Unsupported file format.");
                }
            }

            // Save to database logic
            return Ok($"{businessCards.Count} business cards imported successfully.");
        }

        private List<BusinessCard> ReadCsv(StreamReader stream)
        {
            var cards = new List<BusinessCard>();

            // Assuming first row contains headers and skipping it
            stream.ReadLine();

            while (!stream.EndOfStream)
            {
                var line = stream.ReadLine();
                var values = line.Split(',');

                var card = new BusinessCard
                {
                    Name = values[0],
                    Gender = values[1],
                    DateOfBirth = DateTime.Parse(values[2]),
                    Email = values[3],
                    Phone = values[4],
                    Address = values[5],
                    PhotoBase64 = values[6] // Optional, base64 encoded photo
                };

                // Validate photo size if present
                if (!string.IsNullOrEmpty(card.PhotoBase64) && GetBase64Size(card.PhotoBase64) > 1 * 1024 * 1024)
                    throw new InvalidOperationException($"Photo size for {card.Name} exceeds 1MB.");

                cards.Add(card);
            }

            return cards;
        }

        private List<BusinessCard> ReadXml(StreamReader stream)
        {
            var serializer = new XmlSerializer(typeof(List<BusinessCard>));
            var cards = (List<BusinessCard>)serializer.Deserialize(stream);

            // Validate photo size for each card
            foreach (var card in cards)
            {
                if (!string.IsNullOrEmpty(card.PhotoBase64) && GetBase64Size(card.PhotoBase64) > 1 * 1024 * 1024)
                    throw new InvalidOperationException($"Photo size for {card.Name} exceeds 1MB.");
            }

            return cards;
        }


        // Helper function to calculate the size of the base64 string
        private long GetBase64Size(string base64String)
        {
            // The base64 string may contain a prefix like "data:image/jpeg;base64,"
            int prefixLength = base64String.IndexOf(',') + 1;
            var base64 = base64String.Substring(prefixLength);

            return (long)((4 * Math.Ceiling((double)base64.Length / 3)) / 8); // Calculate in bytes
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
