using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    public class current_condition
    {
        //public string observation_time { get; set; }
        public int temp_C { get; set; }
        //public int temp_F { get; set; }
        //public int weatherCode { get; set; }
        public weatherIconUrl[] weatherIconUrl { get; set; }
        public weatherDesc[] weatherDesc { get; set; }
        //public int windspeedMiles { get; set; }
        public int windspeedKmph { get; set; }
        //public int winddirDegree { get; set; }
        public string winddir16Point { get; set; }
        //public string precipMM { get; set; }
        public float humidity { get; set; }
        //public int visibility { get; set; }
        public int pressure { get; set; }
        public int cloudcover { get; set; }
        //public int FeelsLikeC { get; set; }
        //public int FeelsLikeF { get; set; }
        public int uvIndex { get; set; }

        //public float precipInches;
        //public int visibilityMiles;
        //public float pressureInches;

    }
}
