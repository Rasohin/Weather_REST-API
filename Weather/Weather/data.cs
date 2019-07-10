using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    public class data
    {
        public request[] request { get; set; }
        public current_condition[] current_condition { get; set; }
        public weather[] weather { get; set; }
        public ClimateAverages[] ClimateAverages { get; set; }
    }
}
