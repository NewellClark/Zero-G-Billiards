using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel;

namespace Codonbyte.SpaceBilliards.Arena.GamePieces
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal class CueAimerScript : MonoBehaviour
    {
        [SerializeField]
        private SimpleRayScript _aimerRay;

        [SerializeField]
        private BilliardCueScript _cue;

        [SerializeField]
        [Tooltip("The maximum distance that the aimer-ray should project.")]
        private float _maxDistance = 100;
        /// <summary>
        /// Gets or sets the maximum distance the aimer should extend.
        /// </summary>
        public float MaxDistance
        {
            get { return _maxDistance; }
            set
            {
                _maxDistance = value;
            }
        }

        void Update()
        {
            RaycastHit result;
            Ray ray = new Ray(transform.position, _cue.AimingDirection);
            if (Physics.Raycast(ray, out result, MaxDistance))
            {
                _aimerRay.RayVector = result.point - transform.position;
            }
            else
            {
                _aimerRay.RayVector = _cue.AimingDirection.normalized * MaxDistance;
            }
        }
    } 
}
