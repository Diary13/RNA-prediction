namespace RNA_prediction.childForms
{
    partial class AccueilUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.iconAccueilPred = new FontAwesome.Sharp.IconPictureBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.iconAccueilPred)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(49, 250);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(775, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "PREDICTION ET MODELISATION DE SERIES TEMPORELLES";
            // 
            // iconAccueilPred
            // 
            this.iconAccueilPred.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(21)))), ((int)(((byte)(48)))));
            this.iconAccueilPred.ForeColor = System.Drawing.Color.DarkSalmon;
            this.iconAccueilPred.IconChar = FontAwesome.Sharp.IconChar.Brain;
            this.iconAccueilPred.IconColor = System.Drawing.Color.DarkSalmon;
            this.iconAccueilPred.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconAccueilPred.IconSize = 195;
            this.iconAccueilPred.Location = new System.Drawing.Point(312, 39);
            this.iconAccueilPred.Name = "iconAccueilPred";
            this.iconAccueilPred.Size = new System.Drawing.Size(237, 195);
            this.iconAccueilPred.TabIndex = 1;
            this.iconAccueilPred.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Yu Gothic UI", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(55, 312);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(786, 40);
            this.label2.TabIndex = 2;
            this.label2.Text = "PAR RESEAUX DE NEURONES ARTIFICIELS MULTICOUCHES";
            // 
            // AccueilUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(21)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.label2);
            this.Controls.Add(this.iconAccueilPred);
            this.Controls.Add(this.label1);
            this.Name = "AccueilUserControl";
            this.Size = new System.Drawing.Size(880, 525);
            ((System.ComponentModel.ISupportInitialize)(this.iconAccueilPred)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconPictureBox iconAccueilPred;
        private System.Windows.Forms.Label label2;
    }
}
