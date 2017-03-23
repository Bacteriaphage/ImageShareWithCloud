using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageSharingWebRole.Models
{
    public class Week
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Week() { }

        public Week(int i)
        {
            this.Id = i;
            if (i == 0) this.Name = "this week";
            else if (i == 1) this.Name = "last week";
            else if (i == 2) this.Name = "two weeks ago";
        }
    }
}