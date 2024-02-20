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
using Azure.Core;
using System.Net.Http;
using System.Security.Policy;




namespace http_pratice.Domain
{
    public class StudentDomain : IStudentDomain
    {
        private readonly IStudentDal _studentDal;
        StudentCosmologic studentlogic = new StudentCosmologic();
        public StudentDomain(IStudentDal studentDal)
        {
            _studentDal =  studentDal;
        }
        public async Task<IActionResult> ReadItem()
        {
            return await _studentDal.ReadItem();
        }
        public async Task<IActionResult> ReadItemById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                string gmessage = "Error while getting the student";
                return studentlogic.GenerateBadResponse(gmessage);
            }

           return await _studentDal.ReadItemById(id);
        }


        public async Task<IActionResult> CreateItem(Student StudentData)
        {

            
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(StudentData, new ValidationContext(StudentData, null, null), validationResults, true))
            {
                string errorMessages = string.Join(", ", validationResults.Select(v => v.ErrorMessage));
                return studentlogic.GenerateBadResponse(errorMessages);
            }

            return await _studentDal.CreateItem(StudentData);

        
        }


        public async Task<IActionResult> UpdateItem(Student Studentdata, string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                string gmessage = "Error while deleting student";
                return new BadRequestObjectResult(gmessage);
            }

           return await _studentDal.UpdateItem(Studentdata,id);
        }

        public async Task<IActionResult> DeleteItem( string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                string gmessage = "Error while deleting student";
                return studentlogic.GenerateBadResponse(gmessage);
            }
            return await _studentDal.DeleteItem(id);

        }
    }   
        
}
