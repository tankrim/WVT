namespace WVTLib
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                _cts?.Dispose();
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            keysGroupBox = new GroupBox();
            keyRemoveButton = new Button();
            keyListBox = new ListBox();
            nameTextBox = new TextBox();
            nameLabel = new Label();
            keyAddButton = new Button();
            keyTextBox = new TextBox();
            keyLabel = new Label();
            dailyCheckBox = new CheckBox();
            weeklyCheckBox = new CheckBox();
            specialCheckBox = new CheckBox();
            accountsFlowLayoutPanel = new FlowLayoutPanel();
            updateButton = new Button();
            objectivesDataGridView = new DataGridView();
            titleLabel = new Label();
            statusStrip = new StatusStrip();
            toolStripStatusLabel = new ToolStripStatusLabel();
            hideCompletedCheckBox = new CheckBox();
            stopUpdateButton = new Button();
            keysGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)objectivesDataGridView).BeginInit();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // keysGroupBox
            // 
            keysGroupBox.Controls.Add(keyRemoveButton);
            keysGroupBox.Controls.Add(keyListBox);
            keysGroupBox.Controls.Add(nameTextBox);
            keysGroupBox.Controls.Add(nameLabel);
            keysGroupBox.Controls.Add(keyAddButton);
            keysGroupBox.Controls.Add(keyTextBox);
            keysGroupBox.Controls.Add(keyLabel);
            keysGroupBox.Font = new Font("Segoe UI", 16F);
            keysGroupBox.Location = new Point(38, 35);
            keysGroupBox.Margin = new Padding(5);
            keysGroupBox.Name = "keysGroupBox";
            keysGroupBox.Padding = new Padding(5);
            keysGroupBox.Size = new Size(475, 283);
            keysGroupBox.TabIndex = 40;
            keysGroupBox.TabStop = false;
            keysGroupBox.Text = "Key Managment";
            // 
            // keyRemoveButton
            // 
            keyRemoveButton.Enabled = false;
            keyRemoveButton.Font = new Font("Segoe UI", 14F);
            keyRemoveButton.Location = new Point(215, 226);
            keyRemoveButton.Margin = new Padding(5);
            keyRemoveButton.Name = "keyRemoveButton";
            keyRemoveButton.Size = new Size(197, 38);
            keyRemoveButton.TabIndex = 60;
            keyRemoveButton.Text = "Remove Selected Key";
            keyRemoveButton.UseVisualStyleBackColor = true;
            keyRemoveButton.Click += KeyRemoveButton_Click;
            // 
            // keyListBox
            // 
            keyListBox.Font = new Font("Segoe UI", 14F);
            keyListBox.FormattingEnabled = true;
            keyListBox.ItemHeight = 25;
            keyListBox.Location = new Point(9, 110);
            keyListBox.Margin = new Padding(5);
            keyListBox.Name = "keyListBox";
            keyListBox.Size = new Size(186, 154);
            keyListBox.TabIndex = 50;
            // 
            // nameTextBox
            // 
            nameTextBox.Font = new Font("Segoe UI", 14F);
            nameTextBox.Location = new Point(176, 62);
            nameTextBox.Margin = new Padding(5);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(155, 32);
            nameTextBox.TabIndex = 44;
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Font = new Font("Segoe UI", 14F);
            nameLabel.Location = new Point(176, 32);
            nameLabel.Margin = new Padding(5, 0, 5, 0);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(62, 25);
            nameLabel.TabIndex = 43;
            nameLabel.Text = "Name";
            // 
            // keyAddButton
            // 
            keyAddButton.Font = new Font("Segoe UI", 14F);
            keyAddButton.Location = new Point(347, 59);
            keyAddButton.Margin = new Padding(5);
            keyAddButton.Name = "keyAddButton";
            keyAddButton.Size = new Size(118, 38);
            keyAddButton.TabIndex = 45;
            keyAddButton.Text = "Add Key";
            keyAddButton.UseVisualStyleBackColor = true;
            keyAddButton.Click += KeyAddButton_Click;
            // 
            // keyTextBox
            // 
            keyTextBox.Font = new Font("Segoe UI", 14F);
            keyTextBox.Location = new Point(9, 62);
            keyTextBox.Margin = new Padding(5);
            keyTextBox.Name = "keyTextBox";
            keyTextBox.Size = new Size(155, 32);
            keyTextBox.TabIndex = 42;
            // 
            // keyLabel
            // 
            keyLabel.AutoSize = true;
            keyLabel.Font = new Font("Segoe UI", 14F);
            keyLabel.Location = new Point(9, 32);
            keyLabel.Margin = new Padding(5, 0, 5, 0);
            keyLabel.Name = "keyLabel";
            keyLabel.Size = new Size(118, 25);
            keyLabel.TabIndex = 41;
            keyLabel.Text = "New Api Key";
            // 
            // dailyCheckBox
            // 
            dailyCheckBox.AutoSize = true;
            dailyCheckBox.Checked = true;
            dailyCheckBox.CheckState = CheckState.Checked;
            dailyCheckBox.Enabled = false;
            dailyCheckBox.Location = new Point(566, 230);
            dailyCheckBox.Margin = new Padding(5);
            dailyCheckBox.Name = "dailyCheckBox";
            dailyCheckBox.Size = new Size(73, 29);
            dailyCheckBox.TabIndex = 20;
            dailyCheckBox.Text = "&Daily";
            dailyCheckBox.UseVisualStyleBackColor = true;
            dailyCheckBox.CheckedChanged += DailyCheckBox_CheckedChanged;
            // 
            // weeklyCheckBox
            // 
            weeklyCheckBox.AutoSize = true;
            weeklyCheckBox.Checked = true;
            weeklyCheckBox.CheckState = CheckState.Checked;
            weeklyCheckBox.Enabled = false;
            weeklyCheckBox.Location = new Point(660, 230);
            weeklyCheckBox.Margin = new Padding(5);
            weeklyCheckBox.Name = "weeklyCheckBox";
            weeklyCheckBox.Size = new Size(91, 29);
            weeklyCheckBox.TabIndex = 21;
            weeklyCheckBox.Text = "&Weekly";
            weeklyCheckBox.UseVisualStyleBackColor = true;
            weeklyCheckBox.CheckedChanged += WeeklyCheckBox_CheckedChanged;
            // 
            // specialCheckBox
            // 
            specialCheckBox.AutoSize = true;
            specialCheckBox.Checked = true;
            specialCheckBox.CheckState = CheckState.Checked;
            specialCheckBox.Enabled = false;
            specialCheckBox.Location = new Point(772, 230);
            specialCheckBox.Margin = new Padding(5);
            specialCheckBox.Name = "specialCheckBox";
            specialCheckBox.Size = new Size(91, 29);
            specialCheckBox.TabIndex = 22;
            specialCheckBox.Text = "&Special";
            specialCheckBox.UseVisualStyleBackColor = true;
            specialCheckBox.CheckedChanged += SpecialCheckBox_CheckedChanged;
            // 
            // accountsFlowLayoutPanel
            // 
            accountsFlowLayoutPanel.Location = new Point(566, 145);
            accountsFlowLayoutPanel.Margin = new Padding(5);
            accountsFlowLayoutPanel.Name = "accountsFlowLayoutPanel";
            accountsFlowLayoutPanel.Size = new Size(487, 75);
            accountsFlowLayoutPanel.TabIndex = 30;
            // 
            // updateButton
            // 
            updateButton.Enabled = false;
            updateButton.Font = new Font("Segoe UI", 16F);
            updateButton.Location = new Point(887, 279);
            updateButton.Margin = new Padding(5);
            updateButton.Name = "updateButton";
            updateButton.Size = new Size(118, 38);
            updateButton.TabIndex = 10;
            updateButton.Text = "&Update";
            updateButton.UseVisualStyleBackColor = true;
            updateButton.Click += UpdateButton_Click;
            // 
            // objectivesDataGridView
            // 
            objectivesDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            objectivesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            objectivesDataGridView.Location = new Point(38, 330);
            objectivesDataGridView.Margin = new Padding(5);
            objectivesDataGridView.Name = "objectivesDataGridView";
            objectivesDataGridView.RowHeadersVisible = false;
            objectivesDataGridView.Size = new Size(1025, 569);
            objectivesDataGridView.TabIndex = 2;
            objectivesDataGridView.CellDoubleClick += ObjectivesDataGridView_CellDoubleClick;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI Light", 28F);
            titleLabel.Location = new Point(627, 47);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(364, 51);
            titleLabel.TabIndex = 1;
            titleLabel.Text = "Wizard's Vault Tracker";
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel });
            statusStrip.Location = new Point(0, 933);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1107, 26);
            statusStrip.TabIndex = 8;
            statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            toolStripStatusLabel.Font = new Font("Segoe UI", 12F);
            toolStripStatusLabel.Name = "toolStripStatusLabel";
            toolStripStatusLabel.Size = new Size(53, 21);
            toolStripStatusLabel.Text = "Ready";
            // 
            // hideCompletedCheckBox
            // 
            hideCompletedCheckBox.AutoSize = true;
            hideCompletedCheckBox.Enabled = false;
            hideCompletedCheckBox.Location = new Point(884, 230);
            hideCompletedCheckBox.Margin = new Padding(5);
            hideCompletedCheckBox.Name = "hideCompletedCheckBox";
            hideCompletedCheckBox.Size = new Size(167, 29);
            hideCompletedCheckBox.TabIndex = 23;
            hideCompletedCheckBox.Text = "Hide Co&mpleted";
            hideCompletedCheckBox.UseVisualStyleBackColor = true;
            hideCompletedCheckBox.CheckedChanged += HideCompletedCheckBox_CheckedChanged;
            // 
            // stopUpdateButton
            // 
            stopUpdateButton.BackColor = SystemColors.Control;
            stopUpdateButton.Enabled = false;
            stopUpdateButton.FlatAppearance.BorderSize = 0;
            stopUpdateButton.FlatStyle = FlatStyle.Flat;
            stopUpdateButton.Image = (Image)resources.GetObject("stopUpdateButton.Image");
            stopUpdateButton.Location = new Point(1006, 274);
            stopUpdateButton.Name = "stopUpdateButton";
            stopUpdateButton.Size = new Size(75, 48);
            stopUpdateButton.TabIndex = 11;
            stopUpdateButton.UseVisualStyleBackColor = false;
            stopUpdateButton.Click += stopUpdateButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1107, 959);
            Controls.Add(stopUpdateButton);
            Controls.Add(hideCompletedCheckBox);
            Controls.Add(statusStrip);
            Controls.Add(titleLabel);
            Controls.Add(objectivesDataGridView);
            Controls.Add(updateButton);
            Controls.Add(accountsFlowLayoutPanel);
            Controls.Add(specialCheckBox);
            Controls.Add(weeklyCheckBox);
            Controls.Add(dailyCheckBox);
            Controls.Add(keysGroupBox);
            Font = new Font("Segoe UI", 14F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5);
            Name = "MainForm";
            Text = "WVT";
            FormClosing += MainForm_FormClosing;
            keysGroupBox.ResumeLayout(false);
            keysGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)objectivesDataGridView).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox keysGroupBox;
        private TextBox keyTextBox;
        private Label keyLabel;
        private Label nameLabel;
        private Button keyAddButton;
        private TextBox nameTextBox;
        private Button keyRemoveButton;
        private ListBox keyListBox;
        private CheckBox dailyCheckBox;
        private CheckBox weeklyCheckBox;
        private CheckBox specialCheckBox;
        private FlowLayoutPanel accountsFlowLayoutPanel;
        private Button updateButton;
        private DataGridView objectivesDataGridView;
        private Label titleLabel;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel toolStripStatusLabel;
        private CheckBox hideCompletedCheckBox;
        private Button stopUpdateButton;
    }
}