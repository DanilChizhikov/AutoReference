using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MbsCore.AutoReference.Editor
{
    [CustomPropertyDrawer(typeof(AutoReferenceDrawer), true)]
    internal sealed class AutoReferenceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Object targetObject = property.serializedObject.targetObject;
            Type referenceType = fieldInfo.FieldType.IsArray ? fieldInfo.FieldType.GetElementType() : fieldInfo.FieldType;
            IReferenceInstaller installer = null;
            switch (targetObject)
            {
                case MonoBehaviour monoBehaviour:
                {
                    if (typeof(Component).IsAssignableFrom(referenceType) &&
                        attribute is ComponentAutoReferenceAttribute referenceAttribute)
                    {
                        installer = new MonoReferenceInstaller(fieldInfo, monoBehaviour, referenceAttribute);
                    }
                    else if(typeof(ScriptableObject).IsAssignableFrom(referenceType))
                    {
                        
                    }
                } break;

                case ScriptableObject scriptableObject:
                {
                    
                } break;
            }
            
            EditorGUI.PropertyField(position, property, label);
            if (GUILayout.Button("Search"))
            {
                installer?.Install(property, referenceType);
            }
        }
    }
}