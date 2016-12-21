using UnityEngine;
using System.Collections;

namespace Codonbyte.SpaceBilliards.Cosmos
{
	public class RotateSkyboxWithAxisSpin : MonoBehaviour
	{
#pragma warning disable 649
		[SerializeField]
		private SpinOnAxis spinnerToFollow;
#pragma warning restore 649

		void Update()
		{
			RenderSettings.skybox.SetFloat("_rotation", (float)spinnerToFollow.CurrentRotationFraction);
		}
	} 
}
