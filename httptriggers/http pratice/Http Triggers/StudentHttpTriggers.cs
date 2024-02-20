using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Cosmos;
using http_pratice.CommonUtilities;
using http_pratice.CommonUtilities.Modles;
using http_pratice.Domain;
using Azure.Core;
using System.Net.Http;
using Newtonsoft.Json;
namespace http_pratice
{
    public class StudentHttpTriggers
    {
<<<<<<< HEAD
        //private const string DatabaseName = DataBaseConst.DataBaseName;
        //private const string CollectionName = DataBaseConst.CollectionName;
        //private readonly CosmosClient _cosmosClient;
        //private Microsoft.Azure.Cosmos.Container documentContainer;
=======
        private const string DatabaseName = DataBaseConst.DataBaseName;
        private const string CollectionName = DataBaseConst.CollectionName;
        private readonly CosmosClient _cosmosClient;
        private Microsoft.Azure.Cosmos.Container documentContainer;
>>>>>>> 645198d923440a218678b613b60545179a158961
        private readonly IStudentDomain _studentDomain;
        StudentCosmologic studentlogic = new StudentCosmologic();

        public StudentHttpTriggers(IStudentDomain studentDomain)
        {

            _studentDomain = studentDomain;
        }

        [FunctionName(HttpFunction.GetAllStudent)]
        public async Task<IActionResult> GetAllStudentInfo(
<<<<<<< HEAD
               [HttpTrigger(AuthorizationLevel.Anonymous, Httpverbs.Get, Route = Routes.GetStudent)] HttpRequestMessage req,
                ILogger log)
=======
                [HttpTrigger(AuthorizationLevel.Anonymous, Httpverbs.Get, Route = Routes.GetStudent)] HttpRequestMessage req,
                [CosmosDB(DatabaseName,CollectionName,Connection= "CosmosDbConnectionString" ,CreateIfNotExists = true)]
                System.Collections.Generic.IEnumerable<Student> documents, ILogger log)
>>>>>>> 645198d923440a218678b613b60545179a158961
        {
            log.LogInformation("Getting list of all students");

            try
            {
<<<<<<< HEAD
                return await _studentDomain.ReadItem();
=======
                return await _studentDomain.ReadItem(documents);
>>>>>>> 645198d923440a218678b613b60545179a158961
            }
            catch (CosmosException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                string gmessage = "Invalid Http verb";
                return studentlogic.GenerateBadResponse(gmessage);
            }

        }


        [FunctionName(HttpFunction.GetByIdStudent)]
        public async Task<IActionResult> GetStudentById(
                [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = Routes.GetStudentId)]
             HttpRequestMessage req, ILogger log, string id)
        {
            log.LogInformation($"Reading with ID: {id}");
            try
            {
<<<<<<< HEAD

                //return await _studentDomain.ReadItemById(id, documentContainer);
                return await _studentDomain.ReadItemById(id);

=======
                
                return await _studentDomain.ReadItemById(id, documentContainer);
>>>>>>> 645198d923440a218678b613b60545179a158961
            }
            catch (CosmosException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
           {
                string gmessage = "Invalid input params,Please check";
                return studentlogic.GenerateBadResponse(gmessage);
            }
        }

        [FunctionName(HttpFunction.PostStudent)]
        public async Task<IActionResult> PostStudentInfo(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post",  Route = Routes.PostStudent)]
<<<<<<< HEAD
            HttpRequestMessage req,
       ILogger log)
        {

=======
        HttpRequestMessage req, 
       ILogger log)
        {
           
>>>>>>> 645198d923440a218678b613b60545179a158961
            log.LogInformation("Creating student with ID id");
            string requestData = await req.Content.ReadAsStringAsync();
            var StudentData = JsonConvert.DeserializeObject<Student>(requestData);
            try
            {
<<<<<<< HEAD

                return await _studentDomain.CreateItem(StudentData);
=======
                
                return await _studentDomain.CreateItem(StudentData, documentContainer);
>>>>>>> 645198d923440a218678b613b60545179a158961

            }
            catch (CosmosException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

<<<<<<< HEAD
                return studentlogic.GenerateBadResponse(requestData);
=======
                string gmessage = "Failed to add the student";
                return studentlogic.GenerateBadResponse(gmessage);
>>>>>>> 645198d923440a218678b613b60545179a158961
            }

        }

        [FunctionName(HttpFunction.DeleteStudent)]
        public async Task<IActionResult> DeleteStudentInfo(
                [HttpTrigger(AuthorizationLevel.Anonymous, Httpverbs.Delete, Route = Routes.DeleteStudent)]
<<<<<<< HEAD
                    HttpRequestMessage req, ILogger log, string id)
=======
                HttpRequestMessage req, ILogger log, string id)
>>>>>>> 645198d923440a218678b613b60545179a158961
        {

            try
            {
<<<<<<< HEAD

                log.LogInformation($" record with the {id} is deleted");
                return await _studentDomain.DeleteItem(id);
=======
                return await _studentDomain.DeleteItem(id, documentContainer);
>>>>>>> 645198d923440a218678b613b60545179a158961
            }
            catch (CosmosException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                string gmessage = "Failed to delete the student";
                return studentlogic.GenerateBadResponse(gmessage);
            }
        }

        [FunctionName(HttpFunction.UpadteStudent)]
        public async Task<IActionResult> UpdateStudentInfo(
            [HttpTrigger(AuthorizationLevel.Anonymous, Httpverbs.Put, Route = Routes.UpdateStudent)] HttpRequestMessage req,
            ILogger log, string id)
        {
            string requestData = await req.Content.ReadAsStringAsync();
            var Studentdata = JsonConvert.DeserializeObject<Student>(requestData);
            log.LogInformation($"Update Student Info with ID: {id}");

            try
            {
<<<<<<< HEAD
                return await _studentDomain.UpdateItem(Studentdata, id);
=======
                return await _studentDomain.UpdateItem(Studentdata, id, documentContainer);
>>>>>>> 645198d923440a218678b613b60545179a158961
            }
            catch (CosmosException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                string gmessage = "Failed to update the student";
                return studentlogic.GenerateBadResponse(gmessage);
            }
        }
    }

}

