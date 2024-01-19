using System;
using System.Reflection;
using UnityEditor;
using Object = UnityEngine.Object;

namespace MbsCore.AutoReference.Editor
{
    internal abstract class ReferenceInstaller<TTarget, TAttribute> : IReferenceInstaller
            where TTarget : Object where TAttribute : AutoReferenceAttribute
    {
        protected FieldInfo FieldInfo { get; }
        protected TTarget Target { get; }
        protected TAttribute Attribute { get; }
        
        public ReferenceInstaller(FieldInfo fieldInfo, TTarget target, TAttribute attribute)
        {
            FieldInfo = fieldInfo;
            Target = target;
            Attribute = attribute;
        }

        public abstract void Install(SerializedProperty property, Type referenceType);
    }
}