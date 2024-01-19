using System;
using UnityEngine;

namespace MbsCore.AutoReference
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public abstract class AutoReferenceAttribute : PropertyAttribute { }
}