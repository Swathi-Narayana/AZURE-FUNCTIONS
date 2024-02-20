using Azure.Core;
using http_pratice.CommonUtilities.Modles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace http_pratice.Domain
{
    public interface IStudentDomain
{

            Task<IActionResult> CreateItem(Student StudentData, Microsoft.Azure.Cosmos.Container documentContainer);
            Task<IActionResult> ReadItem(IEnumerable<dynamic> documents);
            Task<IActionResult> ReadItemById(string id, Microsoft.Azure.Cosmos.Container documentContainer);
            Task<IActionResult> UpdateItem(Student Studentdata, string id, Microsoft.Azure.Cosmos.Container documentContainer);
            Task<IActionResult> DeleteItem( string id, Microsoft.Azure.Cosmos.Container documentContainer);
        }
    }

