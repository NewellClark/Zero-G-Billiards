using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Codonbyte
{
    public static partial class Extensions
    {
        /// <summary>
        /// Instantly adds the specified momentum to the rigidbody. 
        /// </summary>
        /// <param name="rb"></param>
        /// <param name="impulse">World-space impulse vector to add, in Newton-seconds.</param>
        public static void AddImpulse(this Rigidbody rb, Vector3 impulse)
        {
            rb.AddForce(impulse, ForceMode.Impulse);
        }

        /// <summary>
        /// Adds a continuous acceleration to the rigidbody.
        /// </summary>
        /// <param name="rb"></param>
        /// <param name="acceleration">The world-space acceleration vector to add, in m/s^2</param>
        public static void AddAcceleration(this Rigidbody rb, Vector3 acceleration)
        {
            rb.AddForce(acceleration, ForceMode.Acceleration);
        }

        /// <summary>
        /// Instantly adds the specified velocity vector to the rigidbody's velocity.
        /// </summary>
        /// <param name="rb"></param>
        /// <param name="velocityChange">World-space velocity vector to add, in meters/second</param>
        public static void AddVelocity(this Rigidbody rb, Vector3 velocityChange)
        {
            rb.AddForce(velocityChange, ForceMode.VelocityChange);
        }

        /// <summary>
        /// Instantly adds the specified momentum to the rigidbody. Momentum is specified in local-space.
        /// </summary>
        /// <param name="rb"></param>
        /// <param name="relativeImpulse">Local-space momentum vector to add, in Newton-seconds.</param>
        public static void AddRelativeImpulse(this Rigidbody rb, Vector3 relativeImpulse)
        {
            rb.AddRelativeForce(relativeImpulse, ForceMode.Impulse);
        }

        /// <summary>
        /// Adds a continuous acceleration to the rigidbody, specified in local-space.
        /// </summary>
        /// <param name="rb"></param>
        /// <param name="relativeAcceleration">Local-space acceleration vector to add, in metersr/second^2</param>
        public static void AddRelativeAcceleration(this Rigidbody rb, Vector3 relativeAcceleration)
        {
            rb.AddRelativeForce(relativeAcceleration, ForceMode.Acceleration);
        }

        /// <summary>
        /// Instantly adds the specified velocity to the rigidbody. Velocity is specified in local-space.
        /// </summary>
        /// <param name="rb"></param>
        /// <param name="relativeVelocity">Local-space velocity vector to add, in meters/second.</param>
        public static void AddRelativeVelocity(this Rigidbody rb, Vector3 relativeVelocity)
        {
            rb.AddRelativeForce(relativeVelocity, ForceMode.VelocityChange);
        }

        /// <summary>
        /// Instantly adds the specified momentum to the rigidbody at the specified position.
        /// </summary>
        /// <param name="rb"></param>
        /// <param name="impulse">Impulse vector in world-coordinates.</param>
        /// <param name="position">Position in world coordinates.</param>
        public static void AddImpulseAtPosition(this Rigidbody rb, Vector3 impulse, Vector3 position)
        {
            rb.AddForceAtPosition(impulse, position, ForceMode.Impulse);
        }

        /// <summary>
        /// Applies a continuous acceleration to the rigidbody at the specified position.
        /// </summary>
        /// <param name="rb"></param>
        /// <param name="acceleration">Acceleration vector in world coordinates.</param>
        /// <param name="position">Position in world coordinates.</param>
        public static void AddAccelerationAtPosition(this Rigidbody rb, Vector3 acceleration, Vector3 position)
        {
            rb.AddForceAtPosition(acceleration, position, ForceMode.Acceleration);
        }

        /// <summary>
        /// Instantly adds the specified velocity to the rigidbody at the specified position.
        /// </summary>
        /// <param name="rb"></param>
        /// <param name="velocity">Velocity vector in world coordinates.</param>
        /// <param name="position">Position in world coordinates.</param>
        public static void AddVelocityAtPosition(this Rigidbody rb, Vector3 velocity, Vector3 position)
        {
            rb.AddForceAtPosition(velocity, position, ForceMode.VelocityChange);
        }

        /// <summary>
        /// Adds an instantaneous angular impulse to the rigidbody, specified in world-space.
        /// This method is identical to AddTorqueImpulse().
        /// </summary>
        /// <param name="rb"></param>
        /// <param name="angularImpulse">Amount of angular momentum to add to the rigidbody, in N*m*s (Newton-meter-seconds). 
        /// Vector must be in world-space.</param>
        public static void AddAngularImpulse(this Rigidbody rb, Vector3 angularImpulse)
        {
            rb.AddTorque(angularImpulse, ForceMode.Impulse);
        }

        /// <summary>
        /// Adds an instantaneous angular impulse to the rigidbody.
        /// This method is identical to AddAngularImpulse().
        /// </summary>
        /// <param name="rb"></param>
        /// <param name="angularImpulse">Amount of angular momentum to add, in N*m*s (Newton-meter-seconds).</param>
        public static void AddTorqueImpulse(this Rigidbody rb, Vector3 angularImpulse)
        {
            rb.AddAngularImpulse(angularImpulse);
        }

        /// <summary>
        /// Adds a continuous angular acceleration to the rigidbody, in world-space.
        /// </summary>
        /// <param name="rb"></param>
        /// <param name="angularAcceleration">Angular acceleration vector, in radians/second^2</param>
        public static void AddAngularAcceleration(this Rigidbody rb, Vector3 angularAcceleration)
        {
            rb.AddTorque(angularAcceleration, ForceMode.Acceleration);
        }

        /// <summary>
        /// Instantly adds the specified angular velocity to the rigidbody.
        /// </summary>
        /// <param name="rb"></param>
        /// <param name="velocityChange">Angular velocity to add, in radians/second.
        /// Vector must be in world-space.</param>
        public static void AddAngularVelocity(this Rigidbody rb, Vector3 velocityChange)
        {
            rb.AddTorque(velocityChange, ForceMode.VelocityChange);
        }

        /// <summary>
        /// Instantly adds the specified amount of angular momentum to the rigidbody. Angular momentum must be specified in local-space.
        /// </summary>
        /// <param name="rb"></param>
        /// <param name="relativeAngularImpulse">Local-space angular-momentum vector to add, in Newton-meter-seconds.</param>
        public static void AddRelativeAngularImpulse(this Rigidbody rb, Vector3 relativeAngularImpulse)
        {
            rb.AddRelativeTorque(relativeAngularImpulse, ForceMode.Impulse);
        }

        /// <summary>
        /// Adds a continuous angular acceleration to the rigidbody. Must be specified in local-space.
        /// </summary>
        /// <param name="rb"></param>
        /// <param name="relativeAngularAcceleration">Local-space angular acceleration vector to add to the rigidbody, in radians/second^2.</param>
        public static void AddRelativeAngularAcceleration(this Rigidbody rb, Vector3 relativeAngularAcceleration)
        {
            rb.AddRelativeTorque(relativeAngularAcceleration, ForceMode.Acceleration);
        }

        /// <summary>
        /// Instantly adds the specified angular velocity to the rigidbody. Must be specified in local-space.
        /// </summary>
        /// <param name="rb"></param>
        /// <param name="relativeAngularVelocity">Local-space angular velocity vector to add to the rigidbody, in radians/second.</param>
        public static void AddRelativeAngularVelocity(this Rigidbody rb, Vector3 relativeAngularVelocity)
        {
            rb.AddRelativeTorque(relativeAngularVelocity, ForceMode.VelocityChange);
        }
    }
}