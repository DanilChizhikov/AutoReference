using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace MbsCore.AutoReference.Editor
{
    internal sealed class MonoReferenceInstaller : ReferenceInstaller<MonoBehaviour, ComponentAutoReferenceAttribute>
    {
        public MonoReferenceInstaller(FieldInfo fieldInfo, MonoBehaviour target, ComponentAutoReferenceAttribute attribute) :
                base(fieldInfo, target, attribute) { }

        public override void Install(SerializedProperty property, Type referenceType)
        {
            var components = new List<Component>(Attribute.SearchInChild
                                                         ? Target.GetComponentsInChildren(referenceType)
                                                         : Target.GetComponents(referenceType));

            if (FieldInfo.FieldType.IsArray)
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
                TryInstall(property, components[0]);
            }  
        }

        private void TryInstall(SerializedProperty property, Component component)
        {
            if (property.objectReferenceValue == null)
            {
                property.objectReferenceValue = component;
            }
        }
    }
}