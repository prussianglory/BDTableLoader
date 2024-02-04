
namespace MultiTableLoader
{
    partial class databaseForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.bottomConsole = new System.Windows.Forms.RichTextBox();
            this.LoadedTableLabel = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.LoadedTableComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.LoadToDBBtn = new System.Windows.Forms.ToolStripButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bottomConsole);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 384);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 66);
            this.panel1.TabIndex = 3;
            // 
            // bottomConsole
            // 
            this.bottomConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomConsole.Location = new System.Drawing.Point(0, 0);
            this.bottomConsole.Name = "bottomConsole";
            this.bottomConsole.Size = new System.Drawing.Size(800, 66);
            this.bottomConsole.TabIndex = 0;
            this.bottomConsole.Text = "";
            // 
            // LoadedTableLabel
            // 
            this.LoadedTableLabel.AutoSize = true;
            this.LoadedTableLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.LoadedTableLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LoadedTableLabel.Location = new System.Drawing.Point(0, 0);
            this.LoadedTableLabel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.LoadedTableLabel.Name = "LoadedTableLabel";
            this.LoadedTableLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LoadedTableLabel.Size = new System.Drawing.Size(85, 24);
            this.LoadedTableLabel.TabIndex = 4;
            this.LoadedTableLabel.Text = "Таблица";
            this.LoadedTableLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.LoadedTableComboBox,
            this.LoadToDBBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 28);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(44, 25);
            this.toolStripLabel1.Text = "Лист:";
            // 
            // LoadedTableComboBox
            // 
            this.LoadedTableComboBox.Name = "LoadedTableComboBox";
            this.LoadedTableComboBox.Size = new System.Drawing.Size(121, 28);
            // 
            // LoadToDBBtn
            // 
            this.LoadToDBBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LoadToDBBtn.Image = global::MultiTableLoader.Properties.Resources.load_to_database1;
            this.LoadToDBBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LoadToDBBtn.Name = "LoadToDBBtn";
            this.LoadToDBBtn.Size = new System.Drawing.Size(29, 25);
            this.LoadToDBBtn.Text = "Загрузить в БД";
            this.LoadToDBBtn.Click += new System.EventHandler(this.LoadtoDBClick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 52);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(800, 332);
            this.dataGridView1.TabIndex = 6;
            // 
            // databaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.LoadedTableLabel);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "databaseForm";
            this.Text = "databaseForm";
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label LoadedTableLabel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        public System.Windows.Forms.ToolStripComboBox LoadedTableComboBox;
        private System.Windows.Forms.ToolStripButton LoadToDBBtn;
        private System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.RichTextBox bottomConsole;
    }
}