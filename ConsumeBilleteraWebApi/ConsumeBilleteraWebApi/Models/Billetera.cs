using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsumeBilleteraWebApi.Models
{
    public class Billetera
    {
        public int id { get; set; }
        public string operacion { get; set; }
        public int monto { get; set; }
    }
}