﻿namespace AntropofagicoCSharp.Forms
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
            SuspendLayout();
            // 
            // formsPlot2
            // 
            formsPlot2.DisplayScale = 1F;
            formsPlot2.Dock = DockStyle.Fill;
            formsPlot2.Location = new Point(0, 0);
            formsPlot2.Margin = new Padding(3, 4, 3, 4);
            formsPlot2.Name = "formsPlot2";
            formsPlot2.Size = new Size(749, 567);
            formsPlot2.TabIndex = 0;
            // 
            // GraficoDeCadaPonto
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(749, 567);
            Controls.Add(formsPlot2);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            Name = "GraficoDeCadaPonto";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        public ScottPlot.WinForms.FormsPlot formsPlot2;
    }
}
