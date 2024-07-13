using System.Configuration;
using WVTrackerLibrary;

public class AppSettings : ApplicationSettingsBase
{
    [UserScopedSetting]
    [SettingsSerializeAs(SettingsSerializeAs.Xml)]
    public List<ApiKeyModel> ApiKeys
    {
        get
        {
            var keys = this[nameof(ApiKeys)] as List<ApiKeyModel>;
            if (keys == null)
            {
                keys = [];
                this[nameof(ApiKeys)] = keys;
            }
            return keys;
        }
        set
        {
            this[nameof(ApiKeys)] = value;
        }
    }

    public new void Save()
    {
        base.Save();
    }

    public void ReloadSettings()
    {
        this.Reload();
    }
}