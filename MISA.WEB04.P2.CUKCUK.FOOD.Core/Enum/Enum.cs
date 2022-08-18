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
        TransactionException = 50,
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

    /// <summary>
    /// @author: VQPhong (08/08/2022)
    /// @desc: Enum for the result of Inserting and Updating
    /// Success <=> 1
    /// Failure <=> 0
    /// </summary>
    public enum InsertUpdateResult
    {
        Failure = 0,
        Success = 1,
    }

    /// <summary>
    /// @author: VQPhong (16/08/2022)
    /// @desc: Enum for mode action (Insert - Update)
    /// </summary>
    public enum ModeAction
    {
        Insert = 0,
        Update = 1,
    }
}
