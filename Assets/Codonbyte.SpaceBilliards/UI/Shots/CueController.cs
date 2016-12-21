using UnityEngine;
using System.Collections;
using Codonbyte.SpaceBilliards.Arena.GamePieces;
using UnityStandardAssets.CrossPlatformInput;

namespace Codonbyte.SpaceBilliards.UI.Shots
{
    /// <summary>
    /// Responsible for controlling cue via user input in a PC app.
    /// </summary>
    public class CueController : MonoBehaviour
    {
        [SerializeField]
        private float _rotateSpeed = 2;
        public float RotateSpeed
        {
            get { return _rotateSpeed; }
            set { _rotateSpeed = value; }
        }

        [SerializeField]
        private float _slowMultiplier = .1f;
        public float SlowMultiplier
        {
            get { return _slowMultiplier; }
            set { _slowMultiplier = value; }
        }

        [SerializeField]
        private string _horizontalAxis = "Horizontal";
        public string HorizontalAxis
        {
            get { return _horizontalAxis; }
            set { _horizontalAxis = value; }
        }
        private float Horizontal
        {
            get { return CrossPlatformInputManager.GetAxis(HorizontalAxis); }
        }

        [SerializeField]
        private string _verticalAxis = "Vertical";
        public string VerticalAxis
        {
            get { return _verticalAxis; }
            set { _verticalAxis = value; }
        }
        private float Vertical
        {
            get { return CrossPlatformInputManager.GetAxis(VerticalAxis); }
        }

        [SerializeField]
        private string _axialRotationAxis = "Rotate";
        public string AxialRotationAxis
        {
            get { return _axialRotationAxis; }
            set { _axialRotationAxis = value; }
        }
        private float AxialRotation
        {
            get { return CrossPlatformInputManager.GetAxis(AxialRotationAxis); }
        }

        [SerializeField]
        private string _slowModifierAxis = "Slow";
        public string SlowModifierAxis
        {
            get { return _slowModifierAxis; }
            set { _slowModifierAxis = value; }
        }

        private bool Slow
        {
            get { return CrossPlatformInputManager.GetAxis(SlowModifierAxis) > .5f; }
        }

        [SerializeField]
        private BilliardCueScript _cue;
        internal BilliardCueScript Cue
        {
            get { return _cue; }
            set { _cue = value; }
        }

        void Update()
        {
            Vector2 tangent = new Vector2(Horizontal, Vertical).normalized * RotateSpeed * Time.deltaTime;
            float axialRotation = -AxialRotation * RotateSpeed * Mathf.Rad2Deg * Time.deltaTime;

            if (Slow)
            {
                tangent *= SlowMultiplier;
                axialRotation *= SlowMultiplier;
            }

            Cue.RotateAroundCueBall(tangent);
            Cue.RotateAroundCueAxis(axialRotation);
        }
    } 
}
