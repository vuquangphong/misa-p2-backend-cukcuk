using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Core.Attributes
{
    /// <summary>
    /// @author: VQPhong (14/07/2022)
    /// @desc: Marking the Properties that are the primary keys
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKey : Attribute
    {
    }

    /// <summary>
    /// @author: VQPhong (14/07/2022)
    /// @desc: Marking the Properties that are not allowed Empty
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotEmpty : Attribute
    {
    }

    /// <summary>
    /// @author: VQPhong (14/07/2022)
    /// @desc: Marking the Properties that are not allowed Duplicated
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotDuplicated : Attribute
    {
    }

    /// <summary>
    /// @author: VQPhong (14/07/2022)
    /// @desc: Marking the Vietnamese Name of Properties
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PropsName : Attribute
    {
        public string Name { get; set; }

        public PropsName(string name)
        {
            Name = name;
        }
    }
}
