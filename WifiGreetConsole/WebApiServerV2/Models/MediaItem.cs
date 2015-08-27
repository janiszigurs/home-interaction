using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServerV2.Models
{
    public class MediaItem
    {
        [System.ComponentModel.DataAnnotations.Key]
        public Guid Id { get; set; }
        public Guid EndpointId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime ItemAdded { get; set; }
        public string FileFormat { get; set; }
    }
}