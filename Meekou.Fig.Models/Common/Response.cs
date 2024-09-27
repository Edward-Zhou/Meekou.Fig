using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meekou.Fig.Models.Common
{
    /// <summary>
    /// This class is used to create standard responses for AJAX/remote requests.
    /// </summary>
    [Serializable]
    public class Response : Response<object>
    {
        /// <summary>
        /// Creates an <see cref="Response"/> object.
        /// <see cref="ResponseBase.Success"/> is set as true.
        /// </summary>
        public Response()
        {

        }

        /// <summary>
        /// Creates an <see cref="Response"/> object with <see cref="ResponseBase.Success"/> specified.
        /// </summary>
        /// <param name="success">Indicates success status of the result</param>
        public Response(bool success)
            : base(success)
        {

        }

        /// <summary>
        /// Creates an <see cref="Response"/> object with <see cref="Response{TResult}.Result"/> specified.
        /// <see cref="ResponseBase.Success"/> is set as true.
        /// </summary>
        /// <param name="result">The actual result object</param>
        public Response(object result)
            : base(result)
        {

        }

        /// <summary>
        /// Creates an <see cref="Response"/> object with <see cref="ResponseBase.Error"/> specified.
        /// <see cref="ResponseBase.Success"/> is set as false.
        /// </summary>
        /// <param name="error">Error details</param>
        public Response(ErrorInfo error)
            : base(error)
        {

        }
    }
}
