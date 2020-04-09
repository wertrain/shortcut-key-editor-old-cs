namespace ShortcutKeyEditor
{
    partial class KeySetControl
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.labelKeyName = new System.Windows.Forms.Label();
            this.textBoxKeyEdit = new System.Windows.Forms.TextBox();
            this.labelSeparator = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelKeyName
            // 
            this.labelKeyName.AutoSize = true;
            this.labelKeyName.Location = new System.Drawing.Point(11, 8);
            this.labelKeyName.Name = "labelKeyName";
            this.labelKeyName.Size = new System.Drawing.Size(83, 12);
            this.labelKeyName.TabIndex = 0;
            this.labelKeyName.Text = "ショートカットキー";
            // 
            // textBoxKeyEdit
            // 
            this.textBoxKeyEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxKeyEdit.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxKeyEdit.Location = new System.Drawing.Point(153, 4);
            this.textBoxKeyEdit.Name = "textBoxKeyEdit";
            this.textBoxKeyEdit.ReadOnly = true;
            this.textBoxKeyEdit.Size = new System.Drawing.Size(150, 19);
            this.textBoxKeyEdit.TabIndex = 1;
            this.textBoxKeyEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxKeyEdit_KeyDown);
            this.textBoxKeyEdit.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.textBoxKeyEdit_PreviewKeyDown);
            // 
            // labelSeparator
            // 
            this.labelSeparator.AutoSize = true;
            this.labelSeparator.Location = new System.Drawing.Point(141, 8);
            this.labelSeparator.Name = "labelSeparator";
            this.labelSeparator.Size = new System.Drawing.Size(7, 12);
            this.labelSeparator.TabIndex = 2;
            this.labelSeparator.Text = ":";
            // 
            // KeySetControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.labelSeparator);
            this.Controls.Add(this.textBoxKeyEdit);
            this.Controls.Add(this.labelKeyName);
            this.Name = "KeySetControl";
            this.Size = new System.Drawing.Size(309, 29);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelKeyName;
        private System.Windows.Forms.TextBox textBoxKeyEdit;
        private System.Windows.Forms.Label labelSeparator;
    }
}
