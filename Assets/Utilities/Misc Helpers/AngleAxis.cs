using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Codonbyte
{

    /// <summary>
    /// Represents a Quaternion as an angle (in degrees) and an axis around which to rotate.
    /// </summary>
    public struct AngleAxis
    {
        /// <summary>
        /// Creates a new AngleAxis structure. 
        /// </summary>
        /// <param name="angle">Angle in degrees.</param>
        /// <param name="axis">Axis of rotation.</param>
        public AngleAxis(float angle, Vector3 axis)
        {
            Angle = angle;
            Axis = axis;
        }

        public float Angle { get; private set; }
        public Vector3 Axis { get; private set; }

        /// <summary>
        /// Gets the Quaternion-representation of the current AngleAxis.
        /// </summary>
        public Quaternion Quaternion
        {
            get { return Quaternion.AngleAxis(Angle, Axis); }
        }

        /// <summary>
        /// Creates a new AngleAxis structure from the specified Quaternion.
        /// </summary>
        /// <param name="q"></param>
        /// <returns>Structure representing the specified Quaternion as angle of rotation (in degrees) around a specified axis.</returns>
        public static AngleAxis FromQuaternion(Quaternion q)
        {
            float angle;
            Vector3 axis;
            q.ToAngleAxis(out angle, out axis);
            return new AngleAxis(angle, axis);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        /// Gets the AngleAxis representation of the current Quaternion.
        /// </summary>
        /// <param name="q"></param>
        /// <returns>AngleAxis representation of current Quaternion.</returns>
        public static AngleAxis AngleAxis(this Quaternion q)
        {
            return Codonbyte.AngleAxis.FromQuaternion(q);
        }
    }
}
