namespace dev422_midterm.music_rental_management
{
    partial class MusicRentalForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.txtGenre = new System.Windows.Forms.TextBox();
            this.txtArtist = new System.Windows.Forms.TextBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblGenre = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.lblArtist = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblAlbumInfo = new System.Windows.Forms.Label();
            this.btnDeleteAlbum = new System.Windows.Forms.Button();
            this.btnRefreshAlbums = new System.Windows.Forms.Button();
            this.dgvAlbums = new System.Windows.Forms.DataGridView();
            this.btnAddAlbum = new System.Windows.Forms.Button();
            this.lblUserId = new System.Windows.Forms.Label();
            this.lblRentalManagement = new System.Windows.Forms.Label();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.btnRentAlbum = new System.Windows.Forms.Button();
            this.btnRefreshRentals = new System.Windows.Forms.Button();
            this.btnReturnAlbum = new System.Windows.Forms.Button();
            this.dgvRentals = new System.Windows.Forms.DataGridView();
            this.lblActiveRentals = new System.Windows.Forms.Label();
            this.lblAlbumId = new System.Windows.Forms.Label();
            this.txtAlbumId = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlbums)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRentals)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(15, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(635, 495);
            this.tabControl1.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnDeleteAlbum);
            this.tabPage1.Controls.Add(this.btnRefreshAlbums);
            this.tabPage1.Controls.Add(this.dgvAlbums);
            this.tabPage1.Controls.Add(this.btnAddAlbum);
            this.tabPage1.Controls.Add(this.txtYear);
            this.tabPage1.Controls.Add(this.txtGenre);
            this.tabPage1.Controls.Add(this.txtArtist);
            this.tabPage1.Controls.Add(this.txtTitle);
            this.tabPage1.Controls.Add(this.lblGenre);
            this.tabPage1.Controls.Add(this.lblYear);
            this.tabPage1.Controls.Add(this.lblArtist);
            this.tabPage1.Controls.Add(this.lblTitle);
            this.tabPage1.Controls.Add(this.lblAlbumInfo);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(627, 469);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Music Albums";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtAlbumId);
            this.tabPage2.Controls.Add(this.lblAlbumId);
            this.tabPage2.Controls.Add(this.lblActiveRentals);
            this.tabPage2.Controls.Add(this.dgvRentals);
            this.tabPage2.Controls.Add(this.btnReturnAlbum);
            this.tabPage2.Controls.Add(this.btnRefreshRentals);
            this.tabPage2.Controls.Add(this.btnRentAlbum);
            this.tabPage2.Controls.Add(this.txtUserId);
            this.tabPage2.Controls.Add(this.lblRentalManagement);
            this.tabPage2.Controls.Add(this.lblUserId);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(627, 469);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Rentals";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(81, 185);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(269, 20);
            this.txtYear.TabIndex = 17;
            // 
            // txtGenre
            // 
            this.txtGenre.Location = new System.Drawing.Point(81, 137);
            this.txtGenre.Name = "txtGenre";
            this.txtGenre.Size = new System.Drawing.Size(269, 20);
            this.txtGenre.TabIndex = 16;
            // 
            // txtArtist
            // 
            this.txtArtist.Location = new System.Drawing.Point(81, 87);
            this.txtArtist.Name = "txtArtist";
            this.txtArtist.Size = new System.Drawing.Size(269, 20);
            this.txtArtist.TabIndex = 15;
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(81, 45);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(269, 20);
            this.txtTitle.TabIndex = 14;
            // 
            // lblGenre
            // 
            this.lblGenre.AutoSize = true;
            this.lblGenre.Location = new System.Drawing.Point(33, 144);
            this.lblGenre.Name = "lblGenre";
            this.lblGenre.Size = new System.Drawing.Size(42, 13);
            this.lblGenre.TabIndex = 13;
            this.lblGenre.Text = "Genre: ";
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(40, 192);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(35, 13);
            this.lblYear.TabIndex = 12;
            this.lblYear.Text = "Year: ";
            // 
            // lblArtist
            // 
            this.lblArtist.AutoSize = true;
            this.lblArtist.Location = new System.Drawing.Point(40, 94);
            this.lblArtist.Name = "lblArtist";
            this.lblArtist.Size = new System.Drawing.Size(33, 13);
            this.lblArtist.TabIndex = 11;
            this.lblArtist.Text = "Artist:";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(40, 52);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(33, 13);
            this.lblTitle.TabIndex = 10;
            this.lblTitle.Text = "Title: ";
            // 
            // lblAlbumInfo
            // 
            this.lblAlbumInfo.AutoSize = true;
            this.lblAlbumInfo.Location = new System.Drawing.Point(33, 19);
            this.lblAlbumInfo.Name = "lblAlbumInfo";
            this.lblAlbumInfo.Size = new System.Drawing.Size(89, 13);
            this.lblAlbumInfo.TabIndex = 9;
            this.lblAlbumInfo.Text = "Add Music Album";
            // 
            // btnDeleteAlbum
            // 
            this.btnDeleteAlbum.Location = new System.Drawing.Point(375, 424);
            this.btnDeleteAlbum.Name = "btnDeleteAlbum";
            this.btnDeleteAlbum.Size = new System.Drawing.Size(100, 23);
            this.btnDeleteAlbum.TabIndex = 21;
            this.btnDeleteAlbum.Text = "Delete Album";
            this.btnDeleteAlbum.UseVisualStyleBackColor = true;
            this.btnDeleteAlbum.Click += new System.EventHandler(this.btnDeleteAlbum_Click);
            // 
            // btnRefreshAlbums
            // 
            this.btnRefreshAlbums.Location = new System.Drawing.Point(486, 424);
            this.btnRefreshAlbums.Name = "btnRefreshAlbums";
            this.btnRefreshAlbums.Size = new System.Drawing.Size(108, 23);
            this.btnRefreshAlbums.TabIndex = 20;
            this.btnRefreshAlbums.Text = "Refresh Albums";
            this.btnRefreshAlbums.UseVisualStyleBackColor = true;
            this.btnRefreshAlbums.Click += new System.EventHandler(this.btnRefreshAlbums_Click);
            // 
            // dgvAlbums
            // 
            this.dgvAlbums.AllowUserToAddRows = false;
            this.dgvAlbums.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAlbums.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlbums.Location = new System.Drawing.Point(36, 220);
            this.dgvAlbums.Name = "dgvAlbums";
            this.dgvAlbums.ReadOnly = true;
            this.dgvAlbums.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAlbums.Size = new System.Drawing.Size(558, 198);
            this.dgvAlbums.TabIndex = 19;
            // 
            // btnAddAlbum
            // 
            this.btnAddAlbum.Location = new System.Drawing.Point(375, 182);
            this.btnAddAlbum.Name = "btnAddAlbum";
            this.btnAddAlbum.Size = new System.Drawing.Size(109, 23);
            this.btnAddAlbum.TabIndex = 18;
            this.btnAddAlbum.Text = "Add Album";
            this.btnAddAlbum.UseVisualStyleBackColor = true;
            this.btnAddAlbum.Click += new System.EventHandler(this.btnAddAlbum_Click);
            // 
            // lblUserId
            // 
            this.lblUserId.AutoSize = true;
            this.lblUserId.Location = new System.Drawing.Point(30, 70);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(49, 13);
            this.lblUserId.TabIndex = 0;
            this.lblUserId.Text = "User ID: ";
            // 
            // lblRentalManagement
            // 
            this.lblRentalManagement.AutoSize = true;
            this.lblRentalManagement.Location = new System.Drawing.Point(30, 22);
            this.lblRentalManagement.Name = "lblRentalManagement";
            this.lblRentalManagement.Size = new System.Drawing.Size(103, 13);
            this.lblRentalManagement.TabIndex = 1;
            this.lblRentalManagement.Text = "Rental Management";
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(85, 63);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(175, 20);
            this.txtUserId.TabIndex = 2;
            // 
            // btnRentAlbum
            // 
            this.btnRentAlbum.Location = new System.Drawing.Point(20, 140);
            this.btnRentAlbum.Name = "btnRentAlbum";
            this.btnRentAlbum.Size = new System.Drawing.Size(75, 23);
            this.btnRentAlbum.TabIndex = 3;
            this.btnRentAlbum.Text = "Rent Album";
            this.btnRentAlbum.UseVisualStyleBackColor = true;
            // 
            // btnRefreshRentals
            // 
            this.btnRefreshRentals.Location = new System.Drawing.Point(225, 140);
            this.btnRefreshRentals.Name = "btnRefreshRentals";
            this.btnRefreshRentals.Size = new System.Drawing.Size(115, 23);
            this.btnRefreshRentals.TabIndex = 4;
            this.btnRefreshRentals.Text = "Refresh Rentals";
            this.btnRefreshRentals.UseVisualStyleBackColor = true;
            // 
            // btnReturnAlbum
            // 
            this.btnReturnAlbum.Location = new System.Drawing.Point(101, 140);
            this.btnReturnAlbum.Name = "btnReturnAlbum";
            this.btnReturnAlbum.Size = new System.Drawing.Size(118, 23);
            this.btnReturnAlbum.TabIndex = 5;
            this.btnReturnAlbum.Text = "Return Album";
            this.btnReturnAlbum.UseVisualStyleBackColor = true;
            // 
            // dgvRentals
            // 
            this.dgvRentals.AllowUserToAddRows = false;
            this.dgvRentals.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRentals.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.dgvRentals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRentals.Location = new System.Drawing.Point(20, 207);
            this.dgvRentals.Name = "dgvRentals";
            this.dgvRentals.ReadOnly = true;
            this.dgvRentals.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRentals.Size = new System.Drawing.Size(587, 241);
            this.dgvRentals.TabIndex = 6;
            // 
            // lblActiveRentals
            // 
            this.lblActiveRentals.AutoSize = true;
            this.lblActiveRentals.Location = new System.Drawing.Point(30, 191);
            this.lblActiveRentals.Name = "lblActiveRentals";
            this.lblActiveRentals.Size = new System.Drawing.Size(76, 13);
            this.lblActiveRentals.TabIndex = 7;
            this.lblActiveRentals.Text = "Active Rentals";
            // 
            // lblAlbumId
            // 
            this.lblAlbumId.AutoSize = true;
            this.lblAlbumId.Location = new System.Drawing.Point(23, 104);
            this.lblAlbumId.Name = "lblAlbumId";
            this.lblAlbumId.Size = new System.Drawing.Size(56, 13);
            this.lblAlbumId.TabIndex = 8;
            this.lblAlbumId.Text = "Album ID: ";
            // 
            // txtAlbumId
            // 
            this.txtAlbumId.Location = new System.Drawing.Point(85, 97);
            this.txtAlbumId.Name = "txtAlbumId";
            this.txtAlbumId.Size = new System.Drawing.Size(175, 20);
            this.txtAlbumId.TabIndex = 9;
            // 
            // MusicRentalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 519);
            this.Controls.Add(this.tabControl1);
            this.Name = "MusicRentalForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VibeHive - Music Rental Management";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlbums)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRentals)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnDeleteAlbum;
        private System.Windows.Forms.Button btnRefreshAlbums;
        private System.Windows.Forms.DataGridView dgvAlbums;
        private System.Windows.Forms.Button btnAddAlbum;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.TextBox txtGenre;
        private System.Windows.Forms.TextBox txtArtist;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblGenre;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Label lblArtist;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblAlbumInfo;
        private System.Windows.Forms.Button btnReturnAlbum;
        private System.Windows.Forms.Button btnRefreshRentals;
        private System.Windows.Forms.Button btnRentAlbum;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Label lblRentalManagement;
        private System.Windows.Forms.Label lblUserId;
        private System.Windows.Forms.DataGridView dgvRentals;
        private System.Windows.Forms.Label lblActiveRentals;
        private System.Windows.Forms.TextBox txtAlbumId;
        private System.Windows.Forms.Label lblAlbumId;
    }
}