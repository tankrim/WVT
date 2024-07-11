using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WVTrackerLibrary;

namespace VWTracker
{
    public partial class MainForm : Form
    {
        private WVClient _wvClient;
        private AppSettings _settings;
        private List<(ObjectiveModel, string)> _allObjectives = new List<(ObjectiveModel, string)>();

        public MainForm()
        {
            InitializeComponent();
            InitializeWVClient();
            _settings = new AppSettings();
            //_settings.ClearSettings();  // Clear settings on startup, for debugging
            _settings.ReloadSettings();
            LoadApiKeys();
            SetupDataGridView();
            StyleDataGridView();
        }

        private void InitializeWVClient()
        {
            string baseUrl = "https://api.guildwars2.com/v2/account/wizardsvault/";
            List<string> endpoints = new List<string> { "daily", "weekly", "special" };
            _wvClient = new WVClient(baseUrl, endpoints);
        }

        private void LoadApiKeys()
        {
            Debug.WriteLine($"Loading API Keys. Count: {_settings.ApiKeys.Count}");
            foreach (var key in _settings.ApiKeys)
            {
                Debug.WriteLine($"Key: {key.Name}, Token: {key.Token}");
            }

            var selectedKey = KeyListBox.SelectedItem as ApiKeyModel;
            KeyListBox.DataSource = null;
            KeyListBox.DataSource = new BindingList<ApiKeyModel>(_settings.ApiKeys);
            KeyListBox.DisplayMember = "Name";
            KeyListBox.ValueMember = "Token";

            if (selectedKey != null)
            {
                var index = _settings.ApiKeys.FindIndex(k => k.Name == selectedKey.Name && k.Token == selectedKey.Token);
                if (index >= 0)
                    KeyListBox.SelectedIndex = index;
            }

            UpdateAccountFilters();
            Debug.WriteLine($"ListBox Items Count: {KeyListBox.Items.Count}");
        }

        private void UpdateAccountFilters()
        {
            AccountsFlowLayoutPanel.Controls.Clear();
            foreach (var apiKey in _settings.ApiKeys)
            {
                var checkBox = new CheckBox { Text = apiKey.Name, Checked = true, Tag = apiKey };
                checkBox.CheckedChanged += (s, e) => UpdateObjectivesGrid();
                AccountsFlowLayoutPanel.Controls.Add(checkBox);
            }
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
                DataPropertyName = "Claimed",
                HeaderText = "Claimed",
                Name = "Claimed",
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
            await FetchAndUpdateObjectives();
        }

        private async void KeyAddButton_Click(object sender, EventArgs e)
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
            }
        }

        private void UpdateObjectivesGrid()
        {
            Debug.WriteLine($"UpdateObjectivesGrid started. Total objectives: {_allObjectives.Count}");
            Debug.WriteLine($"Checkbox states: Daily: {DailyCheckBox.Checked}, Weekly: {WeeklyCheckBox.Checked}, Special: {SpecialCheckBox.Checked}");

            // Log the first few objectives
            for (int i = 0; i < Math.Min(5, _allObjectives.Count); i++)
            {
                var (objective, endpoint) = _allObjectives[i];
                Debug.WriteLine($"Objective {i}: Account: {objective.Account}, Endpoint: {endpoint}, Track: {objective.Track}, Title: {objective.Title}, Claimed: {objective.Claimed}");
            }

            var filteredObjectives = _allObjectives
                .Where(o => (DailyCheckBox.Checked && o.Item2 == "daily") ||
                    (WeeklyCheckBox.Checked && o.Item2 == "weekly") ||
                    (SpecialCheckBox.Checked && o.Item2 == "special"))
                .Where(o => AccountsFlowLayoutPanel.Controls.OfType<CheckBox>()
                    .Any(chk => chk.Checked && ((ApiKeyModel)chk.Tag).Name == o.Item1.Account))
                .GroupBy(o => new { o.Item2, o.Item1.Track, o.Item1.Title })
                .Select(g => new
                {
                    Account = g.First().Item1.Account,
                    Endpoint = g.Key.Item2,
                    Track = g.Key.Track,
                    Title = g.Key.Title,
                    Claimed = g.First().Item1.Claimed,
                    Others = string.Join(", ", g.Skip(1).Select(o => o.Item1.Account))
                })
                .ToList();

            Debug.WriteLine($"Filtered objectives count: {filteredObjectives.Count}");

            ObjectivesDataGridView.DataSource = null;  // Clear the existing data source
            ObjectivesDataGridView.DataSource = filteredObjectives;

            Debug.WriteLine($"DataGridView Rows Count: {ObjectivesDataGridView.Rows.Count}");
            Debug.WriteLine($"DataGridView Columns Count: {ObjectivesDataGridView.Columns.Count}");

            // Log the first few rows for debugging
            for (int i = 0; i < Math.Min(5, ObjectivesDataGridView.Rows.Count); i++)
            {
                var row = ObjectivesDataGridView.Rows[i];
                Debug.WriteLine($"Row {i}: {string.Join(", ", row.Cells.Cast<DataGridViewCell>().Select(c => c.Value))}");
            }

            Debug.WriteLine("UpdateObjectivesGrid completed");
        }

        private async Task FetchAndUpdateObjectives()
        {
            try
            {
                Debug.WriteLine("FetchAndUpdateObjectives started");
                _allObjectives.Clear();

                var selectedEndpoints = new List<string>();
                if (DailyCheckBox.Checked) selectedEndpoints.Add("daily");
                if (WeeklyCheckBox.Checked) selectedEndpoints.Add("weekly");
                if (SpecialCheckBox.Checked) selectedEndpoints.Add("special");

                Debug.WriteLine($"Selected endpoints: {string.Join(", ", selectedEndpoints)}");

                foreach (var apiKey in _settings.ApiKeys)
                {
                    foreach (var endpoint in selectedEndpoints)
                    {
                        try
                        {
                            Debug.WriteLine($"Fetching {endpoint} objectives for key: {apiKey.Name}");
                            var objectives = await _wvClient.GetObjectivesAsync(apiKey, endpoint);
                            _allObjectives.AddRange(objectives.Select(o => (o, endpoint)));
                            Debug.WriteLine($"Fetched {objectives.Count} {endpoint} objectives for key: {apiKey.Name}");
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Error fetching {endpoint} objectives for API key '{apiKey.Name}': {ex.Message}");
                            MessageBox.Show($"Error fetching {endpoint} objectives for API key '{apiKey.Name}': {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                Debug.WriteLine($"Total objectives fetched: {_allObjectives.Count}");
                UpdateObjectivesGrid();
                Debug.WriteLine("FetchAndUpdateObjectives completed");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in FetchAndUpdateObjectives: {ex.Message}");
                MessageBox.Show($"Error fetching objectives: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LogDataGridViewState()
        {
            Debug.WriteLine($"ObjectivesDataGridView State:");
            Debug.WriteLine($"  Visible: {ObjectivesDataGridView.Visible}");
            Debug.WriteLine($"  Enabled: {ObjectivesDataGridView.Enabled}");
            Debug.WriteLine($"  Rows Count: {ObjectivesDataGridView.Rows.Count}");
            Debug.WriteLine($"  Columns Count: {ObjectivesDataGridView.Columns.Count}");
            Debug.WriteLine($"  DataSource: {ObjectivesDataGridView.DataSource?.GetType().Name ?? "null"}");
            Debug.WriteLine($"  Size: {ObjectivesDataGridView.Size}");
            Debug.WriteLine($"  Location: {ObjectivesDataGridView.Location}");
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
    }
}