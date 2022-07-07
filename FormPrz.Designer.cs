namespace Utrzymanie_Ruchu___APP_Niepruszewo
{
    partial class FormPrz
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrz));
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Zadanie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Wynik = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Komentarz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.s_karta_lbl = new System.Windows.Forms.Label();
            this.karta_lbl = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.s_wynik_lbl = new System.Windows.Forms.Label();
            this.zatwierdzony_lbl = new System.Windows.Forms.Label();
            this.s_sygnatura_lbl = new System.Windows.Forms.Label();
            this.s_linia_lbl = new System.Windows.Forms.Label();
            this.maszyna_lbl = new System.Windows.Forms.Label();
            this.linia_lbl = new System.Windows.Forms.Label();
            this.s_maszyna_lbl = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.docx_btn = new System.Windows.Forms.Button();
            this.zatwierdzil_combo = new System.Windows.Forms.ComboBox();
            this.zatwierdzil_lbl = new System.Windows.Forms.Label();
            this.mycie_wykonano_check = new System.Windows.Forms.CheckBox();
            this.mycie_lbl = new System.Windows.Forms.Label();
            this.mycie_zlecono_check = new System.Windows.Forms.CheckBox();
            this.Zapisz_btn = new System.Windows.Forms.Button();
            this.wykonał_combo = new System.Windows.Forms.ComboBox();
            this.nowy_postoj_wprowadzil_lbl = new System.Windows.Forms.Label();
            this.data_lbl = new System.Windows.Forms.Label();
            this.przeglady_data_DTPicker = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.button1.Location = new System.Drawing.Point(293, 94);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 26);
            this.button1.TabIndex = 0;
            this.button1.Text = "Anuluj";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 40;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Zadanie,
            this.Wynik,
            this.Komentarz});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.Location = new System.Drawing.Point(5, 147);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(5);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 50;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(961, 449);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseDoubleClick);
            // 
            // Zadanie
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(1);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Zadanie.DefaultCellStyle = dataGridViewCellStyle2;
            this.Zadanie.FillWeight = 7.614212F;
            this.Zadanie.HeaderText = "Opis czynności kontrolnej:";
            this.Zadanie.Name = "Zadanie";
            this.Zadanie.ReadOnly = true;
            this.Zadanie.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Zadanie.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Zadanie.Width = 485;
            // 
            // Wynik
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.Wynik.DefaultCellStyle = dataGridViewCellStyle3;
            this.Wynik.DisplayStyleForCurrentCellOnly = true;
            this.Wynik.FillWeight = 7.614212F;
            this.Wynik.HeaderText = "Wynik";
            this.Wynik.Items.AddRange(new object[] {
            "Pozytywny",
            "Naprawiony",
            "Dopuszczony",
            "Negatywny"});
            this.Wynik.Name = "Wynik";
            this.Wynik.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Komentarz
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.Komentarz.DefaultCellStyle = dataGridViewCellStyle4;
            this.Komentarz.FillWeight = 7.614212F;
            this.Komentarz.HeaderText = "Uwagi / zalecenia pokontrolne";
            this.Komentarz.Name = "Komentarz";
            this.Komentarz.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Komentarz.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Komentarz.Width = 352;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.s_karta_lbl);
            this.groupBox1.Controls.Add(this.karta_lbl);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.s_linia_lbl);
            this.groupBox1.Controls.Add(this.maszyna_lbl);
            this.groupBox1.Controls.Add(this.linia_lbl);
            this.groupBox1.Controls.Add(this.s_maszyna_lbl);
            this.groupBox1.Location = new System.Drawing.Point(7, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(466, 131);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // s_karta_lbl
            // 
            this.s_karta_lbl.AutoSize = true;
            this.s_karta_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.s_karta_lbl.Location = new System.Drawing.Point(267, 15);
            this.s_karta_lbl.Name = "s_karta_lbl";
            this.s_karta_lbl.Size = new System.Drawing.Size(51, 16);
            this.s_karta_lbl.TabIndex = 19;
            this.s_karta_lbl.Text = "tu karta";
            // 
            // karta_lbl
            // 
            this.karta_lbl.AutoSize = true;
            this.karta_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.karta_lbl.Location = new System.Drawing.Point(175, 17);
            this.karta_lbl.Name = "karta_lbl";
            this.karta_lbl.Size = new System.Drawing.Size(87, 13);
            this.karta_lbl.TabIndex = 18;
            this.karta_lbl.Text = "Karta kontroli:";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox3.Controls.Add(this.s_wynik_lbl);
            this.groupBox3.Controls.Add(this.zatwierdzony_lbl);
            this.groupBox3.Controls.Add(this.s_sygnatura_lbl);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox3.Location = new System.Drawing.Point(6, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(154, 113);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "SYGNATURA";
            // 
            // s_wynik_lbl
            // 
            this.s_wynik_lbl.BackColor = System.Drawing.Color.Pink;
            this.s_wynik_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.s_wynik_lbl.Location = new System.Drawing.Point(6, 51);
            this.s_wynik_lbl.Name = "s_wynik_lbl";
            this.s_wynik_lbl.Size = new System.Drawing.Size(142, 23);
            this.s_wynik_lbl.TabIndex = 18;
            this.s_wynik_lbl.Text = "Niekompletny";
            this.s_wynik_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // zatwierdzony_lbl
            // 
            this.zatwierdzony_lbl.BackColor = System.Drawing.Color.Pink;
            this.zatwierdzony_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.zatwierdzony_lbl.Location = new System.Drawing.Point(6, 74);
            this.zatwierdzony_lbl.Name = "zatwierdzony_lbl";
            this.zatwierdzony_lbl.Size = new System.Drawing.Size(142, 23);
            this.zatwierdzony_lbl.TabIndex = 17;
            this.zatwierdzony_lbl.Text = "Niezatwierdzony";
            this.zatwierdzony_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // s_sygnatura_lbl
            // 
            this.s_sygnatura_lbl.BackColor = System.Drawing.Color.LightGreen;
            this.s_sygnatura_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.s_sygnatura_lbl.Location = new System.Drawing.Point(6, 28);
            this.s_sygnatura_lbl.Name = "s_sygnatura_lbl";
            this.s_sygnatura_lbl.Size = new System.Drawing.Size(142, 23);
            this.s_sygnatura_lbl.TabIndex = 16;
            this.s_sygnatura_lbl.Text = "2020/16/A1006";
            this.s_sygnatura_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // s_linia_lbl
            // 
            this.s_linia_lbl.AutoSize = true;
            this.s_linia_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.s_linia_lbl.Location = new System.Drawing.Point(267, 42);
            this.s_linia_lbl.Name = "s_linia_lbl";
            this.s_linia_lbl.Size = new System.Drawing.Size(45, 16);
            this.s_linia_lbl.TabIndex = 14;
            this.s_linia_lbl.Text = "tu linia";
            // 
            // maszyna_lbl
            // 
            this.maszyna_lbl.AutoSize = true;
            this.maszyna_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.maszyna_lbl.Location = new System.Drawing.Point(202, 71);
            this.maszyna_lbl.Name = "maszyna_lbl";
            this.maszyna_lbl.Size = new System.Drawing.Size(60, 13);
            this.maszyna_lbl.TabIndex = 13;
            this.maszyna_lbl.Text = "Maszyna:";
            // 
            // linia_lbl
            // 
            this.linia_lbl.AutoSize = true;
            this.linia_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.linia_lbl.Location = new System.Drawing.Point(224, 44);
            this.linia_lbl.Name = "linia_lbl";
            this.linia_lbl.Size = new System.Drawing.Size(38, 13);
            this.linia_lbl.TabIndex = 12;
            this.linia_lbl.Text = "Linia:";
            // 
            // s_maszyna_lbl
            // 
            this.s_maszyna_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.s_maszyna_lbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.s_maszyna_lbl.Location = new System.Drawing.Point(267, 69);
            this.s_maszyna_lbl.Name = "s_maszyna_lbl";
            this.s_maszyna_lbl.Size = new System.Drawing.Size(193, 59);
            this.s_maszyna_lbl.TabIndex = 11;
            this.s_maszyna_lbl.Text = "tu maszyna";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.docx_btn);
            this.groupBox2.Controls.Add(this.zatwierdzil_combo);
            this.groupBox2.Controls.Add(this.zatwierdzil_lbl);
            this.groupBox2.Controls.Add(this.mycie_wykonano_check);
            this.groupBox2.Controls.Add(this.mycie_lbl);
            this.groupBox2.Controls.Add(this.mycie_zlecono_check);
            this.groupBox2.Controls.Add(this.Zapisz_btn);
            this.groupBox2.Controls.Add(this.wykonał_combo);
            this.groupBox2.Controls.Add(this.nowy_postoj_wprowadzil_lbl);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.data_lbl);
            this.groupBox2.Controls.Add(this.przeglady_data_DTPicker);
            this.groupBox2.Location = new System.Drawing.Point(482, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(484, 131);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // docx_btn
            // 
            this.docx_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.docx_btn.Location = new System.Drawing.Point(353, 94);
            this.docx_btn.Name = "docx_btn";
            this.docx_btn.Size = new System.Drawing.Size(106, 26);
            this.docx_btn.TabIndex = 27;
            this.docx_btn.Text = "Wyświetl raport";
            this.docx_btn.UseVisualStyleBackColor = true;
            this.docx_btn.Visible = false;
            this.docx_btn.Click += new System.EventHandler(this.docx_btn_Click);
            // 
            // zatwierdzil_combo
            // 
            this.zatwierdzil_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.zatwierdzil_combo.Enabled = false;
            this.zatwierdzil_combo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.zatwierdzil_combo.Location = new System.Drawing.Point(293, 64);
            this.zatwierdzil_combo.Name = "zatwierdzil_combo";
            this.zatwierdzil_combo.Size = new System.Drawing.Size(166, 24);
            this.zatwierdzil_combo.TabIndex = 25;
            this.zatwierdzil_combo.SelectedIndexChanged += new System.EventHandler(this.zatwierdzil_combo_SelectedIndexChanged);
            // 
            // zatwierdzil_lbl
            // 
            this.zatwierdzil_lbl.AutoSize = true;
            this.zatwierdzil_lbl.Enabled = false;
            this.zatwierdzil_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.zatwierdzil_lbl.Location = new System.Drawing.Point(290, 52);
            this.zatwierdzil_lbl.Name = "zatwierdzil_lbl";
            this.zatwierdzil_lbl.Size = new System.Drawing.Size(70, 13);
            this.zatwierdzil_lbl.TabIndex = 26;
            this.zatwierdzil_lbl.Text = "Zatwierdził";
            // 
            // mycie_wykonano_check
            // 
            this.mycie_wykonano_check.AutoSize = true;
            this.mycie_wykonano_check.Enabled = false;
            this.mycie_wykonano_check.Location = new System.Drawing.Point(24, 108);
            this.mycie_wykonano_check.Name = "mycie_wykonano_check";
            this.mycie_wykonano_check.Size = new System.Drawing.Size(78, 17);
            this.mycie_wykonano_check.TabIndex = 24;
            this.mycie_wykonano_check.Text = "Wykonano";
            this.mycie_wykonano_check.UseVisualStyleBackColor = true;
            // 
            // mycie_lbl
            // 
            this.mycie_lbl.AutoSize = true;
            this.mycie_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mycie_lbl.Location = new System.Drawing.Point(21, 67);
            this.mycie_lbl.Name = "mycie_lbl";
            this.mycie_lbl.Size = new System.Drawing.Size(119, 13);
            this.mycie_lbl.TabIndex = 23;
            this.mycie_lbl.Text = "Mycie i dezynfekcja";
            // 
            // mycie_zlecono_check
            // 
            this.mycie_zlecono_check.AutoSize = true;
            this.mycie_zlecono_check.Location = new System.Drawing.Point(24, 85);
            this.mycie_zlecono_check.Name = "mycie_zlecono_check";
            this.mycie_zlecono_check.Size = new System.Drawing.Size(65, 17);
            this.mycie_zlecono_check.TabIndex = 22;
            this.mycie_zlecono_check.Text = "Zlecono";
            this.mycie_zlecono_check.UseVisualStyleBackColor = true;
            this.mycie_zlecono_check.CheckedChanged += new System.EventHandler(this.mycie_zlecono_check_CheckedChanged);
            // 
            // Zapisz_btn
            // 
            this.Zapisz_btn.Enabled = false;
            this.Zapisz_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.Zapisz_btn.Location = new System.Drawing.Point(379, 94);
            this.Zapisz_btn.Name = "Zapisz_btn";
            this.Zapisz_btn.Size = new System.Drawing.Size(80, 26);
            this.Zapisz_btn.TabIndex = 21;
            this.Zapisz_btn.Text = "Zapisz";
            this.Zapisz_btn.UseVisualStyleBackColor = true;
            this.Zapisz_btn.Click += new System.EventHandler(this.Zapisz_btn_Click);
            // 
            // wykonał_combo
            // 
            this.wykonał_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.wykonał_combo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.wykonał_combo.Location = new System.Drawing.Point(293, 27);
            this.wykonał_combo.Name = "wykonał_combo";
            this.wykonał_combo.Size = new System.Drawing.Size(166, 24);
            this.wykonał_combo.TabIndex = 19;
            this.wykonał_combo.SelectedIndexChanged += new System.EventHandler(this.wykonał_combo_SelectedIndexChanged);
            // 
            // nowy_postoj_wprowadzil_lbl
            // 
            this.nowy_postoj_wprowadzil_lbl.AutoSize = true;
            this.nowy_postoj_wprowadzil_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.nowy_postoj_wprowadzil_lbl.Location = new System.Drawing.Point(290, 15);
            this.nowy_postoj_wprowadzil_lbl.Name = "nowy_postoj_wprowadzil_lbl";
            this.nowy_postoj_wprowadzil_lbl.Size = new System.Drawing.Size(108, 13);
            this.nowy_postoj_wprowadzil_lbl.TabIndex = 20;
            this.nowy_postoj_wprowadzil_lbl.Text = "Przegląd wykonał";
            // 
            // data_lbl
            // 
            this.data_lbl.AutoSize = true;
            this.data_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.data_lbl.Location = new System.Drawing.Point(21, 15);
            this.data_lbl.Name = "data_lbl";
            this.data_lbl.Size = new System.Drawing.Size(157, 13);
            this.data_lbl.TabIndex = 3;
            this.data_lbl.Text = "Data wykonania przeglądu";
            // 
            // przeglady_data_DTPicker
            // 
            this.przeglady_data_DTPicker.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.przeglady_data_DTPicker.CustomFormat = "dd MMM yyyy";
            this.przeglady_data_DTPicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.przeglady_data_DTPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.przeglady_data_DTPicker.Location = new System.Drawing.Point(24, 28);
            this.przeglady_data_DTPicker.Name = "przeglady_data_DTPicker";
            this.przeglady_data_DTPicker.Size = new System.Drawing.Size(200, 22);
            this.przeglady_data_DTPicker.TabIndex = 2;
            this.przeglady_data_DTPicker.Value = new System.DateTime(2020, 7, 5, 0, 56, 2, 0);
            // 
            // FormPrz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(976, 606);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormPrz";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Wykonaj przegląd";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Zadanie;
        private System.Windows.Forms.DataGridViewComboBoxColumn Wynik;
        private System.Windows.Forms.DataGridViewTextBoxColumn Komentarz;
        private System.Windows.Forms.DateTimePicker przeglady_data_DTPicker;
        private System.Windows.Forms.Label data_lbl;
        private System.Windows.Forms.Label s_sygnatura_lbl;
        private System.Windows.Forms.Label s_linia_lbl;
        private System.Windows.Forms.Label maszyna_lbl;
        private System.Windows.Forms.Label linia_lbl;
        private System.Windows.Forms.Label s_maszyna_lbl;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox mycie_wykonano_check;
        private System.Windows.Forms.Label mycie_lbl;
        private System.Windows.Forms.CheckBox mycie_zlecono_check;
        private System.Windows.Forms.Button Zapisz_btn;
        private System.Windows.Forms.ComboBox wykonał_combo;
        private System.Windows.Forms.Label nowy_postoj_wprowadzil_lbl;
        private System.Windows.Forms.Label zatwierdzony_lbl;
        private System.Windows.Forms.ComboBox zatwierdzil_combo;
        private System.Windows.Forms.Label zatwierdzil_lbl;
        private System.Windows.Forms.Label s_wynik_lbl;
        private System.Windows.Forms.Label s_karta_lbl;
        private System.Windows.Forms.Label karta_lbl;
        private System.Windows.Forms.Button docx_btn;
    }
}