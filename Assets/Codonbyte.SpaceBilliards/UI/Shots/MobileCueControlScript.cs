using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections.Generic;
using System;
using System.Linq;
using Codonbyte;
using Codonbyte.SpaceBilliards;
using Codonbyte.SpaceBilliards.Arena;
using Codonbyte.Development.TransformRendering;


namespace Codonbyte.SpaceBilliards.UI
{
	public class MobileCueControlScript : MonoBehaviour
	{
#pragma warning disable 649
		[SerializeField]
		private Camera raycastCamera;

		[SerializeField]
		private Transform orbiter;

		[SerializeField]
		private Collider normalCollider;

		[SerializeField]
		private Collider fineTuneCollider;

		[SerializeField]
		private VectorGizmo vectorGizmo;
#pragma warning restore 649

		[SerializeField]
		private string horizontalAxis = "Horizontal";

		[SerializeField]
		private string verticalAxis = "Vertical";


		/*
		[SerializeField]
		[Range(0, 1)]
		private float normalMoveSpeed = 1;*/

		/*
		[SerializeField]
		[Range(0, 1)]
		private float fineTuneMoveSpeed = .05f;*/

		[SerializeField]
		[Range(0, 10)]
		private float raycastRange = 1f;

		[SerializeField]
		[Tooltip("The direction of positive horizontal movement.")]
		private Vector3 horizontalDirection = Vector3.right;

		[SerializeField]
		[Tooltip("The direction of positive vertical movement.")]
		private Vector3 verticalDirection = Vector3.up;

		[SerializeField]
		private string moveSlowAxis = "Slow";

		private void SetAxiesFromVector(Vector3 movement)
		{
			//float h = Vector3.Dot(movement, horizontalDirection) / horizontalDirection.magnitude;
			//float v = Vector3.Dot(movement, verticalDirection) / verticalDirection.magnitude;
			float h = movement.ScalarProjection(horizontalDirection);
			float v = movement.ScalarProjection(verticalDirection);
			
			CrossPlatformInputManager.SetAxis(horizontalAxis, h);
			CrossPlatformInputManager.SetAxis(verticalAxis, v);
		}

		/// <summary>
		/// Performs the raycast. Returns null if unsuccessful.
		/// </summary>
		/// <param name="screenPoint"></param>
		/// <returns>Null if unsuccessful, the raycast hit in question if successful.</returns>
		private RaycastHit? TryRaycast(Vector3 screenPoint)
		{
			Ray ray = raycastCamera.ScreenPointToRay(screenPoint);
			RaycastHit result;
			if (fineTuneCollider.Raycast(ray, out result, raycastRange))
				return result;
			if (normalCollider.Raycast(ray, out result, raycastRange))
				return result;
			return null;
		}

		/// <summary>
		/// Gets a lookup of all the touches that were successfully raycasted.
		/// </summary>
		/// <returns></returns>
		private IDictionary<Touch, RaycastHit> GetSuccessfulTouchRaycastLookup()
		{
			var lookup = new Dictionary<Touch, RaycastHit>();
			foreach (Touch touch in Input.touches)
			{
				RaycastHit? hit = TryRaycast(touch.position);
				if (hit == null) continue;
				lookup.Add(touch, hit.Value);
			}

			return lookup;
		}

		private void DoTouchRaycasts()
		{
			var lookup = GetSuccessfulTouchRaycastLookup();
			Vector3 offset = Vector3.zero;
			float slowValue = 0;
			if (lookup.Count == 1)
			{
				var hit = lookup.First().Value;
				slowValue = hit.collider == fineTuneCollider ? 1 : 0;

				offset = orbiter.InverseTransformPoint(hit.point).normalized;
			}

			CrossPlatformInputManager.SetAxis(moveSlowAxis, slowValue);
			if (vectorGizmo != null) vectorGizmo.Value = offset;

			SetAxiesFromVector(offset);
		}

		void Update()
		{
			//DoMouseRaycasts();
			DoTouchRaycasts();
		}
	} 
}
