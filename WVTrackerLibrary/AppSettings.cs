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
            var keys = this["ApiKeys"] as List<ApiKeyModel>;
            if (keys == null)
            {
                keys = new List<ApiKeyModel>();
                this["ApiKeys"] = keys;
            }
            return keys;
        }
        set
        {
            this["ApiKeys"] = value;
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

    public void ClearSettings()
    {
        this.ApiKeys.Clear();
        this.Save();
        this.Reload();
    }
}