using System;
using System.Drawing;
using System.Windows.Forms;

namespace hw1
{
    public partial class Form1 : Form
    {
        private const string FormTitleText = "\u500b\u4eba\u623f\u8cb8\u8a66\u7b97\u5668";
        // 個人房貸試算器

        private const string SubtitleText = "\u8f38\u5165\u623f\u50f9\u3001\u5229\u7387\u8207\u5e74\u9650\u5f8c\uff0c\u7acb\u5373\u53d6\u5f97\u6708\u4ed8\u91d1\u3001\u9996\u671f\u660e\u7d30\u8207\u7e3d\u9084\u6b3e\u8cc7\u8a0a\u3002";
        // 輸入房價、利率與年限後，立即取得月付金、首期明細與總還款資訊。

        private const string InputTitleText = "\u8cb8\u6b3e\u8cc7\u8a0a\u8f38\u5165";
        // 貸款資訊輸入

        private const string InputHintText = "\u652f\u63f4\u623f\u50f9\u3001\u81ea\u5099\u6b3e\u767e\u5206\u6bd4\u6216\u91d1\u984d\u3001\u5e74\u5229\u7387\u3001\u8cb8\u6b3e\u5e74\u9650\u8207\u5bec\u9650\u671f\u3002";
        // 支援房價、自備款百分比或金額、年利率、貸款年限與寬限期。

        private const string DownPaymentHintText = "\u9810\u8a2d\u4ee5\u767e\u5206\u6bd4\u8f38\u5165\u81ea\u5099\u6b3e\uff0c\u4e5f\u53ef\u4ee5\u5207\u63db\u6210\u76f4\u63a5\u8f38\u5165\u91d1\u984d\u3002";
        // 預設以百分比輸入自備款，也可以切換成直接輸入金額。

        private const string ResultTitleText = "\u8a66\u7b97\u7d50\u679c";
        // 試算結果

        private const string ResultIdleText = "\u8f38\u5165\u5b8c\u6210\u5f8c\u6309\u4e0b\u300c\u8a08\u7b97\u623f\u8cb8\u300d\uff0c\u7d50\u679c\u6703\u986f\u793a\u5728\u4e0b\u65b9\u8cc7\u8a0a\u5361\u3002";
        // 輸入完成後按下「計算房貸」，結果會顯示在下方資訊卡。

        private const string SummaryTitleText = "\u6458\u8981\u8207\u8aaa\u660e";
        // 摘要與說明

        private const string FormulaText = "\u6708\u4ed8\u91d1\u516c\u5f0f\uff1aM = P x [r(1+r)^n] / [(1+r)^n - 1]\uff0c\u5176\u4e2d P \u70ba\u8cb8\u6b3e\u672c\u91d1\u3001r \u70ba\u6708\u5229\u7387\u3001n \u70ba\u671f\u6578\u3002\r\n\u6709\u5bec\u9650\u671f\u6642\uff0c\u524d\u6bb5\u671f\u9593\u50c5\u7e73\u5229\u606f\uff0c\u4e4b\u5f8c\u518d\u4f9d\u5269\u9918\u671f\u6578\u91cd\u65b0\u8a08\u7b97\u672c\u606f\u6514\u9084\u3002";
        // 月付金公式：M = P x [r(1+r)^n] / [(1+r)^n - 1]，其中 P 為貸款本金、r 為月利率、n 為期數。
        // 有寬限期時，前段期間僅繳利息，之後再依剩餘期數重新計算本息攤還。

        private const string ReferenceText = "\u53c3\u8003\u8cc7\u6599\uff1a\u4e2d\u570b\u4fe1\u8a17\u623f\u8cb8\u8a66\u7b97\u8aaa\u660e (CTBC)";
        // 參考資料：中國信託房貸試算說明 (CTBC)

        private const string ReferenceUrl = "https://www.ctbcbank.com/twrbo/zh_tw/index/ctbc_article/hloan_article/blog_mortgage_buying/N2024060400017_0102.html";

