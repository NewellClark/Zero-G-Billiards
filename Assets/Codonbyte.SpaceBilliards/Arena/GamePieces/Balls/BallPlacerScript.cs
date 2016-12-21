using UnityEngine;
using System.Collections;
using Codonbyte;


namespace Codonbyte.SpaceBilliards.Arena.GamePieces
{
    /// <summary>
    /// Allows the player to specify the location of a ball by moving a ghost-ball.
    /// </summary>
    public class BallPlacerScript : MonoBehaviour
    {
        [SerializeField]
        private Color _validColor = Color.green;
        public Color ValidColor
        {
            get { return _validColor; }
            set
            {
                _validColor = value;
            }
        }

        [SerializeField]
        private Color _invalidColor = Color.red;
        public Color InvalidColor
        {
            get { return _invalidColor; }
            set
            {
                _invalidColor = value;
            }
        }

        private bool _isLocationValid;
        public bool IsLocationValid
        {
            get { return _isLocationValid; }
            private set
            {
                _isLocationValid = value;
                Renderer.material.color = IsLocationValid ? ValidColor : InvalidColor;
            }
        }

        private Lazy<Renderer> rend;
        private Renderer Renderer
        {
            get { return rend.Value; }
        }

        [SerializeField]
        private ExposeCollidersTouchingTrigger ballDetector = null;

        void LateUpdate()
        {
            IsLocationValid = ballDetector.ContactingColliders.Count == 0;
        }

        void Awake()
        {
            rend = new Lazy<Renderer>(GetComponent<Renderer>);
            IsLocationValid = IsLocationValid;
        }
    } 
}
