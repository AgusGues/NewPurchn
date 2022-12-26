using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class GeneralResponseMessage
    {
        public GeneralResponseMessage()
        {
            isSuccess = false;
            message = string.Empty;
        }
        public bool isSuccess { get; set; }
        public string message { get; set; }
    }
}
