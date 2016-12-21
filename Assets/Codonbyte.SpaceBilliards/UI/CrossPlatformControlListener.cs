using UnityEngine;
using System.Collections;
using System;
using UnityStandardAssets.CrossPlatformInput;

namespace Codonbyte.SpaceBilliards.UI
{
    /// <summary>
    /// Determines whether a control axis on CrossPlatformInputManager has been activated. 
    /// </summary>
    public class CrossPlatformControlListener : ControlListener
    {
        [SerializeField]
        private string controlAxis;

        protected override bool ControlActivated
        {
            get
            {
                return CrossPlatformInputManager.GetAxis(controlAxis) > .1f;
            }
        }
    } 
}
