using UnityEngine;
using System.Collections.Generic;
using System.ComponentModel;
using Codonbyte.Development;

namespace Codonbyte.Development.TransformRendering
{
    public interface IVectorGizmo
    {
        Vector3 Value { get; set; }

        Space Space { get; set; }
    }
    public interface IFancyVectorGizmo : IVectorGizmo
    {
        float ConeLength { get; set; }

        float ConeDiameter { get; set; }

        float ShaftDiameter { get; set; }

        float OriginDiameter { get; set; }


        void UpdateEverything();
    }
}
