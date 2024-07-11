namespace VWTracker
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
            KeysGroupBox = new GroupBox();
            KeyRemoveButton = new Button();
            KeyListBox = new ListBox();
            NameTextBox = new TextBox();
            NameLabel = new Label();
            KeyAddButton = new Button();
            KeyTextBox = new TextBox();
            KeyLabel = new Label();
            DailyCheckBox = new CheckBox();
            WeeklyCheckBox = new CheckBox();
            SpecialCheckBox = new CheckBox();
            AccountsFlowLayoutPanel = new FlowLayoutPanel();
            UpdateButton = new Button();
            ObjectivesDataGridView = new DataGridView();
            KeysGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ObjectivesDataGridView).BeginInit();
            SuspendLayout();
            // 
            // KeysGroupBox
            // 
            KeysGroupBox.Controls.Add(KeyRemoveButton);
            KeysGroupBox.Controls.Add(KeyListBox);
            KeysGroupBox.Controls.Add(NameTextBox);
            KeysGroupBox.Controls.Add(NameLabel);
            KeysGroupBox.Controls.Add(KeyAddButton);
            KeysGroupBox.Controls.Add(KeyTextBox);
            KeysGroupBox.Controls.Add(KeyLabel);
            KeysGroupBox.Font = new Font("Segoe UI", 16F);
            KeysGroupBox.Location = new Point(38, 35);
            KeysGroupBox.Margin = new Padding(5);
            KeysGroupBox.Name = "KeysGroupBox";
            KeysGroupBox.Padding = new Padding(5);
            KeysGroupBox.Size = new Size(475, 283);
            KeysGroupBox.TabIndex = 0;
            KeysGroupBox.TabStop = false;
            KeysGroupBox.Text = "Key Managment";
            // 
            // KeyRemoveButton
            // 
            KeyRemoveButton.Font = new Font("Segoe UI", 14F);
            KeyRemoveButton.Location = new Point(215, 226);
            KeyRemoveButton.Margin = new Padding(5);
            KeyRemoveButton.Name = "KeyRemoveButton";
            KeyRemoveButton.Size = new Size(197, 38);
            KeyRemoveButton.TabIndex = 6;
            KeyRemoveButton.Text = "Remove Selected Key";
            KeyRemoveButton.UseVisualStyleBackColor = true;
            KeyRemoveButton.Click += KeyRemoveButton_Click;
            // 
            // KeyListBox
            // 
            KeyListBox.Font = new Font("Segoe UI", 14F);
            KeyListBox.FormattingEnabled = true;
            KeyListBox.ItemHeight = 25;
            KeyListBox.Location = new Point(9, 110);
            KeyListBox.Margin = new Padding(5);
            KeyListBox.Name = "KeyListBox";
            KeyListBox.Size = new Size(186, 154);
            KeyListBox.TabIndex = 5;
            // 
            // NameTextBox
            // 
            NameTextBox.Font = new Font("Segoe UI", 14F);
            NameTextBox.Location = new Point(176, 62);
            NameTextBox.Margin = new Padding(5);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.Size = new Size(155, 32);
            NameTextBox.TabIndex = 4;
            // 
            // NameLabel
            // 
            NameLabel.AutoSize = true;
            NameLabel.Font = new Font("Segoe UI", 14F);
            NameLabel.Location = new Point(176, 32);
            NameLabel.Margin = new Padding(5, 0, 5, 0);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new Size(62, 25);
            NameLabel.TabIndex = 3;
            NameLabel.Text = "Name";
            // 
            // KeyAddButton
            // 
            KeyAddButton.Font = new Font("Segoe UI", 14F);
            KeyAddButton.Location = new Point(347, 59);
            KeyAddButton.Margin = new Padding(5);
            KeyAddButton.Name = "KeyAddButton";
            KeyAddButton.Size = new Size(118, 38);
            KeyAddButton.TabIndex = 2;
            KeyAddButton.Text = "Add Key";
            KeyAddButton.UseVisualStyleBackColor = true;
            KeyAddButton.Click += KeyAddButton_Click;
            // 
            // KeyTextBox
            // 
            KeyTextBox.Font = new Font("Segoe UI", 14F);
            KeyTextBox.Location = new Point(9, 62);
            KeyTextBox.Margin = new Padding(5);
            KeyTextBox.Name = "KeyTextBox";
            KeyTextBox.Size = new Size(155, 32);
            KeyTextBox.TabIndex = 1;
            // 
            // KeyLabel
            // 
            KeyLabel.AutoSize = true;
            KeyLabel.Font = new Font("Segoe UI", 14F);
            KeyLabel.Location = new Point(9, 32);
            KeyLabel.Margin = new Padding(5, 0, 5, 0);
            KeyLabel.Name = "KeyLabel";
            KeyLabel.Size = new Size(118, 25);
            KeyLabel.TabIndex = 0;
            KeyLabel.Text = "New Api Key";
            // 
            // DailyCheckBox
            // 
            DailyCheckBox.AutoSize = true;
            DailyCheckBox.Checked = true;
            DailyCheckBox.CheckState = CheckState.Checked;
            DailyCheckBox.Location = new Point(566, 287);
            DailyCheckBox.Margin = new Padding(5);
            DailyCheckBox.Name = "DailyCheckBox";
            DailyCheckBox.Size = new Size(73, 29);
            DailyCheckBox.TabIndex = 1;
            DailyCheckBox.Text = "&Daily";
            DailyCheckBox.UseVisualStyleBackColor = true;
            DailyCheckBox.CheckedChanged += DailyCheckBox_CheckedChanged;
            // 
            // WeeklyCheckBox
            // 
            WeeklyCheckBox.AutoSize = true;
            WeeklyCheckBox.Checked = true;
            WeeklyCheckBox.CheckState = CheckState.Checked;
            WeeklyCheckBox.Location = new Point(669, 287);
            WeeklyCheckBox.Margin = new Padding(5);
            WeeklyCheckBox.Name = "WeeklyCheckBox";
            WeeklyCheckBox.Size = new Size(91, 29);
            WeeklyCheckBox.TabIndex = 2;
            WeeklyCheckBox.Text = "&Weekly";
            WeeklyCheckBox.UseVisualStyleBackColor = true;
            WeeklyCheckBox.CheckedChanged += WeeklyCheckBox_CheckedChanged;
            // 
            // SpecialCheckBox
            // 
            SpecialCheckBox.AutoSize = true;
            SpecialCheckBox.Checked = true;
            SpecialCheckBox.CheckState = CheckState.Checked;
            SpecialCheckBox.Location = new Point(789, 287);
            SpecialCheckBox.Margin = new Padding(5);
            SpecialCheckBox.Name = "SpecialCheckBox";
            SpecialCheckBox.Size = new Size(91, 29);
            SpecialCheckBox.TabIndex = 3;
            SpecialCheckBox.Text = "&Special";
            SpecialCheckBox.UseVisualStyleBackColor = true;
            SpecialCheckBox.CheckedChanged += SpecialCheckBox_CheckedChanged;
            // 
            // AccountsFlowLayoutPanel
            // 
            AccountsFlowLayoutPanel.Location = new Point(566, 97);
            AccountsFlowLayoutPanel.Margin = new Padding(5);
            AccountsFlowLayoutPanel.Name = "AccountsFlowLayoutPanel";
            AccountsFlowLayoutPanel.Size = new Size(314, 167);
            AccountsFlowLayoutPanel.TabIndex = 4;
            // 
            // UpdateButton
            // 
            UpdateButton.Font = new Font("Segoe UI", 16F);
            UpdateButton.Location = new Point(954, 282);
            UpdateButton.Margin = new Padding(5);
            UpdateButton.Name = "UpdateButton";
            UpdateButton.Size = new Size(118, 38);
            UpdateButton.TabIndex = 5;
            UpdateButton.Text = "&Update";
            UpdateButton.UseVisualStyleBackColor = true;
            UpdateButton.Click += UpdateButton_Click;
            // 
            // ObjectivesDataGridView
            // 
            ObjectivesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ObjectivesDataGridView.Location = new Point(47, 367);
            ObjectivesDataGridView.Margin = new Padding(5);
            ObjectivesDataGridView.Name = "ObjectivesDataGridView";
            ObjectivesDataGridView.RowHeadersVisible = false;
            ObjectivesDataGridView.Size = new Size(1025, 1047);
            ObjectivesDataGridView.TabIndex = 6;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1107, 959);
            Controls.Add(ObjectivesDataGridView);
            Controls.Add(UpdateButton);
            Controls.Add(AccountsFlowLayoutPanel);
            Controls.Add(SpecialCheckBox);
            Controls.Add(WeeklyCheckBox);
            Controls.Add(DailyCheckBox);
            Controls.Add(KeysGroupBox);
            Font = new Font("Segoe UI", 14F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5);
            Name = "MainForm";
            Text = "WVT";
            KeysGroupBox.ResumeLayout(false);
            KeysGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ObjectivesDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox KeysGroupBox;
        private TextBox KeyTextBox;
        private Label KeyLabel;
        private Label NameLabel;
        private Button KeyAddButton;
        private TextBox NameTextBox;
        private Button KeyRemoveButton;
        private ListBox KeyListBox;
        private CheckBox DailyCheckBox;
        private CheckBox WeeklyCheckBox;
        private CheckBox SpecialCheckBox;
        private FlowLayoutPanel AccountsFlowLayoutPanel;
        private Button UpdateButton;
        private DataGridView ObjectivesDataGridView;
    }
}