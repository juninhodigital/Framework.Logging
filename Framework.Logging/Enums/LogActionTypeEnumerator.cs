using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Logging
{
    /// <summary>
    /// This enumerator is responsible for setting the current method execution step in order to be used to the log engine
    /// </summary>
    public enum LogActionTypeEnumerator
    {
        /// <summary>
        /// Indicates that a method has just started
        /// </summary>
        Started,

        /// <summary>
        /// Indicates that a method has just finished
        /// </summary>
        Finished
    }
}
