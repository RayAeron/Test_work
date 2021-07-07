using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test_work.Entity;

namespace Test_work.Models
{
    public class ResponseTovar
    {
        public ResponseTovar(tovar responseTovar)
        {
            Name_tovar = responseTovar.Name_tovar;
            Price_tovar = responseTovar.Price_tovar;
        }
        public string Name_tovar { get; set; }
        public string Price_tovar { get; set; }
    }
}