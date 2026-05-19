using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class Game
    {
        public int GameID { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string? OfficialWebsite { get; set; }
        public bool Active { get; set; }
    }

    public class GameVM : Game
    {
        public List<Series> Series {get;set;} = new List<Series>();
    }
}
