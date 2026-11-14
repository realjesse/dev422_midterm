namespace dev422_midterm
{
    partial class Form1
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
            this.albumInventoryMicroserviceButton = new System.Windows.Forms.Button();
            this.collaborativePlaylistBuilderButton = new System.Windows.Forms.Button();
            this.musicRentalManagerButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // albumInventoryMicroserviceButton
            // 
            this.albumInventoryMicroserviceButton.Location = new System.Drawing.Point(171, 44);
            this.albumInventoryMicroserviceButton.Name = "albumInventoryMicroserviceButton";
            this.albumInventoryMicroserviceButton.Size = new System.Drawing.Size(443, 79);
            this.albumInventoryMicroserviceButton.TabIndex = 0;
            this.albumInventoryMicroserviceButton.Text = "Album Inventory Microservice";
            this.albumInventoryMicroserviceButton.UseVisualStyleBackColor = true;
            // 
            // collaborativePlaylistBuilderButton
            // 
            this.collaborativePlaylistBuilderButton.Location = new System.Drawing.Point(171, 184);
            this.collaborativePlaylistBuilderButton.Name = "collaborativePlaylistBuilderButton";
            this.collaborativePlaylistBuilderButton.Size = new System.Drawing.Size(443, 79);
            this.collaborativePlaylistBuilderButton.TabIndex = 1;
            this.collaborativePlaylistBuilderButton.Text = "Collaborative Playlist Builder";
            this.collaborativePlaylistBuilderButton.UseVisualStyleBackColor = true;
            // 
            // musicRentalManagerButton
            // 
            this.musicRentalManagerButton.Location = new System.Drawing.Point(171, 314);
            this.musicRentalManagerButton.Name = "musicRentalManagerButton";
            this.musicRentalManagerButton.Size = new System.Drawing.Size(443, 79);
            this.musicRentalManagerButton.TabIndex = 2;
            this.musicRentalManagerButton.Text = "Music Rental Manager";
            this.musicRentalManagerButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.musicRentalManagerButton);
            this.Controls.Add(this.collaborativePlaylistBuilderButton);
            this.Controls.Add(this.albumInventoryMicroserviceButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button collaborativePlaylistBuilderButton;
        private System.Windows.Forms.Button musicRentalManagerButton;

        private System.Windows.Forms.Button albumInventoryMicroserviceButton;

        #endregion
    }
}

