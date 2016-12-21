using UnityEngine;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using System.Reflection;
using Codonbyte.Development;

namespace Codonbyte.Development.TransformRendering
{
   [CustomEditor(typeof(ObsoleteVectorGizmo))]
   [EditorBrowsable(EditorBrowsableState.Never)]
   [System.Obsolete("Obviously if the target is Obsolete, so is the custom inspector.")]
   public class ObsoleteVectorGizmoInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            var myTarget = (ObsoleteVectorGizmo)target;

            myTarget.Value = EditorGUILayout.Vector3Field("Value", myTarget.Value);

            EditorGUILayout.Space();

            Helpers.ObjectField<GameObject>("Origin Object", target, "origin", true);
            Helpers.ObjectField<GameObject>("Shaft Object", target, "shaft", true);
            Helpers.ObjectField<GameObject>("Arrow Head Object", target, "head", true);

            EditorGUILayout.Space();
            
            myTarget.ConeDiameter = EditorGUILayout.FloatField("Cone Diameter", myTarget.ConeDiameter);
            myTarget.ConeLength = EditorGUILayout.FloatField("Cone Length", myTarget.ConeLength);
            myTarget.OriginDiameter = EditorGUILayout.FloatField("Origin Diameter", myTarget.OriginDiameter);
            myTarget.ShaftDiameter = EditorGUILayout.FloatField("Shaft Diameter", myTarget.ShaftDiameter);
            myTarget.VectorMaterial = (Material)EditorGUILayout.ObjectField("Material", (Material)myTarget.VectorMaterial, typeof(Material), true);

            if (GUI.changed) EditorUtility.SetDirty(target);
        }
    }
}
