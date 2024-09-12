using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meekou.Fig.Models
{
    /// <summary>
    /// input for sum
    /// </summary>
    public class SumInput
    {
        /// <summary>
        /// data in json format
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// path for value to sum
        /// </summary>
        public string Path { get; set; }
    }
}
