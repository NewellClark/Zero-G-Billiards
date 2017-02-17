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

		[SerializeField]
		private float _sensitivityFactor = 1;

		private void Start()
		{
			var stream = Observable.EveryUpdate()
				.Select(_ => Input.touches)
				.Where(x => x.Length == 1)
				.Select(x => x.Single())
				.Select(x => TouchHelpers.GetTargetDirectionForDragRotation(
					_camera, _objectToRotate, 
					x.position, 
					x.position - x.deltaPosition * _sensitivityFactor))
				.Select(x => Quaternion.FromToRotation(_objectToRotate.transform.forward, x))
				;

			stream.Subscribe(StreamAction);
		}

		private void StreamAction(Quaternion rotation)
		{
			if (!gameObject.activeInHierarchy)
				return;
			_objectToRotate.rotation = rotation * _objectToRotate.rotation;
		}

	}
}

