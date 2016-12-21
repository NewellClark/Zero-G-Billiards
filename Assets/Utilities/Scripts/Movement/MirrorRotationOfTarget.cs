using UnityEngine;
using System.Collections;

namespace Codonbyte
{
    /// <summary>
    /// Mirrors the rotation of an object without being a child of that object.
    /// </summary>
    [ExecuteInEditMode]
    public class MirrorRotationOfTarget : MonoBehaviour
    {
        [SerializeField]
        private Transform _targetToMirror;
        public Transform TargetToMirror
        {
            get { return _targetToMirror; }
            set { _targetToMirror = value; }
        }

        void LateUpdate()
        {
            if (TargetToMirror == null) return;
            transform.rotation = TargetToMirror.rotation;
        }
    } 
}
