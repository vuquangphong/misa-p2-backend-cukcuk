using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Core.OtherModels
{
    /// <summary>
    /// @author: VQPhong (10/08/2022)
    /// @desc: Model for response data of API
    /// </summary>
    public class ControllerResponseData
    {
        /// <summary>
        /// The custom status code of response
        /// </summary>
        public int? customStatusCode;

        /// <summary>
        /// The data response
        /// </summary>
        public object? responseData;
    }
}
