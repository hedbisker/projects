namespace Movies_Online_Desktop_App
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
            this.StopBtn = new System.Windows.Forms.Button();
            this.runStatus = new System.Windows.Forms.Label();
            this.typeFilter = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.subtittleFilter = new System.Windows.Forms.TextBox();
            this.moviesCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.startSearch = new System.Windows.Forms.Button();
            this.filterTittle = new System.Windows.Forms.Label();
            this.TittleFiltertextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.sortListBox = new System.Windows.Forms.ListBox();
            this.rateFilter = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Filters = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.categoryFilter = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lengthFilter = new System.Windows.Forms.TextBox();
            this.labble = new System.Windows.Forms.Label();
            this.yearFilter = new System.Windows.Forms.TextBox();
            this.incorrectFilter = new System.Windows.Forms.Label();
            this.gridChanger = new System.Windows.Forms.Button();
            this.gridModText = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // StopBtn
            // 
            this.StopBtn.Font = new System.Drawing.Font("Verdana", 12F);
            this.StopBtn.Location = new System.Drawing.Point(818, 78);
            this.StopBtn.Margin = new System.Windows.Forms.Padding(2);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(222, 34);
            this.StopBtn.TabIndex = 35;
            this.StopBtn.Text = "Stop";
            this.StopBtn.UseVisualStyleBackColor = true;
            // 
            // runStatus
            // 
            this.runStatus.AutoSize = true;
            this.runStatus.Font = new System.Drawing.Font("Verdana", 12F);
            this.runStatus.Location = new System.Drawing.Point(975, 6);
            this.runStatus.Name = "runStatus";
            this.runStatus.Size = new System.Drawing.Size(0, 18);
            this.runStatus.TabIndex = 34;
            // 
            // typeFilter
            // 
            this.typeFilter.Font = new System.Drawing.Font("Verdana", 12F);
            this.typeFilter.FormattingEnabled = true;
            this.typeFilter.Items.AddRange(new object[] {
            "Movies",
            "Series"});
            this.typeFilter.Location = new System.Drawing.Point(350, 185);
            this.typeFilter.Name = "typeFilter";
            this.typeFilter.Size = new System.Drawing.Size(120, 48);
            this.typeFilter.Sorted = true;
            this.typeFilter.TabIndex = 33;
            this.typeFilter.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.typeFilter_ItemCheck);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 12F);
            this.label4.Location = new System.Drawing.Point(9, 45);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 18);
            this.label4.TabIndex = 32;
            this.label4.Text = "Subtittle";
            // 
            // subtittleFilter
            // 
            this.subtittleFilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.subtittleFilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.subtittleFilter.Font = new System.Drawing.Font("Verdana", 12F);
            this.subtittleFilter.Location = new System.Drawing.Point(95, 42);
            this.subtittleFilter.Margin = new System.Windows.Forms.Padding(2);
            this.subtittleFilter.Name = "subtittleFilter";
            this.subtittleFilter.Size = new System.Drawing.Size(249, 27);
            this.subtittleFilter.TabIndex = 31;
            this.subtittleFilter.TextChanged += new System.EventHandler(this.subtittleTextBox_TextChanged);
            this.subtittleFilter.Validating += new System.ComponentModel.CancelEventHandler(this.subtittleTextBox_Validating);
            // 
            // moviesCount
            // 
            this.moviesCount.AutoSize = true;
            this.moviesCount.Font = new System.Drawing.Font("Verdana", 12F);
            this.moviesCount.Location = new System.Drawing.Point(951, 8);
            this.moviesCount.Name = "moviesCount";
            this.moviesCount.Size = new System.Drawing.Size(18, 18);
            this.moviesCount.TabIndex = 30;
            this.moviesCount.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 12F);
            this.label3.Location = new System.Drawing.Point(815, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 18);
            this.label3.TabIndex = 29;
            this.label3.Text = "movies count :";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.Location = new System.Drawing.Point(6, 238);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(807, 461);
            this.dataGridView1.TabIndex = 28;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            this.dataGridView1.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseMove);
            this.dataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseClick);
            this.dataGridView1.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
            // 
            // startSearch
            // 
            this.startSearch.Font = new System.Drawing.Font("Verdana", 12F);
            this.startSearch.Location = new System.Drawing.Point(818, 26);
            this.startSearch.Margin = new System.Windows.Forms.Padding(2);
            this.startSearch.Name = "startSearch";
            this.startSearch.Size = new System.Drawing.Size(222, 48);
            this.startSearch.TabIndex = 23;
            this.startSearch.Text = "Start";
            this.startSearch.UseVisualStyleBackColor = true;
            this.startSearch.Click += new System.EventHandler(this.startSearch_Click);
            // 
            // filterTittle
            // 
            this.filterTittle.AutoSize = true;
            this.filterTittle.Font = new System.Drawing.Font("Verdana", 12F);
            this.filterTittle.Location = new System.Drawing.Point(11, 78);
            this.filterTittle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.filterTittle.Name = "filterTittle";
            this.filterTittle.Size = new System.Drawing.Size(52, 18);
            this.filterTittle.TabIndex = 38;
            this.filterTittle.Text = "Tittle";
            // 
            // TittleFiltertextBox
            // 
            this.TittleFiltertextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.TittleFiltertextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.TittleFiltertextBox.Font = new System.Drawing.Font("Verdana", 12F);
            this.TittleFiltertextBox.Location = new System.Drawing.Point(68, 74);
            this.TittleFiltertextBox.Multiline = true;
            this.TittleFiltertextBox.Name = "TittleFiltertextBox";
            this.TittleFiltertextBox.Size = new System.Drawing.Size(276, 27);
            this.TittleFiltertextBox.TabIndex = 37;
            this.TittleFiltertextBox.TextChanged += new System.EventHandler(this.TittleFiltertextBox_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 12F);
            this.label5.Location = new System.Drawing.Point(589, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(157, 18);
            this.label5.TabIndex = 40;
            this.label5.Text = "The Imporent sort";
            // 
            // sortListBox
            // 
            this.sortListBox.Font = new System.Drawing.Font("Verdana", 12F);
            this.sortListBox.FormattingEnabled = true;
            this.sortListBox.ItemHeight = 18;
            this.sortListBox.Location = new System.Drawing.Point(592, 67);
            this.sortListBox.Name = "sortListBox";
            this.sortListBox.Size = new System.Drawing.Size(220, 166);
            this.sortListBox.TabIndex = 39;
            // 
            // rateFilter
            // 
            this.rateFilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.rateFilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.rateFilter.Font = new System.Drawing.Font("Verdana", 12F);
            this.rateFilter.Location = new System.Drawing.Point(63, 206);
            this.rateFilter.Margin = new System.Windows.Forms.Padding(2);
            this.rateFilter.Name = "rateFilter";
            this.rateFilter.Size = new System.Drawing.Size(281, 27);
            this.rateFilter.TabIndex = 42;
            this.rateFilter.TextChanged += new System.EventHandler(this.rateFilter_TextChanged);
            this.rateFilter.Validating += new System.ComponentModel.CancelEventHandler(this.rateFilter_Validating);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 12F);
            this.label7.Location = new System.Drawing.Point(13, 209);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 18);
            this.label7.TabIndex = 45;
            this.label7.Text = "Rate";
            // 
            // Filters
            // 
            this.Filters.AutoSize = true;
            this.Filters.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Filters.Location = new System.Drawing.Point(5, 6);
            this.Filters.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Filters.Name = "Filters";
            this.Filters.Size = new System.Drawing.Size(86, 25);
            this.Filters.TabIndex = 46;
            this.Filters.Text = "Filters";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 12F);
            this.label8.Location = new System.Drawing.Point(11, 176);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 18);
            this.label8.TabIndex = 48;
            this.label8.Text = "Category";
            // 
            // categoryFilter
            // 
            this.categoryFilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.categoryFilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.categoryFilter.Font = new System.Drawing.Font("Verdana", 12F);
            this.categoryFilter.Location = new System.Drawing.Point(95, 173);
            this.categoryFilter.Multiline = true;
            this.categoryFilter.Name = "categoryFilter";
            this.categoryFilter.Size = new System.Drawing.Size(249, 27);
            this.categoryFilter.TabIndex = 47;
            this.categoryFilter.TextChanged += new System.EventHandler(this.categoryFilter_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 12F);
            this.label9.Location = new System.Drawing.Point(9, 143);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 18);
            this.label9.TabIndex = 54;
            this.label9.Text = "Length";
            // 
            // lengthFilter
            // 
            this.lengthFilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.lengthFilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.lengthFilter.Font = new System.Drawing.Font("Verdana", 12F);
            this.lengthFilter.Location = new System.Drawing.Point(77, 140);
            this.lengthFilter.Margin = new System.Windows.Forms.Padding(2);
            this.lengthFilter.Name = "lengthFilter";
            this.lengthFilter.Size = new System.Drawing.Size(267, 27);
            this.lengthFilter.TabIndex = 53;
            this.lengthFilter.TextChanged += new System.EventHandler(this.lengthFilter_TextChanged);
            this.lengthFilter.Validating += new System.ComponentModel.CancelEventHandler(this.lengthFilter_Validating);
            // 
            // labble
            // 
            this.labble.AutoSize = true;
            this.labble.Font = new System.Drawing.Font("Verdana", 12F);
            this.labble.Location = new System.Drawing.Point(9, 109);
            this.labble.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labble.Name = "labble";
            this.labble.Size = new System.Drawing.Size(44, 18);
            this.labble.TabIndex = 56;
            this.labble.Text = "Year";
            // 
            // yearFilter
            // 
            this.yearFilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.yearFilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.yearFilter.Font = new System.Drawing.Font("Verdana", 12F);
            this.yearFilter.Location = new System.Drawing.Point(57, 106);
            this.yearFilter.Margin = new System.Windows.Forms.Padding(2);
            this.yearFilter.Name = "yearFilter";
            this.yearFilter.Size = new System.Drawing.Size(287, 27);
            this.yearFilter.TabIndex = 55;
            this.yearFilter.TextChanged += new System.EventHandler(this.yearFilter_TextChanged);
            this.yearFilter.Validating += new System.ComponentModel.CancelEventHandler(this.yearFilter_Validating);
            // 
            // incorrectFilter
            // 
            this.incorrectFilter.AutoSize = true;
            this.incorrectFilter.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.incorrectFilter.Location = new System.Drawing.Point(115, 10);
            this.incorrectFilter.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.incorrectFilter.Name = "incorrectFilter";
            this.incorrectFilter.Size = new System.Drawing.Size(0, 25);
            this.incorrectFilter.TabIndex = 57;
            // 
            // gridChanger
            // 
            this.gridChanger.Font = new System.Drawing.Font("Verdana", 12F);
            this.gridChanger.Location = new System.Drawing.Point(818, 185);
            this.gridChanger.Name = "gridChanger";
            this.gridChanger.Size = new System.Drawing.Size(205, 46);
            this.gridChanger.TabIndex = 58;
            this.gridChanger.Text = "To watched list";
            this.gridChanger.UseVisualStyleBackColor = true;
            this.gridChanger.Click += new System.EventHandler(this.gridChanger_Click);
            // 
            // gridModText
            // 
            this.gridModText.AutoSize = true;
            this.gridModText.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridModText.Location = new System.Drawing.Point(819, 124);
            this.gridModText.Name = "gridModText";
            this.gridModText.Size = new System.Drawing.Size(0, 25);
            this.gridModText.TabIndex = 59;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(818, 238);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(318, 461);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 60;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1140, 710);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.gridModText);
            this.Controls.Add(this.gridChanger);
            this.Controls.Add(this.incorrectFilter);
            this.Controls.Add(this.labble);
            this.Controls.Add(this.yearFilter);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lengthFilter);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.categoryFilter);
            this.Controls.Add(this.Filters);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.rateFilter);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.sortListBox);
            this.Controls.Add(this.filterTittle);
            this.Controls.Add(this.TittleFiltertextBox);
            this.Controls.Add(this.StopBtn);
            this.Controls.Add(this.runStatus);
            this.Controls.Add(this.typeFilter);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.subtittleFilter);
            this.Controls.Add(this.moviesCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.startSearch);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button StopBtn;
        private System.Windows.Forms.Label runStatus;
        private System.Windows.Forms.CheckedListBox typeFilter;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox subtittleFilter;
        private System.Windows.Forms.Label moviesCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button startSearch;
        private System.Windows.Forms.Label filterTittle;
        private System.Windows.Forms.TextBox TittleFiltertextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox sortListBox;
        private System.Windows.Forms.TextBox rateFilter;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label Filters;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox categoryFilter;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox lengthFilter;
        private System.Windows.Forms.Label labble;
        private System.Windows.Forms.TextBox yearFilter;
        private System.Windows.Forms.Label incorrectFilter;
        private System.Windows.Forms.Button gridChanger;
        private System.Windows.Forms.Label gridModText;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

