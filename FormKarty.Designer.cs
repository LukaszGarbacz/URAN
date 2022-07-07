namespace Utrzymanie_Ruchu___APP_Niepruszewo
{
    partial class FormKarty
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormKarty));
            this.karty_tabela = new System.Windows.Forms.DataGridView();
            this.nazwa_combo = new System.Windows.Forms.ComboBox();
            this.karty_box = new System.Windows.Forms.GroupBox();
            this.karty_nowa_btn = new System.Windows.Forms.Button();
            this.karty_import_btn = new System.Windows.Forms.Button();
            this.karty_edycja_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.karty_anuluj_btn = new System.Windows.Forms.Button();
            this.karty_zapisz_btn = new System.Windows.Forms.Button();
            this.karty_edycja_box = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.skopiuj_combo = new System.Windows.Forms.ComboBox();
            this.karty_dodaj_polecenie_btn = new System.Windows.Forms.Button();
            this.karty_dodaj_czynnosc_btn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.karty_edycja_nazwa_textBox = new System.Windows.Forms.TextBox();
            this.openFileDialog_imp = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.karty_tabela)).BeginInit();
            this.karty_box.SuspendLayout();
            this.karty_edycja_box.SuspendLayout();
            this.SuspendLayout();
            // 
            // karty_tabela
            // 
            this.karty_tabela.AllowUserToAddRows = false;
            this.karty_tabela.AllowUserToDeleteRows = false;
            this.karty_tabela.AllowUserToResizeColumns = false;
            this.karty_tabela.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 10F);
            this.karty_tabela.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.karty_tabela.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.karty_tabela.BackgroundColor = System.Drawing.SystemColors.Window;
            this.karty_tabela.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.karty_tabela.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.karty_tabela.ColumnHeadersHeight = 30;
            this.karty_tabela.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.karty_tabela.DefaultCellStyle = dataGridViewCellStyle2;
            this.karty_tabela.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.karty_tabela.GridColor = System.Drawing.SystemColors.ControlLight;
            this.karty_tabela.Location = new System.Drawing.Point(14, 116);
            this.karty_tabela.MultiSelect = false;
            this.karty_tabela.Name = "karty_tabela";
            this.karty_tabela.ReadOnly = true;
            this.karty_tabela.RowHeadersVisible = false;
            this.karty_tabela.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.karty_tabela.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.karty_tabela.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.karty_tabela.ShowCellErrors = false;
            this.karty_tabela.ShowCellToolTips = false;
            this.karty_tabela.ShowEditingIcon = false;
            this.karty_tabela.ShowRowErrors = false;
            this.karty_tabela.Size = new System.Drawing.Size(918, 473);
            this.karty_tabela.TabIndex = 0;
            this.karty_tabela.TabStop = false;
            this.karty_tabela.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.karty_tabela_CellValueChanged);
            this.karty_tabela.Click += new System.EventHandler(this.karty_tabela_Click);
            // 
            // nazwa_combo
            // 
            this.nazwa_combo.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.nazwa_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nazwa_combo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.nazwa_combo.FormattingEnabled = true;
            this.nazwa_combo.Location = new System.Drawing.Point(20, 34);
            this.nazwa_combo.Name = "nazwa_combo";
            this.nazwa_combo.Size = new System.Drawing.Size(237, 23);
            this.nazwa_combo.TabIndex = 1;
            this.nazwa_combo.SelectedIndexChanged += new System.EventHandler(this.nazwa_combo_SelectedIndexChanged);
            // 
            // karty_box
            // 
            this.karty_box.BackColor = System.Drawing.SystemColors.Window;
            this.karty_box.Controls.Add(this.karty_nowa_btn);
            this.karty_box.Controls.Add(this.karty_import_btn);
            this.karty_box.Controls.Add(this.karty_edycja_btn);
            this.karty_box.Controls.Add(this.label1);
            this.karty_box.Controls.Add(this.nazwa_combo);
            this.karty_box.Location = new System.Drawing.Point(14, 12);
            this.karty_box.Name = "karty_box";
            this.karty_box.Size = new System.Drawing.Size(286, 98);
            this.karty_box.TabIndex = 2;
            this.karty_box.TabStop = false;
            this.karty_box.Text = "Lista kart";
            // 
            // karty_nowa_btn
            // 
            this.karty_nowa_btn.Location = new System.Drawing.Point(101, 63);
            this.karty_nowa_btn.Name = "karty_nowa_btn";
            this.karty_nowa_btn.Size = new System.Drawing.Size(75, 25);
            this.karty_nowa_btn.TabIndex = 8;
            this.karty_nowa_btn.Text = "Nowa";
            this.karty_nowa_btn.UseVisualStyleBackColor = true;
            this.karty_nowa_btn.Click += new System.EventHandler(this.karty_nowa_btn_Click);
            // 
            // karty_import_btn
            // 
            this.karty_import_btn.Location = new System.Drawing.Point(182, 63);
            this.karty_import_btn.Name = "karty_import_btn";
            this.karty_import_btn.Size = new System.Drawing.Size(75, 25);
            this.karty_import_btn.TabIndex = 7;
            this.karty_import_btn.Text = "Importuj";
            this.karty_import_btn.UseVisualStyleBackColor = true;
            this.karty_import_btn.Click += new System.EventHandler(this.karty_import_btn_Click);
            // 
            // karty_edycja_btn
            // 
            this.karty_edycja_btn.Location = new System.Drawing.Point(20, 63);
            this.karty_edycja_btn.Name = "karty_edycja_btn";
            this.karty_edycja_btn.Size = new System.Drawing.Size(75, 25);
            this.karty_edycja_btn.TabIndex = 3;
            this.karty_edycja_btn.Text = "Edytuj";
            this.karty_edycja_btn.UseVisualStyleBackColor = true;
            this.karty_edycja_btn.Click += new System.EventHandler(this.karty_edycja_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(17, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Karta kontroli:";
            // 
            // karty_anuluj_btn
            // 
            this.karty_anuluj_btn.Location = new System.Drawing.Point(368, 62);
            this.karty_anuluj_btn.Name = "karty_anuluj_btn";
            this.karty_anuluj_btn.Size = new System.Drawing.Size(75, 25);
            this.karty_anuluj_btn.TabIndex = 6;
            this.karty_anuluj_btn.Text = "Aluluj";
            this.karty_anuluj_btn.UseVisualStyleBackColor = true;
            this.karty_anuluj_btn.Click += new System.EventHandler(this.karty_anuluj_btn_Click);
            // 
            // karty_zapisz_btn
            // 
            this.karty_zapisz_btn.Enabled = false;
            this.karty_zapisz_btn.Location = new System.Drawing.Point(449, 63);
            this.karty_zapisz_btn.Name = "karty_zapisz_btn";
            this.karty_zapisz_btn.Size = new System.Drawing.Size(143, 25);
            this.karty_zapisz_btn.TabIndex = 5;
            this.karty_zapisz_btn.Text = "Zapisz";
            this.karty_zapisz_btn.UseVisualStyleBackColor = true;
            this.karty_zapisz_btn.Click += new System.EventHandler(this.karty_zapisz_btn_Click);
            // 
            // karty_edycja_box
            // 
            this.karty_edycja_box.Controls.Add(this.label3);
            this.karty_edycja_box.Controls.Add(this.skopiuj_combo);
            this.karty_edycja_box.Controls.Add(this.karty_dodaj_polecenie_btn);
            this.karty_edycja_box.Controls.Add(this.karty_dodaj_czynnosc_btn);
            this.karty_edycja_box.Controls.Add(this.label2);
            this.karty_edycja_box.Controls.Add(this.karty_edycja_nazwa_textBox);
            this.karty_edycja_box.Controls.Add(this.karty_anuluj_btn);
            this.karty_edycja_box.Controls.Add(this.karty_zapisz_btn);
            this.karty_edycja_box.Location = new System.Drawing.Point(306, 13);
            this.karty_edycja_box.Name = "karty_edycja_box";
            this.karty_edycja_box.Size = new System.Drawing.Size(626, 97);
            this.karty_edycja_box.TabIndex = 6;
            this.karty_edycja_box.TabStop = false;
            this.karty_edycja_box.Text = "Edycja";
            this.karty_edycja_box.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(6, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 15);
            this.label3.TabIndex = 12;
            this.label3.Text = "Skopiuj z:";
            this.label3.Visible = false;
            // 
            // skopiuj_combo
            // 
            this.skopiuj_combo.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.skopiuj_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.skopiuj_combo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.skopiuj_combo.FormattingEnabled = true;
            this.skopiuj_combo.Location = new System.Drawing.Point(9, 33);
            this.skopiuj_combo.Name = "skopiuj_combo";
            this.skopiuj_combo.Size = new System.Drawing.Size(202, 23);
            this.skopiuj_combo.TabIndex = 11;
            this.skopiuj_combo.Visible = false;
            this.skopiuj_combo.SelectedIndexChanged += new System.EventHandler(this.skopiuj_combo_SelectedIndexChanged);
            // 
            // karty_dodaj_polecenie_btn
            // 
            this.karty_dodaj_polecenie_btn.Location = new System.Drawing.Point(233, 29);
            this.karty_dodaj_polecenie_btn.Name = "karty_dodaj_polecenie_btn";
            this.karty_dodaj_polecenie_btn.Size = new System.Drawing.Size(113, 25);
            this.karty_dodaj_polecenie_btn.TabIndex = 10;
            this.karty_dodaj_polecenie_btn.Text = "Dodaj Polecenie";
            this.karty_dodaj_polecenie_btn.UseVisualStyleBackColor = true;
            this.karty_dodaj_polecenie_btn.Visible = false;
            this.karty_dodaj_polecenie_btn.Click += new System.EventHandler(this.karty_dodaj_polecenie_btn_Click);
            // 
            // karty_dodaj_czynnosc_btn
            // 
            this.karty_dodaj_czynnosc_btn.Location = new System.Drawing.Point(233, 62);
            this.karty_dodaj_czynnosc_btn.Name = "karty_dodaj_czynnosc_btn";
            this.karty_dodaj_czynnosc_btn.Size = new System.Drawing.Size(113, 25);
            this.karty_dodaj_czynnosc_btn.TabIndex = 9;
            this.karty_dodaj_czynnosc_btn.Text = "Dodaj Czynność";
            this.karty_dodaj_czynnosc_btn.UseVisualStyleBackColor = true;
            this.karty_dodaj_czynnosc_btn.Click += new System.EventHandler(this.karty_dodaj_czynnosc_btn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(364, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Nazwa:";
            // 
            // karty_edycja_nazwa_textBox
            // 
            this.karty_edycja_nazwa_textBox.Location = new System.Drawing.Point(368, 33);
            this.karty_edycja_nazwa_textBox.Name = "karty_edycja_nazwa_textBox";
            this.karty_edycja_nazwa_textBox.Size = new System.Drawing.Size(224, 21);
            this.karty_edycja_nazwa_textBox.TabIndex = 7;
            this.karty_edycja_nazwa_textBox.TextChanged += new System.EventHandler(this.karty_edycja_nazwa_textBox_TextChanged);
            // 
            // openFileDialog_imp
            // 
            this.openFileDialog_imp.Filter = "Word (*.docx)|*.docx";
            this.openFileDialog_imp.Multiselect = true;
            this.openFileDialog_imp.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_imp_FileOk);
            // 
            // FormKarty
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(944, 601);
            this.Controls.Add(this.karty_edycja_box);
            this.Controls.Add(this.karty_box);
            this.Controls.Add(this.karty_tabela);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormKarty";
            this.Text = "Karty Kontroli";
            this.Load += new System.EventHandler(this.FormKarty_Load);
            ((System.ComponentModel.ISupportInitialize)(this.karty_tabela)).EndInit();
            this.karty_box.ResumeLayout(false);
            this.karty_box.PerformLayout();
            this.karty_edycja_box.ResumeLayout(false);
            this.karty_edycja_box.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView karty_tabela;
        private System.Windows.Forms.ComboBox nazwa_combo;
        private System.Windows.Forms.GroupBox karty_box;
        private System.Windows.Forms.Button karty_import_btn;
        private System.Windows.Forms.Button karty_anuluj_btn;
        private System.Windows.Forms.Button karty_zapisz_btn;
        private System.Windows.Forms.Button karty_edycja_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button karty_nowa_btn;
        private System.Windows.Forms.GroupBox karty_edycja_box;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox karty_edycja_nazwa_textBox;
        private System.Windows.Forms.Button karty_dodaj_czynnosc_btn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox skopiuj_combo;
        private System.Windows.Forms.Button karty_dodaj_polecenie_btn;
        private System.Windows.Forms.OpenFileDialog openFileDialog_imp;
    }
}