using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MbsCore.AutoReference.Editor
{
    [CustomPropertyDrawer(typeof(ScriptableAutoReferenceAttribute))]
    internal sealed class ScriptableAutoReferenceDrawer : PropertyDrawer
    {
        private const string FilterTemplate = "t: {0}";
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.serializedObject.targetObject is ScriptableObject)
            {
                Type soType = fieldInfo.FieldType.IsArray
                                             ? fieldInfo.FieldType.GetElementType()
                                             : fieldInfo.FieldType;

                var scriptableObjects = new List<ScriptableObject>();
                string filter = string.Format(FilterTemplate, soType);
                string[] guids = AssetDatabase.FindAssets(filter);
                for (int i = 0; i < guids.Length; i++)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                    Object soObject = AssetDatabase.LoadAssetAtPath<Object>(path);
                    if (soObject is ScriptableObject scriptableObject &&
                        soType.IsAssignableFrom(scriptableObject.GetType()))
                    {
                        scriptableObjects.Add(scriptableObject);
                    }
                }

                if (fieldInfo.FieldType.IsArray)
                {
                    char[] pathChars = property.propertyPath.ToCharArray();
                    for (int i = 0; i < pathChars.Length; i++)
                    {
                        if (!int.TryParse(pathChars[i].ToString(), out int index))
                        {
                            continue;
                        }
                    
                        if (scriptableObjects.Count > index)
                        {
                            property.objectReferenceValue = scriptableObjects[index];
                        }
                    }
                }
                else if (scriptableObjects.Count > 0)
                {
                    property.objectReferenceValue = scriptableObjects[0];
                }   
            }
            
            EditorGUI.PropertyField(position, property, label);
        }
    }
}