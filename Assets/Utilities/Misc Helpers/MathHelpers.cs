using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Codonbyte
{
    public static class MathHelpers
    {
        /// <summary>
        /// Gets the average of all the arguments.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static float Mean(this IEnumerable<float> @this)
        {
            int count = 0;
            float sum = 0;
            foreach (float arg in @this)
            {
                sum += arg;
                count++;
            }
            return sum / count;
        }

        /// <summary>
        /// Gets the average of all the elements in args.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static float Mean(params float[] args)
        {
            return Mean((IEnumerable<float>)args);
        }

        private static IEnumerable<float> ToEnumerable(this Vector3 @this)
        {
            for (int index = 0; index < 3; index++) yield return @this[index];
        }

        public static float[] ToArray(this Vector3 @this) { return @this.ToEnumerable().ToArray(); }

        private static IEnumerable<float> ToEnumerable(this Vector2 @this)
        {
            for (int index = 0; index < 2; index++) yield return @this[index];
        }

        public static float[] ToArray(this Vector2 @this) { return @this.ToEnumerable().ToArray(); }

        private static IEnumerable<float> ToEnumerable(this Vector4 @this)
        {
            for (int index = 0; index < 4; index++) yield return @this[index];
        }

        public static float[] ToArray(this Vector4 @this) { return @this.ToEnumerable().ToArray(); }
    }
}
