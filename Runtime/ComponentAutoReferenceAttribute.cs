namespace MbsCore.AutoReference
{
    public sealed class ComponentAutoReferenceAttribute : AutoReferenceAttribute
    {
        public bool SearchInChild { get; }

        public ComponentAutoReferenceAttribute(bool searchInChild = false)
        {
            SearchInChild = searchInChild;
        }
    }
}