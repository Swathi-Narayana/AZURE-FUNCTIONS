using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace http_pratice.Domain
{
    public interface IStudentDomain
{

            Task<IActionResult> CreateItem(HttpRequest req, string id, IAsyncCollector<dynamic> documentsOut);
            Task<IActionResult> ReadItem(HttpRequest req, IEnumerable<dynamic> documents);
            Task<IActionResult> ReadItemById(HttpRequest req, string id, Microsoft.Azure.Cosmos.Container documentContainer);
            Task<IActionResult> UpdateItem(HttpRequest req, string id, Microsoft.Azure.Cosmos.Container documentContainer);
            Task<IActionResult> DeleteItem(HttpRequest req, string id, Microsoft.Azure.Cosmos.Container documentContainer);
        }
    }

