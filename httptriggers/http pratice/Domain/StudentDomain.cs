using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using http_pratice.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using http_pratice.CommonUtilities.Modles;
using http_pratice.CommonUtilities;
using Microsoft.Azure.Cosmos;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Azure.WebJobs;
using System.IO;




namespace http_pratice.Domain
{
    public class StudentDomain : IStudentDomain
    {
        private readonly IStudentDal _studentDal;
        public StudentDomain(IStudentDal studentDal)
        { 
            _studentDal = new StudentDal(); 
        }
        public async Task<IActionResult> ReadItem(HttpRequest req, IEnumerable<dynamic> documents)
        {
            return await _studentDal.ReadItem(req, documents);
        }
        public async Task<IActionResult> ReadItemById(HttpRequest req, string id, Microsoft.Azure.Cosmos.Container documentContainer)
        {
            if (string.IsNullOrEmpty(id))
            {
                string gmessage = "Error while getting the student";
                return StudentCosmologic.GenerateResponse(false, null, gmessage);
            }

            return await _studentDal.ReadItemById(req, id, documentContainer);
        }


        public async Task<IActionResult> CreateItem(HttpRequest req, string id, IAsyncCollector<dynamic> documentsOut)
        {
            if (string.IsNullOrEmpty(id))
            {
                string errorMessage = "Error while updating student";
                return StudentCosmologic.GenerateResponse(false, null, errorMessage);
            }

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<Student>(requestBody);

                var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(data, new ValidationContext(data, null, null), validationResults, true))
            {
                string errorMessages = string.Join(", ", validationResults.Select(v => v.ErrorMessage));
                return StudentCosmologic.GenerateResponse(false, null, errorMessages);
            }
            string Id = data?.id ?? req.Query["id"];
            string name = data?.Name ?? req.Query["Name"];
            string age = data?.Age != null ? data.Age.ToString() : req.Query["Age"].ToString() ?? "1";
            string email = data?.Email ?? req.Query["Email"];
            string phone = data?.Phone ?? req.Query["Phone"];

            return await _studentDal.CreateItem(req, Id, name, age,  phone, email, documentsOut);

        }


public async Task<IActionResult> UpdateItem(HttpRequest req, string id, Microsoft.Azure.Cosmos.Container documentContainer)
        {
            if (string.IsNullOrEmpty(id))
            {
                string gmessage = "Error while updating student";
                return StudentCosmologic.GenerateResponse(false, null, gmessage);
            }
            return await _studentDal.UpdateItem(req, id, documentContainer);
        }

        public async Task<IActionResult> DeleteItem(HttpRequest req, string id, Microsoft.Azure.Cosmos.Container documentContainer)
        {
            if (string.IsNullOrEmpty(id))
            {
                string gmessage = "Error while deleting student";
                return StudentCosmologic.GenerateResponse(false, null, gmessage);
            }
            return await _studentDal.DeleteItem(req, id, documentContainer);

        }
    }   
        
}
