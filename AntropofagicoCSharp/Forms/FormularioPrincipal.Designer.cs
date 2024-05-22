namespace AntropofagicoCSharp
{
    partial class FrmPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
            lbl_NomeDaAplicacao = new Label();
            pnl_Panel3 = new Panel();
            grpAplicarEPlotarPCA = new GroupBox();
            lbl_AplicaPCA = new Label();
            btn_AplicarPCA = new Button();
            grp_GerarEExibirArquivosCsv = new GroupBox();
            rtx_ArquivosCsv = new RichTextBox();
            lbl_ConteudoDoDiretorioCsv = new Label();
            lbl_DiretorioDosArquivosCsv = new Label();
            mtx_DiretorioArquivosCsv = new MaskedTextBox();
            lbl_GerarExibirCSV = new Label();
            pnl_ComponentesLeituraDosArquivosTxtEMatrizFinal = new Panel();
            grp_LeituraEExibicaoDosArquivosTxt = new GroupBox();
            rtx_ArquivosTxt = new RichTextBox();
            lbl_ConteudoDoDiretorioTxt = new Label();
            lbl_DiretorioDosArquivosTxt = new Label();
            mtx_DiretorioArquivosTxt = new MaskedTextBox();
            btn_AbrirDiretorio = new Button();
            lbl_LeituraExibicaoDosArquivosTxt = new Label();
            pnl_ = new Panel();
            grp_GerarArquivoCsvComTodosDados = new GroupBox();
            btn_GerarCSV = new Button();
            lbl_GerarArquivoCSV = new Label();
            lbl_DiretorioDaMatrizFinal = new Label();
            mtx_DiretorioDoArquivoCsvFinal = new MaskedTextBox();
            tlp_FundoDosComponentes = new TableLayoutPanel();
            pnl_Panel3.SuspendLayout();
            grpAplicarEPlotarPCA.SuspendLayout();
            grp_GerarEExibirArquivosCsv.SuspendLayout();
            pnl_ComponentesLeituraDosArquivosTxtEMatrizFinal.SuspendLayout();
            grp_LeituraEExibicaoDosArquivosTxt.SuspendLayout();
            grp_GerarArquivoCsvComTodosDados.SuspendLayout();
            tlp_FundoDosComponentes.SuspendLayout();
            SuspendLayout();
            // 
            // lbl_NomeDaAplicacao
            // 
            lbl_NomeDaAplicacao.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lbl_NomeDaAplicacao.AutoEllipsis = true;
            lbl_NomeDaAplicacao.Font = new Font("Microsoft Sans Serif", 14.25F);
            lbl_NomeDaAplicacao.Location = new Point(415, 9);
            lbl_NomeDaAplicacao.Name = "lbl_NomeDaAplicacao";
            lbl_NomeDaAplicacao.Size = new Size(354, 23);
            lbl_NomeDaAplicacao.TabIndex = 3;
            lbl_NomeDaAplicacao.Text = "Análise de Componentes Principais";
            lbl_NomeDaAplicacao.TextAlign = ContentAlignment.MiddleCenter;
            lbl_NomeDaAplicacao.UseWaitCursor = true;
            // 
            // pnl_Panel3
            // 
            pnl_Panel3.Controls.Add(grpAplicarEPlotarPCA);
            pnl_Panel3.Controls.Add(grp_GerarEExibirArquivosCsv);
            pnl_Panel3.Controls.Add(lbl_GerarExibirCSV);
            pnl_Panel3.Dock = DockStyle.Fill;
            pnl_Panel3.Location = new Point(583, 3);
            pnl_Panel3.Name = "pnl_Panel3";
            pnl_Panel3.Size = new Size(574, 754);
            pnl_Panel3.TabIndex = 14;
            pnl_Panel3.UseWaitCursor = true;
            // 
            // grpAplicarEPlotarPCA
            // 
            grpAplicarEPlotarPCA.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpAplicarEPlotarPCA.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            grpAplicarEPlotarPCA.BackColor = SystemColors.ControlLightLight;
            grpAplicarEPlotarPCA.Controls.Add(lbl_AplicaPCA);
            grpAplicarEPlotarPCA.Controls.Add(btn_AplicarPCA);
            grpAplicarEPlotarPCA.ForeColor = Color.LimeGreen;
            grpAplicarEPlotarPCA.ImeMode = ImeMode.NoControl;
            grpAplicarEPlotarPCA.Location = new Point(7, 614);
            grpAplicarEPlotarPCA.Margin = new Padding(5, 6, 5, 6);
            grpAplicarEPlotarPCA.Name = "grpAplicarEPlotarPCA";
            grpAplicarEPlotarPCA.Padding = new Padding(3, 4, 3, 4);
            grpAplicarEPlotarPCA.Size = new Size(564, 134);
            grpAplicarEPlotarPCA.TabIndex = 10;
            grpAplicarEPlotarPCA.TabStop = false;
            grpAplicarEPlotarPCA.UseWaitCursor = true;
            // 
            // lbl_AplicaPCA
            // 
            lbl_AplicaPCA.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lbl_AplicaPCA.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_AplicaPCA.ForeColor = Color.Black;
            lbl_AplicaPCA.Location = new Point(145, 32);
            lbl_AplicaPCA.Name = "lbl_AplicaPCA";
            lbl_AplicaPCA.Size = new Size(267, 23);
            lbl_AplicaPCA.TabIndex = 7;
            lbl_AplicaPCA.Text = "Aplicar PCA | Plotar dados pelo PCA";
            lbl_AplicaPCA.TextAlign = ContentAlignment.MiddleCenter;
            lbl_AplicaPCA.UseWaitCursor = true;
            // 
            // btn_AplicarPCA
            // 
            btn_AplicarPCA.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btn_AplicarPCA.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btn_AplicarPCA.BackColor = Color.DarkBlue;
            btn_AplicarPCA.BackgroundImageLayout = ImageLayout.Stretch;
            btn_AplicarPCA.Cursor = Cursors.Hand;
            btn_AplicarPCA.FlatAppearance.BorderSize = 0;
            btn_AplicarPCA.FlatAppearance.MouseDownBackColor = Color.FromArgb(64, 64, 64);
            btn_AplicarPCA.FlatStyle = FlatStyle.Flat;
            btn_AplicarPCA.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_AplicarPCA.ForeColor = Color.White;
            btn_AplicarPCA.Location = new Point(8, 84);
            btn_AplicarPCA.Name = "btn_AplicarPCA";
            btn_AplicarPCA.Size = new Size(550, 39);
            btn_AplicarPCA.TabIndex = 7;
            btn_AplicarPCA.Text = "Aplicar";
            btn_AplicarPCA.UseVisualStyleBackColor = false;
            btn_AplicarPCA.UseWaitCursor = true;
            // 
            // grp_GerarEExibirArquivosCsv
            // 
            grp_GerarEExibirArquivosCsv.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grp_GerarEExibirArquivosCsv.Controls.Add(rtx_ArquivosCsv);
            grp_GerarEExibirArquivosCsv.Controls.Add(lbl_ConteudoDoDiretorioCsv);
            grp_GerarEExibirArquivosCsv.Controls.Add(lbl_DiretorioDosArquivosCsv);
            grp_GerarEExibirArquivosCsv.Controls.Add(mtx_DiretorioArquivosCsv);
            grp_GerarEExibirArquivosCsv.Location = new Point(7, 38);
            grp_GerarEExibirArquivosCsv.Name = "grp_GerarEExibirArquivosCsv";
            grp_GerarEExibirArquivosCsv.Size = new Size(564, 567);
            grp_GerarEExibirArquivosCsv.TabIndex = 12;
            grp_GerarEExibirArquivosCsv.TabStop = false;
            grp_GerarEExibirArquivosCsv.UseWaitCursor = true;
            // 
            // rtx_ArquivosCsv
            // 
            rtx_ArquivosCsv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtx_ArquivosCsv.Location = new Point(8, 110);
            rtx_ArquivosCsv.Name = "rtx_ArquivosCsv";
            rtx_ArquivosCsv.ReadOnly = true;
            rtx_ArquivosCsv.Size = new Size(550, 451);
            rtx_ArquivosCsv.TabIndex = 15;
            rtx_ArquivosCsv.Text = "";
            rtx_ArquivosCsv.UseWaitCursor = true;
            // 
            // lbl_ConteudoDoDiretorioCsv
            // 
            lbl_ConteudoDoDiretorioCsv.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lbl_ConteudoDoDiretorioCsv.AutoEllipsis = true;
            lbl_ConteudoDoDiretorioCsv.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_ConteudoDoDiretorioCsv.Location = new Point(183, 86);
            lbl_ConteudoDoDiretorioCsv.Name = "lbl_ConteudoDoDiretorioCsv";
            lbl_ConteudoDoDiretorioCsv.Size = new Size(218, 21);
            lbl_ConteudoDoDiretorioCsv.TabIndex = 14;
            lbl_ConteudoDoDiretorioCsv.Text = "Conteúdo do diretório";
            lbl_ConteudoDoDiretorioCsv.TextAlign = ContentAlignment.MiddleCenter;
            lbl_ConteudoDoDiretorioCsv.UseWaitCursor = true;
            // 
            // lbl_DiretorioDosArquivosCsv
            // 
            lbl_DiretorioDosArquivosCsv.AutoSize = true;
            lbl_DiretorioDosArquivosCsv.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_DiretorioDosArquivosCsv.ForeColor = Color.Black;
            lbl_DiretorioDosArquivosCsv.Location = new Point(6, 39);
            lbl_DiretorioDosArquivosCsv.Name = "lbl_DiretorioDosArquivosCsv";
            lbl_DiretorioDosArquivosCsv.Size = new Size(73, 20);
            lbl_DiretorioDosArquivosCsv.TabIndex = 12;
            lbl_DiretorioDosArquivosCsv.Text = "Diretório:";
            lbl_DiretorioDosArquivosCsv.TextAlign = ContentAlignment.MiddleCenter;
            lbl_DiretorioDosArquivosCsv.UseWaitCursor = true;
            // 
            // mtx_DiretorioArquivosCsv
            // 
            mtx_DiretorioArquivosCsv.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mtx_DiretorioArquivosCsv.Location = new Point(84, 38);
            mtx_DiretorioArquivosCsv.Margin = new Padding(3, 4, 3, 4);
            mtx_DiretorioArquivosCsv.Name = "mtx_DiretorioArquivosCsv";
            mtx_DiretorioArquivosCsv.Size = new Size(474, 23);
            mtx_DiretorioArquivosCsv.TabIndex = 5;
            mtx_DiretorioArquivosCsv.UseWaitCursor = true;
            // 
            // lbl_GerarExibirCSV
            // 
            lbl_GerarExibirCSV.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lbl_GerarExibirCSV.AutoEllipsis = true;
            lbl_GerarExibirCSV.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_GerarExibirCSV.Location = new Point(190, 11);
            lbl_GerarExibirCSV.Name = "lbl_GerarExibirCSV";
            lbl_GerarExibirCSV.Size = new Size(218, 21);
            lbl_GerarExibirCSV.TabIndex = 4;
            lbl_GerarExibirCSV.Text = "Gerar | Exibir arquivos csv";
            lbl_GerarExibirCSV.TextAlign = ContentAlignment.MiddleCenter;
            lbl_GerarExibirCSV.UseWaitCursor = true;
            // 
            // pnl_ComponentesLeituraDosArquivosTxtEMatrizFinal
            // 
            pnl_ComponentesLeituraDosArquivosTxtEMatrizFinal.Controls.Add(grp_LeituraEExibicaoDosArquivosTxt);
            pnl_ComponentesLeituraDosArquivosTxtEMatrizFinal.Controls.Add(lbl_LeituraExibicaoDosArquivosTxt);
            pnl_ComponentesLeituraDosArquivosTxtEMatrizFinal.Controls.Add(pnl_);
            pnl_ComponentesLeituraDosArquivosTxtEMatrizFinal.Controls.Add(grp_GerarArquivoCsvComTodosDados);
            pnl_ComponentesLeituraDosArquivosTxtEMatrizFinal.Dock = DockStyle.Fill;
            pnl_ComponentesLeituraDosArquivosTxtEMatrizFinal.Location = new Point(3, 3);
            pnl_ComponentesLeituraDosArquivosTxtEMatrizFinal.Name = "pnl_ComponentesLeituraDosArquivosTxtEMatrizFinal";
            pnl_ComponentesLeituraDosArquivosTxtEMatrizFinal.Size = new Size(574, 754);
            pnl_ComponentesLeituraDosArquivosTxtEMatrizFinal.TabIndex = 15;
            pnl_ComponentesLeituraDosArquivosTxtEMatrizFinal.UseWaitCursor = true;
            // 
            // grp_LeituraEExibicaoDosArquivosTxt
            // 
            grp_LeituraEExibicaoDosArquivosTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grp_LeituraEExibicaoDosArquivosTxt.Controls.Add(rtx_ArquivosTxt);
            grp_LeituraEExibicaoDosArquivosTxt.Controls.Add(lbl_ConteudoDoDiretorioTxt);
            grp_LeituraEExibicaoDosArquivosTxt.Controls.Add(lbl_DiretorioDosArquivosTxt);
            grp_LeituraEExibicaoDosArquivosTxt.Controls.Add(mtx_DiretorioArquivosTxt);
            grp_LeituraEExibicaoDosArquivosTxt.Controls.Add(btn_AbrirDiretorio);
            grp_LeituraEExibicaoDosArquivosTxt.Location = new Point(5, 38);
            grp_LeituraEExibicaoDosArquivosTxt.Name = "grp_LeituraEExibicaoDosArquivosTxt";
            grp_LeituraEExibicaoDosArquivosTxt.Size = new Size(564, 567);
            grp_LeituraEExibicaoDosArquivosTxt.TabIndex = 11;
            grp_LeituraEExibicaoDosArquivosTxt.TabStop = false;
            grp_LeituraEExibicaoDosArquivosTxt.UseWaitCursor = true;
            // 
            // rtx_ArquivosTxt
            // 
            rtx_ArquivosTxt.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtx_ArquivosTxt.Location = new Point(6, 110);
            rtx_ArquivosTxt.Name = "rtx_ArquivosTxt";
            rtx_ArquivosTxt.ReadOnly = true;
            rtx_ArquivosTxt.Size = new Size(550, 398);
            rtx_ArquivosTxt.TabIndex = 9;
            rtx_ArquivosTxt.Text = "";
            rtx_ArquivosTxt.UseWaitCursor = true;
            // 
            // lbl_ConteudoDoDiretorioTxt
            // 
            lbl_ConteudoDoDiretorioTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lbl_ConteudoDoDiretorioTxt.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_ConteudoDoDiretorioTxt.Location = new Point(187, 89);
            lbl_ConteudoDoDiretorioTxt.Name = "lbl_ConteudoDoDiretorioTxt";
            lbl_ConteudoDoDiretorioTxt.Size = new Size(164, 21);
            lbl_ConteudoDoDiretorioTxt.TabIndex = 8;
            lbl_ConteudoDoDiretorioTxt.Text = "Conteúdo do diretório";
            lbl_ConteudoDoDiretorioTxt.TextAlign = ContentAlignment.MiddleCenter;
            lbl_ConteudoDoDiretorioTxt.UseWaitCursor = true;
            // 
            // lbl_DiretorioDosArquivosTxt
            // 
            lbl_DiretorioDosArquivosTxt.AutoSize = true;
            lbl_DiretorioDosArquivosTxt.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_DiretorioDosArquivosTxt.ForeColor = Color.Black;
            lbl_DiretorioDosArquivosTxt.Location = new Point(6, 39);
            lbl_DiretorioDosArquivosTxt.Name = "lbl_DiretorioDosArquivosTxt";
            lbl_DiretorioDosArquivosTxt.Size = new Size(73, 20);
            lbl_DiretorioDosArquivosTxt.TabIndex = 7;
            lbl_DiretorioDosArquivosTxt.Text = "Diretório:";
            lbl_DiretorioDosArquivosTxt.TextAlign = ContentAlignment.MiddleCenter;
            lbl_DiretorioDosArquivosTxt.UseWaitCursor = true;
            // 
            // mtx_DiretorioArquivosTxt
            // 
            mtx_DiretorioArquivosTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mtx_DiretorioArquivosTxt.Font = new Font("Bookman Old Style", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            mtx_DiretorioArquivosTxt.Location = new Point(84, 39);
            mtx_DiretorioArquivosTxt.Margin = new Padding(3, 4, 3, 4);
            mtx_DiretorioArquivosTxt.Name = "mtx_DiretorioArquivosTxt";
            mtx_DiretorioArquivosTxt.Size = new Size(472, 23);
            mtx_DiretorioArquivosTxt.TabIndex = 1;
            mtx_DiretorioArquivosTxt.UseWaitCursor = true;
            // 
            // btn_AbrirDiretorio
            // 
            btn_AbrirDiretorio.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btn_AbrirDiretorio.BackColor = Color.DarkBlue;
            btn_AbrirDiretorio.BackgroundImageLayout = ImageLayout.Stretch;
            btn_AbrirDiretorio.Cursor = Cursors.Hand;
            btn_AbrirDiretorio.FlatAppearance.BorderSize = 0;
            btn_AbrirDiretorio.FlatAppearance.MouseDownBackColor = Color.FromArgb(64, 64, 64);
            btn_AbrirDiretorio.FlatStyle = FlatStyle.Flat;
            btn_AbrirDiretorio.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold);
            btn_AbrirDiretorio.ForeColor = Color.White;
            btn_AbrirDiretorio.Location = new Point(6, 514);
            btn_AbrirDiretorio.Name = "btn_AbrirDiretorio";
            btn_AbrirDiretorio.Size = new Size(552, 47);
            btn_AbrirDiretorio.TabIndex = 2;
            btn_AbrirDiretorio.Text = "Abrir";
            btn_AbrirDiretorio.UseVisualStyleBackColor = false;
            btn_AbrirDiretorio.UseWaitCursor = true;
            btn_AbrirDiretorio.Click += btnAbrir_Click;
            // 
            // lbl_LeituraExibicaoDosArquivosTxt
            // 
            lbl_LeituraExibicaoDosArquivosTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lbl_LeituraExibicaoDosArquivosTxt.Font = new Font("Microsoft Sans Serif", 12F);
            lbl_LeituraExibicaoDosArquivosTxt.Location = new Point(165, 14);
            lbl_LeituraExibicaoDosArquivosTxt.Name = "lbl_LeituraExibicaoDosArquivosTxt";
            lbl_LeituraExibicaoDosArquivosTxt.Size = new Size(240, 21);
            lbl_LeituraExibicaoDosArquivosTxt.TabIndex = 2;
            lbl_LeituraExibicaoDosArquivosTxt.Text = "Leitura | Exibição dos arquivos txt";
            lbl_LeituraExibicaoDosArquivosTxt.TextAlign = ContentAlignment.MiddleCenter;
            lbl_LeituraExibicaoDosArquivosTxt.UseWaitCursor = true;
            // 
            // pnl_
            // 
            pnl_.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnl_.AutoSize = true;
            pnl_.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnl_.BackColor = Color.Transparent;
            pnl_.ForeColor = Color.Black;
            pnl_.Location = new Point(5, 495);
            pnl_.MaximumSize = new Size(1230, 200);
            pnl_.Name = "pnl_";
            pnl_.Size = new Size(0, 0);
            pnl_.TabIndex = 8;
            pnl_.UseWaitCursor = true;
            // 
            // grp_GerarArquivoCsvComTodosDados
            // 
            grp_GerarArquivoCsvComTodosDados.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grp_GerarArquivoCsvComTodosDados.BackColor = SystemColors.ControlLightLight;
            grp_GerarArquivoCsvComTodosDados.Controls.Add(btn_GerarCSV);
            grp_GerarArquivoCsvComTodosDados.Controls.Add(lbl_GerarArquivoCSV);
            grp_GerarArquivoCsvComTodosDados.Controls.Add(lbl_DiretorioDaMatrizFinal);
            grp_GerarArquivoCsvComTodosDados.Controls.Add(mtx_DiretorioDoArquivoCsvFinal);
            grp_GerarArquivoCsvComTodosDados.FlatStyle = FlatStyle.Popup;
            grp_GerarArquivoCsvComTodosDados.ForeColor = Color.LimeGreen;
            grp_GerarArquivoCsvComTodosDados.ImeMode = ImeMode.On;
            grp_GerarArquivoCsvComTodosDados.Location = new Point(5, 614);
            grp_GerarArquivoCsvComTodosDados.Margin = new Padding(5, 6, 5, 6);
            grp_GerarArquivoCsvComTodosDados.Name = "grp_GerarArquivoCsvComTodosDados";
            grp_GerarArquivoCsvComTodosDados.Padding = new Padding(3, 4, 3, 4);
            grp_GerarArquivoCsvComTodosDados.Size = new Size(564, 134);
            grp_GerarArquivoCsvComTodosDados.TabIndex = 3;
            grp_GerarArquivoCsvComTodosDados.TabStop = false;
            grp_GerarArquivoCsvComTodosDados.UseWaitCursor = true;
            // 
            // btn_GerarCSV
            // 
            btn_GerarCSV.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btn_GerarCSV.BackColor = Color.DarkBlue;
            btn_GerarCSV.BackgroundImageLayout = ImageLayout.Stretch;
            btn_GerarCSV.Cursor = Cursors.Hand;
            btn_GerarCSV.FlatAppearance.BorderSize = 0;
            btn_GerarCSV.FlatAppearance.MouseDownBackColor = Color.FromArgb(64, 64, 64);
            btn_GerarCSV.FlatStyle = FlatStyle.Flat;
            btn_GerarCSV.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_GerarCSV.ForeColor = Color.White;
            btn_GerarCSV.Location = new Point(6, 84);
            btn_GerarCSV.Name = "btn_GerarCSV";
            btn_GerarCSV.Size = new Size(552, 39);
            btn_GerarCSV.TabIndex = 4;
            btn_GerarCSV.Text = "Gerar";
            btn_GerarCSV.UseVisualStyleBackColor = false;
            btn_GerarCSV.UseWaitCursor = true;
            btn_GerarCSV.Click += btn_GerarCSV_Click;
            // 
            // lbl_GerarArquivoCSV
            // 
            lbl_GerarArquivoCSV.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lbl_GerarArquivoCSV.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_GerarArquivoCSV.ForeColor = Color.Black;
            lbl_GerarArquivoCSV.Location = new Point(138, 20);
            lbl_GerarArquivoCSV.Name = "lbl_GerarArquivoCSV";
            lbl_GerarArquivoCSV.Size = new Size(288, 21);
            lbl_GerarArquivoCSV.TabIndex = 6;
            lbl_GerarArquivoCSV.Text = "Gerar o arquivo csv com todos os dados";
            lbl_GerarArquivoCSV.TextAlign = ContentAlignment.MiddleCenter;
            lbl_GerarArquivoCSV.UseWaitCursor = true;
            // 
            // lbl_DiretorioDaMatrizFinal
            // 
            lbl_DiretorioDaMatrizFinal.AutoSize = true;
            lbl_DiretorioDaMatrizFinal.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_DiretorioDaMatrizFinal.ForeColor = Color.Black;
            lbl_DiretorioDaMatrizFinal.Location = new Point(6, 51);
            lbl_DiretorioDaMatrizFinal.Name = "lbl_DiretorioDaMatrizFinal";
            lbl_DiretorioDaMatrizFinal.Size = new Size(72, 20);
            lbl_DiretorioDaMatrizFinal.TabIndex = 3;
            lbl_DiretorioDaMatrizFinal.Text = "Diretório:";
            lbl_DiretorioDaMatrizFinal.TextAlign = ContentAlignment.MiddleCenter;
            lbl_DiretorioDaMatrizFinal.UseWaitCursor = true;
            // 
            // mtx_DiretorioDoArquivoCsvFinal
            // 
            mtx_DiretorioDoArquivoCsvFinal.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mtx_DiretorioDoArquivoCsvFinal.Location = new Point(84, 51);
            mtx_DiretorioDoArquivoCsvFinal.Margin = new Padding(3, 4, 3, 4);
            mtx_DiretorioDoArquivoCsvFinal.Name = "mtx_DiretorioDoArquivoCsvFinal";
            mtx_DiretorioDoArquivoCsvFinal.Size = new Size(472, 23);
            mtx_DiretorioDoArquivoCsvFinal.TabIndex = 3;
            mtx_DiretorioDoArquivoCsvFinal.UseWaitCursor = true;
            // 
            // tlp_FundoDosComponentes
            // 
            tlp_FundoDosComponentes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tlp_FundoDosComponentes.ColumnCount = 2;
            tlp_FundoDosComponentes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlp_FundoDosComponentes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlp_FundoDosComponentes.Controls.Add(pnl_ComponentesLeituraDosArquivosTxtEMatrizFinal, 0, 0);
            tlp_FundoDosComponentes.Controls.Add(pnl_Panel3, 1, 0);
            tlp_FundoDosComponentes.Location = new Point(12, 35);
            tlp_FundoDosComponentes.Name = "tlp_FundoDosComponentes";
            tlp_FundoDosComponentes.RowCount = 1;
            tlp_FundoDosComponentes.RowStyles.Add(new RowStyle());
            tlp_FundoDosComponentes.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlp_FundoDosComponentes.Size = new Size(1160, 760);
            tlp_FundoDosComponentes.TabIndex = 16;
            tlp_FundoDosComponentes.UseWaitCursor = true;
            // 
            // FrmPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(1184, 807);
            Controls.Add(tlp_FundoDosComponentes);
            Controls.Add(lbl_NomeDaAplicacao);
            Font = new Font("Sitka Text", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(1078, 846);
            Name = "FrmPrincipal";
            RightToLeftLayout = true;
            Text = "Análise de Componentes Principais (PCA)";
            TopMost = true;
            UseWaitCursor = true;
            Load += FormularioPrincipal_Load;
            pnl_Panel3.ResumeLayout(false);
            grpAplicarEPlotarPCA.ResumeLayout(false);
            grp_GerarEExibirArquivosCsv.ResumeLayout(false);
            grp_GerarEExibirArquivosCsv.PerformLayout();
            pnl_ComponentesLeituraDosArquivosTxtEMatrizFinal.ResumeLayout(false);
            pnl_ComponentesLeituraDosArquivosTxtEMatrizFinal.PerformLayout();
            grp_LeituraEExibicaoDosArquivosTxt.ResumeLayout(false);
            grp_LeituraEExibicaoDosArquivosTxt.PerformLayout();
            grp_GerarArquivoCsvComTodosDados.ResumeLayout(false);
            grp_GerarArquivoCsvComTodosDados.PerformLayout();
            tlp_FundoDosComponentes.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Label lbl_NomeDaAplicacao;
        private Panel pnl_Panel3;
        private GroupBox grpAplicarEPlotarPCA;
        private Label lbl_AplicaPCA;
        private Button btn_AplicarPCA;
        private GroupBox grp_GerarEExibirArquivosCsv;
        private Label lbl_DiretorioDosArquivosCsv;
        private MaskedTextBox mtx_DiretorioArquivosCsv;
        private Label lbl_GerarExibirCSV;
        private Panel pnl_ComponentesLeituraDosArquivosTxtEMatrizFinal;
        private GroupBox grp_LeituraEExibicaoDosArquivosTxt;
        private Label lbl_ConteudoDoDiretorioTxt;
        private Label lbl_DiretorioDosArquivosTxt;
        private MaskedTextBox mtx_DiretorioArquivosTxt;
        private Button btn_AbrirDiretorio;
        private Label lbl_LeituraExibicaoDosArquivosTxt;
        private Panel pnl_;
        private GroupBox grp_GerarArquivoCsvComTodosDados;
        private Button btn_GerarCSV;
        private Label lbl_GerarArquivoCSV;
        private Label lbl_DiretorioDaMatrizFinal;
        private MaskedTextBox mtx_DiretorioDoArquivoCsvFinal;
        private TableLayoutPanel tlp_FundoDosComponentes;
        private Label lbl_ConteudoDoDiretorioCsv;
        private RichTextBox rtx_ArquivosTxt;
        private RichTextBox rtx_ArquivosCsv;
    }
}
