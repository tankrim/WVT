using System.Configuration;

namespace WVTrackerLibrary
{
    public class AppSettings : ApplicationSettingsBase
    {
        [UserScopedSetting]
        [SettingsSerializeAs(SettingsSerializeAs.Xml)]
        public List<ApiKeyModel> ApiKeys
        {
            get => this[nameof(ApiKeys)] as List<ApiKeyModel> ?? [];
            set => this[nameof(ApiKeys)] = value;
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