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

namespace http_pratice.DAL
{


    public  interface IStudentDal
    {
        Task<IActionResult> CreateItem(HttpRequest req, string id, string name, string Age,  string Phone, string Email, IAsyncCollector<dynamic> documentsOut);
        
        Task<IActionResult> ReadItem(HttpRequest req, IEnumerable<dynamic> documents);
        Task<IActionResult> ReadItemById(HttpRequest req, string id, Microsoft.Azure.Cosmos.Container documentContainer);
       Task<IActionResult> UpdateItem(HttpRequest req, string id, Microsoft.Azure.Cosmos.Container documentContainer);
        Task<IActionResult> DeleteItem(HttpRequest req, string id, Microsoft.Azure.Cosmos.Container documentContainer);
    }
    

}



