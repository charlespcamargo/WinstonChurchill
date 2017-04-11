using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WinstonChurchill.API.Common.Erros
{
    public class CustomValidationException : ArgumentException
    {
        public CustomValidationException()
            : base() { }

        public CustomValidationException(List<ErrorMessage> erroMsg)
        {
            string msg = string.Empty;
            foreach (var item in erroMsg)
            {
                msg += string.Format("|{0}", item.MensagemValidacao);
            }
            msg = msg.Substring(1);
            throw new ArgumentException(msg);
        }

        public CustomValidationException(string message)
            : base(message) { }

        public CustomValidationException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public CustomValidationException(string message, Exception innerException)
            : base(message, innerException) { }

        public CustomValidationException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected CustomValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
