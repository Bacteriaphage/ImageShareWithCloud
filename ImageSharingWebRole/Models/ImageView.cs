using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace ImageSharingWebRole.Models
{
    public class ImageView
    {
 
        [Required]
        [StringLength(40)]
        [RegularExpression(@"[a-zA-Z0-9_]+")] 
        public String Caption {get; set; }
        [Required]
        public int TagId { get; set; }
        [Required] 
        [StringLength(200)]
        public String Description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}",ApplyFormatInEditMode=true)]
        public DateTime DateTaken { get; set; }

        [ScaffoldColumn(false)]
        public int Id;
        [ScaffoldColumn(false)]
        public String TagName { get; set; }
        [ScaffoldColumn(false)]
        public String UserId { get; set; }

        public bool Approved { get; set; }

        [ScaffoldColumn(false)]
        public String Uri { get; set; }

        public ImageView()
        {

        }
    }
}