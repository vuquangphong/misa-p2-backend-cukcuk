using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Core.Enum
{
    /// <summary>
    /// @author: VQPhong (14/07/2022).
    /// @desc: Enum Self-defined Status Code.
    /// </summary>
    public enum CustomizeStatusCode
    {
        GetOkay = 10,
        Created = 11,
        Updated = 12,
        Deleted = 13,
        NoContent = 14,
        BadRequest = 40,
        NormalException = 55,
    }

    /// <summary>
    /// @author: VQPhong (14/07/2022).
    /// @desc: Enum for the appearance of food on menu.
    /// </summary>
    public enum AppearOnMenu
    {
        Appear = 0,
        NotApear = 1,
    }
}
