using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ImageSharingWebRole.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(40)]
        public String Name { get; set; }

        public List<Image> Images { get; set; }

        public Tag()
        {

        }
    }
}