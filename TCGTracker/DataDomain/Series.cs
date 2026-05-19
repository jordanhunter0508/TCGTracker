using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class Series
    {
        public int SeriesID { get; set; }
        public int GameID { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public bool Active { get; set; }
    }
}
