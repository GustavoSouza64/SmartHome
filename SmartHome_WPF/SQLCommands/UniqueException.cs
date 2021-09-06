using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome_WPF.SQLCommands
{
    public class UniqueException : Exception
    {
        public UniqueException() : base()
        {

        }
        public UniqueException(String Mensagem) : base(Mensagem)
        {

        }

        public UniqueException(String Mensagem, System.Exception inner) : base(Mensagem, inner)
        {

        }

        protected UniqueException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {

        }
    }
}

