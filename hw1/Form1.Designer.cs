namespace hw1
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            // 調整 ClientSize 以剛好包覆標題與內容，避免多餘空白
            this.ClientSize = new System.Drawing.Size(1025, 465);
            this.Text = "個人房貸試算器";
            this.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // 主面板
            System.Windows.Forms.Panel mainPanel = new System.Windows.Forms.Panel();
            mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            mainPanel.AutoScroll = true;
            mainPanel.AutoSize = false; // 固定面板尺寸，避免內容變動時自動改變視窗
            mainPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            mainPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            mainPanel.Padding = new System.Windows.Forms.Padding(0);

            // 使用小邊距並精確定位，避免多餘空白
            int margin = 8;
            int titleHeight = 48;
            int currentY = margin;

            System.Windows.Forms.Label titleLabel = new System.Windows.Forms.Label();
            titleLabel.Text = "🏠 個人房貸試算器";
            titleLabel.Font = new System.Drawing.Font("微軟正黑體", 20F, System.Drawing.FontStyle.Bold);
            titleLabel.ForeColor = System.Drawing.Color.FromArgb(41, 128, 185);
            titleLabel.Location = new System.Drawing.Point(margin, currentY);
            titleLabel.Size = new System.Drawing.Size(1000 - margin * 2, titleHeight);
            titleLabel.AutoSize = false;
            mainPanel.Controls.Add(titleLabel);
            currentY += titleHeight + margin;

            // (說明區已移除，畫面改為左右兩欄)

            // ==================== 左右內容容器 ====================
            System.Windows.Forms.Panel contentPanel = new System.Windows.Forms.Panel();
            contentPanel.Location = new System.Drawing.Point(margin, currentY);
            contentPanel.Size = new System.Drawing.Size(1000 - margin * 2, 395 - margin);
            contentPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            contentPanel.AutoSize = false;

            // ============ 左側：輸入區 ============
            System.Windows.Forms.GroupBox inputGroup = new System.Windows.Forms.GroupBox();
            inputGroup.Text = "📝 貸款資訊輸入";
            inputGroup.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            inputGroup.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            inputGroup.Location = new System.Drawing.Point(0, 0);
            inputGroup.Size = new System.Drawing.Size(480, 395);
            inputGroup.AutoSize = false; // 固定輸入區大小
            inputGroup.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            inputGroup.BackColor = System.Drawing.Color.White;
            inputGroup.Padding = new System.Windows.Forms.Padding(20, 25, 20, 15);

            int groupY = 0;
            int labelWidth = 140;
            int inputWidth = 180;
            int controlHeight = 28;
            int spacing = 50;

            // 房屋總價
            System.Windows.Forms.Label lblHousePrice = new System.Windows.Forms.Label();
            lblHousePrice.Text = "房屋總價 (新台幣)";
            lblHousePrice.Font = new System.Drawing.Font("微軟正黑體", 12F);
            lblHousePrice.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            lblHousePrice.Location = new System.Drawing.Point(20, groupY);
            lblHousePrice.Size = new System.Drawing.Size(labelWidth, 24);
            inputGroup.Controls.Add(lblHousePrice);

            this.txtHousePrice = new System.Windows.Forms.TextBox();
            this.txtHousePrice.Location = new System.Drawing.Point(labelWidth + 40, groupY);
            this.txtHousePrice.Size = new System.Drawing.Size(inputWidth, controlHeight);
            this.txtHousePrice.Text = "3000000";
            this.txtHousePrice.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.txtHousePrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            inputGroup.Controls.Add(this.txtHousePrice);

            System.Windows.Forms.Label lblHousePriceUnit = new System.Windows.Forms.Label();
            lblHousePriceUnit.Text = "元";
            lblHousePriceUnit.Font = new System.Drawing.Font("微軟正黑體", 12F);
            lblHousePriceUnit.ForeColor = System.Drawing.Color.Gray;
            lblHousePriceUnit.Location = new System.Drawing.Point(labelWidth + 185, groupY + 6);
            lblHousePriceUnit.Size = new System.Drawing.Size(30, 20);
            inputGroup.Controls.Add(lblHousePriceUnit);
            groupY += spacing;

            // 自備款選擇
            System.Windows.Forms.Label lblDownPaymentType = new System.Windows.Forms.Label();
            lblDownPaymentType.Text = "自備款方式";
            lblDownPaymentType.Font = new System.Drawing.Font("微軟正黑體", 12F);
            lblDownPaymentType.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            lblDownPaymentType.Location = new System.Drawing.Point(20, groupY);
            lblDownPaymentType.Size = new System.Drawing.Size(labelWidth, 24);
            inputGroup.Controls.Add(lblDownPaymentType);

            this.cmbDownPaymentType = new System.Windows.Forms.ComboBox();
            this.cmbDownPaymentType.Location = new System.Drawing.Point(labelWidth + 40, groupY);
            this.cmbDownPaymentType.Size = new System.Drawing.Size(inputWidth, controlHeight);
            this.cmbDownPaymentType.Items.AddRange(new string[] { "百分比 (%)", "金額 (元)" });
            this.cmbDownPaymentType.SelectedIndex = 0;
            this.cmbDownPaymentType.SelectedIndexChanged += new System.EventHandler(this.CmbDownPaymentType_SelectedIndexChanged);
            inputGroup.Controls.Add(this.cmbDownPaymentType);
            groupY += spacing;

            // 自備款
            this.lblDownPayment = new System.Windows.Forms.Label();
            this.lblDownPayment.Text = "自備款 (%)";
            this.lblDownPayment.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.lblDownPayment.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.lblDownPayment.Location = new System.Drawing.Point(20, groupY);
            this.lblDownPayment.Size = new System.Drawing.Size(labelWidth, 24);
            inputGroup.Controls.Add(this.lblDownPayment);

            this.txtDownPayment = new System.Windows.Forms.TextBox();
            this.txtDownPayment.Location = new System.Drawing.Point(labelWidth + 40, groupY);
            this.txtDownPayment.Size = new System.Drawing.Size(inputWidth, controlHeight);
            this.txtDownPayment.Text = "20";
            this.txtDownPayment.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.txtDownPayment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            inputGroup.Controls.Add(this.txtDownPayment);

            this.lblDownPaymentUnit = new System.Windows.Forms.Label();
            this.lblDownPaymentUnit.Text = "%";
            this.lblDownPaymentUnit.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.lblDownPaymentUnit.ForeColor = System.Drawing.Color.Gray;
            this.lblDownPaymentUnit.Location = new System.Drawing.Point(labelWidth + 185, groupY + 6);
            this.lblDownPaymentUnit.Size = new System.Drawing.Size(30, 20);
            inputGroup.Controls.Add(this.lblDownPaymentUnit);
            groupY += spacing;

            // 年利率
            System.Windows.Forms.Label lblInterestRate = new System.Windows.Forms.Label();
            lblInterestRate.Text = "年利率 (%)";
            lblInterestRate.Font = new System.Drawing.Font("微軟正黑體", 12F);
            lblInterestRate.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            lblInterestRate.Location = new System.Drawing.Point(20, groupY);
            lblInterestRate.Size = new System.Drawing.Size(labelWidth, 24);
            inputGroup.Controls.Add(lblInterestRate);

            this.txtInterestRate = new System.Windows.Forms.TextBox();
            this.txtInterestRate.Location = new System.Drawing.Point(labelWidth + 40, groupY);
            this.txtInterestRate.Size = new System.Drawing.Size(inputWidth, controlHeight);
            this.txtInterestRate.Text = "2.68";
            this.txtInterestRate.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.txtInterestRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            inputGroup.Controls.Add(this.txtInterestRate);

            System.Windows.Forms.Label lblInterestRateUnit = new System.Windows.Forms.Label();
            lblInterestRateUnit.Text = "%";
            lblInterestRateUnit.Font = new System.Drawing.Font("微軟正黑體", 12F);
            lblInterestRateUnit.ForeColor = System.Drawing.Color.Gray;
            lblInterestRateUnit.Location = new System.Drawing.Point(labelWidth + 185, groupY + 6);
            lblInterestRateUnit.Size = new System.Drawing.Size(30, 20);
            inputGroup.Controls.Add(lblInterestRateUnit);
            groupY += spacing;

            // 貸款年限
            System.Windows.Forms.Label lblLoanTerm = new System.Windows.Forms.Label();
            lblLoanTerm.Text = "貸款年限 (年)";
            lblLoanTerm.Font = new System.Drawing.Font("微軟正黑體", 12F);
            lblLoanTerm.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            lblLoanTerm.Location = new System.Drawing.Point(20, groupY);
            lblLoanTerm.Size = new System.Drawing.Size(labelWidth, 24);
            inputGroup.Controls.Add(lblLoanTerm);

            this.txtLoanTerm = new System.Windows.Forms.TextBox();
            this.txtLoanTerm.Location = new System.Drawing.Point(labelWidth + 40, groupY);
            this.txtLoanTerm.Size = new System.Drawing.Size(inputWidth, controlHeight);
            this.txtLoanTerm.Text = "30";
            this.txtLoanTerm.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.txtLoanTerm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            inputGroup.Controls.Add(this.txtLoanTerm);

            System.Windows.Forms.Label lblLoanTermUnit = new System.Windows.Forms.Label();
            lblLoanTermUnit.Text = "年";
            lblLoanTermUnit.Font = new System.Drawing.Font("微軟正黑體", 12F);
            lblLoanTermUnit.ForeColor = System.Drawing.Color.Gray;
            lblLoanTermUnit.Location = new System.Drawing.Point(labelWidth + 185, groupY + 6);
            lblLoanTermUnit.Size = new System.Drawing.Size(30, 20);
            inputGroup.Controls.Add(lblLoanTermUnit);
            groupY += spacing;

            // 寬限期
            System.Windows.Forms.Label lblGracePeriod = new System.Windows.Forms.Label();
            lblGracePeriod.Text = "寬限期 (年)";
            lblGracePeriod.Font = new System.Drawing.Font("微軟正黑體", 12F);
            lblGracePeriod.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            lblGracePeriod.Location = new System.Drawing.Point(20, groupY);
            lblGracePeriod.Size = new System.Drawing.Size(labelWidth, 24);
            inputGroup.Controls.Add(lblGracePeriod);

            this.txtGracePeriod = new System.Windows.Forms.TextBox();
            this.txtGracePeriod.Location = new System.Drawing.Point(labelWidth + 40, groupY);
            this.txtGracePeriod.Size = new System.Drawing.Size(inputWidth, controlHeight);
            this.txtGracePeriod.Text = "0";
            this.txtGracePeriod.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.txtGracePeriod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            inputGroup.Controls.Add(this.txtGracePeriod);

            System.Windows.Forms.Label lblGracePeriodUnit = new System.Windows.Forms.Label();
            lblGracePeriodUnit.Text = "年（選填）";
            lblGracePeriodUnit.Font = new System.Drawing.Font("微軟正黑體", 12F);
            lblGracePeriodUnit.ForeColor = System.Drawing.Color.Gray;
            lblGracePeriodUnit.Location = new System.Drawing.Point(labelWidth + 185, groupY + 6);
            lblGracePeriodUnit.Size = new System.Drawing.Size(90, 20);
            inputGroup.Controls.Add(lblGracePeriodUnit);
            groupY += spacing;

            // 計算按鈕
            System.Windows.Forms.Button btnCalculate = new System.Windows.Forms.Button();
            btnCalculate.Text = "💰 計算房貸";
            btnCalculate.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            btnCalculate.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            btnCalculate.ForeColor = System.Drawing.Color.White;
            btnCalculate.Location = new System.Drawing.Point(20, groupY);
            btnCalculate.Size = new System.Drawing.Size(120, 36);
            btnCalculate.Cursor = System.Windows.Forms.Cursors.Hand;
            btnCalculate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCalculate.Click += new System.EventHandler(this.BtnCalculate_Click);
            inputGroup.Controls.Add(btnCalculate);

            // 重設按鈕
            System.Windows.Forms.Button btnReset = new System.Windows.Forms.Button();
            btnReset.Text = "🔄 重設";
            btnReset.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            btnReset.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            btnReset.ForeColor = System.Drawing.Color.White;
            btnReset.Location = new System.Drawing.Point(150, groupY);
            btnReset.Size = new System.Drawing.Size(120, 36);
            btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnReset.Click += new System.EventHandler(this.BtnReset_Click);
            inputGroup.Controls.Add(btnReset);

            // ============ 右側：結果區 ============
            System.Windows.Forms.GroupBox resultGroup = new System.Windows.Forms.GroupBox();
            resultGroup.Text = "📊 試算結果";
            resultGroup.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            resultGroup.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            resultGroup.Location = new System.Drawing.Point(495, 0);
            resultGroup.Size = new System.Drawing.Size(505, 395);
            resultGroup.BackColor = System.Drawing.Color.White;
            resultGroup.Padding = new System.Windows.Forms.Padding(10, 10, 10, 10);

            this.txtResults = new System.Windows.Forms.TextBox();
            this.txtResults.Location = new System.Drawing.Point(10, 28);
            this.txtResults.Size = new System.Drawing.Size(485, 240);
            this.txtResults.Multiline = true;
            this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResults.ReadOnly = true;
            this.txtResults.Font = new System.Drawing.Font("Courier New", 11F);
            this.txtResults.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.txtResults.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resultGroup.Controls.Add(this.txtResults);

            // 公式標題
            System.Windows.Forms.Label lblFormulaTitle = new System.Windows.Forms.Label();
            lblFormulaTitle.Text = "公式與參考來源";
            lblFormulaTitle.Font = new System.Drawing.Font("微軟正黑體", 10F, System.Drawing.FontStyle.Bold);
            lblFormulaTitle.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            lblFormulaTitle.Location = new System.Drawing.Point(10, 270);
            lblFormulaTitle.Size = new System.Drawing.Size(200, 22);
            resultGroup.Controls.Add(lblFormulaTitle);

            // 公式說明文字框
            this.txtFormula = new System.Windows.Forms.TextBox();
            this.txtFormula.Location = new System.Drawing.Point(10, 295);
            this.txtFormula.Size = new System.Drawing.Size(485, 60);
            this.txtFormula.Multiline = true;
            this.txtFormula.ReadOnly = true;
            this.txtFormula.Font = new System.Drawing.Font("微軟正黑體", 10F);
            this.txtFormula.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            this.txtFormula.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFormula.Text = "月付金公式: M = P × [r(1+r)^n] / [(1+r)^n - 1]  (P=貸款本金, r=月利率, n=期數)\r\n寬限期(僅利息): 每月利息 = P × r ; 寬限期後本金示例: P_after = P × (1+r)^g";
            resultGroup.Controls.Add(this.txtFormula);

            // 參考來源 LinkLabel
            this.linkSource = new System.Windows.Forms.LinkLabel();
            this.linkSource.Location = new System.Drawing.Point(10, 360);
            this.linkSource.Size = new System.Drawing.Size(480, 20);
            this.linkSource.Text = "參考來源：中國信託房貸試算說明 (CTBC)";
            this.linkSource.Links.Add(6, 6, "https://www.ctbcbank.com/content/dam/minisite/long/loan/mortgage/index.html");
            this.linkSource.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkSource_LinkClicked);
            resultGroup.Controls.Add(this.linkSource);

            // 把輸入區與結果區加入 contentPanel
            contentPanel.Controls.Add(inputGroup);
            contentPanel.Controls.Add(resultGroup);

            mainPanel.Controls.Add(contentPanel);
            this.Controls.Add(mainPanel);
        }

        #endregion

        private System.Windows.Forms.TextBox txtHousePrice;
        private System.Windows.Forms.TextBox txtDownPayment;
        private System.Windows.Forms.TextBox txtInterestRate;
        private System.Windows.Forms.TextBox txtLoanTerm;
        private System.Windows.Forms.TextBox txtGracePeriod;
        private System.Windows.Forms.TextBox txtResults;
        private System.Windows.Forms.TextBox txtFormula;
        private System.Windows.Forms.LinkLabel linkSource;
        private System.Windows.Forms.ComboBox cmbDownPaymentType;
        private System.Windows.Forms.Label lblDownPayment;
        private System.Windows.Forms.Label lblDownPaymentUnit;
    }
}

