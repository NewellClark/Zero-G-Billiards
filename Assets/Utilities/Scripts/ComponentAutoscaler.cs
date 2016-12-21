using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Codonbyte.Development;

namespace Codonbyte
{
    /// <summary>
    /// NOT DONE DO NOT USE DO NOT USE DO NOT USE!
    /// </summary>
    public abstract class ComponentAutoscaler : MonoBehaviour
    {
        protected struct ResizeArgs
        {
            public float OldCondensedScale { get; private set; }
            public float NewCondensedScale { get; private set; }
            public Vector3 OldLossyScale { get; private set; }
            public Vector3 NewLossyScale { get; private set; }

            public ResizeArgs(Vector3 oldLossyScale, Vector3 newLossyScale) : this()
            {
                OldLossyScale = oldLossyScale;
                NewLossyScale = newLossyScale;
                OldCondensedScale = OldLossyScale.ToArray().Mean();
                NewCondensedScale = NewLossyScale.ToArray().Mean();
            }
        }
        protected delegate void ResizeComponent(ResizeArgs resizeInfo);

    }
}
