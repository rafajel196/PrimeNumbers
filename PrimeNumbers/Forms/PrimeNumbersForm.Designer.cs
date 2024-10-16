namespace PrimeNumbers
{
    partial class PrimeNumbersForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblStatus = new Label();
            btnStart = new Button();
            btnStop = new Button();
            listView = new ListView();
            btnDownloadResults = new Button();
            SuspendLayout();
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(12, 9);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(125, 15);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "Obliczanie nieaktywne";
            // 
            // btnStart
            // 
            btnStart.Location = new Point(12, 27);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(75, 23);
            btnStart.TabIndex = 1;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += this.btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(93, 27);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(75, 23);
            btnStop.TabIndex = 2;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += this.btnStop_Click;
            // 
            // listView
            // 
            listView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listView.Location = new Point(12, 56);
            listView.Name = "listView";
            listView.Size = new Size(750, 389);
            listView.TabIndex = 3;
            listView.UseCompatibleStateImageBehavior = false;
            // 
            // btnDownloadResults
            // 
            btnDownloadResults.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDownloadResults.Location = new Point(12, 451);
            btnDownloadResults.Name = "btnDownloadResults";
            btnDownloadResults.Size = new Size(185, 23);
            btnDownloadResults.TabIndex = 4;
            btnDownloadResults.Text = "Pobierz wyniki w pliku xml";
            btnDownloadResults.UseVisualStyleBackColor = true;
            btnDownloadResults.Click += this.btnDownloadResults_Click;
            // 
            // PrimeNumbersForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(774, 486);
            Controls.Add(btnDownloadResults);
            Controls.Add(listView);
            Controls.Add(btnStop);
            Controls.Add(btnStart);
            Controls.Add(lblStatus);
            Name = "PrimeNumbersForm";
            Text = "Liczby pierwsze";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblStatus;
        private Button btnStart;
        private Button btnStop;
        private ListView listView;
        private Button btnDownloadResults;
    }
}
