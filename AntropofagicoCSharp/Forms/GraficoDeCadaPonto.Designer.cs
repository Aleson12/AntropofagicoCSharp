namespace AntropofagicoCSharp.Forms
{
    partial class GraficoDeCadaPonto
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
            formsPlot2 = new ScottPlot.WinForms.FormsPlot();
            SuspendLayout();
            // 
            // formsPlot2
            // 
            formsPlot2.DisplayScale = 1F;
            formsPlot2.Dock = DockStyle.Fill;
            formsPlot2.Location = new Point(0, 0);
            formsPlot2.Name = "formsPlot2";
            formsPlot2.Size = new Size(655, 425);
            formsPlot2.TabIndex = 0;
            // 
            // GraficoDeCadaPonto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(655, 425);
            Controls.Add(formsPlot2);
            Name = "GraficoDeCadaPonto";
            Text = "Form1";
            TopMost = true;
            ResumeLayout(false);
        }

        #endregion

        public ScottPlot.WinForms.FormsPlot formsPlot2;
    }
}
