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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraficoDeCadaPonto));
            formsPlot2 = new ScottPlot.WinForms.FormsPlot();
            button1 = new Button();
            SuspendLayout();
            // 
            // formsPlot2
            // 
            formsPlot2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            formsPlot2.DisplayScale = 1F;
            formsPlot2.Location = new Point(0, 67);
            formsPlot2.Name = "formsPlot2";
            formsPlot2.Size = new Size(1130, 606);
            formsPlot2.TabIndex = 0;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button1.Cursor = Cursors.Hand;
            button1.Location = new Point(60, 12);
            button1.Name = "button1";
            button1.Size = new Size(1057, 49);
            button1.TabIndex = 1;
            button1.Text = "Mostrar este gráfico em escala logarítmica";
            button1.UseVisualStyleBackColor = true;
            button1.UseWaitCursor = true;
            button1.Click += button1_Click;
            // 
            // GraficoDeCadaPonto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1129, 674);
            Controls.Add(button1);
            Controls.Add(formsPlot2);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "GraficoDeCadaPonto";
            Text = "GraficoDeCadaPonto";
            ResumeLayout(false);
        }

        #endregion

        private ScottPlot.WinForms.FormsPlot formsPlot2;
        private Button button1;
    }
}