using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Codonbyte.SpaceBilliards.UI.MobileInput
{
	public class PinchToZoomController : MonoBehaviour
	{
		[SerializeField]
		private Transform _cameraRig;

		[SerializeField]
		private float _pixelsPerUnit = 100;

		private void Start()
		{
			TouchHelpers.GetPinchToZoomStream()
				.Subscribe(HandleStream);

		}

		private void HandleStream(float value)
		{
			_cameraRig.localPosition += Vector3.forward * value / _pixelsPerUnit;
		}
	}
}
