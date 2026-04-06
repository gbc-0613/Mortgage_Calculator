using System;
using System.Text;
using System.Windows.Forms;

namespace hw1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void CmbDownPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDownPaymentType.SelectedIndex == 0)
            {
                // 百分比模式
                lblDownPayment.Text = "自備款 (%)";
                lblDownPaymentUnit.Text = "%";
                txtDownPayment.Text = "20";
            }
            else
            {
                // 金額模式
                lblDownPayment.Text = "自備款 (元)";
                lblDownPaymentUnit.Text = "元";
                txtDownPayment.Text = "600000";
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            // 恢復所有輸入欄位至預設值
            txtHousePrice.Text = "3000000";
            cmbDownPaymentType.SelectedIndex = 0;
            txtDownPayment.Text = "20";
            txtInterestRate.Text = "2.68";
            txtLoanTerm.Text = "30";
            txtGracePeriod.Text = "0";
            txtResults.Text = "";
            lblDownPayment.Text = "自備款 (%)";
            lblDownPaymentUnit.Text = "%";
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                // 驗證並解析輸入
                decimal housePrice = decimal.Parse(txtHousePrice.Text);
                decimal interestRate = decimal.Parse(txtInterestRate.Text) / 100; // 轉換為小數
                int loanTerm = int.Parse(txtLoanTerm.Text);
                int gracePeriod = string.IsNullOrWhiteSpace(txtGracePeriod.Text) ? 0 : int.Parse(txtGracePeriod.Text);

                // 驗證輸入的合理性
                if (housePrice <= 0)
                {
                    MessageBox.Show("房屋總價必須大於 0", "輸入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (loanTerm <= 0)
                {
                    MessageBox.Show("貸款年限必須大於 0", "輸入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (gracePeriod > loanTerm)
                {
                    MessageBox.Show("寬限期不能大於貸款年限", "輸入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 計算自備款和貸款金額
                decimal downPaymentAmount;
                if (cmbDownPaymentType.SelectedIndex == 0)
                {
                    // 百分比模式
                    decimal downPaymentPercent = decimal.Parse(txtDownPayment.Text) / 100;
                    downPaymentAmount = housePrice * downPaymentPercent;
                }
                else
                {
                    // 金額模式
                    downPaymentAmount = decimal.Parse(txtDownPayment.Text);
                }

                if (downPaymentAmount < 0 || downPaymentAmount > housePrice)
                {
                    MessageBox.Show("自備款金額不能為負數或超過房屋總價", "輸入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal loanAmount = housePrice - downPaymentAmount;

                if (loanAmount <= 0)
                {
                    MessageBox.Show("貸款金額必須大於 0", "輸入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 計算月利率
                decimal monthlyRate = interestRate / 12;
                int totalMonths = loanTerm * 12;
                int graceMonths = gracePeriod * 12;

                // 計算每月本息金額（攤還期間）
                decimal monthlyPayment = 0;
                if (monthlyRate > 0)
                {
                    // 使用標準房貸公式：M = P * [r(1+r)^n] / [(1+r)^n - 1]
                    // 其中 P = 貸款金額, r = 月利率, n = 攤還月數
                    decimal numerator = monthlyRate * (decimal)Math.Pow((double)(1 + monthlyRate), totalMonths);
                    decimal denominator = (decimal)Math.Pow((double)(1 + monthlyRate), totalMonths) - 1;
                    monthlyPayment = loanAmount * (numerator / denominator);
                }
                else
                {
                    monthlyPayment = loanAmount / totalMonths;
                }

                // 計算首期利息和本金
                decimal firstMonthInterest = loanAmount * monthlyRate;
                decimal firstMonthPrincipal = monthlyPayment - firstMonthInterest;

                // 計算總利息和總還款金額
                decimal totalInterest = (monthlyPayment * (totalMonths - graceMonths)) - (loanAmount - (gracePeriod > 0 ? loanAmount * (decimal)Math.Pow((double)(1 + monthlyRate), graceMonths) : 0));
                decimal totalRepayment = loanAmount + totalInterest;

                // 更正總利息計算（考慮寬限期）
                if (gracePeriod > 0)
                {
                    // 寬限期內只繳利息
                    decimal graceInterest = loanAmount * monthlyRate * graceMonths;

                    // 寬限期後本金餘額（寬限期內本金會累積利息）
                    decimal principalAfterGrace = loanAmount * (decimal)Math.Pow((double)(1 + monthlyRate), graceMonths);

                    // 重新計算攤還期間的月付金
                    int amortizationMonths = totalMonths - graceMonths;
                    if (monthlyRate > 0 && amortizationMonths > 0)
                    {
                        decimal numerator2 = monthlyRate * (decimal)Math.Pow((double)(1 + monthlyRate), amortizationMonths);
                        decimal denominator2 = (decimal)Math.Pow((double)(1 + monthlyRate), amortizationMonths) - 1;
                        monthlyPayment = principalAfterGrace * (numerator2 / denominator2);
                    }
                    else
                    {
                        monthlyPayment = principalAfterGrace / amortizationMonths;
                    }

                    totalInterest = graceInterest + (monthlyPayment * amortizationMonths - principalAfterGrace);
                    totalRepayment = loanAmount + totalInterest;

                    // 更新首期數據（寬限期後的第一期）
                    firstMonthInterest = principalAfterGrace * monthlyRate;
                    firstMonthPrincipal = monthlyPayment - firstMonthInterest;
                }

                // 格式化輸出
                StringBuilder results = new StringBuilder();
                results.AppendLine("═══════════════════════════════════════════════════");
                results.AppendLine("                    房貸試算結果摘要                  ");
                results.AppendLine("═══════════════════════════════════════════════════");
                results.AppendLine();
                results.AppendLine("【 基本資訊 】");
                results.AppendLine($"  房屋總價          {housePrice,15:N2} 元");
                results.AppendLine($"  自備款金額        {downPaymentAmount,15:N2} 元");
                results.AppendLine($"  自備款比例        {(downPaymentAmount / housePrice * 100),14:F2}%");
                results.AppendLine($"  貸款金額          {loanAmount,15:N2} 元");
                results.AppendLine();
                results.AppendLine("【 利率與期限 】");
                results.AppendLine($"  年利率            {(interestRate * 100),14:F2}%");
                results.AppendLine($"  月利率            {(monthlyRate * 100),14:F4}%");
                results.AppendLine($"  貸款年限          {loanTerm,19} 年 ({totalMonths} 個月)");
                if (gracePeriod > 0)
                {
                    results.AppendLine($"  寬限期            {gracePeriod,19} 年 ({graceMonths} 個月)");
                    results.AppendLine($"  攤還期            {loanTerm - gracePeriod,19} 年");
                }
                results.AppendLine();
                results.AppendLine("【 月付金資訊 】");
                if (gracePeriod > 0)
                {
                    results.AppendLine($"  寬限期內月付金    {(loanAmount * monthlyRate),15:N2} 元 (僅利息)");
                    results.AppendLine($"  攤還期月付金      {monthlyPayment,15:N2} 元 (本+息)");
                }
                else
                {
                    results.AppendLine($"  每月應繳金額      {monthlyPayment,15:N2} 元 (本+息)");
                }
                results.AppendLine();
                results.AppendLine("【 首期還款詳情 】");
                if (gracePeriod > 0)
                {
                    results.AppendLine($"  寬限期首期利息    {(loanAmount * monthlyRate),15:N2} 元");
                    results.AppendLine($"  攤還期首期利息    {firstMonthInterest,15:N2} 元");
                }
                else
                {
                    results.AppendLine($"  首期利息          {firstMonthInterest,15:N2} 元");
                }
                results.AppendLine($"  首期本金          {firstMonthPrincipal,15:N2} 元");
                results.AppendLine();
                results.AppendLine("【 還款總額統計 】");
                results.AppendLine($"  總利息支出        {totalInterest,15:N2} 元");
                results.AppendLine($"  總還款金額        {totalRepayment,15:N2} 元");
                results.AppendLine($"  利息佔比          {(totalInterest / totalRepayment * 100),14:F2}%");
                results.AppendLine();
                results.AppendLine("═══════════════════════════════════════════════════");

                txtResults.Text = results.ToString();
            }
            catch (FormatException)
            {
                MessageBox.Show("請輸入有效的數字", "輸入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtResults.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"計算出錯：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtResults.Text = "";
            }
        }

        private void LinkSource_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(e.Link.LinkData.ToString()) { UseShellExecute = true });
            }
            catch { }
        }
    }
}
