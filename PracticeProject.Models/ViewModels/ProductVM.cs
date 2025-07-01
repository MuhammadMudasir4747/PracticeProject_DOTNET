using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering; // ✅ Correct one for ASP.NET Core

namespace PracticeProject.Models.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; }
        [ValidateNever]
        public IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> CategoryList { get; set; }
    }
}
