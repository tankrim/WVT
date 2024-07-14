using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml.Serialization;

namespace WVTrackerLibrary
{
    public class AppSettings : ApplicationSettingsBase
    {
        [UserScopedSetting]
        [SettingsSerializeAs(SettingsSerializeAs.Xml)]
        [XmlArray]
        public List<ApiKeyModel> ApiKeys
        {
            get
            {
                var value = this[nameof(ApiKeys)];
                if (value == null)
                {
                    value = new List<ApiKeyModel>();
                    this[nameof(ApiKeys)] = value;
                }
                return (List<ApiKeyModel>)value;
            }
            set => this[nameof(ApiKeys)] = value;
        }

        [UserScopedSetting]
        [SettingsSerializeAs(SettingsSerializeAs.Xml)]
        [XmlArray]
        public List<LocalObjectiveStatus> LocalObjectiveStatuses
        {
            get
            {
                var value = this[nameof(LocalObjectiveStatuses)];
                if (value == null)
                {
                    value = new List<LocalObjectiveStatus>();
                    this[nameof(LocalObjectiveStatuses)] = value;
                }
                return (List<LocalObjectiveStatus>)value;
            }
            set => this[nameof(LocalObjectiveStatuses)] = value;
        }

        public new void Save()
        {
            base.Save();
        }

        public void ReloadSettings()
        {
            Reload();
        }
    }
}