        private readonly Font titleFont = new Font("Microsoft JhengHei UI", 20F, FontStyle.Bold);
        private readonly Font sectionFont = new Font("Microsoft JhengHei UI", 13F, FontStyle.Bold);
        private readonly Font bodyFont = new Font("Microsoft JhengHei UI", 10F, FontStyle.Regular);
        private readonly Font bodyBoldFont = new Font("Microsoft JhengHei UI", 10F, FontStyle.Bold);
        private readonly Font unitFont = new Font("Microsoft JhengHei UI", 9F, FontStyle.Regular);
        private readonly Font metricTitleFont = new Font("Microsoft JhengHei UI", 8.8F, FontStyle.Bold);
        private readonly Font metricValueFont = new Font("Microsoft JhengHei UI", 12.5F, FontStyle.Bold);
        private readonly Font noteFont = new Font("Microsoft JhengHei UI", 8.6F, FontStyle.Regular);

        private TextBox txtHousePrice;
        private TextBox txtDownPayment;
        private TextBox txtInterestRate;
        private TextBox txtLoanTerm;
        private TextBox txtGracePeriod;
        private ComboBox cmbDownPaymentType;
        private Label lblDownPayment;
        private Label lblDownPaymentUnit;

        private Label lblResultStatus;
        private Label lblMonthlyPaymentNote;
        private Label lblGracePeriodSummary;
        private Label lblLoanAmountValue;
        private Label lblMonthlyPaymentValue;
        private Label lblFirstInterestValue;
        private Label lblFirstPrincipalValue;
        private Label lblTotalInterestValue;
        private Label lblTotalRepaymentValue;
        private LinkLabel linkSource;

        public Form1()
        {
            InitializeComponent();
            BuildUi();
            ResetResultDisplay();
        }

