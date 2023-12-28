using System;
using System.Reflection;
using UnityEditor;

namespace MbsCore.AutoReference.Editor
{
    internal abstract class ReferenceInstaller
    {
        protected FieldInfo FieldInfo { get; }
        
        public ReferenceInstaller(FieldInfo fieldInfo)
        {
            FieldInfo = fieldInfo;
        }

        public abstract void Install(SerializedProperty property, Type referenceType);
    }
}