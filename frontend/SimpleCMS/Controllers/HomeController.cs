using Microsoft.AspNetCore.Mvc;
using SimpleCMS.Models;
using System.Collections.Generic;

namespace SimpleCMS.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            List<Document> documentsMock = new List<Document>()
            {
                new Document
                {
                    Description = "Sample document"
                }
            };
            return View(documentsMock);
        }
    }
}
