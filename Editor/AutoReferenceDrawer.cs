using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MbsCore.AutoReference.Editor
{
    [CustomPropertyDrawer(typeof(AutoReferenceAttribute))]
    internal sealed class AutoReferenceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var referenceAttribute = attribute as AutoReferenceAttribute;
            if (property.serializedObject.targetObject is MonoBehaviour target)
            {
                Type componentType = fieldInfo.FieldType.IsArray
                                             ? fieldInfo.FieldType.GetElementType()
                                             : fieldInfo.FieldType;
                var components = new List<Component>(referenceAttribute.IncludeChild
                                                             ? target.GetComponentsInChildren(componentType)
                                                             : target.GetComponents(componentType));

                if (fieldInfo.FieldType.IsArray)
                {
                    char[] pathChars = property.propertyPath.ToCharArray();
                    for (int i = 0; i < pathChars.Length; i++)
                    {
                        if (!int.TryParse(pathChars[i].ToString(), out int index))
                        {
                            continue;
                        }
                    
                        if (components.Count > index)
                        {
                            property.objectReferenceValue = components[index];
                        }
                    }
                }
                else if (components.Count > 0)
                {
                    property.objectReferenceValue = components[0];
                }
            }
            
            EditorGUI.PropertyField(position, property, label);
        }
    }
}