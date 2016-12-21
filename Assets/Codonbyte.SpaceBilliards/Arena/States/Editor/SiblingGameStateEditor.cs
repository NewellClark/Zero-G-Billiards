using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEditor;
using System.Linq;
using System.Reflection;

namespace Codonbyte.SpaceBilliards.Arena.States
{
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    [CustomPropertyDrawer(typeof(SiblingGameStateAttribute))]
    public class SiblingGameStatePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect newPosition = EditorGUI.PrefixLabel(position, label);
            FieldInfo field = GetFieldInfo(property);
            if (field.FieldType != typeof(GameState))
            {
                EditorGUI.HelpBox(newPosition, 
                    "SiblingGameStateAttribute can only be applied to fields of type GameState.", 
                    MessageType.Error);
                return;
            }

            StateMachine sm = GetStateMachine(property);

            if (sm == null)
            {
                EditorGUI.HelpBox(newPosition, "StateMachine component required.", MessageType.Error);
                return;
            }

            ShowPopup(newPosition, property, sm);
        }

        private FieldInfo GetFieldInfo(SerializedProperty property)
        {
            return property.serializedObject.targetObject.GetType().GetField(property.name, BindingFlags.Instance | BindingFlags.NonPublic);
        }
        private GameState GetValue(SerializedProperty property)
        {
            FieldInfo field = GetFieldInfo(property);
            return (GameState)field.GetValue(property.serializedObject.targetObject);
        }

        private void SetValue(SerializedProperty property, GameState value)
        {
            FieldInfo field = GetFieldInfo(property);
            field.SetValue(property.serializedObject.targetObject, value);
        }

        private StateMachine GetStateMachine(SerializedProperty property)
        {
            return (property.serializedObject.targetObject as Component).gameObject.GetComponent<StateMachine>();
        }

        private void ShowPopup(Rect position, SerializedProperty property, StateMachine sm)
        {
            var stateList = sm.GameStates.ToList();
            var nameList = (from state in stateList select state.GetType().Name).ToList();

            stateList.Insert(0, null);
            nameList.Insert(0, "<none>");

            GameState value = GetValue(property);
            int index = stateList.Contains(value) ? stateList.IndexOf(value) : 0;

            int newIndex = EditorGUI.Popup(position, index, nameList.ToArray());
            SetValue(property, stateList[newIndex]);
        }

        /*
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            StateMachine sm = GetStateMachine(property);
            if (sm == null)
            {
                EditorGUILayout.HelpBox("No StateMachine component on current game object.", MessageType.Error);
                return;
            }

            DrawStatePopup(position, property, sm);
        }

        private StateMachine GetStateMachine(SerializedProperty property)
        {
            Component comp = property.serializedObject.targetObject as Component;
            if (comp == null) return null;
            return comp.gameObject.GetComponent<StateMachine>();
        }

        private GameState GetCurrent(SerializedProperty property)
        {
            if (property.objectReferenceValue == null) return null;
            FieldInfo info = property.serializedObject.targetObject.GetType().GetField(property.name, BindingFlags.Instance | BindingFlags.NonPublic);
            return (GameState)info.GetValue(property.serializedObject.targetObject);
        }


        private void SetCurrent(SerializedProperty property, GameState value)
        {
            FieldInfo info = property.serializedObject.targetObject.GetType().GetField(property.name, BindingFlags.Instance | BindingFlags.NonPublic);
            info.SetValue(property.serializedObject.targetObject, value);
        }

        private void DrawStatePopup(Rect position, SerializedProperty property, StateMachine sm)
        {
            List<GameState> stateList = sm.GameStates.ToList();
            List<string> nameList = (from state in stateList select state.GetType().Name).ToList();
            GameState currentValue = GetCurrent(property);
            int currentIndex = stateList.Contains(currentValue) ? stateList.IndexOf(currentValue) : 0;
            int newIndex = EditorGUI.Popup(position, currentIndex, nameList.ToArray());
            if (newIndex >= stateList.Count) return;
            GameState newValue = stateList[newIndex];
            if (newValue == null) return;

            SetCurrent(property, newValue);
        }*/
        /*
        private void DrawStatePopup(StateMachine sm, SerializedProperty property)
        {
            var stateList = sm.GameStates.ToList();
            var stateNameList = (from state in stateList select state.GetType().Name).ToList();

            var current = GetCurrent(property);
            if (!stateList.Contains(current)) current = null;

            stateNameList.Insert(0, "<None>");
            stateList.Insert(0, null);
            int index = stateList.IndexOf(current);
            SetCurrent(
                property,
                stateList[EditorGUILayout.Popup(index, stateNameList.ToArray())]);
        }*/
    }
}
