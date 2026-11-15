using System.ComponentModel;

namespace dev422_midterm.CollaborativePlaylistBuilder
{
    partial class MainCPB
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.signOutButton = new System.Windows.Forms.Button();
            this.playlistDataGridView = new System.Windows.Forms.DataGridView();
            this.openPlaylistButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.playlistNameTextBox = new System.Windows.Forms.TextBox();
            this.createPlaylistGroupBox = new System.Windows.Forms.GroupBox();
            this.createPlaylistButton = new System.Windows.Forms.Button();
            this.isCollaborativeCheckBox = new System.Windows.Forms.CheckBox();
            this.playlistNameLabel = new System.Windows.Forms.Label();
            this.songLibraryButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.playlistDataGridView)).BeginInit();
            this.createPlaylistGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // signOutButton
            // 
            this.signOutButton.Location = new System.Drawing.Point(25, 387);
            this.signOutButton.Name = "signOutButton";
            this.signOutButton.Size = new System.Drawing.Size(96, 37);
            this.signOutButton.TabIndex = 0;
            this.signOutButton.Text = "Sign Out";
            this.signOutButton.UseVisualStyleBackColor = true;
            // 
            // playlistDataGridView
            // 
            this.playlistDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.playlistDataGridView.Location = new System.Drawing.Point(25, 31);
            this.playlistDataGridView.Name = "playlistDataGridView";
            this.playlistDataGridView.Size = new System.Drawing.Size(504, 227);
            this.playlistDataGridView.TabIndex = 1;
            // 
            // openPlaylistButton
            // 
            this.openPlaylistButton.Location = new System.Drawing.Point(622, 44);
            this.openPlaylistButton.Name = "openPlaylistButton";
            this.openPlaylistButton.Size = new System.Drawing.Size(89, 41);
            this.openPlaylistButton.TabIndex = 2;
            this.openPlaylistButton.Text = "Open Playlist";
            this.openPlaylistButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(548, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "Create Playlist";
            // 
            // playlistNameTextBox
            // 
            this.playlistNameTextBox.Location = new System.Drawing.Point(43, 44);
            this.playlistNameTextBox.Name = "playlistNameTextBox";
            this.playlistNameTextBox.Size = new System.Drawing.Size(115, 20);
            this.playlistNameTextBox.TabIndex = 4;
            // 
            // createPlaylistGroupBox
            // 
            this.createPlaylistGroupBox.Controls.Add(this.createPlaylistButton);
            this.createPlaylistGroupBox.Controls.Add(this.isCollaborativeCheckBox);
            this.createPlaylistGroupBox.Controls.Add(this.playlistNameLabel);
            this.createPlaylistGroupBox.Controls.Add(this.playlistNameTextBox);
            this.createPlaylistGroupBox.Location = new System.Drawing.Point(567, 132);
            this.createPlaylistGroupBox.Name = "createPlaylistGroupBox";
            this.createPlaylistGroupBox.Size = new System.Drawing.Size(200, 186);
            this.createPlaylistGroupBox.TabIndex = 5;
            this.createPlaylistGroupBox.TabStop = false;
            this.createPlaylistGroupBox.Text = "Create Playlist";
            // 
            // createPlaylistButton
            // 
            this.createPlaylistButton.Location = new System.Drawing.Point(55, 123);
            this.createPlaylistButton.Name = "createPlaylistButton";
            this.createPlaylistButton.Size = new System.Drawing.Size(89, 41);
            this.createPlaylistButton.TabIndex = 6;
            this.createPlaylistButton.Text = "Create Playlist";
            this.createPlaylistButton.UseVisualStyleBackColor = true;
            // 
            // isCollaborativeCheckBox
            // 
            this.isCollaborativeCheckBox.Location = new System.Drawing.Point(43, 80);
            this.isCollaborativeCheckBox.Name = "isCollaborativeCheckBox";
            this.isCollaborativeCheckBox.Size = new System.Drawing.Size(104, 24);
            this.isCollaborativeCheckBox.TabIndex = 7;
            this.isCollaborativeCheckBox.Text = "Collaborative";
            this.isCollaborativeCheckBox.UseVisualStyleBackColor = true;
            // 
            // playlistNameLabel
            // 
            this.playlistNameLabel.Location = new System.Drawing.Point(15, 18);
            this.playlistNameLabel.Name = "playlistNameLabel";
            this.playlistNameLabel.Size = new System.Drawing.Size(79, 23);
            this.playlistNameLabel.TabIndex = 6;
            this.playlistNameLabel.Text = "Playlist Name";
            // 
            // songLibraryButton
            // 
            this.songLibraryButton.Location = new System.Drawing.Point(651, 387);
            this.songLibraryButton.Name = "songLibraryButton";
            this.songLibraryButton.Size = new System.Drawing.Size(96, 37);
            this.songLibraryButton.TabIndex = 6;
            this.songLibraryButton.Text = "Song Library";
            this.songLibraryButton.UseVisualStyleBackColor = true;
            // 
            // MainCPB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.songLibraryButton);
            this.Controls.Add(this.createPlaylistGroupBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.openPlaylistButton);
            this.Controls.Add(this.playlistDataGridView);
            this.Controls.Add(this.signOutButton);
            this.Name = "MainCPB";
            this.Text = "MainCPB";
            ((System.ComponentModel.ISupportInitialize)(this.playlistDataGridView)).EndInit();
            this.createPlaylistGroupBox.ResumeLayout(false);
            this.createPlaylistGroupBox.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button openPlaylistButton;
        private System.Windows.Forms.Button signOutButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox playlistNameTextBox;
        private System.Windows.Forms.GroupBox createPlaylistGroupBox;
        private System.Windows.Forms.Label playlistNameLabel;
        private System.Windows.Forms.CheckBox isCollaborativeCheckBox;
        private System.Windows.Forms.Button createPlaylistButton;

        private System.Windows.Forms.Button songLibraryButton;
        private System.Windows.Forms.DataGridView playlistDataGridView;

        #endregion
    }
}