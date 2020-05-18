namespace BorderLessEr
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.processComboBox = new System.Windows.Forms.ComboBox();
            this.processListLabel = new System.Windows.Forms.Label();
            this.refreshButton = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            this.locationZeroCheckBox = new System.Windows.Forms.CheckBox();
            this.fullscreenCheckBox = new System.Windows.Forms.CheckBox();
            this.releaseButton = new System.Windows.Forms.Button();
            this.minimumCheckBox = new System.Windows.Forms.CheckBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // processComboBox
            // 
            this.processComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.processComboBox.FormattingEnabled = true;
            this.processComboBox.Location = new System.Drawing.Point(98, 13);
            this.processComboBox.Name = "processComboBox";
            this.processComboBox.Size = new System.Drawing.Size(572, 21);
            this.processComboBox.TabIndex = 0;
            // 
            // processListLabel
            // 
            this.processListLabel.AutoSize = true;
            this.processListLabel.Location = new System.Drawing.Point(14, 16);
            this.processListLabel.Name = "processListLabel";
            this.processListLabel.Size = new System.Drawing.Size(78, 14);
            this.processListLabel.TabIndex = 1;
            this.processListLabel.Text = "プロセス一覧";
            // 
            // refreshButton
            // 
            this.refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshButton.Location = new System.Drawing.Point(682, 11);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(87, 25);
            this.refreshButton.TabIndex = 2;
            this.refreshButton.Text = "再検索";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // applyButton
            // 
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.applyButton.Location = new System.Drawing.Point(322, 50);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(348, 97);
            this.applyButton.TabIndex = 3;
            this.applyButton.Text = "ボーダーレス化";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // locationZeroCheckBox
            // 
            this.locationZeroCheckBox.AutoSize = true;
            this.locationZeroCheckBox.Checked = true;
            this.locationZeroCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.locationZeroCheckBox.Location = new System.Drawing.Point(162, 67);
            this.locationZeroCheckBox.Name = "locationZeroCheckBox";
            this.locationZeroCheckBox.Size = new System.Drawing.Size(154, 18);
            this.locationZeroCheckBox.TabIndex = 4;
            this.locationZeroCheckBox.Text = "座標を0,0に移動させる";
            this.locationZeroCheckBox.UseVisualStyleBackColor = true;
            // 
            // fullscreenCheckBox
            // 
            this.fullscreenCheckBox.AutoSize = true;
            this.fullscreenCheckBox.Checked = true;
            this.fullscreenCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.fullscreenCheckBox.Location = new System.Drawing.Point(68, 91);
            this.fullscreenCheckBox.Name = "fullscreenCheckBox";
            this.fullscreenCheckBox.Size = new System.Drawing.Size(242, 18);
            this.fullscreenCheckBox.TabIndex = 5;
            this.fullscreenCheckBox.Text = "ウィンドウサイズをモニタサイズに拡張する";
            this.fullscreenCheckBox.UseVisualStyleBackColor = true;
            // 
            // releaseButton
            // 
            this.releaseButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.releaseButton.Location = new System.Drawing.Point(682, 50);
            this.releaseButton.Name = "releaseButton";
            this.releaseButton.Size = new System.Drawing.Size(88, 97);
            this.releaseButton.TabIndex = 6;
            this.releaseButton.Text = "解除";
            this.releaseButton.UseVisualStyleBackColor = true;
            this.releaseButton.Click += new System.EventHandler(this.releaseButton_Click);
            // 
            // minimumCheckBox
            // 
            this.minimumCheckBox.AutoSize = true;
            this.minimumCheckBox.Checked = true;
            this.minimumCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.minimumCheckBox.Location = new System.Drawing.Point(133, 47);
            this.minimumCheckBox.Name = "minimumCheckBox";
            this.minimumCheckBox.Size = new System.Drawing.Size(183, 18);
            this.minimumCheckBox.TabIndex = 7;
            this.minimumCheckBox.Text = "最小化時タスクトレイに格納";
            this.minimumCheckBox.UseVisualStyleBackColor = true;
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipText = "BorderLessEr";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "BorderLessEr";
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_DoubleClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 159);
            this.Controls.Add(this.minimumCheckBox);
            this.Controls.Add(this.releaseButton);
            this.Controls.Add(this.fullscreenCheckBox);
            this.Controls.Add(this.locationZeroCheckBox);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.processListLabel);
            this.Controls.Add(this.processComboBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Borderless Modding";
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox processComboBox;
        private System.Windows.Forms.Label processListLabel;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.CheckBox locationZeroCheckBox;
        private System.Windows.Forms.CheckBox fullscreenCheckBox;
        private System.Windows.Forms.Button releaseButton;
        private System.Windows.Forms.CheckBox minimumCheckBox;
        private System.Windows.Forms.NotifyIcon notifyIcon;
    }
}

