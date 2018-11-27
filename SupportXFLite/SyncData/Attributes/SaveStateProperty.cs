using System;
namespace SupportXFLite.SyncData.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class SaveStateProperty : Attribute
    {
        public bool AllowSync { set; get; }


        public SaveStateProperty()
        {
             
        }
    }
}