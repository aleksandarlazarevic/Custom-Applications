﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TemplateWebApp.Controllers
{
    public class MessageWallController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}