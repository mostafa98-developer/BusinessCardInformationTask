using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCardInformation.Core.Common
{
    public class Error
    {
        public string Code { get; set; }
        public string ExtraMessage { get; set; }
        public object[] Parameters { get; set; }

        public Error(string code, string extraMessage = null, params object[] parameters)
        {
            Code = code;
            ExtraMessage = extraMessage;
            Parameters = parameters ?? new object[] { };
        }
    }
}
