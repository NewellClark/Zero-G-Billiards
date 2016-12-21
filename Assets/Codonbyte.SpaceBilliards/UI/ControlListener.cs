using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Codonbyte.SpaceBilliards.UI
{
    /// <summary>
    /// Base class for an object that listens to see if the player has activated some sort of control in the current update cycle.
    /// </summary>
    public abstract class ControlListener : MonoBehaviour
    {
        /// <summary>
        /// Gets a value indicating whether the submit control has been activated in the current update cycle.
        /// When overriding, should only return true on the cycle when the control is first activated.
        /// </summary>
        protected abstract bool ControlActivated { get; }

        /// <summary>
        /// Raised on the update event where the control is first activated.
        /// </summary>
        public event EventHandler OnControlActivated;

        protected virtual void Update()
        {
            if (ControlActivated)
            {
                if (OnControlActivated != null) OnControlActivated(this, new EventArgs());
            }
        }
    }
}
