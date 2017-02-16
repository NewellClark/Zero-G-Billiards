using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UniRx;

namespace Codonbyte.SpaceBilliards.UI.MobileInput
{
	internal static class TouchHelpers
	{
		public static Vector3 GetTargetDirectionForDragRotation(
			Camera camera, Transform rotatee, 
			Vector3 currentScreenPosition, 
			Vector3 previousScreenPosition)
		{
			//Vector3 forward = rotatee.forward;
			//Vector3 screenTarget = camera.WorldToScreenPoint(forward + camera.transform.position) +
			//	touch.deltaPosition.ToVector3();
			//Vector3 target = camera.ScreenPointToRay(screenTarget).direction;

			//return Quaternion.FromToRotation(forward, target);

			Vector3 forward = rotatee.forward;
			Vector3 screenTarget = camera.WorldToScreenPoint(forward + camera.transform.position) +
				currentScreenPosition - previousScreenPosition;
			Vector3 target = camera.ScreenPointToRay(screenTarget).direction;

			return target;
		}
	}
}
