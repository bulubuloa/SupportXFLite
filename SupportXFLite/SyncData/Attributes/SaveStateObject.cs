using System;
namespace SupportXFLite.SyncData.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class SaveStateObject : System.Attribute
    {
        public bool AllowSync { set; get; }

        public bool SaveAllProperty { set; get; }

        public SaveStateObject()
        {
            AllowSync = false;
            SaveAllProperty = false;
        }
    }
}