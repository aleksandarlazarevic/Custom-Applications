#pragma checksum "D:\001-AleksandarLazarevic\001-Documents\004-GitHub\Custom-Applications\WebApplications\TemplateWebShop\TemplateWebShop\Views\Admin\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "acb24c9e5a7e99a0255007adc5a8e618732ac03c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_Index), @"mvc.1.0.view", @"/Views/Admin/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\001-AleksandarLazarevic\001-Documents\004-GitHub\Custom-Applications\WebApplications\TemplateWebShop\TemplateWebShop\Views\_ViewImports.cshtml"
using TemplateWebShop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\001-AleksandarLazarevic\001-Documents\004-GitHub\Custom-Applications\WebApplications\TemplateWebShop\TemplateWebShop\Views\_ViewImports.cshtml"
using TemplateWebShop.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"acb24c9e5a7e99a0255007adc5a8e618732ac03c", @"/Views/Admin/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"07623364a1ad93aef7d80075de7f7334002505e6", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<TemplateWebShop.Models.LoginViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/js/jquery-3.3.1.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/js/bootstrap.bundle.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\001-AleksandarLazarevic\001-Documents\004-GitHub\Custom-Applications\WebApplications\TemplateWebShop\TemplateWebShop\Views\Admin\Index.cshtml"
  
    ViewBag.Title = "Index";
    Layout = null;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<html>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "acb24c9e5a7e99a0255007adc5a8e618732ac03c4627", async() => {
                WriteLiteral("\r\n    <title>Online Shopping</title>\r\n    <link href=\"/CSS/admin_stylesheet.css\" rel=\"stylesheet\" type=\"text/css\" />\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "acb24c9e5a7e99a0255007adc5a8e618732ac03c5719", async() => {
                WriteLiteral(@"
    <style>
        .AdminLogin {
            margin: 7% 30%;
            padding: 6% 10%;
            background: rgb(236, 233, 233);
        }

        .SignIn {
            text-align: center;
            padding: 5PX 30PX;
            background-color: rgb(30, 95, 209);
            color: #FFF;
            margin: 10% 0 10% 30%;
        }

        .forgot {
            margin-top: 8%;
            font-size: 14px;
        }
    </style>

    <div class=""AdminLogin"">

");
#nullable restore
#line 36 "D:\001-AleksandarLazarevic\001-Documents\004-GitHub\Custom-Applications\WebApplications\TemplateWebShop\TemplateWebShop\Views\Admin\Index.cshtml"
         using (Html.BeginForm("Index", "Admin", new { ReturnUrl = ViewBag.ReturnUrl }, FormMethod.Post, true, new { role = "form" }))
        {
            

#line default
#line hidden
#nullable disable
#nullable restore
#line 38 "D:\001-AleksandarLazarevic\001-Documents\004-GitHub\Custom-Applications\WebApplications\TemplateWebShop\TemplateWebShop\Views\Admin\Index.cshtml"
       Write(Html.AntiForgeryToken());

#line default
#line hidden
#nullable disable
                WriteLiteral("            <h1 style=\"text-align: center; margin-bottom: 20px\">Admin Login</h1>\r\n            <div>\r\n                <div class=\"form-group row\">\r\n                    ");
#nullable restore
#line 42 "D:\001-AleksandarLazarevic\001-Documents\004-GitHub\Custom-Applications\WebApplications\TemplateWebShop\TemplateWebShop\Views\Admin\Index.cshtml"
               Write(Html.TextBoxFor(m => m.UserEmailId, new { @placeholder = "Username" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    ");
#nullable restore
#line 43 "D:\001-AleksandarLazarevic\001-Documents\004-GitHub\Custom-Applications\WebApplications\TemplateWebShop\TemplateWebShop\Views\Admin\Index.cshtml"
               Write(Html.ValidationMessageFor(m => m.UserEmailId));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                </div>\r\n                <div class=\"form-group row\">\r\n                    ");
#nullable restore
#line 46 "D:\001-AleksandarLazarevic\001-Documents\004-GitHub\Custom-Applications\WebApplications\TemplateWebShop\TemplateWebShop\Views\Admin\Index.cshtml"
               Write(Html.PasswordFor(m => m.Password, new { @type = "password", @placeholder = "Password" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    ");
#nullable restore
#line 47 "D:\001-AleksandarLazarevic\001-Documents\004-GitHub\Custom-Applications\WebApplications\TemplateWebShop\TemplateWebShop\Views\Admin\Index.cshtml"
               Write(Html.ValidationMessageFor(m => m.Password));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                </div>\r\n\r\n                <label style=\"display: none;\">");
#nullable restore
#line 50 "D:\001-AleksandarLazarevic\001-Documents\004-GitHub\Custom-Applications\WebApplications\TemplateWebShop\TemplateWebShop\Views\Admin\Index.cshtml"
                                         Write(Html.RadioButton("UserType", 1, new { @checked = "checked", }));

#line default
#line hidden
#nullable disable
                WriteLiteral(" Admin</label>\r\n                <div style=\"display: none;\" class=\"form-group row\">\r\n\r\n                    ");
#nullable restore
#line 53 "D:\001-AleksandarLazarevic\001-Documents\004-GitHub\Custom-Applications\WebApplications\TemplateWebShop\TemplateWebShop\Views\Admin\Index.cshtml"
               Write(Html.CheckBoxFor(m => m.RememberMe, new { @class = "csscheck" }));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                    <label for=""RememberMe"">Keep Me Signed In</label>
                    <a href=""/Account/ForgotPassword"">Forgot Password?</a>
                </div>
                <div class=""form-group row""> 
                    <input type=""checkbox"" name=""RememberMe1"" id=""c1"" checked>
                    <label for=""c1"">Remember me</label>
                    <a href=""/Account/ForgotPassword"">Forgot Password?</a>
                </div>

                <input type=""submit"" value=""Sign In"">
            </div>
");
#nullable restore
#line 65 "D:\001-AleksandarLazarevic\001-Documents\004-GitHub\Custom-Applications\WebApplications\TemplateWebShop\TemplateWebShop\Views\Admin\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
                WriteLiteral("    </div>\r\n");
                DefineSection("scripts", async() => {
                    WriteLiteral(@"
        <script>
            $(function () {
                $('#submit').on('click', function (evt) {
                    evt.preventDefault();
                    $.post('', $('form').serialize(), function () {
                        alert('Posted using jQuery');
                    });
                });
            });
        </script>
    ");
                }
                );
                WriteLiteral("    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "acb24c9e5a7e99a0255007adc5a8e618732ac03c11176", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "acb24c9e5a7e99a0255007adc5a8e618732ac03c12276", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</html>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<TemplateWebShop.Models.LoginViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591