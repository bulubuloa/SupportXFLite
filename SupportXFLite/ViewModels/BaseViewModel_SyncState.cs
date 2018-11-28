using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using SupportXFLite.SyncData;
using SupportXFLite.SyncData.Attributes;
using Xamarin.Forms;

namespace SupportXFLite.ViewModels
{
    public abstract partial class BaseViewModel : ISyncStateObject
    {
        public virtual Task InitializeAsync(object navigationData)
        {
            RunActionOnNewThread(() =>
            {
                IF_GetStateOfProperty();
            });
            return Task.FromResult(false);
        }

        public virtual Task OnViewAppearingAsync(VisualElement view)
        {
            return Task.FromResult(true);
        }

        public virtual Task OnViewDisappearingAsync(VisualElement view)
        {
            RunActionOnNewThread(() =>
            {
                IF_SaveStateOfProperty();
            });
            return Task.FromResult(true);
        }

        private bool IsAllowSaveState()
        {
            Type type = GetType();
            object[] attributes = type.GetCustomAttributes(true);
            foreach (object attribute in attributes)
            {
                SaveStateObject saveStateObject = attribute as SaveStateObject;
                if (saveStateObject != null)
                {
                    return saveStateObject.AllowSync;
                }
            }
            return false;
        }

        private string GetKeyOfProperty(PropertyInfo propertyInfo)
        {
            return GetType().ToString() + "." + propertyInfo.Name;
        }

        public virtual void IF_GetStateOfProperty()
        {
            if (IsAllowSaveState())
            {
                var storage = IF_GetSessionStorage();
                if (storage == null)
                    return;

                PropertyInfo[] properties = GetType().GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    var atts = property.GetCustomAttributes(true);
                    foreach (object attribute in atts)
                    {
                        var saveStateProperty = attribute as SaveStateProperty;
                        if (saveStateProperty != null)
                        {
                            if (saveStateProperty.AllowSync)
                            {
                                var key = GetKeyOfProperty(property);

                                if (storage.ContainsKey(key))
                                {
                                    var data = storage[key];
                                    property.SetValue(this, data);
                                }
                            }
                        }
                    }
                }
            }
        }

        public virtual void IF_SaveStateOfProperty()
        {
            if (IsAllowSaveState())
            {
                var storage = IF_GetSessionStorage();
                if (storage == null)
                    return;

                PropertyInfo[] properties = GetType().GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    var atts = property.GetCustomAttributes(true);
                    foreach (object attribute in atts)
                    {
                        var saveStateProperty = attribute as SaveStateProperty;
                        if (saveStateProperty != null)
                        {
                            if (saveStateProperty.AllowSync)
                            {
                                var key = GetKeyOfProperty(property);

                                if (storage.ContainsKey(key))
                                {
                                    storage.Remove(key);
                                }
                                storage.Add(key, property.GetValue(this, null));
                            }
                        }
                    }
                }
            }
        }

        public virtual Dictionary<string, object> IF_GetSessionStorage()
        {
            return null;
        }
    }
}