        private void BuildUi()
        {
            SuspendLayout();

            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(946, 470);
            Text = FormTitleText;
            BackColor = Color.FromArgb(241, 245, 249);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = true;
            Font = bodyFont;

            Controls.Clear();

            TableLayoutPanel rootLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Padding = new Padding(14, 12, 14, 12),
            };
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 376F));

            rootLayout.Controls.Add(CreateHeaderPanel(), 0, 0);
            rootLayout.Controls.Add(CreateContentLayout(), 0, 1);

            Controls.Add(rootLayout);

            ResumeLayout(false);
        }

        private Control CreateHeaderPanel()
        {
            Panel panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(241, 245, 249)
            };

            Label titleLabel = new Label
            {
                AutoSize = true,
                Location = new Point(0, 2),
                Font = titleFont,
                ForeColor = Color.FromArgb(34, 122, 191),
                Text = FormTitleText
            };

            Label subtitleLabel = new Label
            {
                AutoSize = true,
                Location = new Point(2, 35),
                Font = new Font("Microsoft JhengHei UI", 9F),
                ForeColor = Color.FromArgb(98, 113, 132),
                Text = SubtitleText
            };

            panel.Controls.Add(titleLabel);
            panel.Controls.Add(subtitleLabel);
            return panel;
        }

        private Control CreateContentLayout()
        {
            TableLayoutPanel contentLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1
            };
            contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 350F));
            contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 552F));

            Panel inputPanel = CreateInputCard();
            inputPanel.Margin = new Padding(0, 0, 10, 0);

            Panel resultPanel = CreateResultCard();
            resultPanel.Margin = new Padding(0);

            contentLayout.Controls.Add(inputPanel, 0, 0);
            contentLayout.Controls.Add(resultPanel, 1, 0);

            return contentLayout;
        }

        private Panel CreateInputCard()
        {
            Panel panel = CreateCardPanel();
            panel.Padding = new Padding(14, 12, 14, 12);

            Label title = new Label
            {
                AutoSize = true,
                Font = sectionFont,
                ForeColor = Color.FromArgb(45, 62, 80),
                Location = new Point(0, 0),
                Text = InputTitleText
            };

            Label hint = new Label
            {
                AutoSize = true,
                Font = new Font("Microsoft JhengHei UI", 9F),
                ForeColor = Color.FromArgb(118, 130, 145),
                Location = new Point(1, 26),
                Text = InputHintText
            };

            TableLayoutPanel inputTable = new TableLayoutPanel
            {
                Location = new Point(0, 54),
                Size = new Size(310, 252),
                ColumnCount = 3,
                RowCount = 6
            };
            inputTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 92F));
            inputTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 154F));
            inputTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 34F));

            for (int i = 0; i < 6; i++)
            {
                inputTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            }

            inputTable.Controls.Add(CreateFieldLabel("\u623f\u5c4b\u7e3d\u50f9"), 0, 0);
            // 房屋總價
            txtHousePrice = CreateInputTextBox("3000000");
            inputTable.Controls.Add(txtHousePrice, 1, 0);
            inputTable.Controls.Add(CreateUnitLabel("\u5143"), 2, 0);
            // 元

            inputTable.Controls.Add(CreateFieldLabel("\u81ea\u5099\u6b3e\u65b9\u5f0f"), 0, 1);
            // 自備款方式
            cmbDownPaymentType = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = bodyFont,
                Margin = new Padding(0, 0, 8, 8)
            };
            cmbDownPaymentType.Items.AddRange(new object[] { "\u767e\u5206\u6bd4 (%)", "\u91d1\u984d (\u5143)" });
            // 百分比 (%)、金額 (元)
            cmbDownPaymentType.SelectedIndex = 0;
            cmbDownPaymentType.SelectedIndexChanged += CmbDownPaymentType_SelectedIndexChanged;
            inputTable.Controls.Add(cmbDownPaymentType, 1, 1);
            inputTable.Controls.Add(CreateUnitLabel("\u5207\u63db"), 2, 1);
            // 切換

            lblDownPayment = CreateFieldLabel("\u81ea\u5099\u6b3e\u6bd4\u4f8b");
            // 自備款比例
            inputTable.Controls.Add(lblDownPayment, 0, 2);
            txtDownPayment = CreateInputTextBox("20");
            inputTable.Controls.Add(txtDownPayment, 1, 2);
            lblDownPaymentUnit = CreateUnitLabel("%");
            inputTable.Controls.Add(lblDownPaymentUnit, 2, 2);

            inputTable.Controls.Add(CreateFieldLabel("\u8cb8\u6b3e\u5229\u7387"), 0, 3);
            // 貸款利率
            txtInterestRate = CreateInputTextBox("2.15");
            inputTable.Controls.Add(txtInterestRate, 1, 3);
            inputTable.Controls.Add(CreateUnitLabel("% / \u5e74"), 2, 3);
            // % / 年

            inputTable.Controls.Add(CreateFieldLabel("\u8cb8\u6b3e\u5e74\u9650"), 0, 4);
            // 貸款年限
            txtLoanTerm = CreateInputTextBox("30");
            inputTable.Controls.Add(txtLoanTerm, 1, 4);
            inputTable.Controls.Add(CreateUnitLabel("\u5e74"), 2, 4);
            // 年

            inputTable.Controls.Add(CreateFieldLabel("\u5bec\u9650\u671f"), 0, 5);
            // 寬限期
            txtGracePeriod = CreateInputTextBox("0");
            inputTable.Controls.Add(txtGracePeriod, 1, 5);
            inputTable.Controls.Add(CreateUnitLabel("\u5e74\uff0c\u9078\u586b"), 2, 5);
            // 年，選填

            Label note = new Label
            {
                AutoSize = true,
                Font = noteFont,
                ForeColor = Color.FromArgb(118, 130, 145),
                Location = new Point(1, 310),
                Text = DownPaymentHintText
            };

            Panel buttonRow = new Panel
            {
                Location = new Point(0, 338),
                Size = new Size(310, 38)
            };

            Button btnCalculate = new Button
            {
                Text = "\u8a08\u7b97\u623f\u8cb8",
                // 計算房貸
                Font = new Font("Microsoft JhengHei UI", 10.5F, FontStyle.Bold),
                BackColor = Color.FromArgb(31, 122, 214),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Location = new Point(0, 0),
                Size = new Size(136, 36)
            };
            btnCalculate.FlatAppearance.BorderSize = 0;
            btnCalculate.Click += BtnCalculate_Click;

            Button btnReset = new Button
            {
                Text = "\u91cd\u8a2d",
                // 重設
                Font = new Font("Microsoft JhengHei UI", 10.5F, FontStyle.Bold),
                BackColor = Color.FromArgb(231, 236, 243),
                ForeColor = Color.FromArgb(76, 91, 108),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Location = new Point(146, 0),
                Size = new Size(82, 36)
            };
            btnReset.FlatAppearance.BorderSize = 0;
            btnReset.Click += BtnReset_Click;

            buttonRow.Controls.Add(btnCalculate);
            buttonRow.Controls.Add(btnReset);

            panel.Controls.Add(title);
            panel.Controls.Add(hint);
            panel.Controls.Add(inputTable);
            panel.Controls.Add(note);
            panel.Controls.Add(buttonRow);

            return panel;
        }

        private Panel CreateResultCard()
        {
            Panel panel = CreateCardPanel();
            panel.Padding = new Padding(14, 12, 14, 12);

            Label title = new Label
            {
                AutoSize = true,
                Font = sectionFont,
                ForeColor = Color.FromArgb(45, 62, 80),
                Location = new Point(0, 0),
                Text = ResultTitleText
            };

            lblResultStatus = new Label
            {
                AutoSize = true,
                Font = new Font("Microsoft JhengHei UI", 9.5F),
                ForeColor = Color.FromArgb(73, 101, 131),
                Location = new Point(1, 27),
                Text = ResultIdleText
            };

            TableLayoutPanel resultGrid = new TableLayoutPanel
            {
                Location = new Point(0, 52),
                Size = new Size(560, 162),
                ColumnCount = 2,
                RowCount = 3
            };
            resultGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            resultGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            resultGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            resultGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            resultGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));

            resultGrid.Controls.Add(CreateMetricCard("\u8cb8\u6b3e\u7e3d\u91d1\u984d", out lblLoanAmountValue), 0, 0);
            // 貸款總金額
            resultGrid.Controls.Add(CreateMetricCard("\u6bcf\u6708\u61c9\u7e73\u91d1\u984d", out lblMonthlyPaymentValue), 1, 0);
            // 每月應繳金額
            resultGrid.Controls.Add(CreateMetricCard("\u9996\u671f\u5229\u606f", out lblFirstInterestValue), 0, 1);
            // 首期利息
            resultGrid.Controls.Add(CreateMetricCard("\u9996\u671f\u672c\u91d1", out lblFirstPrincipalValue), 1, 1);
            // 首期本金
            resultGrid.Controls.Add(CreateMetricCard("\u7e3d\u5229\u606f\u652f\u51fa", out lblTotalInterestValue), 0, 2);
            // 總利息支出
            resultGrid.Controls.Add(CreateMetricCard("\u7e3d\u9084\u6b3e\u91d1\u984d", out lblTotalRepaymentValue), 1, 2);
            // 總還款金額

            Panel summaryPanel = new Panel
            {
                Location = new Point(0, 222),
                Size = new Size(560, 122),
                BackColor = Color.FromArgb(246, 249, 252),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(12, 10, 12, 10)
            };

            Label summaryTitle = new Label
            {
                AutoSize = true,
                Font = new Font("Microsoft JhengHei UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(63, 79, 96),
                Location = new Point(0, 0),
                Text = SummaryTitleText
            };

            lblMonthlyPaymentNote = new Label
            {
                AutoSize = true,
                Font = new Font("Microsoft JhengHei UI", 9.3F),
                ForeColor = Color.FromArgb(90, 103, 118),
                Location = new Point(1, 24)
            };

            lblGracePeriodSummary = new Label
            {
                AutoSize = true,
                Font = new Font("Microsoft JhengHei UI", 9.3F),
                ForeColor = Color.FromArgb(90, 103, 118),
                Location = new Point(1, 48)
            };

            TextBox txtFormula = new TextBox
            {
                Location = new Point(0, 74),
                Size = new Size(532, 32),
                Multiline = true,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                BackColor = Color.FromArgb(246, 249, 252),
                Font = new Font("Microsoft JhengHei UI", 8.4F),
                Text = FormulaText
            };

            linkSource = new LinkLabel
            {
                Location = new Point(0, 354),
                Size = new Size(560, 22),
                Font = new Font("Microsoft JhengHei UI", 9.5F),
                Text = ReferenceText
            };
            linkSource.Links.Add(0, linkSource.Text.Length, ReferenceUrl);
            linkSource.LinkClicked += LinkSource_LinkClicked;

            summaryPanel.Controls.Add(summaryTitle);
            summaryPanel.Controls.Add(lblMonthlyPaymentNote);
            summaryPanel.Controls.Add(lblGracePeriodSummary);
            summaryPanel.Controls.Add(txtFormula);

            panel.Controls.Add(title);
            panel.Controls.Add(lblResultStatus);
            panel.Controls.Add(resultGrid);
            panel.Controls.Add(summaryPanel);
            panel.Controls.Add(linkSource);

            return panel;
        }

        private Panel CreateMetricCard(string title, out Label valueLabel)
        {
            Panel card = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(248, 250, 253),
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0, 0, 8, 8),
                Padding = new Padding(10, 8, 10, 8)
            };

            Label titleLabel = new Label
            {
                AutoSize = true,
                Font = metricTitleFont,
                ForeColor = Color.FromArgb(67, 94, 122),
                Location = new Point(0, 0),
                Text = title
            };

            valueLabel = new Label
            {
                AutoSize = true,
                Font = metricValueFont,
                ForeColor = Color.FromArgb(30, 50, 74),
                Location = new Point(0, 20),
                Text = "\u5c1a\u672a\u8a08\u7b97"
                // 尚未計算
            };

            card.Controls.Add(titleLabel);
            card.Controls.Add(valueLabel);
            return card;
        }

        private static Panel CreateCardPanel()
        {
            return new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
        }

        private Label CreateFieldLabel(string text)
        {
            return new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = bodyBoldFont,
                ForeColor = Color.FromArgb(60, 74, 89),
                Margin = new Padding(0, 0, 6, 8)
            };
        }

        private TextBox CreateInputTextBox(string defaultValue)
        {
            return new TextBox
            {
                Text = defaultValue,
                Dock = DockStyle.Fill,
                Font = bodyFont,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0, 0, 8, 8)
            };
        }

        private Label CreateUnitLabel(string text)
        {
            return new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = unitFont,
                ForeColor = Color.FromArgb(118, 130, 145),
                Margin = new Padding(0, 0, 0, 8)
            };
        }

        private void CmbDownPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDownPaymentType.SelectedIndex == 0)
            {
                lblDownPayment.Text = "\u81ea\u5099\u6b3e\u6bd4\u4f8b";
                // 自備款比例
                lblDownPaymentUnit.Text = "%";
                txtDownPayment.Text = "20";
            }
            else
            {
                lblDownPayment.Text = "\u81ea\u5099\u6b3e\u91d1\u984d";
                // 自備款金額
                lblDownPaymentUnit.Text = "\u5143";
                // 元
                txtDownPayment.Text = "600000";
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            txtHousePrice.Text = "3000000";
            cmbDownPaymentType.SelectedIndex = 0;
            txtDownPayment.Text = "20";
            txtInterestRate.Text = "2.15";
            txtLoanTerm.Text = "30";
            txtGracePeriod.Text = "0";
            lblDownPayment.Text = "\u81ea\u5099\u6b3e\u6bd4\u4f8b";
            // 自備款比例
            lblDownPaymentUnit.Text = "%";
            ResetResultDisplay();
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                decimal housePrice = decimal.Parse(txtHousePrice.Text);
                decimal annualInterestRate = decimal.Parse(txtInterestRate.Text) / 100m;
                int loanTermYears = int.Parse(txtLoanTerm.Text);
                int gracePeriodYears = string.IsNullOrWhiteSpace(txtGracePeriod.Text) ? 0 : int.Parse(txtGracePeriod.Text);

                if (housePrice <= 0)
                {
                    ShowInputError("\u623f\u5c4b\u7e3d\u50f9\u5fc5\u9808\u5927\u65bc 0");
                    // 房屋總價必須大於 0
                    return;
                }

                if (annualInterestRate < 0)
                {
                    ShowInputError("\u8cb8\u6b3e\u5229\u7387\u4e0d\u80fd\u5c0f\u65bc 0");
                    // 貸款利率不能小於 0
                    return;
                }

                if (loanTermYears <= 0)
                {
                    ShowInputError("\u8cb8\u6b3e\u5e74\u9650\u5fc5\u9808\u5927\u65bc 0");
                    // 貸款年限必須大於 0
                    return;
                }

                if (gracePeriodYears < 0 || gracePeriodYears > loanTermYears)
                {
                    ShowInputError("\u5bec\u9650\u671f\u5fc5\u9808\u4ecb\u65bc 0 \u8207\u8cb8\u6b3e\u5e74\u9650\u4e4b\u9593");
                    // 寬限期必須介於 0 與貸款年限之間
                    return;
                }

                decimal downPaymentAmount;
                if (cmbDownPaymentType.SelectedIndex == 0)
                {
                    decimal downPaymentPercent = decimal.Parse(txtDownPayment.Text);
                    if (downPaymentPercent < 0 || downPaymentPercent >= 100)
                    {
                        ShowInputError("\u81ea\u5099\u6b3e\u6bd4\u4f8b\u5fc5\u9808\u4ecb\u65bc 0 \u5230 100 \u4e4b\u9593");
                        // 自備款比例必須介於 0 到 100 之間
                        return;
                    }

                    downPaymentAmount = housePrice * (downPaymentPercent / 100m);
                }
                else
                {
                    downPaymentAmount = decimal.Parse(txtDownPayment.Text);
                }

                if (downPaymentAmount < 0 || downPaymentAmount >= housePrice)
                {
                    ShowInputError("\u81ea\u5099\u6b3e\u91d1\u984d\u5fc5\u9808\u5927\u65bc\u7b49\u65bc 0\uff0c\u4e14\u5c0f\u65bc\u623f\u5c4b\u7e3d\u50f9");
                    // 自備款金額必須大於等於 0，且小於房屋總價
                    return;
                }

                decimal loanAmount = housePrice - downPaymentAmount;
                decimal monthlyRate = annualInterestRate / 12m;
                int totalMonths = loanTermYears * 12;
                int graceMonths = gracePeriodYears * 12;

                decimal monthlyPayment;
                decimal firstMonthInterest;
                decimal firstMonthPrincipal;
                decimal totalInterest;
                decimal totalRepayment;

                if (graceMonths > 0)
                {
                    int amortizationMonths = totalMonths - graceMonths;
                    if (amortizationMonths <= 0)
                    {
                        ShowInputError("\u5bec\u9650\u671f\u4e0d\u80fd\u7b49\u65bc\u6216\u8d85\u904e\u8cb8\u6b3e\u7e3d\u671f\u6578");
                        // 寬限期不能等於或超過貸款總期數
                        return;
                    }

                    decimal graceMonthlyInterest = loanAmount * monthlyRate;
                    decimal principalAfterGrace = loanAmount * (decimal)Math.Pow((double)(1 + monthlyRate), graceMonths);

                    monthlyPayment = CalculateMonthlyPayment(principalAfterGrace, monthlyRate, amortizationMonths);
                    firstMonthInterest = principalAfterGrace * monthlyRate;
                    firstMonthPrincipal = monthlyPayment - firstMonthInterest;
                    totalInterest = graceMonthlyInterest * graceMonths + (monthlyPayment * amortizationMonths - principalAfterGrace);
                    totalRepayment = loanAmount + totalInterest;

                    lblResultStatus.Text = "\u5df2\u5b8c\u6210\u542b\u5bec\u9650\u671f\u7684\u623f\u8cb8\u8a66\u7b97\u3002";
                    // 已完成含寬限期的房貸試算。
                    lblMonthlyPaymentNote.Text =
                        $"\u5bec\u9650\u671f {gracePeriodYears} \u5e74\u5167\u6bcf\u6708\u50c5\u7e73\u5229\u606f {FormatCurrency(graceMonthlyInterest)}\uff1b\u5bec\u9650\u671f\u5f8c\u6bcf\u6708\u672c\u606f {FormatCurrency(monthlyPayment)}\u3002";
                    // 寬限期 X 年內每月僅繳利息；寬限期後每月本息 X 元。
                    lblGracePeriodSummary.Text =
                        $"\u9996\u671f\u5229\u606f\u8207\u9996\u671f\u672c\u91d1\u986f\u793a\u7684\u662f\u5bec\u9650\u671f\u7d50\u675f\u5f8c\u7b2c\u4e00\u671f\u7684\u6514\u9084\u660e\u7d30\uff0c\u5269\u9918\u6514\u9084\u671f\u70ba {amortizationMonths} \u500b\u6708\u3002";
                    // 首期利息與首期本金顯示的是寬限期結束後第一期的攤還明細，剩餘攤還期為 X 個月。
                }
                else
                {
                    monthlyPayment = CalculateMonthlyPayment(loanAmount, monthlyRate, totalMonths);
                    firstMonthInterest = loanAmount * monthlyRate;
                    firstMonthPrincipal = monthlyPayment - firstMonthInterest;
                    totalInterest = monthlyPayment * totalMonths - loanAmount;
                    totalRepayment = loanAmount + totalInterest;

                    lblResultStatus.Text = "\u5df2\u5b8c\u6210\u6a19\u6e96\u672c\u606f\u6514\u9084\u8a66\u7b97\u3002";
                    // 已完成標準本息攤還試算。
                    lblMonthlyPaymentNote.Text = $"\u6bcf\u6708\u61c9\u7e73\u91d1\u984d\u70ba {FormatCurrency(monthlyPayment)}\uff0c\u91d1\u984d\u5df2\u5305\u542b\u672c\u91d1\u8207\u5229\u606f\u3002";
                    // 每月應繳金額為 X 元，金額已包含本金與利息。
                    lblGracePeriodSummary.Text = "\u672a\u8a2d\u5b9a\u5bec\u9650\u671f\uff0c\u9996\u671f\u5229\u606f\u8207\u9996\u671f\u672c\u91d1\u5373\u70ba\u7b2c\u4e00\u500b\u6708\u7684\u5be6\u969b\u9084\u6b3e\u660e\u7d30\u3002";
                    // 未設定寬限期，首期利息與首期本金即為第一個月的實際還款明細。
                }

                lblLoanAmountValue.Text = FormatCurrency(loanAmount);
                lblMonthlyPaymentValue.Text = FormatCurrency(monthlyPayment);
                lblFirstInterestValue.Text = FormatCurrency(firstMonthInterest);
                lblFirstPrincipalValue.Text = FormatCurrency(firstMonthPrincipal);
                lblTotalInterestValue.Text = FormatCurrency(totalInterest);
                lblTotalRepaymentValue.Text = FormatCurrency(totalRepayment);
            }
            catch (FormatException)
            {
                ShowInputError("\u8acb\u8f38\u5165\u6709\u6548\u7684\u6578\u5b57");
                // 請輸入有效的數字
                ResetResultDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"\u8a08\u7b97\u51fa\u932f\uff1a{ex.Message}", "\u932f\u8aa4", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // 計算出錯
                ResetResultDisplay();
            }
        }

        private decimal CalculateMonthlyPayment(decimal principal, decimal monthlyRate, int months)
        {
            if (months <= 0)
            {
                throw new InvalidOperationException("\u671f\u6578\u5fc5\u9808\u5927\u65bc 0");
                // 期數必須大於 0
            }

            if (monthlyRate == 0)
            {
                return principal / months;
            }

            decimal growthFactor = (decimal)Math.Pow((double)(1 + monthlyRate), months);
            decimal numerator = monthlyRate * growthFactor;
            decimal denominator = growthFactor - 1m;
            return principal * (numerator / denominator);
        }

        private void ResetResultDisplay()
        {
            const string notCalculated = "\u5c1a\u672a\u8a08\u7b97";
            // 尚未計算

            lblLoanAmountValue.Text = notCalculated;
            lblMonthlyPaymentValue.Text = notCalculated;
            lblFirstInterestValue.Text = notCalculated;
            lblFirstPrincipalValue.Text = notCalculated;
            lblTotalInterestValue.Text = notCalculated;
            lblTotalRepaymentValue.Text = notCalculated;

            lblResultStatus.Text = ResultIdleText;
            lblMonthlyPaymentNote.Text = "\u6bcf\u6708\u61c9\u7e73\u91d1\u984d\u6703\u5728\u8a08\u7b97\u5f8c\u986f\u793a\u672c\u606f\u6514\u9084\u7d50\u679c\u3002";
            // 每月應繳金額會在計算後顯示本息攤還結果。
            lblGracePeriodSummary.Text = "\u5bec\u9650\u671f\u82e5\u70ba 0\uff0c\u8868\u793a\u8cb8\u6b3e\u671f\u9593\u81ea\u9996\u6708\u8d77\u958b\u59cb\u6514\u9084\u672c\u91d1\u3002";
            // 寬限期若為 0，表示貸款期間自首月起開始攤還本金。
        }

        private void ShowInputError(string message)
        {
            MessageBox.Show(message, "\u8f38\u5165\u932f\u8aa4", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            // 輸入錯誤
        }

        private static string FormatCurrency(decimal amount)
        {
            return $"{amount:N2} \u5143";
        }

        private void LinkSource_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(e.Link.LinkData.ToString())
                {
                    UseShellExecute = true
                };
                System.Diagnostics.Process.Start(startInfo);
            }
            catch
            {
            }
        }
    }
}
