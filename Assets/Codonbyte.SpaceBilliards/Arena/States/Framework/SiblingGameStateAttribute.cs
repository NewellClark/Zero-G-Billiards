using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Codonbyte.SpaceBilliards.Arena.States
{
    /// <summary>
    /// Indicates that the specified field should only be assigned to a GameState component that is attached to the 
    /// same GameObject that the holder of the field is attached to.
    /// </summary>
    public class SiblingGameStateAttribute : PropertyAttribute
    {
        public SiblingGameStateAttribute() { }
    }
}
