namespace iTasks.Views
{
    partial class frmDetalhesPrevisao
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
            this.lstTarefasPrevisao = new System.Windows.Forms.ListBox();
            this.lbTotalHoras = new System.Windows.Forms.Label();
            this.btnFechar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstTarefasPrevisao
            // 
            this.lstTarefasPrevisao.FormattingEnabled = true;
            this.lstTarefasPrevisao.Location = new System.Drawing.Point(12, 12);
            this.lstTarefasPrevisao.Name = "lstTarefasPrevisao";
            this.lstTarefasPrevisao.Size = new System.Drawing.Size(392, 329);
            this.lstTarefasPrevisao.TabIndex = 0;
            // 
            // lbTotalHoras
            // 
            this.lbTotalHoras.AutoSize = true;
            this.lbTotalHoras.Location = new System.Drawing.Point(12, 358);
            this.lbTotalHoras.Name = "lbTotalHoras";
            this.lbTotalHoras.Size = new System.Drawing.Size(35, 13);
            this.lbTotalHoras.TabIndex = 1;
            this.lbTotalHoras.Text = "label1";
            // 
            // btnFechar
            // 
            this.btnFechar.Location = new System.Drawing.Point(15, 393);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(92, 33);
            this.btnFechar.TabIndex = 2;
            this.btnFechar.Text = "Fechar";
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // frmDetalhesPrevisao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 438);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.lbTotalHoras);
            this.Controls.Add(this.lstTarefasPrevisao);
            this.Name = "frmDetalhesPrevisao";
            this.Text = "frmDetalhesPrevisao";
            this.Load += new System.EventHandler(this.frmDetalhesPrevisao_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstTarefasPrevisao;
        private System.Windows.Forms.Label lbTotalHoras;
        private System.Windows.Forms.Button btnFechar;
    }
}