namespace AntropofagicoCSharp
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label3 = new Label();
            panel3 = new Panel();
            groupBox6 = new GroupBox();
            label9 = new Label();
            button1 = new Button();
            groupBox2 = new GroupBox();
            label11 = new Label();
            groupBox4 = new GroupBox();
            label7 = new Label();
            maskedTextBox2 = new MaskedTextBox();
            label5 = new Label();
            panel4 = new Panel();
            groupBox1 = new GroupBox();
            groupBox3 = new GroupBox();
            label4 = new Label();
            label2 = new Label();
            maskedTextBox1 = new MaskedTextBox();
            button3 = new Button();
            label1 = new Label();
            panel1 = new Panel();
            groupBox5 = new GroupBox();
            button2 = new Button();
            label8 = new Label();
            label10 = new Label();
            maskedTextBox3 = new MaskedTextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel3.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox2.SuspendLayout();
            panel4.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox5.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoEllipsis = true;
            label3.Font = new Font("Verdana", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(415, 9);
            label3.Name = "label3";
            label3.Size = new Size(354, 23);
            label3.TabIndex = 3;
            label3.Text = "Análise de Componentes Principais";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            panel3.Controls.Add(groupBox6);
            panel3.Controls.Add(groupBox2);
            panel3.Controls.Add(label5);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(583, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(574, 754);
            panel3.TabIndex = 14;
            // 
            // groupBox6
            // 
            groupBox6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox6.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox6.BackColor = SystemColors.ControlLightLight;
            groupBox6.Controls.Add(label9);
            groupBox6.Controls.Add(button1);
            groupBox6.ForeColor = Color.LimeGreen;
            groupBox6.ImeMode = ImeMode.NoControl;
            groupBox6.Location = new Point(7, 627);
            groupBox6.Margin = new Padding(5, 6, 5, 6);
            groupBox6.Name = "groupBox6";
            groupBox6.Padding = new Padding(3, 4, 3, 4);
            groupBox6.Size = new Size(564, 121);
            groupBox6.TabIndex = 10;
            groupBox6.TabStop = false;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.Black;
            label9.Location = new Point(163, 20);
            label9.Name = "label9";
            label9.Size = new Size(267, 23);
            label9.TabIndex = 7;
            label9.Text = "Aplicar PCA | Plotar dados pelo PCA";
            label9.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button1.BackColor = Color.Green;
            button1.Cursor = Cursors.Hand;
            button1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.BlanchedAlmond;
            button1.Location = new Point(6, 67);
            button1.Name = "button1";
            button1.Size = new Size(552, 47);
            button1.TabIndex = 6;
            button1.Text = "Abrir";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(label11);
            groupBox2.Controls.Add(groupBox4);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(maskedTextBox2);
            groupBox2.Location = new Point(7, 38);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(564, 567);
            groupBox2.TabIndex = 12;
            groupBox2.TabStop = false;
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label11.AutoEllipsis = true;
            label11.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label11.Location = new Point(183, 86);
            label11.Name = "label11";
            label11.Size = new Size(218, 21);
            label11.TabIndex = 14;
            label11.Text = "Conteúdo do diretório";
            label11.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBox4
            // 
            groupBox4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox4.Location = new Point(6, 110);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(552, 451);
            groupBox4.TabIndex = 13;
            groupBox4.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.Black;
            label7.Location = new Point(6, 39);
            label7.Name = "label7";
            label7.Size = new Size(72, 20);
            label7.TabIndex = 12;
            label7.Text = "Diretório:";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // maskedTextBox2
            // 
            maskedTextBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            maskedTextBox2.Location = new Point(84, 38);
            maskedTextBox2.Margin = new Padding(3, 4, 3, 4);
            maskedTextBox2.Name = "maskedTextBox2";
            maskedTextBox2.Size = new Size(474, 23);
            maskedTextBox2.TabIndex = 5;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label5.AutoEllipsis = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(190, 11);
            label5.Name = "label5";
            label5.Size = new Size(218, 21);
            label5.TabIndex = 4;
            label5.Text = "Gerar | Exibir arquivos csv";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            panel4.Controls.Add(groupBox1);
            panel4.Controls.Add(label1);
            panel4.Controls.Add(panel1);
            panel4.Controls.Add(groupBox5);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(3, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(574, 754);
            panel4.TabIndex = 15;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(groupBox3);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(maskedTextBox1);
            groupBox1.Controls.Add(button3);
            groupBox1.Location = new Point(5, 38);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(564, 567);
            groupBox1.TabIndex = 11;
            groupBox1.TabStop = false;
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox3.Location = new Point(6, 113);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(552, 395);
            groupBox3.TabIndex = 9;
            groupBox3.TabStop = false;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(187, 89);
            label4.Name = "label4";
            label4.Size = new Size(164, 21);
            label4.TabIndex = 8;
            label4.Text = "Conteúdo do diretório";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(6, 39);
            label2.Name = "label2";
            label2.Size = new Size(72, 20);
            label2.TabIndex = 7;
            label2.Text = "Diretório:";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // maskedTextBox1
            // 
            maskedTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            maskedTextBox1.Location = new Point(84, 39);
            maskedTextBox1.Margin = new Padding(3, 4, 3, 4);
            maskedTextBox1.Name = "maskedTextBox1";
            maskedTextBox1.Size = new Size(472, 23);
            maskedTextBox1.TabIndex = 1;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button3.BackColor = Color.Green;
            button3.Cursor = Cursors.Hand;
            button3.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button3.ForeColor = Color.BlanchedAlmond;
            button3.Location = new Point(6, 514);
            button3.Name = "button3";
            button3.Size = new Size(552, 47);
            button3.TabIndex = 2;
            button3.Text = "Abrir";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click_1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(157, 11);
            label1.Name = "label1";
            label1.Size = new Size(240, 21);
            label1.TabIndex = 2;
            label1.Text = "Leitura | Exibição dos arquivos txt";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.AutoSize = true;
            panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel1.BackColor = Color.Transparent;
            panel1.ForeColor = Color.Black;
            panel1.Location = new Point(5, 495);
            panel1.MaximumSize = new Size(1230, 200);
            panel1.Name = "panel1";
            panel1.Size = new Size(0, 0);
            panel1.TabIndex = 8;
            // 
            // groupBox5
            // 
            groupBox5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox5.BackColor = SystemColors.ControlLightLight;
            groupBox5.Controls.Add(button2);
            groupBox5.Controls.Add(label8);
            groupBox5.Controls.Add(label10);
            groupBox5.Controls.Add(maskedTextBox3);
            groupBox5.FlatStyle = FlatStyle.Popup;
            groupBox5.ForeColor = Color.LimeGreen;
            groupBox5.ImeMode = ImeMode.On;
            groupBox5.Location = new Point(0, 627);
            groupBox5.Margin = new Padding(5, 6, 5, 6);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new Padding(3, 4, 3, 4);
            groupBox5.Size = new Size(569, 121);
            groupBox5.TabIndex = 3;
            groupBox5.TabStop = false;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button2.BackColor = Color.Green;
            button2.Cursor = Cursors.Hand;
            button2.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ForeColor = Color.BlanchedAlmond;
            button2.Location = new Point(6, 75);
            button2.Name = "button2";
            button2.Size = new Size(557, 39);
            button2.TabIndex = 4;
            button2.Text = "Abrir";
            button2.UseVisualStyleBackColor = false;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label8.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.Black;
            label8.Location = new Point(134, 20);
            label8.Name = "label8";
            label8.Size = new Size(293, 21);
            label8.TabIndex = 6;
            label8.Text = "Gerar o arquivo csv com todos os dados";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.Black;
            label10.Location = new Point(6, 46);
            label10.Name = "label10";
            label10.Size = new Size(72, 20);
            label10.TabIndex = 3;
            label10.Text = "Diretório:";
            label10.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // maskedTextBox3
            // 
            maskedTextBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            maskedTextBox3.Location = new Point(84, 45);
            maskedTextBox3.Margin = new Padding(3, 4, 3, 4);
            maskedTextBox3.Name = "maskedTextBox3";
            maskedTextBox3.Size = new Size(479, 23);
            maskedTextBox3.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(panel4, 0, 0);
            tableLayoutPanel1.Controls.Add(panel3, 1, 0);
            tableLayoutPanel1.Location = new Point(12, 35);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(1160, 760);
            tableLayoutPanel1.TabIndex = 16;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(1184, 807);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(label3);
            Font = new Font("Sitka Text", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(1200, 820);
            Name = "Form1";
            RightToLeftLayout = true;
            Text = "Form1";
            panel3.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Label label3;
        private Panel panel3;
        private GroupBox groupBox6;
        private Label label9;
        private Button button1;
        private GroupBox groupBox2;
        private GroupBox groupBox4;
        private Label label7;
        private MaskedTextBox maskedTextBox2;
        private Label label5;
        private Panel panel4;
        private GroupBox groupBox1;
        private GroupBox groupBox3;
        private Label label4;
        private Label label2;
        private MaskedTextBox maskedTextBox1;
        private Button button3;
        private Label label1;
        private Panel panel1;
        private GroupBox groupBox5;
        private Button button2;
        private Label label8;
        private Label label10;
        private MaskedTextBox maskedTextBox3;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label11;
    }
}
