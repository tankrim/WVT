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
            KeysGroupBox.Location = new Point(24, 21);
            KeysGroupBox.Name = "KeysGroupBox";
            KeysGroupBox.Size = new Size(302, 170);
            KeysGroupBox.TabIndex = 0;
            KeysGroupBox.TabStop = false;
            KeysGroupBox.Text = "Key Managment";
            // 
            // KeyRemoveButton
            // 
            KeyRemoveButton.Location = new Point(137, 137);
            KeyRemoveButton.Name = "KeyRemoveButton";
            KeyRemoveButton.Size = new Size(88, 23);
            KeyRemoveButton.TabIndex = 6;
            KeyRemoveButton.Text = "Remove Key";
            KeyRemoveButton.UseVisualStyleBackColor = true;
            KeyRemoveButton.Click += KeyRemoveButton_Click;
            // 
            // KeyListBox
            // 
            KeyListBox.FormattingEnabled = true;
            KeyListBox.ItemHeight = 15;
            KeyListBox.Location = new Point(6, 66);
            KeyListBox.Name = "KeyListBox";
            KeyListBox.Size = new Size(120, 94);
            KeyListBox.TabIndex = 5;
            // 
            // NameTextBox
            // 
            NameTextBox.Location = new Point(112, 37);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.Size = new Size(100, 23);
            NameTextBox.TabIndex = 4;
            // 
            // NameLabel
            // 
            NameLabel.AutoSize = true;
            NameLabel.Location = new Point(112, 19);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new Size(39, 15);
            NameLabel.TabIndex = 3;
            NameLabel.Text = "Name";
            // 
            // KeyAddButton
            // 
            KeyAddButton.Location = new Point(218, 37);
            KeyAddButton.Name = "KeyAddButton";
            KeyAddButton.Size = new Size(75, 23);
            KeyAddButton.TabIndex = 2;
            KeyAddButton.Text = "Add Key";
            KeyAddButton.UseVisualStyleBackColor = true;
            KeyAddButton.Click += KeyAddButton_Click;
            // 
            // KeyTextBox
            // 
            KeyTextBox.Location = new Point(6, 37);
            KeyTextBox.Name = "KeyTextBox";
            KeyTextBox.Size = new Size(100, 23);
            KeyTextBox.TabIndex = 1;
            // 
            // KeyLabel
            // 
            KeyLabel.AutoSize = true;
            KeyLabel.Location = new Point(6, 19);
            KeyLabel.Name = "KeyLabel";
            KeyLabel.Size = new Size(74, 15);
            KeyLabel.TabIndex = 0;
            KeyLabel.Text = "New Api Key";
            // 
            // DailyCheckBox
            // 
            DailyCheckBox.AutoSize = true;
            DailyCheckBox.Checked = true;
            DailyCheckBox.CheckState = CheckState.Checked;
            DailyCheckBox.Location = new Point(360, 172);
            DailyCheckBox.Name = "DailyCheckBox";
            DailyCheckBox.Size = new Size(52, 19);
            DailyCheckBox.TabIndex = 1;
            DailyCheckBox.Text = "Daily";
            DailyCheckBox.UseVisualStyleBackColor = true;
            // 
            // WeeklyCheckBox
            // 
            WeeklyCheckBox.AutoSize = true;
            WeeklyCheckBox.Checked = true;
            WeeklyCheckBox.CheckState = CheckState.Checked;
            WeeklyCheckBox.Location = new Point(426, 172);
            WeeklyCheckBox.Name = "WeeklyCheckBox";
            WeeklyCheckBox.Size = new Size(64, 19);
            WeeklyCheckBox.TabIndex = 2;
            WeeklyCheckBox.Text = "Weekly";
            WeeklyCheckBox.UseVisualStyleBackColor = true;
            // 
            // SpecialCheckBox
            // 
            SpecialCheckBox.AutoSize = true;
            SpecialCheckBox.Checked = true;
            SpecialCheckBox.CheckState = CheckState.Checked;
            SpecialCheckBox.Location = new Point(496, 172);
            SpecialCheckBox.Name = "SpecialCheckBox";
            SpecialCheckBox.Size = new Size(63, 19);
            SpecialCheckBox.TabIndex = 3;
            SpecialCheckBox.Text = "Special";
            SpecialCheckBox.UseVisualStyleBackColor = true;
            // 
            // AccountsFlowLayoutPanel
            // 
            AccountsFlowLayoutPanel.Location = new Point(360, 58);
            AccountsFlowLayoutPanel.Name = "AccountsFlowLayoutPanel";
            AccountsFlowLayoutPanel.Size = new Size(200, 100);
            AccountsFlowLayoutPanel.TabIndex = 4;
            // 
            // UpdateButton
            // 
            UpdateButton.Location = new Point(607, 168);
            UpdateButton.Name = "UpdateButton";
            UpdateButton.Size = new Size(75, 23);
            UpdateButton.TabIndex = 5;
            UpdateButton.Text = "Update";
            UpdateButton.UseVisualStyleBackColor = true;
            UpdateButton.Click += UpdateButton_Click;
            // 
            // ObjectivesDataGridView
            // 
            ObjectivesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ObjectivesDataGridView.Location = new Point(30, 220);
            ObjectivesDataGridView.Name = "ObjectivesDataGridView";
            ObjectivesDataGridView.Size = new Size(652, 628);
            ObjectivesDataGridView.TabIndex = 6;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 987);
            Controls.Add(ObjectivesDataGridView);
            Controls.Add(UpdateButton);
            Controls.Add(AccountsFlowLayoutPanel);
            Controls.Add(SpecialCheckBox);
            Controls.Add(WeeklyCheckBox);
            Controls.Add(DailyCheckBox);
            Controls.Add(KeysGroupBox);
            Name = "MainForm";
            Text = "MainForm";
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