using BusinessCardInformation.Core.Entities;
using System.Xml.Serialization;
using System.Drawing;
using Newtonsoft.Json; // Ensure you have this for JSON deserialization



namespace BusinessCardInformation.Common.Utilities
{
    internal static class FileManager
    {

        internal static List<BusinessCard> ReadCsv(StreamReader stream)
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

        internal static List<BusinessCard> ReadXml(StreamReader stream)
        {
            var serializer = new XmlSerializer(typeof(List<BusinessCard>));
            var cards = (List<BusinessCard>)serializer.Deserialize(stream);

            // Validate photo size for each card
            foreach (var card in cards)
            {
                if (!string.IsNullOrEmpty(card.PhotoBase64) && GetBase64Size(card.PhotoBase64) > (1 * 1024 * 1024))
                    throw new InvalidOperationException($"Photo size for {card.Name} exceeds 1MB.");
            }

            return cards;
        }


        // Helper function to calculate the size of the base64 string
        internal static long GetBase64Size(string base64String)
        {
            // The base64 string may contain a prefix like "data:image/jpeg;base64,"
            int prefixLength = base64String.IndexOf(',') + 1;
            string base64 = base64String.Substring(prefixLength);

            // Calculate the padding (Base64 strings use '=' for padding)
            int padding = 0;
            if (base64.EndsWith("==")) padding = 2;
            else if (base64.EndsWith("=")) padding = 1;

            // Base64-encoded string size (in bytes)
            return (base64.Length * 3 / 4) - padding;
        }

        internal static async Task<BusinessCard> ReadQRCodeFromFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File cannot be null or empty.", nameof(file));

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            return ParseBusinessCard(DecodeQRCode(stream.ToArray()));
        }

        private static string DecodeQRCode(byte[] imageData)
        {

            var reader = new ZXing.Windows.Compatibility.BarcodeReader();
            var result = reader.Decode(ByteToBitmap(imageData));
            return result.Text;
            
        }

        public static Bitmap ByteToBitmap(byte[] byteArray)
        {
            Bitmap target;
            using (var stream = new MemoryStream(byteArray))
            {
                target = new Bitmap(stream);
            }

            return target;
        }

        private static BusinessCard ParseBusinessCard(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException("Decoded data is empty or null.", nameof(json));

            return JsonConvert.DeserializeObject<BusinessCard>(json);
        }

    }
}
