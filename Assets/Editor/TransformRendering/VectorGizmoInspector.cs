using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using Codonbyte.Development;
using System.ComponentModel;

namespace Codonbyte.Development.TransformRendering
{
    [CustomEditor(typeof(VectorGizmo))]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class VectorGizmoInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var myTarget = (VectorGizmo)target;
            if (GUI.changed)
            {
                myTarget.UpdateEverything();
                EditorUtility.SetDirty(myTarget);
            }
        }
    }
}
