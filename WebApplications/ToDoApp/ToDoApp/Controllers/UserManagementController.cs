using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Controllers
{
    public class UserManagementController : Controller
    {
        public IActionResult Index()
        {
            bool isAdmin = User.IsInRole("admin"); 


            if (isAdmin) { return View("~/Views/TodoList/Index.cshtml"); }
            else
            {
                return View(); }
        }
    }
}
