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
        Task<IActionResult> CreateItem(Student StudentData);
        Task<IActionResult> ReadItem();
        Task<IActionResult> ReadItemById(string id);
        Task<IActionResult> UpdateItem(Student Studentdata, string id);
        Task<IActionResult> DeleteItem(string id);
    }
    

}



