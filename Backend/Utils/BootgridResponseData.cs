using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinstonChurchill.Backend.Utils
{
    public class BootgridResponseData<T> where T : class
    {
        public int current { get; set; }

        public int rowCount { get; set; }

        public IEnumerable<T> rows { get; set; }  

        public int total { get; set; }  
    }
}
