using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MbsCore.AutoReference.Editor
{
    internal sealed class ScriptableReferenceInstaller : ReferenceInstaller
    {
        private const string FilterTemplate = "t: {0}";
        
        public ScriptableReferenceInstaller(FieldInfo fieldInfo) : base(fieldInfo) { }
        
        public override void Install(SerializedProperty property, Type referenceType)
        {
            var scriptableObjects = new List<ScriptableObject>();
            string filter = string.Format(FilterTemplate, referenceType);
            string[] guids = AssetDatabase.FindAssets(filter);
            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                Object soObject = AssetDatabase.LoadAssetAtPath<Object>(path);
                if (soObject is ScriptableObject scriptableObject &&
                    referenceType.IsAssignableFrom(scriptableObject.GetType()))
                {
                    scriptableObjects.Add(scriptableObject);
                }
            }

            if (FieldInfo.FieldType.IsArray)
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
    }
}