// Developed by Chris Harris
using System;
using System.Threading.Tasks;
using System.Web.UI;

namespace Application
{
    public partial class CurrencyExchange : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected async void btnGetRate_Click(object sender, EventArgs e)
        {
            string baseCurrency = txtBaseCurrency.Text.Trim().ToUpper();
            string targetCurrency = txtTargetCurrency.Text.Trim().ToUpper();

            if (string.IsNullOrWhiteSpace(baseCurrency) || string.IsNullOrWhiteSpace(targetCurrency))
            {
                lblError.Text = "Please enter both base and target currencies.";
                lblResult.Text = string.Empty;
                return;
            }

            try
            {
                // Use the CurrencyExchangeRateService to get the exchange rate
                CurrencyExchangeRateService service = new CurrencyExchangeRateService();
                decimal exchangeRate = await service.GetExchangeRate(baseCurrency, targetCurrency);

                lblResult.Text = $"Exchange Rate ({baseCurrency} to {targetCurrency}): {exchangeRate}";
                lblError.Text = string.Empty;
            }
            catch (Exception ex)
            {
                lblError.Text = $"Error: {ex.Message}";
                lblResult.Text = string.Empty;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}