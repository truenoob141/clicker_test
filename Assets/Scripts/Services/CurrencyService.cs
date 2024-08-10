using System;
using System.Collections.Generic;

namespace ClickerTest.Services
{
    public class CurrencyService
    {
        public event Action<string, long> OnCurrencyChanged; 
        
        private readonly Dictionary<string, long> _currencies = new();
        
        public void AddCurrency(string currencyId, long amount)
        {
            _currencies.TryGetValue(currencyId, out long current);
            _currencies[currencyId] = current + amount;

            OnCurrencyChanged?.Invoke(currencyId, _currencies[currencyId]);
        }
        
        public void SetCurrency(string currencyId, long amount)
        {
            _currencies[currencyId] = amount;

            OnCurrencyChanged?.Invoke(currencyId, _currencies[currencyId]);
        }
        
        public void TakeCurrency(string currencyId, long amount)
        {
            _currencies.TryGetValue(currencyId, out long current);
            _currencies[currencyId] = Math.Max(0, current - amount);
            
            OnCurrencyChanged?.Invoke(currencyId, _currencies[currencyId]);
        }

        public long GetCurrency(string currencyId)
        {
            _currencies.TryGetValue(currencyId, out long current);
            return current;
        }
    }
}