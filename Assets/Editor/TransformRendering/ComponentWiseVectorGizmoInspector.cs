using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System;
using System.ComponentModel;
using Codonbyte.Development;

namespace Codonbyte.Development.TransformRendering
{
    [CustomEditor(typeof(ComponentWiseVectorGizmo))]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class ComponentWiseVectorGizmoInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUI.changed)
            {
                var myTarget = (ComponentWiseVectorGizmo)target;
                myTarget.UpdateEverything();
                EditorUtility.SetDirty(myTarget);
            }
        }
    }
}
