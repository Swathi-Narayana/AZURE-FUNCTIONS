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
<<<<<<< HEAD
        {
            _studentDal =  studentDal;
        }
        public async Task<IActionResult> ReadItem()
        {
            return await _studentDal.ReadItem();
        }
        public async Task<IActionResult> ReadItemById(string id)
=======
        { 
            _studentDal = new StudentDal(); 
        }
        public async Task<IActionResult> ReadItem(IEnumerable<dynamic> documents)
        {
            return await _studentDal.ReadItem(documents);
        }
        public async Task<IActionResult> ReadItemById(string id, Microsoft.Azure.Cosmos.Container documentContainer)
>>>>>>> 645198d923440a218678b613b60545179a158961
        {
            if (string.IsNullOrEmpty(id))
            {
                string gmessage = "Error while getting the student";
                return studentlogic.GenerateBadResponse(gmessage);
            }

<<<<<<< HEAD
           return await _studentDal.ReadItemById(id);
        }


        public async Task<IActionResult> CreateItem(Student StudentData)
=======
            return await _studentDal.ReadItemById(id, documentContainer);
        }


        public async Task<IActionResult> CreateItem(Student StudentData, Microsoft.Azure.Cosmos.Container documentContainer)
>>>>>>> 645198d923440a218678b613b60545179a158961
        {

            
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(StudentData, new ValidationContext(StudentData, null, null), validationResults, true))
            {
                string errorMessages = string.Join(", ", validationResults.Select(v => v.ErrorMessage));
                return studentlogic.GenerateBadResponse(errorMessages);
            }

<<<<<<< HEAD
            return await _studentDal.CreateItem(StudentData);
=======
            return await _studentDal.CreateItem(StudentData, documentContainer);
>>>>>>> 645198d923440a218678b613b60545179a158961

        
        }


<<<<<<< HEAD
        public async Task<IActionResult> UpdateItem(Student Studentdata, string id)
=======
        public async Task<IActionResult> UpdateItem(Student Studentdata, string id, Microsoft.Azure.Cosmos.Container documentContainer)
>>>>>>> 645198d923440a218678b613b60545179a158961
        {
            if (string.IsNullOrEmpty(id))
            {
                string gmessage = "Error while deleting student";
<<<<<<< HEAD
                return new BadRequestObjectResult(gmessage);
            }

           return await _studentDal.UpdateItem(Studentdata,id);
        }

        public async Task<IActionResult> DeleteItem( string id)
=======
                return studentlogic.GenerateBadResponse(gmessage);
            }

            return await _studentDal.UpdateItem(Studentdata, id, documentContainer);
        }

        public async Task<IActionResult> DeleteItem( string id, Microsoft.Azure.Cosmos.Container documentContainer)
>>>>>>> 645198d923440a218678b613b60545179a158961
        {
            if (string.IsNullOrEmpty(id))
            {
                string gmessage = "Error while deleting student";
                return studentlogic.GenerateBadResponse(gmessage);
            }
<<<<<<< HEAD
            return await _studentDal.DeleteItem(id);
=======
            return await _studentDal.DeleteItem(id, documentContainer);
>>>>>>> 645198d923440a218678b613b60545179a158961

        }
    }   
        
}
