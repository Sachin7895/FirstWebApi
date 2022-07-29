using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApis.Models
{
    public class AddressViewModel
    {
        public int StudentId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public static implicit operator AddressViewModel(StudentViewModel v)
        {
            throw new NotImplementedException();
        }
    }
}