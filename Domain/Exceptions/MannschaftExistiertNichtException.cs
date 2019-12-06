using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class MannschaftExistiertNichtException : Exception
    {
        public MannschaftExistiertNichtException(string message) : base(message)
        {
            
        }
    }
}
