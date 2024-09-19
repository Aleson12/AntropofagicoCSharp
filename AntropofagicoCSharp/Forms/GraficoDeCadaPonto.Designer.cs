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
            Button button2;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraficoDeCadaPonto));
            formsPlot2 = new ScottPlot.WinForms.FormsPlot();
            button2 = new Button();
            SuspendLayout();
            // 
            // button2
            // 
            button2.AutoSize = true;
            button2.BackgroundImageLayout = ImageLayout.Stretch;
            button2.Cursor = Cursors.Hand;
            button2.Dock = DockStyle.Top;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Location = new Point(0, 0);
            button2.Name = "button2";
            button2.Size = new Size(1276, 42);
            button2.TabIndex = 1;
            button2.TabStop = false;
            button2.Text = "Mostrar gráfico com escala logarítmica";
            button2.UseVisualStyleBackColor = true;
            button2.Click += MostrarGraficoEmEscalaLogaritmica;
            // 
            // formsPlot2
            // 
            formsPlot2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            formsPlot2.AutoSize = true;
            formsPlot2.DisplayScale = 1F;
            formsPlot2.Location = new Point(-1, 60);
            formsPlot2.Name = "formsPlot2";
            formsPlot2.Size = new Size(1279, 701);
            formsPlot2.TabIndex = 0;
            // 
            // GraficoDeCadaPonto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1276, 761);
            Controls.Add(button2);
            Controls.Add(formsPlot2);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "GraficoDeCadaPonto";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ScottPlot.WinForms.FormsPlot formsPlot2;
        private Button button1;
        private Button button2;
        private Panel panel1;
    }
}
