using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Salil_MVC.Models.ViewModel
{
    public class ProductVM
    {
        //For Product
        public Product product { get; set; }

        // For dropdown Select Item , we need to have IEnumerable
        [ValidateNever]
        public IEnumerable<SelectListItem> categoryList { get; set; }

    }
}
