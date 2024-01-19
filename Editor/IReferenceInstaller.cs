using System;
using UnityEditor;

namespace MbsCore.AutoReference.Editor
{
    internal interface IReferenceInstaller
    {
        void Install(SerializedProperty property, Type referenceType);
    }
}