using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Codonbyte.Development;
using System.ComponentModel;

namespace Codonbyte.Development.TransformRendering
{
    public class VectorGizmo : MonoBehaviour, IFancyVectorGizmo
    {
        [SerializeField]
        private GameObject origin;

        [SerializeField]
        private GameObject shaft;

        [SerializeField]
        private GameObject head;

        [SerializeField]
        private Vector3 _value = Vector3.forward;
        public Vector3 Value
        {
            get { return _value; }
            set
            {
                _value = value;
                UpdateEverything();
            }
        }

        [SerializeField]
        private Space _space = Space.Self;
        public Space Space
        {
            get { return _space; }
            set
            {
                _space = value;
                UpdateEverything();
            }
        }

        [SerializeField]
        private float _originDiameter = .1f;
        public float OriginDiameter
        {
            get { return _originDiameter; }
            set
            {
                _originDiameter = value;
                UpdateEverything();
            }
        }

        [SerializeField]
        private float _shaftDiameter = .04f;
        public float ShaftDiameter
        {
            get { return _shaftDiameter; }
            set
            {
                _shaftDiameter = value;
                UpdateEverything();
            }
        }

        [SerializeField]
        private float _coneLength = .4f;
        public float ConeLength
        {
            get { return _coneLength; }
            set
            {
                _coneLength = value;
                UpdateEverything();
            }
        }

        [SerializeField]
        private float _coneDiameter = .225f;
        public float ConeDiameter
        {
            get { return _coneDiameter; }
            set
            {
                _coneDiameter = value;
                UpdateEverything();
            }
        }

        [SerializeField]
        private float _maxConeToMagnitudeRatio = .6f;
        public float MaxConeToMagnitudeRatio
        {
            get { return _maxConeToMagnitudeRatio; }
            set
            {
                _maxConeToMagnitudeRatio = value;
                UpdateEverything();
            }
        }
        /// <summary>
        /// Gets the the length that the head of the VectorGizmo actually has, taking into account
        /// downsizing that will occur if the magnitude of Value gets to be too low.
        /// </summary>
        public float ActualConeLength
        {
            get
            {
                float length = ConeLength;
                if (Value.magnitude == 0 || length / Value.magnitude > MaxConeToMagnitudeRatio)
                {
                    length = MaxConeToMagnitudeRatio * Value.magnitude;
                }
                return length;
            }
        }

        [SerializeField]
        private Material _material;
        public Material Material
        {
            get { return _material; }
            set
            {
                _material = value;
                SetMaterial();
            }
        }

        /// <summary>
        /// Updates all properties of the VectorGizmo.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void UpdateEverything()
        {
            SetOriginDimensions();
            SetShaftDimensions();
            SetHeadDimensions();
            SetHeadPosition();
            RotateGizmoInAssignedSpace();
            SetMaterial();
        }

        private void SetOriginDimensions()
        {
            if (origin == null) return;
            origin.transform.localScale = OriginDiameter * Vector3.one;
        }
        private void SetShaftDimensions()
        {
            if (shaft == null) return;
            shaft.transform.localScale =
                Mathf.Clamp(Value.magnitude - ActualConeLength, 0, Value.magnitude) *
                Vector3.forward;

            shaft.transform.localScale += ShaftDiameter * (Vector3.right + Vector3.up);
        }
        private void SetHeadDimensions()
        {
            if (head == null) return;
            head.transform.localScale = ActualConeLength * Vector3.forward +
                ConeDiameter * (Vector3.right + Vector3.up);
        }
        private void SetHeadPosition()
        {
            if (head == null) return;
            head.transform.localPosition = Value.magnitude * Vector3.forward;
        }
        private void RotateGizmoInAssignedSpace()
        {
            ResetRotation();
            var q = Quaternion.FromToRotation(Vector3.forward, Value);
            var aa = q.AngleAxis();
            transform.Rotate(aa.Axis, aa.Angle, Space);
        }
        private void SetMaterial()
        {
            foreach (var renderer in GetComponentsInChildren<MeshRenderer>())
            {
                renderer.material = _material;
            }
        }

        private void ResetRotation()
        {
            if (Space == Space.Self) transform.localRotation = Quaternion.identity;
            else if (Space == Space.World) transform.rotation = Quaternion.identity;
        }

        void Update()
        {
            if (Space == Space.World) UpdateEverything();
        }
    }
}
