using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoapApiPhone.Model
{
    public class CatalogPhone
    {
        public List<Phone> Phones { get; set; }

        public CatalogPhone() {
            this.Phones = new List<Phone>();
        }
    }
}
