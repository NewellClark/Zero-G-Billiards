using UnityEngine;
using System.Collections.Generic;

namespace Codonbyte.Development.TransformRendering
{
    /// <summary>
    /// A standard, run-of-the-mill 3D Vector3 sprite for displaying the value of a vector.
    /// The Value property is in local space.
    /// </summary>
    [System.Obsolete("Don't use this anymore. Use the other, better VectorGizmo class.")]
    public class ObsoleteVectorGizmo : MonoBehaviour//, IVectorGizmo
    {
        [SerializeField]
        private GameObject origin;

        [SerializeField]
        private GameObject shaft;

        [SerializeField]
        private GameObject head;

        [SerializeField]
        private Vector3 _value = Vector3.forward;
        private void UpdateConePosition(Vector3 value)
        {
            if (head == null) return;
            head.transform.localPosition = new Vector3(
                head.transform.localPosition.x, 
                head.transform.localPosition.y, 
                value.magnitude);
        }
        private void UpdateShaftLength(float value)
        {
            if (shaft == null) return;
            shaft.transform.localScale = new Vector3(shaft.transform.localScale.x, shaft.transform.localScale.y, value);
        }
        private void UpdateValue(Vector3 value)
        {
            _value = value;
            UpdateConePosition(value);
            float newLength = Mathf.Clamp(value.magnitude - ConeLength, 0, value.magnitude);
            UpdateShaftLength(newLength);
            transform.localRotation = Quaternion.FromToRotation(Vector3.forward, value);
        }
        public Vector3 Value
        {
            get { return _value; }
            set { UpdateValue(value); }
        }

        [SerializeField]
        private float _coneDiameter = .275f;
        private void UpdateConeDiameter(float value)
        {
            _coneDiameter = value;
            if (head == null) return;
            head.transform.localScale = new Vector3(value, value, head.transform.localScale.z);
        }
        public float ConeDiameter
        {
            get { return _coneDiameter; }
            set { UpdateConeDiameter(value); }
        }

        [SerializeField]
        private float _coneLength = .5f;
        private void UpdateConeLength(float value)
        {
            _coneLength = value;
            if (head == null) return;

            head.transform.localScale = new Vector3(head.transform.localScale.x, head.transform.localScale.y, value);
            UpdateShaftLength(Value.magnitude - ConeLength);
        }
        public float ConeLength
        {
            get { return _coneLength; }
            set
            {
                UpdateConeLength(value);
            }
        }



        [SerializeField]
        private float _shaftDiameter = .1f;
        private void UpdateShaftDiameter(float value)
        {
            _shaftDiameter = value;
            if (shaft == null) return;
            shaft.transform.localScale = new Vector3(value, value, shaft.transform.localScale.z);
        }
        public float ShaftDiameter
        {
            get { return _shaftDiameter; }
            set { UpdateShaftDiameter(value); }
        }

        [SerializeField]
        private float _originDiameter = .175f;
        private void UpdateOriginDiameter(float value)
        {
            _originDiameter = value;
            if (origin == null) return;
            origin.transform.localScale = Vector3.one * value;
        }
        public float OriginDiameter
        {
            get { return _originDiameter; }
            set { UpdateOriginDiameter(value); }
        }

        [SerializeField]
        private Material _vectorMaterial;
        private void UpdateVectorMaterial(Material value)
        {
            _vectorMaterial = value;
            var meshes = GetComponentsInChildren<MeshRenderer>();
            foreach (var mesh in meshes) mesh.material = value;
        }
        public Material VectorMaterial
        {
            get { return _vectorMaterial; }
            set { UpdateVectorMaterial(value); }
        }

        private void UpdateAllProperties()
        {

            UpdateConeDiameter(ConeDiameter);
            UpdateConeLength(ConeLength);
            UpdateShaftDiameter(ShaftDiameter);
            UpdateOriginDiameter(OriginDiameter);
            UpdateValue(Value);
        }

        // Use this for initialization
        void Start()
        {
            UpdateAllProperties();
        }

        // Update is called once per frame
        void Update()
        {

        }
    } 
}
