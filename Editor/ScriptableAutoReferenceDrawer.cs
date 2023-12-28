using System;
using UnityEditor;
using UnityEngine;

namespace MbsCore.AutoReference.Editor
{
    [CustomPropertyDrawer(typeof(ScriptableAutoReferenceAttribute))]
    internal sealed class ScriptableAutoReferenceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Type referenceType = fieldInfo.FieldType.IsArray
                                         ? fieldInfo.FieldType.GetElementType()
                                         : fieldInfo.FieldType;

            ReferenceInstaller installer = null;
            if(typeof(ScriptableObject).IsAssignableFrom(referenceType))
            {
                installer = new ScriptableReferenceInstaller(fieldInfo);
            }
                
            installer?.Install(property, referenceType);
            
            
            EditorGUI.PropertyField(position, property, label);
        }
    }
}