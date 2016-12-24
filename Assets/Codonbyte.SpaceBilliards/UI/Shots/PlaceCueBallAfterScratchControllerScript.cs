using UnityEngine;
using System.Collections.Generic;
using Codonbyte.SpaceBilliards;
using Codonbyte.SpaceBilliards.Arena;
using Codonbyte.SpaceBilliards.GameLogic;
using Codonbyte.SpaceBilliards.Arena.States;
using Codonbyte;
using UnityStandardAssets.CrossPlatformInput;
using System.Linq;

namespace Codonbyte.SpaceBilliards.UI
{
	public class PlaceCueBallAfterScratchControllerScript : MonoBehaviour
	{
#pragma warning disable 649
		[SerializeField]
		private PlayerScratchedGameState scratchedState;

		[SerializeField]
		private Collider raycastTarget;

		[SerializeField]
		private Camera uiCamera;
#pragma warning restore 649

		[SerializeField]
		private string submitAxis = "Submit";

		

		void Update()
		{
			Vector3? pointer = GetPointerPosition();
			if (pointer == null) return;

			Ray ray = uiCamera.ScreenPointToRay(pointer.Value);
			RaycastHit hit;
			if (raycastTarget.Raycast(ray, out hit, 100))
			{
				scratchedState.PlacementPosition = hit.point;
			}

			if (CrossPlatformInputManager.GetAxis(submitAxis) > .5f)
				scratchedState.PlaceCueBall();
		}

		private Vector3? GetPointerPosition()
		{
			if (Input.GetMouseButton(0)) return Input.mousePosition;

			return null;
		}
	} 
}
