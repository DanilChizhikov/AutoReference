using System;
using UnityEngine;

namespace MbsCore.AutoReference
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class AutoReferenceAttribute : PropertyAttribute
    {
        public bool IncludeChild { get; }
        
        public AutoReferenceAttribute(bool includeChild = false)
        {
            IncludeChild = includeChild;
        }
    }
}
