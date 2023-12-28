using System;
using UnityEditor;
using UnityEngine;

namespace MbsCore.AutoReference.Editor
{
    [CustomPropertyDrawer(typeof(MonoAutoReferenceAttribute))]
    internal sealed class MonoAutoReferenceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.serializedObject.targetObject is MonoBehaviour target)
            {
                Type referenceType = fieldInfo.FieldType.IsArray
                                             ? fieldInfo.FieldType.GetElementType()
                                             : fieldInfo.FieldType;

                ReferenceInstaller installer = null;
                if (typeof(Component).IsAssignableFrom(referenceType))
                {
                    installer = new MonoReferenceInstaller(fieldInfo, target, attribute as MonoAutoReferenceAttribute);
                }
                else if(typeof(ScriptableObject).IsAssignableFrom(referenceType))
                {
                    installer = new ScriptableReferenceInstaller(fieldInfo);
                }
                
                installer?.Install(property, referenceType);
            }
            
            EditorGUI.PropertyField(position, property, label);
        }
    }
}