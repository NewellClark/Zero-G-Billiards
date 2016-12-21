using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Codonbyte
{
    public static class TransformHelpers
    {
        /// <summary>
        /// Returns a new quaternion that is the current quaternion rotated by the specified amount. The current quaternion is NOT modified.
        /// The rotation is specified by giving a rotation amount, specified as a vector tangent to a point on a sphere enclosing the quaternion.
        /// This method is useful for rotating objects via user input, since touches or clicks on a flat screen can easily produce a tangent vector which
        /// can be used in this method.
        /// </summary>
        /// <param name="@this"></param>
        /// <param name="pointOnSphere">A vector specifying a point on a sphere that is centered on the current quaternion. The rotation will be specified
        /// as a vector that is tangent to this point on the sphere. </param>
        /// <param name="rotationAmount">The tangent vector to rotate the quaternion by, specified in whatever space the quaternion is specified in.
        /// NOTE: this means that if you use transform.localRotation, you need to have the vector be specified in the PARENTs space, 
        /// i.e. use transform.parent.localPosition, NOT transform.localPosition.</param>
        /// <returns></returns>
        public static Quaternion RotateByTangentVector(this Quaternion @this, Vector3 pointOnSphere, Vector2 rotationAmount)
        {

            var standardDirection = Vector3.forward;
            var fromStandardToPointOnSphere = Quaternion.FromToRotation(standardDirection, pointOnSphere);
            var tangent = fromStandardToPointOnSphere * new Vector3(rotationAmount.x, rotationAmount.y);
            var rotation = Quaternion.FromToRotation(pointOnSphere, pointOnSphere + tangent);
            return @this * rotation;
        }

        /// <summary>
        /// Returns the scalar-projection of the current Vector3 onto the specified Vector3. The result will be negative if 
        /// the resulting vector projection points the opposite direction. 
        /// </summary>
        /// <param name="@this"></param>
        /// <param name="onNormal"></param>
        /// <returns></returns>
        public static float ScalarProjection(this Vector3 @this, Vector3 onNormal)
        {
            return Vector3.Dot(@this, onNormal) / onNormal.magnitude;
        }
    }
}
