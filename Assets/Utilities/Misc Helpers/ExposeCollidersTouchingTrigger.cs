using UnityEngine;
using System.Collections.Generic;

namespace Codonbyte
{
    public class ExposeCollidersTouchingTrigger : MonoBehaviour
    {
        private List<Collider> touchingColliders = new List<Collider>();
        private Lazy<ICollection<Collider>> _readOnlyColliders;
        public ICollection<Collider> ContactingColliders
        {
            get { return _readOnlyColliders.Value; }
        }

        void Awake()
        {
            _readOnlyColliders = new Lazy<ICollection<Collider>>(touchingColliders.AsReadOnly);
        }

        void OnTriggerEnter(Collider other)
        {
            touchingColliders.Add(other);
        }

        void OnTriggerExit(Collider other)
        {
            touchingColliders.Remove(other);
        }
    } 
}
