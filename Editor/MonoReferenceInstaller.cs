using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace MbsCore.AutoReference.Editor
{
    internal sealed class MonoReferenceInstaller : ReferenceInstaller
    {
        private readonly MonoBehaviour _target;
        private readonly MonoAutoReferenceAttribute _attribute;
        
        public MonoReferenceInstaller(FieldInfo fieldInfo, MonoBehaviour target, MonoAutoReferenceAttribute attribute) : base(fieldInfo)
        {
            _target = target;
            _attribute = attribute;
        }

        public override void Install(SerializedProperty property, Type referenceType)
        {
            var components = new List<Component>(_attribute.IncludeChild
                                                         ? _target.GetComponentsInChildren(referenceType)
                                                         : _target.GetComponents(referenceType));

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
                property.objectReferenceValue = components[0];
            }  
        }
    }
}