namespace GameAppBattleField
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
            this.components = new System.ComponentModel.Container();
            this.panelGame = new System.Windows.Forms.Panel();
            this.txtBxColumns = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBxRows = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.tmrGame = new System.Windows.Forms.Timer(this.components);
            this.btnStartGame = new System.Windows.Forms.Button();
            this.nmrTimeInterval = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nmrTimeInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // panelGame
            // 
            this.panelGame.Location = new System.Drawing.Point(48, 46);
            this.panelGame.Name = "panelGame";
            this.panelGame.Size = new System.Drawing.Size(800, 600);
            this.panelGame.TabIndex = 0;
            this.panelGame.Paint += new System.Windows.Forms.PaintEventHandler(this.panelGame_Paint);
            // 
            // txtBxColumns
            // 
            this.txtBxColumns.Location = new System.Drawing.Point(887, 74);
            this.txtBxColumns.Name = "txtBxColumns";
            this.txtBxColumns.Size = new System.Drawing.Size(188, 27);
            this.txtBxColumns.TabIndex = 1;
            this.txtBxColumns.Text = "7";
            this.txtBxColumns.TextChanged += new System.EventHandler(this.txtBxColumns_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(895, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "X (количество столбцов)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(909, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Y ( количество строк)";
            // 
            // txtBxRows
            // 
            this.txtBxRows.Location = new System.Drawing.Point(888, 139);
            this.txtBxRows.Name = "txtBxRows";
            this.txtBxRows.Size = new System.Drawing.Size(187, 27);
            this.txtBxRows.TabIndex = 3;
            this.txtBxRows.Text = "7";
            this.txtBxRows.TextChanged += new System.EventHandler(this.txtBxRows_TextChanged);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(887, 172);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(187, 29);
            this.btnGenerate.TabIndex = 5;
            this.btnGenerate.Text = "Генерировать поле";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // tmrGame
            // 
            this.tmrGame.Tick += new System.EventHandler(this.tmrGame_Tick);
            // 
            // btnStartGame
            // 
            this.btnStartGame.Location = new System.Drawing.Point(888, 372);
            this.btnStartGame.Name = "btnStartGame";
            this.btnStartGame.Size = new System.Drawing.Size(187, 29);
            this.btnStartGame.TabIndex = 6;
            this.btnStartGame.Text = "Начать игру";
            this.btnStartGame.UseVisualStyleBackColor = true;
            this.btnStartGame.Click += new System.EventHandler(this.btnStartGame_Click);
            // 
            // nmrTimeInterval
            // 
            this.nmrTimeInterval.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nmrTimeInterval.Location = new System.Drawing.Point(888, 339);
            this.nmrTimeInterval.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nmrTimeInterval.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nmrTimeInterval.Name = "nmrTimeInterval";
            this.nmrTimeInterval.Size = new System.Drawing.Size(187, 27);
            this.nmrTimeInterval.TabIndex = 7;
            this.nmrTimeInterval.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(929, 306);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Скорость игры";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1119, 671);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nmrTimeInterval);
            this.Controls.Add(this.btnStartGame);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBxRows);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBxColumns);
            this.Controls.Add(this.panelGame);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.nmrTimeInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panelGame;
        private TextBox txtBxColumns;
        private Label label1;
        private Label label2;
        private TextBox txtBxRows;
        private Button btnGenerate;
        private System.Windows.Forms.Timer tmrGame;
        private Button btnStartGame;
        private NumericUpDown nmrTimeInterval;
        private Label label3;
    }
}