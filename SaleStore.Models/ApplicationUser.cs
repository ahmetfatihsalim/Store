using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleStore.Model
{
    public class ApplicationUser : IdentityUser // To add more properties to our IdentityUser we create a new child class. Proeprties of thyis class will be added to ASPNetUsers Table
    {
        [Required]
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string StreetAddress { get; set; }
    }
}
