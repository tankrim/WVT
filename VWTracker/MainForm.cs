using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using WVTracker;
using WVTrackerLibrary;

namespace VWTracker
{
    public partial class MainForm : Form
    {
        private WVClient? _wvClient;
        private readonly AppSettings _settings;
        private readonly List<(ObjectiveModel, string)> _allObjectives = [];

        public MainForm()
        {
            InitializeComponent();
            InitializeWVClient();
            _settings = new AppSettings();
            _settings.ReloadSettings();
            LoadApiKeys();
            SetupDataGridView();
            StyleDataGridView();
            UpdateControlsEnabledState();
            StartBackgroundRefresh();
        }

        private void InitializeWVClient()
        {
            string baseUrl = "https://api.guildwars2.com/v2/account/wizardsvault/";
            List<string> endpoints = ["daily", "weekly", "special"];
            _wvClient = new WVClient(baseUrl, endpoints);
        }

        private void LoadApiKeys()
        {
            KeyListBox.DataSource = null;
            KeyListBox.DataSource = new BindingList<ApiKeyModel>(_settings.ApiKeys);
            KeyListBox.DisplayMember = "DisplayName";
            KeyListBox.ValueMember = "Token";


            if (KeyListBox.SelectedItem is ApiKeyModel selectedKey)
            {
                var index = _settings.ApiKeys.FindIndex(k => k.Name == selectedKey.Name && k.Token == selectedKey.Token);
                if (index >= 0)
                    KeyListBox.SelectedIndex = index;
            }

            UpdateAccountFilters();
            UpdateControlsEnabledState();
        }

        private void UpdateAccountFilters()
        {
            AccountsFlowLayoutPanel.Controls.Clear();
            foreach (var apiKey in _settings.ApiKeys.Where(k => k.IsValid))
            {
                var checkBox = new CheckBox { Text = apiKey.Name, Checked = true, Tag = apiKey };
                checkBox.CheckedChanged += AccountCheckBox_CheckedChanged;
                AccountsFlowLayoutPanel.Controls.Add(checkBox);
            }
        }

        private void AccountCheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            UpdateObjectivesGrid();
        }

