using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleStore.Model.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        [ValidateNever] // this property does not need to get validated
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
