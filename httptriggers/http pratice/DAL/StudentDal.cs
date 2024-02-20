using Azure;
using Azure.Core;
using http_pratice.CommonUtilities;
using http_pratice.CommonUtilities.Modles;
using http_pratice.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;

public class StudentDal : IStudentDal
{
    StudentCosmologic studentlogic = new StudentCosmologic();


    public async Task<IActionResult> ReadItem(IEnumerable<dynamic> documents)
    {
        string gmessage = "Retrieved all items successfully";
        return studentlogic.GenerateResponse(true, documents, gmessage);
    }


    public async Task<IActionResult> ReadItemById(string id, Microsoft.Azure.Cosmos.Container documentContainer)
    {
        var readstudent = await documentContainer.ReadItemAsync<Student>(id, new Microsoft.Azure.Cosmos.PartitionKey(id));
        string gmessage = ($" Retrieved an item successfully by {id} ");
        return studentlogic.GenerateResponse(true, readstudent.Resource, gmessage);
    }



    public async Task<IActionResult> DeleteItem(string id, Microsoft.Azure.Cosmos.Container documentContainer)
    {
        var del = await documentContainer.DeleteItemAsync<Student>(id, new Microsoft.Azure.Cosmos.PartitionKey(id));
        string gmessage = "Deleted successfully";
        return studentlogic.GenerateResponse(true, null, gmessage);

    }

    public async Task<IActionResult> UpdateItem(Student Studentdata, string id, Microsoft.Azure.Cosmos.Container documentContainer)
    {
        
        //string requestData = await req.Content.ReadAsStringAsync();
        //var data = JsonConvert.DeserializeObject<UpdateStudent>(requestData);
        var updatedStudent = await documentContainer.ReadItemAsync<Student>(id, new Microsoft.Azure.Cosmos.PartitionKey(id));
        updatedStudent.Resource.Name = Studentdata.Name;
        await documentContainer.UpsertItemAsync(updatedStudent.Resource);
        string gmessage = "Updated successfully";
        return studentlogic.GenerateResponse(true, updatedStudent.Resource, gmessage);
    }

    public async Task<IActionResult> CreateItem(Student StudentData, Microsoft.Azure.Cosmos.Container documentContainer)
    {
        //var existingItem = await documentContainer.ReadItemAsync<Student>(StudentData.id, new PartitionKey(StudentData.id));

        //if (existingItem != null)
        //{
        //    string errorMessage = "Item with id already exists.";
        //    return studentlogic.GenerateBadResponse(errorMessage);
        //}

        var newitem =await documentContainer.UpsertItemAsync(StudentData, new PartitionKey(StudentData.id));
        string successMessage = "Created an item successfully";
        return studentlogic.GenerateResponse(true, StudentData, successMessage);

       
    }
}
