using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        }

        private void InitializeWVClient()
        {
            string baseUrl = "https://api.guildwars2.com/v2/account/wizardsvault/";
            List<string> endpoints = ["daily", "weekly", "special"];
            _wvClient = new WVClient(baseUrl, endpoints);
        }

        private void LoadApiKeys()
        {
            Debug.WriteLine($"Loading API Keys. Count: {_settings.ApiKeys.Count}");
            foreach (var key in _settings.ApiKeys)
            {
                Debug.WriteLine($"Key: {key.Name}, Token: {key.Token}");
            }

            KeyListBox.DataSource = null;
            KeyListBox.DataSource = new BindingList<ApiKeyModel>(_settings.ApiKeys);
            KeyListBox.DisplayMember = "Name";
            KeyListBox.ValueMember = "Token";

            if (KeyListBox.SelectedItem is ApiKeyModel selectedKey)
            {
                var index = _settings.ApiKeys.FindIndex(k => k.Name == selectedKey.Name && k.Token == selectedKey.Token);
                if (index >= 0)
                    KeyListBox.SelectedIndex = index;
            }

            UpdateAccountFilters();
            UpdateControlsEnabledState();
            Debug.WriteLine($"ListBox Items Count: {KeyListBox.Items.Count}");
        }

        private void UpdateAccountFilters()
        {
            AccountsFlowLayoutPanel.Controls.Clear();
            foreach (var apiKey in _settings.ApiKeys)
            {
                var checkBox = new CheckBox { Text = apiKey.Name, Checked = true, Tag = apiKey };
                checkBox.CheckedChanged += AccountCheckBox_CheckedChanged;
                AccountsFlowLayoutPanel.Controls.Add(checkBox);
            }
        }

        private void AccountCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateObjectivesGrid();
        }

        private void SetupDataGridView()
        {
            Debug.WriteLine("SetupDataGridView started");

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

            Debug.WriteLine($"DataGridView Columns Count after setup: {ObjectivesDataGridView.Columns.Count}");
            Debug.WriteLine("SetupDataGridView completed");
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
            Debug.WriteLine($"UpdateObjectivesGrid started. Total objectives: {_allObjectives.Count}");
            Debug.WriteLine($"Checkbox states: Daily: {DailyCheckBox.Checked}, Weekly: {WeeklyCheckBox.Checked}, Special: {SpecialCheckBox.Checked}");

            if (AccountsFlowLayoutPanel.Controls.Count > 0)
            {
                var selectedAccounts = AccountsFlowLayoutPanel.Controls.OfType<CheckBox>()
                    .Where(chk => chk.Checked)
                    .Select(chk => ((ApiKeyModel)chk.Tag).Name)
                    .ToList();

                Debug.WriteLine($"Selected accounts: {string.Join(", ", selectedAccounts)}");

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

                Debug.WriteLine($"Filtered objectives count: {filteredObjectives.Count}");

                ObjectivesDataGridView.DataSource = null;
                ObjectivesDataGridView.DataSource = filteredObjectives;

            }
            Debug.WriteLine($"DataGridView Rows Count: {ObjectivesDataGridView.Rows.Count}");
            Debug.WriteLine($"DataGridView Columns Count: {ObjectivesDataGridView.Columns.Count}");

            Debug.WriteLine("UpdateObjectivesGrid completed");
        }

        private async Task FetchAndUpdateObjectives()
        {
            try
            {
                toolStripStatusLabel.Text = "Updating objectives...";
                Debug.WriteLine("FetchAndUpdateObjectives started");
                _allObjectives.Clear();

                string[] endpoints = ["daily", "weekly", "special"];

                int totalApiKeys = _settings.ApiKeys.Count;
                int currentApiKey = 0;

                foreach (var apiKey in _settings.ApiKeys)
                {
                    currentApiKey++;
                    foreach (var endpoint in endpoints)
                    {
                        try
                        {
                            toolStripStatusLabel.Text = $"Fetching {endpoint} objectives for {apiKey.Name} ({currentApiKey}/{totalApiKeys})...";
                            Application.DoEvents(); // Allows the UI to update

                            Debug.WriteLine($"Fetching {endpoint} objectives for key: {apiKey.Name}");
                            var objectives = await _wvClient.GetObjectivesAsync(apiKey, endpoint);
                            _allObjectives.AddRange(objectives.Select(o => (o, endpoint)));
                            Debug.WriteLine($"Fetched {objectives.Count} {endpoint} objectives for key: {apiKey.Name}");
                        }
                        // TODO - This is thrown 3 times (daily, weekly, special). Make the message box appear only once.
                        catch (UnauthorizedAccessException ex)
                        {
                            Debug.WriteLine($"Unauthorized error for API key '{apiKey.Name}': {ex.Message}");
                            MessageBox.Show($"The API key '{apiKey.Name}' appears to be invalid or unauthorized. Please check the key and try again.", "Invalid API Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Error fetching {endpoint} objectives for API key '{apiKey.Name}': {ex.Message}");
                            MessageBox.Show($"Error fetching {endpoint} objectives for API key '{apiKey.Name}': {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                Debug.WriteLine($"Total objectives fetched: {_allObjectives.Count}");
                toolStripStatusLabel.Text = "Updating grid...";
                Application.DoEvents(); // Allows the UI to update

                UpdateObjectivesGrid();

                toolStripStatusLabel.Text = "Update completed.";
                Debug.WriteLine("FetchAndUpdateObjectives completed");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in FetchAndUpdateObjectives: {ex.Message}");
                MessageBox.Show($"Error fetching objectives: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel.Text = "Update failed.";
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