using UnityEngine;
using System.Collections;

namespace Codonbyte.SpaceBilliards.Cosmos
{
    public class RotateSkyboxWithAxisSpin : MonoBehaviour
    {
        [SerializeField]
        private SpinOnAxis spinnerToFollow;

        void Update()
        {
            RenderSettings.skybox.SetFloat("_rotation", (float)spinnerToFollow.CurrentRotationFraction);
        }
    } 
}
