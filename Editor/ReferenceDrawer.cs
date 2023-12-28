using System;
using UnityEditor;
using UnityEngine;

namespace MbsCore.AutoReference.Editor
{
    internal abstract class ReferenceDrawer
    {
        public const string InvalidPropertyMessage = "Unsupported property type! Use this attribute with Unity components!";
        
        protected Type ComponentType { get; }
        protected AutoReferenceAttribute Attribute { get; }

        public ReferenceDrawer(Type componentType, AutoReferenceAttribute attribute)
        {
            ComponentType = componentType;
            Attribute = attribute;
        }

        public abstract void OnGUI(Rect position, SerializedProperty property);
    }
}