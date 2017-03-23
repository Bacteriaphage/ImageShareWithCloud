using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageSharingWebRole.Models
{
    public class SelectItemViewTag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }

        public SelectItemViewTag(int id, string name, bool c)
        {
            this.Id = id;
            this.Name = name;
            this.Checked = c;
        }

        public SelectItemViewTag()
        {
        }

    }
}