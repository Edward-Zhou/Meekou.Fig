using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Meekou.Fig.Models.Common
{
    /// <summary>
    /// This class is used to create standard responses for  requests.
    /// </summary>
    [Serializable]
    public class Response<TResult> : ResponseBase
    {
        /// <summary>
        /// The actual result object of AJAX request.
        /// It is set if <see cref="ResponseBase.Success"/> is true.
        /// </summary>
        public TResult Result { get; set; }

        /// <summary>
        /// Creates an <see cref="Response"/> object with <see cref="Result"/> specified.
        /// <see cref="ResponseBase.Success"/> is set as true.
        /// </summary>
        /// <param name="result">The actual result object of AJAX request</param>
        public Response(TResult result)
        {
            Result = result;
            Success = true;
        }

        /// <summary>
        /// Creates an <see cref="Response"/> object.
        /// <see cref="ResponseBase.Success"/> is set as true.
        /// </summary>
        public Response()
        {
            Success = true;
        }

        /// <summary>
        /// Creates an <see cref="Response"/> object with <see cref="ResponseBase.Success"/> specified.
        /// </summary>
        /// <param name="success">Indicates success status of the result</param>
        public Response(bool success)
        {
            Success = success;
        }

        /// <summary>
        /// Creates an <see cref="Response"/> object with <see cref="ResponseBase.Error"/> specified.
        /// <see cref="ResponseBase.Success"/> is set as false.
        /// </summary>
        /// <param name="error">Error details</param>
        public Response(ErrorInfo error)
        {
            Error = error;
            Success = false;
        }
    }
}
