using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject1.TestDataSupportFolder
{
    public class Data
    {
        public Addition? Addition { get ; set; }
        public Addition? Subtraction { get; set; }
        public Data()
        {
            Addition = new Addition();
            Subtraction = new Addition();
        }
    }
}
