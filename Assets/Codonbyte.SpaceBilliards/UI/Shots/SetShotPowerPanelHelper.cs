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
        [SerializeField]
        private Slider shotPowerSlider;

        [SerializeField]
        private SetShotPowerGameState setPowerGameState;

        public void SetShotPowerOnSetPowerGameState()
        {
            setPowerGameState.NormalizedShotPower = shotPowerSlider.value;
        }
    } 
}
