using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ImageSharingWebRole.Models
{
    public class UserView
    {
        [Required]
        [RegularExpression(@"[a-zA-Z0-9_]+")]
        public String Userid { get; set; }
        [Required]
        public bool ADA { get; set; }
        public UserView()
        {

        }
    }
}