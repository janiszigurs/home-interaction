using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServerV2.Models
{
    public class Endpoint
    {
        [System.ComponentModel.DataAnnotations.Key]
        public Guid Id { get; set; }
        public string Owner { get; set; }
        public string IPAdress { get; set; }
        public int Port { get; set; }
        public DateTime Created { get; set; }
        public bool Online { get; set; }
        public bool Enabled { get; set; }
        public int EnpointType { get; set; }
    }
}