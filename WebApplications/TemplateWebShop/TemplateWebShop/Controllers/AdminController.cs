using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemplateWebShop.Models;

namespace TemplateWebShop.Controllers
{
    public class AdminController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index(string returnUrl)
        {
            LoginViewModel loginModel = new LoginViewModel();
            loginModel.UserEmailId = Request.Cookies["RememberMe_UserEmailId"] != null ? Request.Cookies["RememberMe_UserEmailId"] : "";
            loginModel.Password = Request.Cookies["RememberMe_Password"] != null ? Request.Cookies["RememberMe_Password"] : "";

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Index( LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //string EncryptedPassword = EncryptDecrypt.Encrypt(model.Password, true);
                //var user = _unitOfWork.GetRepositoryInstance<Tbl_Members>().GetFirstOrDefaultByParameter(i => i.EmailId == model.UserEmailId && i.Password == EncryptedPassword && i.IsActive == true && i.IsDelete == false);
            }
            return View();
        }
    }
}
