using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Codonbyte.SpaceBilliards.Arena.States;

namespace Codonbyte.SpaceBilliards.UI.Debugging
{
	[ExecuteInEditMode]
	internal class DrawCurrentShotPowerSliderValue : UIBehaviour
	{
#pragma warning disable 649
		[SerializeField]
		private Text text;

		[SerializeField]
		private Slider slider;
#pragma warning restore 649

		void Update()
		{
			if (slider == null || text == null) return;
			text.text = "Shot Power Slider Value: " + slider.value;
		}
	}
}
