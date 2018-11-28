using System;
using System.Collections.Generic;

namespace SupportXFLite.SyncData
{
    public interface ISyncStateObject
    {
        void IF_GetStateOfProperty();
        void IF_SaveStateOfProperty();
        Dictionary<string, object> IF_GetSessionStorage();
    }
}