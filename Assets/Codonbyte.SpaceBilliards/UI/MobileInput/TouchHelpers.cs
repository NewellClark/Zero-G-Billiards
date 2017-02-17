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

		public static IObservable<float> GetPinchToZoomStream()
		{
			return Observable.EveryUpdate()
				.Select(_ => Input.touches)
				.Where(x => x.Length == 2)
				.Pairwise()
				.Where(x => x.Current.TouchSequencesContainSameTouches(x.Previous))
				.Select(x => x.Current)
				.Select(x => new
				{
					Current = x[0].position - x[1].position,
					Previous = (x[0].position - x[0].deltaPosition) - (x[1].position - x[1].deltaPosition)
				})
				.Select(x => x.Current.magnitude - x.Previous.magnitude)
				;
		}

		private static bool TouchSequencesContainSameTouches(this IEnumerable<Touch> @this, IEnumerable<Touch> other)
		{
			return @this.Select(x => x.fingerId).Intersect(other.Select(y => y.fingerId)).Count() == @this.Count();
		}
	}
}
