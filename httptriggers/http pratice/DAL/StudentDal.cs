using Azure;
using http_pratice.CommonUtilities;
using http_pratice.CommonUtilities.Modles;
using http_pratice.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

public class StudentDal : IStudentDal
{

    public async Task<IActionResult> ReadItem(HttpRequest req, IEnumerable<dynamic> documents)
    {
        string gmessage = "Retrieved all items successfully";
        return StudentCosmologic.GenerateResponse(true, documents, gmessage);
    }


    public async Task<IActionResult> ReadItemById(HttpRequest req, string id, Microsoft.Azure.Cosmos.Container documentContainer)
    {
        var readstudent = await documentContainer.ReadItemAsync<Student>(id, new Microsoft.Azure.Cosmos.PartitionKey(id));
        string gmessage = "Retrieved an item successfully by Id";
        return StudentCosmologic.GenerateResponse(true, readstudent.Resource, gmessage);
    }



    public async Task<IActionResult> DeleteItem(HttpRequest req, string id, Microsoft.Azure.Cosmos.Container documentContainer)
    {
        var del = await documentContainer.DeleteItemAsync<Student>(id, new Microsoft.Azure.Cosmos.PartitionKey(id));
        string gmessage = "Deleted successfully";
        return StudentCosmologic.GenerateResponse(true, null, gmessage);

    }

    public async Task<IActionResult> UpdateItem(HttpRequest req, string id, Microsoft.Azure.Cosmos.Container documentContainer)
    {
        string requestData = await new StreamReader(req.Body).ReadToEndAsync();
        var data = JsonConvert.DeserializeObject<UpdateStudent>(requestData);
        var updatedStudent = await documentContainer.ReadItemAsync<Student>(id, new Microsoft.Azure.Cosmos.PartitionKey(id));
        updatedStudent.Resource.Name = data.Name;
      
        await documentContainer.UpsertItemAsync(updatedStudent.Resource);
        string gmessage = "Updated successfully";
        return StudentCosmologic.GenerateResponse(true, updatedStudent.Resource, gmessage);
    }

    public async Task<IActionResult> CreateItem(HttpRequest req, String id, String Name, String Age, String Phone, String Email, IAsyncCollector<dynamic> documentsOut)
    {

        var dataObject = new
        {
            id = id,
            Name = Name,
            Age = Age,
            Phone = Phone,
            Email = Email
        };

        await documentsOut.AddAsync(dataObject);
        string successMessage = "Created an item successfully";
        return StudentCosmologic.GenerateResponse(true, dataObject, successMessage);
    }


}

