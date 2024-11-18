//Developed by Chris Harris
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Services;
using Newtonsoft.Json;

[WebService(Namespace = "http://example.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.ComponentModel.ToolboxItem(false)]
public class CurrencyExchangeRateService : WebService
{
    [WebMethod]
    public async Task<decimal> GetExchangeRate(string baseCurrency, string targetCurrency)
    {
        // Validate inputs
        if (string.IsNullOrWhiteSpace(baseCurrency) || string.IsNullOrWhiteSpace(targetCurrency))
        {
            throw new ArgumentException("BaseCurrency and TargetCurrency must be provided.");
        }

        baseCurrency = baseCurrency.ToUpper();
        targetCurrency = targetCurrency.ToUpper();

        try
        {
            // Fetch the exchange rate from a real API
            decimal exchangeRate = await FetchRealExchangeRate(baseCurrency, targetCurrency);
            return exchangeRate;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching exchange rate: {ex.Message}");
        }
    }

    // Example of using HttpClient to fetch data from a real API
    private async Task<decimal> FetchRealExchangeRate(string baseCurrency, string targetCurrency)
    {
        string apiUrl = $"https://api.exchangerate-api.com/v4/latest/{baseCurrency}";
        using (HttpClient client = new HttpClient())
        {
            string response = await client.GetStringAsync(apiUrl);
            dynamic data = JsonConvert.DeserializeObject(response);

            // Check if the target currency exists in the response
            if (data.rates[targetCurrency] == null)
            {
                throw new Exception($"Exchange rate for {targetCurrency} not found.");
            }

            return (decimal)data.rates[targetCurrency];
        }
    }
}