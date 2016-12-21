using UnityEngine;
using System.Collections;

namespace Codonbyte
{
    /// <summary>
    /// Follows a specified object without being a child of that object.
    /// </summary>
    [ExecuteInEditMode]
    public class FollowTargetObject : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The object that should be followed.")]
        private Transform _targetToFollow;
        /// <summary>
        /// Gets or sets the Transform that should be followed.
        /// </summary>
        public Transform TargetToFollow
        {
            get { return _targetToFollow; }
            set
            {
                _targetToFollow = value;
            }
        }

        [SerializeField]
        [Tooltip("The displacement to maintain between itself and the target object, in world-space.")]
        private Vector3 _displacementFromTarget = Vector3.zero;
        /// <summary>
        /// Gets or sets the displacement to maintain between itself and the target object, in world-space.
        /// </summary>
        public Vector3 DisplacementFromTarget
        {
            get { return _displacementFromTarget; }
            set
            {
                _displacementFromTarget = value;
            }
        }

        void Update()
        {
            if (TargetToFollow == null) return;
            transform.position = TargetToFollow.position + DisplacementFromTarget;
        }
    } 

}
