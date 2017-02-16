using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Operators;
using System;

namespace Codonbyte.SpaceBilliards.UI.MobileInput
{
	public class DragToAimController : MonoBehaviour
	{
		[SerializeField]
		private Camera _camera;

		[SerializeField]
		private Transform _objectToRotate;

		private void Start()
		{
			var stream = Observable.EveryUpdate()
				.Select(_ => Input.touches)
				.Where(x => x.Length == 1)
				//.Select(x => RotateUsingTouch(_camera, _objectToRotate, x.Single()));
				.Select(x => x.Single())
				.Select(x => TouchHelpers.GetTargetDirectionForDragRotation(
					_camera, _objectToRotate, x.position, x.position - x.deltaPosition))
				.Select(x => Quaternion.FromToRotation(_objectToRotate.transform.forward, x))
				;

			stream.Subscribe(StreamAction);
		}

		private static Quaternion RotateUsingTouch(Camera camera, Transform rotatee, Touch touch)
		{
			Vector3 forward = rotatee.forward;
			Vector3 screenTarget = camera.WorldToScreenPoint(forward + camera.transform.position) +
				touch.deltaPosition.ToVector3();
			Vector3 target = camera.ScreenPointToRay(screenTarget).direction;

			return Quaternion.FromToRotation(forward, target);
		}

		private void StreamAction(Quaternion rotation)
		{
			_objectToRotate.rotation = rotation * _objectToRotate.rotation;
		}
	}
}

