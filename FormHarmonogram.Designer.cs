namespace Utrzymanie_Ruchu___APP_Niepruszewo
{
    partial class FormHarmonogram
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHarmonogram));
            this.harmonogram_tabela = new System.Windows.Forms.DataGridView();
            this.Karta = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Styczeń = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Luty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Marzec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kwiecień = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Maj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Czerwiec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lipiec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sierpień = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Wrzesień = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pażdziernik = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Listopad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Grudzień = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lata_combo = new System.Windows.Forms.ComboBox();
            this.wyswietlacz = new System.Windows.Forms.GroupBox();
            this.btn_generuj = new System.Windows.Forms.Button();
            this.btn_edytuj = new System.Windows.Forms.Button();
            this.btn_zapisz = new System.Windows.Forms.Button();
            this.btn_anuluj = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.wybierz_rok_lbl = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.usun_btn = new System.Windows.Forms.Button();
            this.przeglady_spc_listView = new System.Windows.Forms.ListView();
            this.syg = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lin = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.masz = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.kart = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sta = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.doda_nowy_box = new System.Windows.Forms.GroupBox();
            this.spc_dodaj_btn = new System.Windows.Forms.Button();
            this.spc_powtorz_combo = new System.Windows.Forms.ComboBox();
            this.powt_lbl = new System.Windows.Forms.Label();
            this.spc_co_combo = new System.Windows.Forms.ComboBox();
            this.co_lbl = new System.Windows.Forms.Label();
            this.spc_cykl_check = new System.Windows.Forms.CheckBox();
            this.spc_termin_combo = new System.Windows.Forms.ComboBox();
            this.scp_termin_lbl = new System.Windows.Forms.Label();
            this.spc_karta_combo = new System.Windows.Forms.ComboBox();
            this.spc_karta_lbl = new System.Windows.Forms.Label();
            this.spc_linia_combo = new System.Windows.Forms.ComboBox();
            this.spc_linia_lbl = new System.Windows.Forms.Label();
            this.spc_maszyna_combo = new System.Windows.Forms.ComboBox();
            this.spc_maszyna_lbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.harmonogram_tabela)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.doda_nowy_box.SuspendLayout();
            this.SuspendLayout();
            // 
            // harmonogram_tabela
            // 
            this.harmonogram_tabela.AllowUserToAddRows = false;
            this.harmonogram_tabela.AllowUserToDeleteRows = false;
            this.harmonogram_tabela.AllowUserToResizeColumns = false;
            this.harmonogram_tabela.AllowUserToResizeRows = false;
            this.harmonogram_tabela.BackgroundColor = System.Drawing.SystemColors.Window;
            this.harmonogram_tabela.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.harmonogram_tabela.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.harmonogram_tabela.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.harmonogram_tabela.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Karta,
            this.Styczeń,
            this.Luty,
            this.Marzec,
            this.Kwiecień,
            this.Maj,
            this.Czerwiec,
            this.Lipiec,
            this.Sierpień,
            this.Wrzesień,
            this.Pażdziernik,
            this.Listopad,
            this.Grudzień});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.harmonogram_tabela.DefaultCellStyle = dataGridViewCellStyle3;
            this.harmonogram_tabela.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.harmonogram_tabela.EnableHeadersVisualStyles = false;
            this.harmonogram_tabela.Location = new System.Drawing.Point(17, 101);
            this.harmonogram_tabela.MultiSelect = false;
            this.harmonogram_tabela.Name = "harmonogram_tabela";
            this.harmonogram_tabela.ReadOnly = true;
            this.harmonogram_tabela.RowHeadersWidth = 250;
            this.harmonogram_tabela.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.harmonogram_tabela.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.harmonogram_tabela.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.harmonogram_tabela.ShowCellErrors = false;
            this.harmonogram_tabela.ShowCellToolTips = false;
            this.harmonogram_tabela.ShowEditingIcon = false;
            this.harmonogram_tabela.ShowRowErrors = false;
            this.harmonogram_tabela.Size = new System.Drawing.Size(1111, 764);
            this.harmonogram_tabela.TabIndex = 0;
            this.harmonogram_tabela.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.harmonogram_tabela_CellEndEdit);
            // 
            // Karta
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Karta.DefaultCellStyle = dataGridViewCellStyle2;
            this.Karta.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.Karta.DisplayStyleForCurrentCellOnly = true;
            this.Karta.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Karta.HeaderText = "Karta Kontroli";
            this.Karta.Items.AddRange(new object[] {
            "fg",
            "vdbbb"});
            this.Karta.Name = "Karta";
            this.Karta.ReadOnly = true;
            this.Karta.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Karta.Width = 120;
            // 
            // Styczeń
            // 
            this.Styczeń.HeaderText = "Sty";
            this.Styczeń.Name = "Styczeń";
            this.Styczeń.ReadOnly = true;
            this.Styczeń.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Styczeń.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Styczeń.ToolTipText = "tekst";
            this.Styczeń.Width = 60;
            // 
            // Luty
            // 
            this.Luty.HeaderText = "Lut";
            this.Luty.Name = "Luty";
            this.Luty.ReadOnly = true;
            this.Luty.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Luty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Luty.Width = 60;
            // 
            // Marzec
            // 
            this.Marzec.HeaderText = "Mar";
            this.Marzec.Name = "Marzec";
            this.Marzec.ReadOnly = true;
            this.Marzec.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Marzec.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Marzec.Width = 60;
            // 
            // Kwiecień
            // 
            this.Kwiecień.HeaderText = "Kwi";
            this.Kwiecień.Name = "Kwiecień";
            this.Kwiecień.ReadOnly = true;
            this.Kwiecień.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Kwiecień.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Kwiecień.Width = 60;
            // 
            // Maj
            // 
            this.Maj.HeaderText = "Maj";
            this.Maj.Name = "Maj";
            this.Maj.ReadOnly = true;
            this.Maj.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Maj.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Maj.Width = 60;
            // 
            // Czerwiec
            // 
            this.Czerwiec.HeaderText = "Cze";
            this.Czerwiec.Name = "Czerwiec";
            this.Czerwiec.ReadOnly = true;
            this.Czerwiec.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Czerwiec.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Czerwiec.Width = 60;
            // 
            // Lipiec
            // 
            this.Lipiec.HeaderText = "Lip";
            this.Lipiec.Name = "Lipiec";
            this.Lipiec.ReadOnly = true;
            this.Lipiec.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Lipiec.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Lipiec.Width = 60;
            // 
            // Sierpień
            // 
            this.Sierpień.HeaderText = "Sie";
            this.Sierpień.Name = "Sierpień";
            this.Sierpień.ReadOnly = true;
            this.Sierpień.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Sierpień.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Sierpień.Width = 60;
            // 
            // Wrzesień
            // 
            this.Wrzesień.HeaderText = "Wrz";
            this.Wrzesień.Name = "Wrzesień";
            this.Wrzesień.ReadOnly = true;
            this.Wrzesień.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Wrzesień.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Wrzesień.Width = 60;
            // 
            // Pażdziernik
            // 
            this.Pażdziernik.HeaderText = "Paż";
            this.Pażdziernik.Name = "Pażdziernik";
            this.Pażdziernik.ReadOnly = true;
            this.Pażdziernik.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Pażdziernik.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Pażdziernik.Width = 60;
            // 
            // Listopad
            // 
            this.Listopad.HeaderText = "Lis";
            this.Listopad.Name = "Listopad";
            this.Listopad.ReadOnly = true;
            this.Listopad.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Listopad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Listopad.Width = 60;
            // 
            // Grudzień
            // 
            this.Grudzień.HeaderText = "Gru";
            this.Grudzień.Name = "Grudzień";
            this.Grudzień.ReadOnly = true;
            this.Grudzień.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Grudzień.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Grudzień.Width = 60;
            // 
            // lata_combo
            // 
            this.lata_combo.FormattingEnabled = true;
            this.lata_combo.Location = new System.Drawing.Point(25, 39);
            this.lata_combo.Name = "lata_combo";
            this.lata_combo.Size = new System.Drawing.Size(152, 21);
            this.lata_combo.TabIndex = 4;
            this.lata_combo.SelectedIndexChanged += new System.EventHandler(this.lata_combo_SelectedIndexChanged);
            // 
            // wyswietlacz
            // 
            this.wyswietlacz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.wyswietlacz.Location = new System.Drawing.Point(385, 3);
            this.wyswietlacz.Margin = new System.Windows.Forms.Padding(0);
            this.wyswietlacz.Name = "wyswietlacz";
            this.wyswietlacz.Padding = new System.Windows.Forms.Padding(0);
            this.wyswietlacz.Size = new System.Drawing.Size(722, 120);
            this.wyswietlacz.TabIndex = 5;
            this.wyswietlacz.TabStop = false;
            // 
            // btn_generuj
            // 
            this.btn_generuj.Enabled = false;
            this.btn_generuj.Location = new System.Drawing.Point(25, 68);
            this.btn_generuj.Name = "btn_generuj";
            this.btn_generuj.Size = new System.Drawing.Size(133, 23);
            this.btn_generuj.TabIndex = 6;
            this.btn_generuj.Text = "Generuj nowy rok";
            this.btn_generuj.UseVisualStyleBackColor = true;
            this.btn_generuj.Visible = false;
            this.btn_generuj.Click += new System.EventHandler(this.btn_generuj_Click);
            // 
            // btn_edytuj
            // 
            this.btn_edytuj.Enabled = false;
            this.btn_edytuj.Location = new System.Drawing.Point(183, 37);
            this.btn_edytuj.Name = "btn_edytuj";
            this.btn_edytuj.Size = new System.Drawing.Size(137, 23);
            this.btn_edytuj.TabIndex = 7;
            this.btn_edytuj.Text = "Edytuj harmonogram";
            this.btn_edytuj.UseVisualStyleBackColor = true;
            this.btn_edytuj.Click += new System.EventHandler(this.btn_edycaj_Click);
            // 
            // btn_zapisz
            // 
            this.btn_zapisz.Enabled = false;
            this.btn_zapisz.Location = new System.Drawing.Point(245, 68);
            this.btn_zapisz.Name = "btn_zapisz";
            this.btn_zapisz.Size = new System.Drawing.Size(75, 23);
            this.btn_zapisz.TabIndex = 8;
            this.btn_zapisz.Text = "Zapisz";
            this.btn_zapisz.UseVisualStyleBackColor = true;
            this.btn_zapisz.Visible = false;
            this.btn_zapisz.Click += new System.EventHandler(this.btn_zapisz_Click);
            // 
            // btn_anuluj
            // 
            this.btn_anuluj.Enabled = false;
            this.btn_anuluj.Location = new System.Drawing.Point(164, 68);
            this.btn_anuluj.Name = "btn_anuluj";
            this.btn_anuluj.Size = new System.Drawing.Size(75, 23);
            this.btn_anuluj.TabIndex = 9;
            this.btn_anuluj.Text = "Anuluj";
            this.btn_anuluj.UseVisualStyleBackColor = true;
            this.btn_anuluj.Visible = false;
            this.btn_anuluj.Click += new System.EventHandler(this.btn_anuluj_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1136, 898);
            this.tabControl1.TabIndex = 10;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.wybierz_rok_lbl);
            this.tabPage1.Controls.Add(this.wyswietlacz);
            this.tabPage1.Controls.Add(this.lata_combo);
            this.tabPage1.Controls.Add(this.harmonogram_tabela);
            this.tabPage1.Controls.Add(this.btn_anuluj);
            this.tabPage1.Controls.Add(this.btn_zapisz);
            this.tabPage1.Controls.Add(this.btn_generuj);
            this.tabPage1.Controls.Add(this.btn_edytuj);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1128, 872);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Harmonogram stały";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // wybierz_rok_lbl
            // 
            this.wybierz_rok_lbl.AutoSize = true;
            this.wybierz_rok_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.wybierz_rok_lbl.Location = new System.Drawing.Point(22, 26);
            this.wybierz_rok_lbl.Name = "wybierz_rok_lbl";
            this.wybierz_rok_lbl.Size = new System.Drawing.Size(74, 13);
            this.wybierz_rok_lbl.TabIndex = 10;
            this.wybierz_rok_lbl.Text = "Wybierz rok";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.doda_nowy_box);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1128, 1008);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Przeglądy dodatkowe";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.usun_btn);
            this.groupBox1.Controls.Add(this.przeglady_spc_listView);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox1.Location = new System.Drawing.Point(17, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(732, 457);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Zaplanowane przeglądy specjalne";
            // 
            // usun_btn
            // 
            this.usun_btn.Enabled = false;
            this.usun_btn.Location = new System.Drawing.Point(635, 413);
            this.usun_btn.Name = "usun_btn";
            this.usun_btn.Size = new System.Drawing.Size(75, 23);
            this.usun_btn.TabIndex = 5;
            this.usun_btn.Text = "Usuń";
            this.usun_btn.UseVisualStyleBackColor = true;
            this.usun_btn.Click += new System.EventHandler(this.usun_btn_Click);
            // 
            // przeglady_spc_listView
            // 
            this.przeglady_spc_listView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.przeglady_spc_listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.syg,
            this.lin,
            this.masz,
            this.kart,
            this.sta});
            this.przeglady_spc_listView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.przeglady_spc_listView.FullRowSelect = true;
            this.przeglady_spc_listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.przeglady_spc_listView.HideSelection = false;
            this.przeglady_spc_listView.LabelWrap = false;
            this.przeglady_spc_listView.Location = new System.Drawing.Point(35, 30);
            this.przeglady_spc_listView.MultiSelect = false;
            this.przeglady_spc_listView.Name = "przeglady_spc_listView";
            this.przeglady_spc_listView.Size = new System.Drawing.Size(675, 362);
            this.przeglady_spc_listView.TabIndex = 3;
            this.przeglady_spc_listView.UseCompatibleStateImageBehavior = false;
            this.przeglady_spc_listView.View = System.Windows.Forms.View.Details;
            // 
            // syg
            // 
            this.syg.Text = "Sygnatura";
            this.syg.Width = 128;
            // 
            // lin
            // 
            this.lin.Text = "Linia";
            this.lin.Width = 100;
            // 
            // masz
            // 
            this.masz.Text = "Maszyna";
            this.masz.Width = 194;
            // 
            // kart
            // 
            this.kart.Text = "Karta kontroli";
            this.kart.Width = 120;
            // 
            // sta
            // 
            this.sta.Text = "Status";
            this.sta.Width = 122;
            // 
            // doda_nowy_box
            // 
            this.doda_nowy_box.Controls.Add(this.spc_dodaj_btn);
            this.doda_nowy_box.Controls.Add(this.spc_powtorz_combo);
            this.doda_nowy_box.Controls.Add(this.powt_lbl);
            this.doda_nowy_box.Controls.Add(this.spc_co_combo);
            this.doda_nowy_box.Controls.Add(this.co_lbl);
            this.doda_nowy_box.Controls.Add(this.spc_cykl_check);
            this.doda_nowy_box.Controls.Add(this.spc_termin_combo);
            this.doda_nowy_box.Controls.Add(this.scp_termin_lbl);
            this.doda_nowy_box.Controls.Add(this.spc_karta_combo);
            this.doda_nowy_box.Controls.Add(this.spc_karta_lbl);
            this.doda_nowy_box.Controls.Add(this.spc_linia_combo);
            this.doda_nowy_box.Controls.Add(this.spc_linia_lbl);
            this.doda_nowy_box.Controls.Add(this.spc_maszyna_combo);
            this.doda_nowy_box.Controls.Add(this.spc_maszyna_lbl);
            this.doda_nowy_box.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.doda_nowy_box.Location = new System.Drawing.Point(755, 15);
            this.doda_nowy_box.Name = "doda_nowy_box";
            this.doda_nowy_box.Size = new System.Drawing.Size(256, 457);
            this.doda_nowy_box.TabIndex = 0;
            this.doda_nowy_box.TabStop = false;
            this.doda_nowy_box.Text = "Dodaj przegląd specjalny";
            // 
            // spc_dodaj_btn
            // 
            this.spc_dodaj_btn.Enabled = false;
            this.spc_dodaj_btn.Location = new System.Drawing.Point(144, 413);
            this.spc_dodaj_btn.Name = "spc_dodaj_btn";
            this.spc_dodaj_btn.Size = new System.Drawing.Size(75, 23);
            this.spc_dodaj_btn.TabIndex = 19;
            this.spc_dodaj_btn.Text = "Dodaj";
            this.spc_dodaj_btn.UseVisualStyleBackColor = true;
            this.spc_dodaj_btn.Click += new System.EventHandler(this.spc_dodaj_btn_Click);
            // 
            // spc_powtorz_combo
            // 
            this.spc_powtorz_combo.BackColor = System.Drawing.SystemColors.Control;
            this.spc_powtorz_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.spc_powtorz_combo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.spc_powtorz_combo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.spc_powtorz_combo.Items.AddRange(new object[] {
            "2 razy",
            "3 razy",
            "4 razy"});
            this.spc_powtorz_combo.Location = new System.Drawing.Point(23, 357);
            this.spc_powtorz_combo.Name = "spc_powtorz_combo";
            this.spc_powtorz_combo.Size = new System.Drawing.Size(196, 24);
            this.spc_powtorz_combo.TabIndex = 17;
            this.spc_powtorz_combo.Visible = false;
            this.spc_powtorz_combo.SelectedIndexChanged += new System.EventHandler(this.spc_powtorz_combo_SelectedIndexChanged);
            // 
            // powt_lbl
            // 
            this.powt_lbl.AutoSize = true;
            this.powt_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.powt_lbl.Location = new System.Drawing.Point(19, 345);
            this.powt_lbl.Name = "powt_lbl";
            this.powt_lbl.Size = new System.Drawing.Size(52, 13);
            this.powt_lbl.TabIndex = 18;
            this.powt_lbl.Text = "Powtórz";
            this.powt_lbl.Visible = false;
            // 
            // spc_co_combo
            // 
            this.spc_co_combo.BackColor = System.Drawing.SystemColors.Control;
            this.spc_co_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.spc_co_combo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.spc_co_combo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.spc_co_combo.Items.AddRange(new object[] {
            "1 Tydzień",
            "2 Tygodnie",
            "1 Miesiąc",
            "3 Miesiące",
            "6 Miesięcy",
            "1 Rok"});
            this.spc_co_combo.Location = new System.Drawing.Point(23, 303);
            this.spc_co_combo.Name = "spc_co_combo";
            this.spc_co_combo.Size = new System.Drawing.Size(196, 24);
            this.spc_co_combo.TabIndex = 15;
            this.spc_co_combo.Visible = false;
            this.spc_co_combo.SelectedIndexChanged += new System.EventHandler(this.spc_co_combo_SelectedIndexChanged);
            // 
            // co_lbl
            // 
            this.co_lbl.AutoSize = true;
            this.co_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.co_lbl.Location = new System.Drawing.Point(19, 291);
            this.co_lbl.Name = "co_lbl";
            this.co_lbl.Size = new System.Drawing.Size(22, 13);
            this.co_lbl.TabIndex = 16;
            this.co_lbl.Text = "Co";
            this.co_lbl.Visible = false;
            // 
            // spc_cykl_check
            // 
            this.spc_cykl_check.AutoSize = true;
            this.spc_cykl_check.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.spc_cykl_check.Location = new System.Drawing.Point(23, 261);
            this.spc_cykl_check.Name = "spc_cykl_check";
            this.spc_cykl_check.Size = new System.Drawing.Size(79, 17);
            this.spc_cykl_check.TabIndex = 14;
            this.spc_cykl_check.Text = "Cykliczny";
            this.spc_cykl_check.UseVisualStyleBackColor = true;
            this.spc_cykl_check.CheckedChanged += new System.EventHandler(this.spc_cykl_check_CheckedChanged);
            // 
            // spc_termin_combo
            // 
            this.spc_termin_combo.BackColor = System.Drawing.SystemColors.Control;
            this.spc_termin_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.spc_termin_combo.Enabled = false;
            this.spc_termin_combo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.spc_termin_combo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.spc_termin_combo.Items.AddRange(new object[] {
            "Od razu",
            "W przyszłym tygodniu",
            "Za 2 tygodnie",
            "Za miesiąc"});
            this.spc_termin_combo.Location = new System.Drawing.Point(22, 213);
            this.spc_termin_combo.Name = "spc_termin_combo";
            this.spc_termin_combo.Size = new System.Drawing.Size(196, 24);
            this.spc_termin_combo.TabIndex = 12;
            this.spc_termin_combo.SelectedIndexChanged += new System.EventHandler(this.spc_termin_combo_SelectedIndexChanged);
            // 
            // scp_termin_lbl
            // 
            this.scp_termin_lbl.AutoSize = true;
            this.scp_termin_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.scp_termin_lbl.Location = new System.Drawing.Point(18, 201);
            this.scp_termin_lbl.Name = "scp_termin_lbl";
            this.scp_termin_lbl.Size = new System.Drawing.Size(45, 13);
            this.scp_termin_lbl.TabIndex = 13;
            this.scp_termin_lbl.Text = "Termin";
            // 
            // spc_karta_combo
            // 
            this.spc_karta_combo.BackColor = System.Drawing.SystemColors.Control;
            this.spc_karta_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.spc_karta_combo.Enabled = false;
            this.spc_karta_combo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.spc_karta_combo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.spc_karta_combo.Location = new System.Drawing.Point(23, 154);
            this.spc_karta_combo.Name = "spc_karta_combo";
            this.spc_karta_combo.Size = new System.Drawing.Size(196, 24);
            this.spc_karta_combo.TabIndex = 10;
            this.spc_karta_combo.SelectedIndexChanged += new System.EventHandler(this.spc_karta_combo_SelectedIndexChanged);
            // 
            // spc_karta_lbl
            // 
            this.spc_karta_lbl.AutoSize = true;
            this.spc_karta_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.spc_karta_lbl.Location = new System.Drawing.Point(19, 142);
            this.spc_karta_lbl.Name = "spc_karta_lbl";
            this.spc_karta_lbl.Size = new System.Drawing.Size(83, 13);
            this.spc_karta_lbl.TabIndex = 11;
            this.spc_karta_lbl.Text = "Karta kontroli";
            // 
            // spc_linia_combo
            // 
            this.spc_linia_combo.BackColor = System.Drawing.SystemColors.Control;
            this.spc_linia_combo.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.spc_linia_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.spc_linia_combo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.spc_linia_combo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.spc_linia_combo.Location = new System.Drawing.Point(22, 42);
            this.spc_linia_combo.Name = "spc_linia_combo";
            this.spc_linia_combo.Size = new System.Drawing.Size(196, 24);
            this.spc_linia_combo.TabIndex = 6;
            this.spc_linia_combo.SelectedIndexChanged += new System.EventHandler(this.spc_linia_combo_SelectedIndexChanged);
            // 
            // spc_linia_lbl
            // 
            this.spc_linia_lbl.AutoSize = true;
            this.spc_linia_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.spc_linia_lbl.Location = new System.Drawing.Point(18, 30);
            this.spc_linia_lbl.Name = "spc_linia_lbl";
            this.spc_linia_lbl.Size = new System.Drawing.Size(34, 13);
            this.spc_linia_lbl.TabIndex = 7;
            this.spc_linia_lbl.Text = "Linia";
            // 
            // spc_maszyna_combo
            // 
            this.spc_maszyna_combo.BackColor = System.Drawing.SystemColors.Control;
            this.spc_maszyna_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.spc_maszyna_combo.Enabled = false;
            this.spc_maszyna_combo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.spc_maszyna_combo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.spc_maszyna_combo.Location = new System.Drawing.Point(22, 95);
            this.spc_maszyna_combo.Name = "spc_maszyna_combo";
            this.spc_maszyna_combo.Size = new System.Drawing.Size(196, 24);
            this.spc_maszyna_combo.TabIndex = 8;
            this.spc_maszyna_combo.SelectedIndexChanged += new System.EventHandler(this.spc_maszyna_combo_SelectedIndexChanged);
            // 
            // spc_maszyna_lbl
            // 
            this.spc_maszyna_lbl.AutoSize = true;
            this.spc_maszyna_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.spc_maszyna_lbl.Location = new System.Drawing.Point(18, 83);
            this.spc_maszyna_lbl.Name = "spc_maszyna_lbl";
            this.spc_maszyna_lbl.Size = new System.Drawing.Size(56, 13);
            this.spc_maszyna_lbl.TabIndex = 9;
            this.spc_maszyna_lbl.Text = "Maszyna";
            // 
            // FormHarmonogram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1143, 912);
            this.Controls.Add(this.tabControl1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormHarmonogram";
            this.Text = "Harmonogram Przegladow";
            ((System.ComponentModel.ISupportInitialize)(this.harmonogram_tabela)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.doda_nowy_box.ResumeLayout(false);
            this.doda_nowy_box.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView harmonogram_tabela;
        private System.Windows.Forms.ComboBox lata_combo;
        private System.Windows.Forms.DataGridViewComboBoxColumn Karta;
        private System.Windows.Forms.DataGridViewTextBoxColumn Styczeń;
        private System.Windows.Forms.DataGridViewTextBoxColumn Luty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Marzec;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kwiecień;
        private System.Windows.Forms.DataGridViewTextBoxColumn Maj;
        private System.Windows.Forms.DataGridViewTextBoxColumn Czerwiec;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lipiec;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sierpień;
        private System.Windows.Forms.DataGridViewTextBoxColumn Wrzesień;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pażdziernik;
        private System.Windows.Forms.DataGridViewTextBoxColumn Listopad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Grudzień;
        private System.Windows.Forms.GroupBox wyswietlacz;
        private System.Windows.Forms.Button btn_generuj;
        private System.Windows.Forms.Button btn_edytuj;
        private System.Windows.Forms.Button btn_zapisz;
        private System.Windows.Forms.Button btn_anuluj;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label wybierz_rok_lbl;
        private System.Windows.Forms.GroupBox doda_nowy_box;
        private System.Windows.Forms.Button spc_dodaj_btn;
        private System.Windows.Forms.ComboBox spc_powtorz_combo;
        private System.Windows.Forms.Label powt_lbl;
        private System.Windows.Forms.ComboBox spc_co_combo;
        private System.Windows.Forms.Label co_lbl;
        private System.Windows.Forms.CheckBox spc_cykl_check;
        private System.Windows.Forms.ComboBox spc_termin_combo;
        private System.Windows.Forms.Label scp_termin_lbl;
        private System.Windows.Forms.ComboBox spc_karta_combo;
        private System.Windows.Forms.Label spc_karta_lbl;
        private System.Windows.Forms.ComboBox spc_linia_combo;
        private System.Windows.Forms.Label spc_linia_lbl;
        private System.Windows.Forms.ComboBox spc_maszyna_combo;
        private System.Windows.Forms.Label spc_maszyna_lbl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView przeglady_spc_listView;
        private System.Windows.Forms.ColumnHeader syg;
        private System.Windows.Forms.ColumnHeader lin;
        private System.Windows.Forms.ColumnHeader masz;
        private System.Windows.Forms.ColumnHeader sta;
        private System.Windows.Forms.ColumnHeader kart;
        private System.Windows.Forms.Button usun_btn;
    }
}