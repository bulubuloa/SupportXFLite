using System;
namespace SupportXFLite.SyncData.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class SaveStateObject : System.Attribute
    {
        public bool AllowSync { set; get; }

        public SaveStateObject(bool _AllowSync)
        {
            AllowSync = _AllowSync;
        }
    }
}