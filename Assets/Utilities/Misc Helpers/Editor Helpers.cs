using UnityEngine;
using System.Collections.Generic;

using System.Reflection;

#if UNITY_EDITOR
using UnityEditor;
namespace Codonbyte.Development
{
    public static class Helpers
    {
        /// <summary>
        /// Creates an object field in an inspector for a specific field in the object being inspected.
        /// </summary>
        /// <typeparam name="T">The type of object for the object field to accept.</typeparam>
        /// <param name="caption">The caption of the object field.</param>
        /// <param name="inspectee">The object being inspected.</param>
        /// <param name="fieldOnInspectee">The C# field (by "field" I mean the type of member in a C# class that should generally be private) for the
        /// object field (here I'm talking about the GUI field in the inspector) to read and write.</param>
        /// <param name="allowSceneObjects"></param>
        /// <returns>The value obtained by the object field.</returns>
        public static T ObjectField<T>(string caption, object inspectee, FieldInfo fieldOnInspectee, bool allowSceneObjects) where T : Object
        {
            T value = (T)EditorGUILayout.ObjectField(caption, (T)fieldOnInspectee.GetValue(inspectee), typeof(T), allowSceneObjects);
            fieldOnInspectee.SetValue(inspectee, value);
            return value;
        }

        /// <summary>
        /// Creates an object field in an inspector for a specific field in the object being inspected.
        /// </summary>
        /// <typeparam name="T">The type of object for the object field to accept.</typeparam>
        /// <param name="caption">The caption of the object field.</param>
        /// <param name="inspectee">The object being inspected.</param>
        /// <param name="nameOfFieldOnInspectee">The name of the C# field (by "field" I mean the type of member in a C# class that should generally be private) for the
        /// object field (here I'm talking about the GUI field in the inspector) to read and write.</param>
        /// <param name="allowSceneObjects"></param>
        /// <returns>The value obtained by the object field.</returns>
        public static T ObjectField<T>(string caption, object inspectee, string nameOfFieldOnInspectee, bool allowSceneObjects) where T : Object
        {
            FieldInfo field = inspectee.GetType().GetField(nameOfFieldOnInspectee, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            return ObjectField<T>(caption, inspectee, field, allowSceneObjects);
        }
    }
}
#endif
