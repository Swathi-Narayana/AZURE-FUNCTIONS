using http_pratice.CommonUtilities;
using http_pratice.CommonUtilities.Modles;
using http_pratice.DAL;
using http_pratice.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class StudentDal : IStudentDal
{
    StudentCosmologic studentlogic = new StudentCosmologic();

    private const string DatabaseName = DataBaseConst.DataBaseName;
    private const string CollectionName = DataBaseConst.CollectionName;
    private readonly CosmosClient _cosmosClient;
    private Microsoft.Azure.Cosmos.Container documentContainer;

    public StudentDal(CosmosClient cosmosClient)
    {
        _cosmosClient = cosmosClient;
        documentContainer = _cosmosClient.GetContainer(DataBaseConst.DataBaseName, DataBaseConst.CollectionName);
        
    }
    public async Task<IActionResult> ReadItem()
    {
        // Retrieve documents from Cosmos DB container
        var query = "SELECT * FROM c";
        var iterator = documentContainer.GetItemQueryIterator<Student>(query);
        var documents = new List<Student>();

        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            documents.AddRange(response);
        }

        string gmessage = "Retrieved all items successfully";
        return studentlogic.GenerateResponse(true, documents, gmessage);
    }


    public async Task<IActionResult> ReadItemById(string id)
    {
        var readstudent = await documentContainer.ReadItemAsync<Student>(id, new Microsoft.Azure.Cosmos.PartitionKey(id));
        string gmessage = ($" Retrieved an item successfully by {id} ");
        return studentlogic.GenerateResponse(true, readstudent.Resource, gmessage);
    }
    public async Task<IActionResult> DeleteItem(string id)
    {
        var del = await documentContainer.DeleteItemAsync<Student>(id, new Microsoft.Azure.Cosmos.PartitionKey(id));
        string gmessage = "Deleted successfully";
        return studentlogic.GenerateResponse(true, null, gmessage);

    }

    public async Task<IActionResult> UpdateItem(Student Studentdata, string id)
    {

            var updatedStudent = await documentContainer.ReadItemAsync<Student>(id, new PartitionKey(id));
       
            //updatedStudent.Resource.Name = Studentdata.Name
            //\\
            //updatedStudent.Resource.Phone = Studentdata.Phone;
            if (!string.IsNullOrEmpty(Studentdata.Name))
            {
               updatedStudent.Resource.Name = Studentdata.Name;
            }

            if (!string.IsNullOrEmpty(Studentdata.Phone))
            {
                updatedStudent.Resource.Phone = Studentdata.Phone;
            }

        await documentContainer.UpsertItemAsync(updatedStudent.Resource);
        string gmessage = "Updated successfully";
        return studentlogic.GenerateResponse(true, updatedStudent.Resource, gmessage);
    }

    public async Task<IActionResult> CreateItem(Student StudentData)
    {

        try
        {

            await documentContainer.ReadItemAsync<Student>(StudentData.id, new PartitionKey(StudentData.id));
            string errorMessage = "Item with id already exists.";
            return studentlogic.GenerateBadResponse(errorMessage);
         }
        catch (Exception ex)
        {

            await documentContainer.UpsertItemAsync(StudentData, new PartitionKey(StudentData.id));
            string successMessage = "Created an item successfully";
            return studentlogic.GenerateResponse(true, StudentData, successMessage);
        }
       
    }
}
