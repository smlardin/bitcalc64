namespace BitCalc64
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.hexMotorola = new System.Windows.Forms.TextBox();
            this.decMotorola = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.clearAllButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.shiftBits = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.highBitsCheck = new System.Windows.Forms.CheckBox();
            this.bitGridView = new System.Windows.Forms.DataGridView();
            this.Start = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.End = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Desciption = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Hex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Decimal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Binary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.setRowButton = new System.Windows.Forms.Button();
            this.clearRowButton = new System.Windows.Forms.Button();
            this.incRowButton = new System.Windows.Forms.Button();
            this.decRowButton = new System.Windows.Forms.Button();
            this.predefBitsCombo = new System.Windows.Forms.ComboBox();
            this.saveTimer = new System.Windows.Forms.Timer(this.components);
            this.ibmBitCount = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.labelEdit = new System.Windows.Forms.TextBox();
            this.shiftRight = new System.Windows.Forms.Button();
            this.shiftLeft = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.msbBit0 = new System.Windows.Forms.CheckBox();
            this.EndianSwap = new System.Windows.Forms.Button();
            this.EndianSwapPage = new System.Windows.Forms.Button();
            this.filterList = new SearchList();
            ((System.ComponentModel.ISupportInitialize)(this.bitGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // hexMotorola
            // 
            this.hexMotorola.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hexMotorola.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hexMotorola.Location = new System.Drawing.Point(484, 142);
            this.hexMotorola.Name = "hexMotorola";
            this.hexMotorola.Size = new System.Drawing.Size(202, 20);
            this.hexMotorola.TabIndex = 7;
            this.hexMotorola.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.hexMotorola.TextChanged += new System.EventHandler(this.hexMotorola_TextChanged);
            // 
            // decMotorola
            // 
            this.decMotorola.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.decMotorola.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.decMotorola.Location = new System.Drawing.Point(484, 117);
            this.decMotorola.Name = "decMotorola";
            this.decMotorola.Size = new System.Drawing.Size(202, 20);
            this.decMotorola.TabIndex = 6;
            this.decMotorola.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.decMotorola.TextChanged += new System.EventHandler(this.decMotorola_TextChanged);
            // 
            // label33
            // 
            this.label33.Location = new System.Drawing.Point(428, 122);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(50, 20);
            this.label33.TabIndex = 4;
            this.label33.Text = "Decimal:";
            // 
            // label34
            // 
            this.label34.Location = new System.Drawing.Point(428, 147);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(50, 20);
            this.label34.TabIndex = 5;
            this.label34.Text = "Hex:";
            // 
            // clearAllButton
            // 
            this.clearAllButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.clearAllButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clearAllButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.clearAllButton.Location = new System.Drawing.Point(473, 225);
            this.clearAllButton.Name = "clearAllButton";
            this.clearAllButton.Size = new System.Drawing.Size(50, 22);
            this.clearAllButton.TabIndex = 0;
            this.clearAllButton.Text = "Clear";
            this.clearAllButton.UseVisualStyleBackColor = false;
            this.clearAllButton.Click += new System.EventHandler(this.clearAllButton_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button2.Location = new System.Drawing.Point(529, 225);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(50, 22);
            this.button2.TabIndex = 1;
            this.button2.Text = "Set";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button3.Location = new System.Drawing.Point(585, 225);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(50, 22);
            this.button3.TabIndex = 2;
            this.button3.Text = "Invert";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button4.Location = new System.Drawing.Point(473, 250);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(50, 22);
            this.button4.TabIndex = 3;
            this.button4.Text = "Clear";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button5.Location = new System.Drawing.Point(529, 250);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(50, 22);
            this.button5.TabIndex = 4;
            this.button5.Text = "Set";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button6.Location = new System.Drawing.Point(585, 250);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(50, 22);
            this.button6.TabIndex = 5;
            this.button6.Text = "Invert";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // shiftBits
            // 
            this.shiftBits.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.shiftBits.Location = new System.Drawing.Point(617, 170);
            this.shiftBits.Name = "shiftBits";
            this.shiftBits.Size = new System.Drawing.Size(34, 20);
            this.shiftBits.TabIndex = 9;
            this.shiftBits.Text = "1";
            this.shiftBits.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(495, 175);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(80, 13);
            this.label35.TabIndex = 15;
            this.label35.Text = "Shift Operation:";
            // 
            // highBitsCheck
            // 
            this.highBitsCheck.AutoSize = true;
            this.highBitsCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.highBitsCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.highBitsCheck.Location = new System.Drawing.Point(493, 198);
            this.highBitsCheck.Name = "highBitsCheck";
            this.highBitsCheck.Size = new System.Drawing.Size(73, 17);
            this.highBitsCheck.TabIndex = 16;
            this.highBitsCheck.Text = "High Page";
            this.highBitsCheck.UseVisualStyleBackColor = true;
            this.highBitsCheck.CheckedChanged += new System.EventHandler(this.highBitsCheck_CheckedChanged);
            // 
            // bitGridView
            // 
            this.bitGridView.AllowUserToResizeColumns = false;
            this.bitGridView.AllowUserToResizeRows = false;
            this.bitGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bitGridView.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.bitGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.bitGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.bitGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.bitGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.bitGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Start,
            this.End,
            this.Desciption,
            this.Hex,
            this.Decimal,
            this.Binary});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.bitGridView.DefaultCellStyle = dataGridViewCellStyle7;
            this.bitGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.bitGridView.Location = new System.Drawing.Point(58, 329);
            this.bitGridView.Name = "bitGridView";
            this.bitGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.bitGridView.RowHeadersWidth = 10;
            this.bitGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.bitGridView.RowTemplate.Height = 18;
            this.bitGridView.Size = new System.Drawing.Size(964, 164);
            this.bitGridView.TabIndex = 18;
            this.bitGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.bitGridView_CellEndEdit);
            this.bitGridView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.bitGridView_CellEnter);
            this.bitGridView.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.bitGridView_CellMouseClick);
            this.bitGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.bitGridView_RowsRemoved);
            this.bitGridView.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.bitGridView_SortCompare);
            // 
            // Start
            // 
            this.Start.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            this.Start.DefaultCellStyle = dataGridViewCellStyle2;
            this.Start.HeaderText = "Start";
            this.Start.Name = "Start";
            this.Start.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Start.Width = 54;
            // 
            // End
            // 
            this.End.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = null;
            this.End.DefaultCellStyle = dataGridViewCellStyle3;
            this.End.HeaderText = "End";
            this.End.Name = "End";
            this.End.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.End.Width = 51;
            // 
            // Desciption
            // 
            this.Desciption.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Desciption.DefaultCellStyle = dataGridViewCellStyle4;
            this.Desciption.FillWeight = 50F;
            this.Desciption.HeaderText = "Name";
            this.Desciption.Name = "Desciption";
            // 
            // Hex
            // 
            this.Hex.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.NullValue = null;
            this.Hex.DefaultCellStyle = dataGridViewCellStyle5;
            this.Hex.HeaderText = "Hex";
            this.Hex.Name = "Hex";
            this.Hex.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Hex.Width = 51;
            // 
            // Decimal
            // 
            this.Decimal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N0";
            dataGridViewCellStyle6.NullValue = null;
            this.Decimal.DefaultCellStyle = dataGridViewCellStyle6;
            this.Decimal.HeaderText = "Decimal";
            this.Decimal.Name = "Decimal";
            this.Decimal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Decimal.Width = 70;
            // 
            // Binary
            // 
            this.Binary.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Binary.FillWeight = 50F;
            this.Binary.HeaderText = "Binary";
            this.Binary.Name = "Binary";
            this.Binary.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // setRowButton
            // 
            this.setRowButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.setRowButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.setRowButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.setRowButton.Location = new System.Drawing.Point(6, 357);
            this.setRowButton.Name = "setRowButton";
            this.setRowButton.Size = new System.Drawing.Size(46, 22);
            this.setRowButton.TabIndex = 14;
            this.setRowButton.Text = "Set";
            this.setRowButton.UseVisualStyleBackColor = false;
            this.setRowButton.Click += new System.EventHandler(this.setRowButton_Click);
            // 
            // clearRowButton
            // 
            this.clearRowButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.clearRowButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clearRowButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.clearRowButton.Location = new System.Drawing.Point(6, 329);
            this.clearRowButton.Name = "clearRowButton";
            this.clearRowButton.Size = new System.Drawing.Size(46, 22);
            this.clearRowButton.TabIndex = 13;
            this.clearRowButton.Text = "Clear";
            this.clearRowButton.UseVisualStyleBackColor = false;
            this.clearRowButton.Click += new System.EventHandler(this.clearRowButton_Click);
            // 
            // incRowButton
            // 
            this.incRowButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.incRowButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.incRowButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.incRowButton.Location = new System.Drawing.Point(6, 385);
            this.incRowButton.Name = "incRowButton";
            this.incRowButton.Size = new System.Drawing.Size(46, 22);
            this.incRowButton.TabIndex = 15;
            this.incRowButton.Text = "++";
            this.incRowButton.UseVisualStyleBackColor = false;
            this.incRowButton.Click += new System.EventHandler(this.incRowButton_Click);
            // 
            // decRowButton
            // 
            this.decRowButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.decRowButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.decRowButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.decRowButton.Location = new System.Drawing.Point(6, 413);
            this.decRowButton.Name = "decRowButton";
            this.decRowButton.Size = new System.Drawing.Size(46, 22);
            this.decRowButton.TabIndex = 16;
            this.decRowButton.Text = "--";
            this.decRowButton.UseVisualStyleBackColor = false;
            this.decRowButton.Click += new System.EventHandler(this.decRowButton_Click);
            // 
            // predefBitsCombo
            // 
            this.predefBitsCombo.DropDownWidth = 113;
            this.predefBitsCombo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.predefBitsCombo.FormattingEnabled = true;
            this.predefBitsCombo.Location = new System.Drawing.Point(409, 278);
            this.predefBitsCombo.Name = "predefBitsCombo";
            this.predefBitsCombo.Size = new System.Drawing.Size(308, 23);
            this.predefBitsCombo.Sorted = true;
            this.predefBitsCombo.TabIndex = 12;
            this.predefBitsCombo.Text = "Last Edit";
            this.predefBitsCombo.SelectedValueChanged += new System.EventHandler(this.predefBitsCombo_SelectedValueChanged);
            this.predefBitsCombo.TextChanged += new System.EventHandler(this.predefBitsCombo_TextChanged);
            // 
            // saveTimer
            // 
            this.saveTimer.Enabled = true;
            this.saveTimer.Interval = 1000;
            this.saveTimer.Tick += new System.EventHandler(this.saveTimer_Tick);
            // 
            // ibmBitCount
            // 
            this.ibmBitCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ibmBitCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ibmBitCount.Location = new System.Drawing.Point(652, 195);
            this.ibmBitCount.Name = "ibmBitCount";
            this.ibmBitCount.Size = new System.Drawing.Size(34, 20);
            this.ibmBitCount.TabIndex = 11;
            this.ibmBitCount.Text = "64";
            this.ibmBitCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ibmBitCount.TextChanged += new System.EventHandler(this.ibmBitCount_TextChanged);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(572, 200);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(74, 13);
            this.label36.TabIndex = 24;
            this.label36.Text = "Left Bit Count:";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label36.Click += new System.EventHandler(this.label36_Click);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(432, 230);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(21, 13);
            this.label37.TabIndex = 128;
            this.label37.Text = "All:";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(427, 255);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(35, 13);
            this.label38.TabIndex = 129;
            this.label38.Text = "Page:";
            // 
            // labelEdit
            // 
            this.labelEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelEdit.Font = new System.Drawing.Font("Arial Narrow", 8.25F);
            this.labelEdit.Location = new System.Drawing.Point(428, 171);
            this.labelEdit.Multiline = true;
            this.labelEdit.Name = "labelEdit";
            this.labelEdit.Size = new System.Drawing.Size(25, 40);
            this.labelEdit.TabIndex = 130;
            this.labelEdit.Visible = false;
            this.labelEdit.Leave += new System.EventHandler(this.labelEdit_Leave);
            // 
            // shiftRight
            // 
            this.shiftRight.BackColor = System.Drawing.SystemColors.Control;
            this.shiftRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.shiftRight.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.shiftRight.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.shiftRight.Image = global::BitCalc64.Properties.Resources.rightbsm;
            this.shiftRight.Location = new System.Drawing.Point(657, 170);
            this.shiftRight.Name = "shiftRight";
            this.shiftRight.Size = new System.Drawing.Size(29, 20);
            this.shiftRight.TabIndex = 10;
            this.shiftRight.UseVisualStyleBackColor = false;
            this.shiftRight.Click += new System.EventHandler(this.shiftRight_Click);
            // 
            // shiftLeft
            // 
            this.shiftLeft.BackColor = System.Drawing.SystemColors.Control;
            this.shiftLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.shiftLeft.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.shiftLeft.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.shiftLeft.Image = global::BitCalc64.Properties.Resources.leftbsm;
            this.shiftLeft.Location = new System.Drawing.Point(582, 170);
            this.shiftLeft.Name = "shiftLeft";
            this.shiftLeft.Size = new System.Drawing.Size(29, 20);
            this.shiftLeft.TabIndex = 8;
            this.shiftLeft.UseVisualStyleBackColor = false;
            this.shiftLeft.Click += new System.EventHandler(this.shiftLeft_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button7.Location = new System.Drawing.Point(6, 441);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(46, 22);
            this.button7.TabIndex = 17;
            this.button7.Text = "Invert";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.invertRowButton_Click);
            // 
            // msbBit0
            // 
            this.msbBit0.AutoSize = true;
            this.msbBit0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.msbBit0.Location = new System.Drawing.Point(636, 306);
            this.msbBit0.Name = "msbBit0";
            this.msbBit0.Size = new System.Drawing.Size(81, 17);
            this.msbBit0.TabIndex = 132;
            this.msbBit0.Text = "Bit 0 in MSB";
            this.msbBit0.UseVisualStyleBackColor = true;
            this.msbBit0.Click += new System.EventHandler(this.msbBit0_Click);
            // 
            // EndianSwap
            // 
            this.EndianSwap.BackColor = System.Drawing.SystemColors.ControlDark;
            this.EndianSwap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EndianSwap.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.EndianSwap.Location = new System.Drawing.Point(641, 225);
            this.EndianSwap.Name = "EndianSwap";
            this.EndianSwap.Size = new System.Drawing.Size(50, 22);
            this.EndianSwap.TabIndex = 134;
            this.EndianSwap.Text = "Endian";
            this.EndianSwap.UseVisualStyleBackColor = false;
            this.EndianSwap.Click += new System.EventHandler(this.EndianSwap_Click);
            // 
            // EndianSwapPage
            // 
            this.EndianSwapPage.BackColor = System.Drawing.SystemColors.ControlDark;
            this.EndianSwapPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EndianSwapPage.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.EndianSwapPage.Location = new System.Drawing.Point(641, 250);
            this.EndianSwapPage.Name = "EndianSwapPage";
            this.EndianSwapPage.Size = new System.Drawing.Size(50, 22);
            this.EndianSwapPage.TabIndex = 135;
            this.EndianSwapPage.Text = "Endian";
            this.EndianSwapPage.UseVisualStyleBackColor = false;
            this.EndianSwapPage.Click += new System.EventHandler(this.EndianSwapPage_Click);
            // 
            // filterList
            // 
            this.filterList.BackColor = System.Drawing.Color.Gainsboro;
            this.filterList.FilterText = "";
            this.filterList.FullRowSelect = false;
            this.filterList.HideSelection = false;
            this.filterList.Location = new System.Drawing.Point(6, 114);
            this.filterList.Name = "filterList";
            this.filterList.Size = new System.Drawing.Size(397, 209);
            this.filterList.TabIndex = 131;
            this.filterList.UseCompatibleStateImageBehavior = false;
            this.filterList.View = System.Windows.Forms.View.Details;
            this.filterList.VirtualListSize = 0;
            this.filterList.VirtualMode = true;

            // In InitializeComponent(), after predefBitsCombo initialization:
            this.predefBitsSaveButton = new System.Windows.Forms.Button();
            this.predefBitsSaveButton.Location = new System.Drawing.Point(722, 278);
            this.predefBitsSaveButton.Name = "predefBitsSaveButton";
            this.predefBitsSaveButton.Size = new System.Drawing.Size(60, 23);
            this.predefBitsSaveButton.TabIndex = 136;
            this.predefBitsSaveButton.Text = "Save";
            this.predefBitsSaveButton.UseVisualStyleBackColor = true;
            this.predefBitsSaveButton.Click += new System.EventHandler(this.predefBitsSaveButton_Click);

            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 505);
            this.Controls.Add(this.EndianSwapPage);
            this.Controls.Add(this.EndianSwap);
            this.Controls.Add(this.msbBit0);
            this.Controls.Add(this.filterList);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.labelEdit);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.label37);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.ibmBitCount);
            this.Controls.Add(this.predefBitsCombo);
            this.Controls.Add(this.decRowButton);
            this.Controls.Add(this.incRowButton);
            this.Controls.Add(this.clearRowButton);
            this.Controls.Add(this.setRowButton);
            this.Controls.Add(this.bitGridView);
            this.Controls.Add(this.highBitsCheck);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.shiftRight);
            this.Controls.Add(this.shiftLeft);
            this.Controls.Add(this.shiftBits);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.clearAllButton);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.decMotorola);
            this.Controls.Add(this.hexMotorola);
            this.Controls.Add(this.predefBitsSaveButton);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Bit Calc 64";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.Form1_Click);
            this.DoubleClick += new System.EventHandler(this.Form1_DoubleClick);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.bitGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox hexMotorola;
        private System.Windows.Forms.TextBox decMotorola;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Button clearAllButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox shiftBits;
        private System.Windows.Forms.Button shiftLeft;
        private System.Windows.Forms.Button shiftRight;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.CheckBox highBitsCheck;
        private System.Windows.Forms.DataGridView bitGridView;
        private System.Windows.Forms.Button setRowButton;
        private System.Windows.Forms.Button clearRowButton;
        private System.Windows.Forms.Button incRowButton;
        private System.Windows.Forms.Button decRowButton;
        private System.Windows.Forms.ComboBox predefBitsCombo;
        private System.Windows.Forms.Button predefBitsSaveButton; 
        private System.Windows.Forms.Timer saveTimer;
        private System.Windows.Forms.TextBox ibmBitCount;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.TextBox labelEdit;
        private System.Windows.Forms.Button button7;
        private SearchList filterList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Start;
        private System.Windows.Forms.DataGridViewTextBoxColumn End;
        private System.Windows.Forms.DataGridViewTextBoxColumn Desciption;
        private System.Windows.Forms.DataGridViewTextBoxColumn Hex;
        private System.Windows.Forms.DataGridViewTextBoxColumn Decimal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Binary;
        private System.Windows.Forms.CheckBox msbBit0;
        private System.Windows.Forms.Button EndianSwap;
        private System.Windows.Forms.Button EndianSwapPage;
        
    }
}

