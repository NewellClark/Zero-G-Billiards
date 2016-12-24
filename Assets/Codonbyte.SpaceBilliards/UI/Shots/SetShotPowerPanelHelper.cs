using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Codonbyte.SpaceBilliards.Arena.States;
namespace Codonbyte.SpaceBilliards.UI
{
	[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
	internal class SetShotPowerPanelHelper : UIBehaviour
	{
#pragma warning disable 649
		[SerializeField]
		private Slider shotPowerSlider;

		[SerializeField]
		private SetShotPowerGameState setPowerGameState;
#pragma warning restore 649

		public void SetShotPowerOnSetPowerGameState()
		{
			setPowerGameState.NormalizedShotPower = shotPowerSlider.value;
		}
	} 
}
