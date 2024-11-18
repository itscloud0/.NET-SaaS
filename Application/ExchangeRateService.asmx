<%@ WebService Language="C#" Class="CurrencyExchangeRateService" %>
<!--Developed by Chris Harris-->
using System;
using System.Collections.Generic;
using System.Web.Services;

[WebService(Namespace = "http://example.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.ComponentModel.ToolboxItem(false)]
public class CurrencyExchangeRateService : WebService
{
    // Mock exchange rate data
    private readonly Dictionary<string, Dictionary<string, decimal>> exchangeRates = new Dictionary<string, Dictionary<string, decimal>>
    {
        { "USD", new Dictionary<string, decimal> { { "EUR", 0.85m }, { "GBP", 0.75m }, { "JPY", 110.00m } } },
        { "EUR", new Dictionary<string, decimal> { { "USD", 1.18m }, { "GBP", 0.88m }, { "JPY", 130.00m } } },
        { "GBP", new Dictionary<string, decimal> { { "USD", 1.34m }, { "EUR", 1.14m }, { "JPY", 150.00m } } }
    };

    [WebMethod]
    public decimal GetExchangeRate(string baseCurrency, string targetCurrency)
    {
        // Validate inputs
        if (string.IsNullOrWhiteSpace(baseCurrency) || string.IsNullOrWhiteSpace(targetCurrency))
        {
            throw new ArgumentException("BaseCurrency and TargetCurrency must be provided.");
        }

        baseCurrency = baseCurrency.ToUpper();
        targetCurrency = targetCurrency.ToUpper();

        if (!exchangeRates.ContainsKey(baseCurrency) || !exchangeRates[baseCurrency].ContainsKey(targetCurrency))
        {
            throw new ArgumentException("Exchange rate for the specified currency pair is not available.");
        }

        // Return the exchange rate
        return exchangeRates[baseCurrency][targetCurrency];
    }
}