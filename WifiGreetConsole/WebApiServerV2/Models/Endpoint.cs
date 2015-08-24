using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.NetworkInformation;

namespace WebApiServerV2.Models
{
    public class Endpoint
    {
        [System.ComponentModel.DataAnnotations.Key]
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Owner { get; set; }
        public string IPAdress { get; set; }
        public int Port { get; set; }
        public PhysicalAddress HardwareAddress {get;set;}
        public DateTime Created { get; set; }
        public DateTime LastHeartBeat { get; set; }
        //public bool Online { get; set; }
        public bool Enabled { get; set; }
        //sets type. Will be used by alarms
        public int EndpointType { get; set; }
    }
}