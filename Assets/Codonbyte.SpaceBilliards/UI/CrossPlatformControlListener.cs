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
#pragma warning disable 649
		[SerializeField]
		private string controlAxis;
#pragma warning restore 649

		protected override bool ControlActivated
		{
			get
			{
				return CrossPlatformInputManager.GetAxis(controlAxis) > .1f;
			}
		}
	} 
}
