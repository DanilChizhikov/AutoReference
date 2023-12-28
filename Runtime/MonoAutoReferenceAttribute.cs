using System;
using UnityEngine;

namespace MbsCore.AutoReference
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class MonoAutoReferenceAttribute : PropertyAttribute
    {
        public bool IncludeChild { get; }
        
        public MonoAutoReferenceAttribute(bool includeChild = false)
        {
            IncludeChild = includeChild;
        }
    }
}
