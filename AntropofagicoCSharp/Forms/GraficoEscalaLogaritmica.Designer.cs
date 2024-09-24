namespace AntropofagicoCSharp.Forms
{
    partial class GraficoEscalaLogaritmica
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraficoEscalaLogaritmica));
            formsPlot3 = new ScottPlot.WinForms.FormsPlot();
            SuspendLayout();
            // 
            // formsPlot3
            // 
            formsPlot3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            formsPlot3.DisplayScale = 1F;
            formsPlot3.Location = new Point(0, -3);
            formsPlot3.Name = "formsPlot3";
            formsPlot3.Size = new Size(802, 453);
            formsPlot3.TabIndex = 0;
            // 
            // GraficoEscalaLogaritmica
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(formsPlot3);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "GraficoEscalaLogaritmica";
            Text = "Gráfico em Escala Logarítmica";
            ResumeLayout(false);
        }

        #endregion

        private ScottPlot.WinForms.FormsPlot formsPlot3;
    }
}