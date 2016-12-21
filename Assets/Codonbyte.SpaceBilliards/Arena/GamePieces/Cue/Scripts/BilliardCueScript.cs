using UnityEngine;
using System.Collections;
using Codonbyte;
using System.Linq;
using Codonbyte.Development;

namespace Codonbyte.SpaceBilliards.Arena.GamePieces
{
    /// <summary>
    /// Script for the cue that the player uses to take their shot. 
    /// 
    /// </summary>
    internal class BilliardCueScript : MonoBehaviour
    {
#pragma warning disable 649
		[SerializeField]
        private Transform cueBody;

        [SerializeField]
        private BilliardBall cueBall;
#pragma warning restore 649

		[SerializeField]
        [Tooltip("The maximum amount of momentum that can be imparted on a cue-ball when taking a shot.")]
        [Range(0, 100)]
        private float _maxImpulse = 15f;
        /// <summary>
        /// Gets or sets the maximum amount of momentum that can be imparted on the cue-ball when taking a shot.
        /// </summary>
        public float MaxImpulse
        {
            get { return _maxImpulse; }
            set { _maxImpulse = value; }
        }

        /// <summary>
        /// The normalized direction that the cue is currently pointing, in local-space.
        /// </summary>
        public Vector3 LocalAimingDirection
        {
            get
            {
                if (cueBody == null) return transform.localRotation * Vector3.forward;
                return transform.InverseTransformVector(cueBody.forward);
            }
        }

        /// <summary>
        /// The normalized direction that the cue is currently pointing, in world-space.
        /// </summary>
        public Vector3 AimingDirection
        {
            get
            {
                if (cueBody == null) return transform.rotation * Vector3.forward;
                return cueBody.forward;
            }
        }

        /// <summary>
        /// Rotates the cue around the cue-ball using a vector that is perpendicular  to the direction the cue is aiming.
        /// </summary>
        /// <param name="normalToAimingDirection"></param>
        public void RotateAroundCueBall(Vector2 normalToAimingDirection)
        {
            /*
            var tangent = new Vector3(normalToAimingDirection.x, normalToAimingDirection.y, 0);
            var rotation = Quaternion.FromToRotation(Vector3.forward, Vector3.forward + tangent);
            transform.localRotation *= rotation;*/
            transform.localRotation *= Quaternion.identity.RotateByTangentVector(Vector3.forward, normalToAimingDirection);
        }

        /// <summary>
        /// Rotates the cue around the axis along which it is aiming.
        /// </summary>
        /// <param name="angle">Angle in degrees.</param>
        public void RotateAroundCueAxis(float angle)
        {
            Vector3 axis = Vector3.forward;
            var q = new AngleAxis(angle, axis).Quaternion;
            transform.localRotation *= q;
        }

        /// <summary>
        /// Applies an impulse to the cue-ball with a magnitude specified relative to MaxImpulse.
        /// </summary>
        /// <param name="normalizedImpulse">The impulse to apply to the cue-ball, relative to MaxImpulse. Must be between 0 and 1, inclusive.
        /// Value will be clamped between 0 and 1.</param>
        public bool ApplyImpulseNormalized(float normalizedImpulse)
        {
            return ApplyImpulseAbsolute(normalizedImpulse * MaxImpulse);
        }

        /// <summary>
        /// Applies an impulse of the specified magnitude to the cue-ball.
        /// </summary>
        /// <param name="impulse">The magnitude of the impulse to apply, in Newton-seconds. Value will be clamped between 0 and MaxImpulse.</param>
        /// <returns>True if the impulse was successfully applied. Impulse may fail to be applied if the cue-offset from the center of the cue-ball
        /// is so great that the cue-tip misses the cue-ball.</returns>
        public bool ApplyImpulseAbsolute(float impulse)
        {
            if (!IsApplyImpulseAbsoluteMethodAllowedToProceedOrAreThereErrorsThatMakeItUnsuccessful()) return false;

            Ray ray = new Ray(cueBody.position, AimingDirection);
            foreach (var hit in Physics.RaycastAll(ray, 2 * distanceFromCueBall))
            {
                var ball = hit.collider.GetComponent<BilliardBall>();
                if (ball != cueBall) continue;
                var rb = ball.GetComponent<Rigidbody>();
                Vector3 momentum = Mathf.Clamp(impulse, 0, MaxImpulse) * AimingDirection.normalized;
                rb.AddImpulseAtPosition(momentum, hit.point);
                return true;
            }

            return false;
        }

        private bool IsApplyImpulseAbsoluteMethodAllowedToProceedOrAreThereErrorsThatMakeItUnsuccessful()
        {
            if (cueBall == null)
            {
                Debug.Log("Cue has no target cue-ball!");
                return false;
            }
            if (cueBody == null)
            {
                Debug.Log("Cue body not specified.");
                return false;
            }
            return true;
        }

        private float distanceFromCueBall
        {
            get
            {
                return (cueBall.transform.position - cueBody.transform.position).magnitude;
            }
        }
    }
}
