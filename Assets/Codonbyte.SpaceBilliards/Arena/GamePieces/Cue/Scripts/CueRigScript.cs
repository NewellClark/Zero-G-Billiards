using UnityEngine;
using System.Collections;
using System;

namespace Codonbyte.SpaceBilliards.Arena.GamePieces
{
    public class CueRigScript : MonoBehaviour, ICueRig
    {
        public bool FollowCueBall
        {
            get
            {
                return followCueBehaviour.enabled;
            }

            set
            {
                followCueBehaviour.enabled = value;
            }
        }

        public bool ModelVisible
        {
            get
            {
                return cueModel.activeSelf;
            }

            set
            {
                cueModel.SetActive(value);
            }
        }

        public bool AimingControllerActive
        {
            get { return aimingController.activeSelf; }
            set { aimingController.SetActive(value); }
        }

        [SerializeField]
        private GameObject cueModel;

        [SerializeField]
        private MonoBehaviour followCueBehaviour;

        [SerializeField]
        private GameObject aimingController;
    }
}
