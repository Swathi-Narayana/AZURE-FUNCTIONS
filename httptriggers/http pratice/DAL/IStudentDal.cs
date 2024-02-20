using http_pratice.CommonUtilities.Modles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using http_pratice.DAL;
using System.Threading.Tasks;
using Azure.Core;
using System.Net.Http;

namespace http_pratice.DAL
{


    public  interface IStudentDal
    {
<<<<<<< HEAD
        Task<IActionResult> CreateItem(Student StudentData);
        Task<IActionResult> ReadItem();
        Task<IActionResult> ReadItemById(string id);
        Task<IActionResult> UpdateItem(Student Studentdata, string id);
        Task<IActionResult> DeleteItem(string id);
=======
        //Task<IActionResult> CreateItem(HttpRequestMessage req, string id, string name, string Age,  string Phone, string Email, IAsyncCollector<dynamic> documentsOut);
        Task<IActionResult> CreateItem(Student StudentData, Microsoft.Azure.Cosmos.Container documentContainer);
        Task<IActionResult> ReadItem(IEnumerable<dynamic> documents);
        Task<IActionResult> ReadItemById(string id, Microsoft.Azure.Cosmos.Container documentContainer);
       Task<IActionResult> UpdateItem(Student Studentdata, string id, Microsoft.Azure.Cosmos.Container documentContainer);
        Task<IActionResult> DeleteItem(string id, Microsoft.Azure.Cosmos.Container documentContainer);
>>>>>>> 645198d923440a218678b613b60545179a158961
    }
    

}



