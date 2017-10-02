namespace Autosuggest
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
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lbSuggestions = new System.Windows.Forms.ListBox();
            this.lblSpellChecker = new System.Windows.Forms.Label();
            this.lblSuggestionTime = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(13, 13);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(259, 20);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lbSuggestions
            // 
            this.lbSuggestions.FormattingEnabled = true;
            this.lbSuggestions.Location = new System.Drawing.Point(13, 39);
            this.lbSuggestions.Name = "lbSuggestions";
            this.lbSuggestions.Size = new System.Drawing.Size(259, 186);
            this.lbSuggestions.TabIndex = 1;
            // 
            // lblSpellChecker
            // 
            this.lblSpellChecker.AutoSize = true;
            this.lblSpellChecker.Location = new System.Drawing.Point(13, 236);
            this.lblSpellChecker.Name = "lblSpellChecker";
            this.lblSpellChecker.Size = new System.Drawing.Size(35, 13);
            this.lblSpellChecker.TabIndex = 2;
            this.lblSpellChecker.Text = "label1";
            // 
            // lblSuggestionTime
            // 
            this.lblSuggestionTime.AutoSize = true;
            this.lblSuggestionTime.Location = new System.Drawing.Point(13, 259);
            this.lblSuggestionTime.Name = "lblSuggestionTime";
            this.lblSuggestionTime.Size = new System.Drawing.Size(0, 13);
            this.lblSuggestionTime.TabIndex = 4;
            this.lblSuggestionTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 288);
            this.Controls.Add(this.lblSuggestionTime);
            this.Controls.Add(this.lblSpellChecker);
            this.Controls.Add(this.lbSuggestions);
            this.Controls.Add(this.txtSearch);
            this.Name = "Form1";
            this.Text = "Movie Search";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ListBox lbSuggestions;
        private System.Windows.Forms.Label lblSpellChecker;
        private System.Windows.Forms.Label lblSuggestionTime;
    }
}

