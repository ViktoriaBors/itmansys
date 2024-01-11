using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeadandoViktoriaBorsPajuste.Classes
{
    enum FunctionResultType
    {
        ok,
        error,
        fatal
    }
    public class FunctionResult
    // class providing detailed information about function outcomes. Customized, error messages, result
    {
        string message;
        bool result;
        FunctionResultType fresult;

        public FunctionResult()
        {
            Message = string.Empty;
            Result = true;
            Fresult = FunctionResultType.ok;
        }

        public string Message { get => message; set => message = value; }
        public bool Result { get => result; set => result = value; }
        internal FunctionResultType Fresult { get => fresult; set => fresult = value; }
    }
}
