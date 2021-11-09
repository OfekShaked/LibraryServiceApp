using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.Common.Models
{
    public class Genre
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public Genre(string name)
        {
            this.Name = name;
            this.ID = 0; //get from last id created
        }
        public override string ToString()
        {
            return $"{Name},{ID}";
        }
    }
}
