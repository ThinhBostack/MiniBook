using Microsoft.AspNetCore.Mvc;
using MiniBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBookIdentity.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.OkResult("Hello World");
        }
    }
}