        private void SetupDataGridView()
        {
            ObjectivesDataGridView.AutoGenerateColumns = false;
            ObjectivesDataGridView.Columns.Clear();

            ObjectivesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Account",
                HeaderText = "Account",
                Name = "Account",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            ObjectivesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Endpoint",
                HeaderText = "Endpoint",
                Name = "Endpoint",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            ObjectivesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Track",
                HeaderText = "Track",
                Name = "Track",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            ObjectivesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Title",
                HeaderText = "Title",
                Name = "Title",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            ObjectivesDataGridView.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "Completed",
                HeaderText = "Completed",
                Name = "Completed",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            ObjectivesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Others",
                HeaderText = "Others",
                Name = "Others",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            ObjectivesDataGridView.AllowUserToAddRows = false;
            ObjectivesDataGridView.AllowUserToDeleteRows = false;
            ObjectivesDataGridView.ReadOnly = true;
            ObjectivesDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ObjectivesDataGridView.MultiSelect = false;
            ObjectivesDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void StyleDataGridView()
        {
            ObjectivesDataGridView.BorderStyle = BorderStyle.None;
            ObjectivesDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            ObjectivesDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            ObjectivesDataGridView.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            ObjectivesDataGridView.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            ObjectivesDataGridView.BackgroundColor = Color.White;

            ObjectivesDataGridView.EnableHeadersVisualStyles = false;
            ObjectivesDataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            ObjectivesDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            ObjectivesDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private async void UpdateButton_Click(object sender, EventArgs e)
        {
            UpdateButton.Enabled = false;
            toolStripStatusLabel.Text = "Starting update...";
            try
            {
                await FetchAndUpdateObjectives();
            }
            finally
            {
                UpdateButton.Enabled = true;
            }
        }

        private void KeyAddButton_Click(object sender, EventArgs e)
        {
            try
            {
                string name = NameTextBox.Text.Trim();
                string key = KeyTextBox.Text.Trim();

                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Name can not be empty or blank.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(key))
                {
                    MessageBox.Show("Key can not be empty or blank.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_settings.ApiKeys.Any(k => k.Name.Equals(name, StringComparison.OrdinalIgnoreCase) || k.Token == key))
                {
                    MessageBox.Show("An API key with this name or token already exists.", "Duplicate Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var newKey = new ApiKeyModel(name, key);

                _settings.ApiKeys.Add(newKey);

                _settings.Save();

                LoadApiKeys();

                NameTextBox.Clear();
                KeyTextBox.Clear();
                UpdateControlsEnabledState();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding API key: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void KeyRemoveButton_Click(object sender, EventArgs e)
        {
            if (KeyListBox.SelectedItem is ApiKeyModel selectedKey)
            {
                _settings.ApiKeys.Remove(selectedKey);
                _settings.Save();
                LoadApiKeys();
                UpdateObjectivesGrid();
                UpdateControlsEnabledState();
            }
        }

        private void UpdateObjectivesGrid()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(UpdateObjectivesGrid));
                return;
            }

            if (AccountsFlowLayoutPanel.Controls.Count > 0)
            {
                var selectedAccounts = AccountsFlowLayoutPanel.Controls.OfType<CheckBox>()
                    .Where(chk => chk.Checked && chk.Tag is ApiKeyModel model && model.Name != null)
                    .Select(chk => ((ApiKeyModel)chk.Tag!).Name)
                    .ToList();

                // First, prepare all the data without filtering
                var allObjectivesGrouped = _allObjectives
                    .GroupBy(o => new { o.Item2, o.Item1.Track, o.Item1.Title })
                    .Select(g => new
                    {
                        Endpoint = g.Key.Item2,
                        g.Key.Track,
                        g.Key.Title,
                        Accounts = g.Select(o => o.Item1.Account).Distinct().ToList(),
                        Completed = g.Any(o => o.Item1.Completed)
                    })
                    .ToList();

                // Then, filter and prepare for display
                var filteredObjectives = allObjectivesGrouped
                    .Where(o => (DailyCheckBox.Checked && o.Endpoint == "daily") ||
                                (WeeklyCheckBox.Checked && o.Endpoint == "weekly") ||
                                (SpecialCheckBox.Checked && o.Endpoint == "special"))
                    .Where(o => o.Accounts.Any(a => selectedAccounts.Contains(a)))
                    .Where(o => !hideCompletedCheckBox.Checked || !o.Completed)
                    .Select(o => new
                    {
                        Account = o.Accounts.First(a => selectedAccounts.Contains(a)),
                        o.Endpoint,
                        o.Track,
                        o.Title,
                        o.Completed,
                        Others = string.Join(", ", o.Accounts.Where(a => a != o.Accounts.First(sa => selectedAccounts.Contains(sa))))
                    })
                    .ToList();

                ObjectivesDataGridView.DataSource = null;
                ObjectivesDataGridView.DataSource = filteredObjectives;

            }
        }

        private async Task FetchAndUpdateObjectives()
        {
            if (_wvClient == null)
            {
                MessageBox.Show("WVClient is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                await this.InvokeAsync(() => toolStripStatusLabel.Text = "Updating objectives...");
                _allObjectives.Clear();
                var fetchTasks = _settings.ApiKeys
                    .Where(k => k.IsValid)
                    .SelectMany(apiKey => new[] { "daily", "weekly", "special" }
                        .Select(endpoint => FetchObjectivesForEndpoint(apiKey, endpoint)))
                    .ToList();

                var results = await Task.WhenAll(fetchTasks);

                var invalidatedKeys = new HashSet<string>();
                foreach (var result in results)
                {
                    if (result.Exception != null)
                    {
                        if (result.Exception is UnauthorizedAccessException)
                        {
                            if (!invalidatedKeys.Contains(result.ApiKey.Name))
                            {
                                result.ApiKey.IsValid = false;
                                invalidatedKeys.Add(result.ApiKey.Name);
                            }
                        }
                    }
                    else if (result.Objectives != null)
                    {
                        _allObjectives.AddRange(result.Objectives.Select(o => (o, result.Endpoint)));
                    }
                }

                await this.InvokeAsync(() =>
                {
                    if (invalidatedKeys.Count > 0)
                    {
                        _settings.Save();
                        LoadApiKeys();
                        string invalidKeyNames = string.Join(", ", invalidatedKeys);
                        MessageBox.Show($"The following API key(s) are invalid or unauthorized and have been marked as invalid: {invalidKeyNames}",
                                        "Invalid API Key(s)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    UpdateObjectivesGrid();
                    toolStripStatusLabel.Text = "Update completed.";
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching objectives: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel.Text = "Update failed.";
            }
        }

        private async Task<(ApiKeyModel ApiKey, string Endpoint, List<ObjectiveModel>? Objectives, Exception? Exception)> FetchObjectivesForEndpoint(ApiKeyModel apiKey, string endpoint)
        {
            if (_wvClient == null)
            {
                return (apiKey, endpoint, null, new InvalidOperationException("WVClient is not initialized."));
            }

            try
            {
                var objectives = await _wvClient.GetObjectivesAsync(apiKey, endpoint);
                return (apiKey, endpoint, objectives, null);
            }
            catch (UnauthorizedAccessException ex)
            {
                return (apiKey, endpoint, null, new UnauthorizedAccessException($"API key '{apiKey.Name}' is unauthorized for endpoint '{endpoint}'.", ex));
            }
            catch (Exception ex)
            {
                return (apiKey, endpoint, null, ex);
            }
        }

        // Background refresh
        private readonly System.Threading.Timer? _refreshTimer;

        private void StartBackgroundRefresh()
        {
            Task.Run(async () =>
            {
                while (!this.IsDisposed)
                {
                    await Task.Delay(TimeSpan.FromMinutes(15));
                    if (!this.IsDisposed)
                    {
                        await RefreshInBackground();
                    }
                }
            });
        }

        private async Task RefreshInBackground()
        {
            try
            {
                await FetchAndUpdateObjectives();

                // Marshal all UI updates back to the UI thread
                await this.InvokeAsync(() =>
                {
                    UpdateObjectivesGrid();
                    toolStripStatusLabel.Text = "Background refresh completed.";
                });
            }
            catch (Exception ex)
            {
                // Marshal error handling back to the UI thread
                await this.InvokeAsync(() =>
                {
                    Debug.WriteLine($"Error in background refresh: {ex.Message}");
                    toolStripStatusLabel.Text = "Background refresh failed.";
                    // Optionally, show a non-modal message to inform the user
                    // MessageBox.Show($"Background refresh failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
            }
        }

        private void UpdateControlsEnabledState()
        {
            bool hasKeys = KeyListBox.Items.Count > 0;

            KeyRemoveButton.Enabled = hasKeys;
            DailyCheckBox.Enabled = hasKeys;
            WeeklyCheckBox.Enabled = hasKeys;
            SpecialCheckBox.Enabled = hasKeys;
            hideCompletedCheckBox.Enabled = hasKeys;
            UpdateButton.Enabled = hasKeys;
        }

        private void DailyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateObjectivesGrid();
        }

        private void WeeklyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateObjectivesGrid();
        }

        private void SpecialCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateObjectivesGrid();
        }

        private void HideCompletedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateObjectivesGrid();
        }
    }
}