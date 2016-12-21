using UnityEngine;
using System.Collections.Generic;
using Codonbyte.Development;

namespace Codonbyte.Development.TransformRendering
{
    public class RigidBodyRandomRotationAtStart : MonoBehaviour
    {
        [SerializeField]
        private float _minAngularMomentum = 5;

        [SerializeField]
        private float _maxAngularMomentum = 8;

        private bool initialized = false;
        private Rigidbody rb;

        void Update()
        {
            if (!initialized)
            {
                rb = GetComponent<Rigidbody>();
                if (rb == null) return;

                Vector3 varience = Random.insideUnitSphere;
                Vector3 angularMomentum = varience.normalized * _minAngularMomentum + varience * (_maxAngularMomentum - _minAngularMomentum);

                rb.AddAngularImpulse(angularMomentum);

                initialized = true;
            }
        }
    }
}
