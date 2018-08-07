
using System;
using UnityEngine;

namespace FoundationFramework
{
    public class CurrencyDataHandler : UseProfileDataHandler
    {
        [SerializeField] private CurrencyDefaultAmount _defaultCurrencies;
        public Action OnCurrencyChange;
        public CurrenciesData CurrenciesData { get; private set; }
        
        #region SAVING & LOADING
        protected override void BuildDefaultData()
        {
            FileName = "Currencies";
            CurrenciesData = new CurrenciesData();
        }
        
        public override void SaveData()
        {
            DataContainer.Save(CurrenciesData);
        }

        public override void LoadData()
        {
            if (!IsLoaded)
            {
                IsLoaded = true;
                var tempData = DataContainer.Load<CurrenciesData>();
                if (tempData != null)
                {
                    CurrenciesData = tempData;
                }

                TryAddDefaultCurrencies();
            }    
        }

        private void TryAddDefaultCurrencies()
        {
            if (CurrenciesData.IsInitialized == false)
            {
                CurrenciesData.IsInitialized = true;
               
                if (_defaultCurrencies)
                {
                    foreach (var currency in _defaultCurrencies.Currencies)
                    {
                        CurrenciesData.Currencies.Add(currency.Currency.name,currency.Count);
                    }
                    
                    DoCurrencyChangeEvent();
                   SaveData();
                }
            }
        }
        
        #endregion
        
        #region TRANSACTIONS

        public bool HasEnoughCurrency(string currencyName, int amount)
        {
            int currencyFound;

            CurrenciesData.Currencies.TryGetValue(currencyName, out currencyFound);
            
            return currencyFound>=amount;
            
        }
        
        public bool HasEnoughCurrencies(CurrencyAmount[] amounts)
        {
            for (int i = 0; i < amounts.Length; i++)
            {
                bool canbuy = HasEnoughCurrency(amounts[i].Currency.name, amounts[i].Count);
               
                if (canbuy == false)
                    return false;
            }
           
            return true;
        }

        public bool TrySpendCurrencies(CurrencyAmount[] amounts)
        {
            for (int i = 0; i < amounts.Length; i++)
            {
                bool canbuy = HasEnoughCurrency(amounts[i].Currency.name, amounts[i].Count);
               
                if (canbuy == false)
                    return false;
            }
            
            for (int i = 0; i < amounts.Length; i++)
            {
                bool spent = TrySpendCurrency(amounts[i].Currency.name, amounts[i].Count);
               
                if (spent == false)
                    return false;
            }

            DoCurrencyChangeEvent();
            SaveData();
            return true;
        }

        public bool TrySpendCurrency(string currencyName, int amount)
        {
            int currencyFound;

            CurrenciesData.Currencies.TryGetValue(currencyName, out currencyFound);

            if (currencyFound < amount) return false;
            
            if(CurrenciesData.Currencies.ContainsKey(currencyName))
                CurrenciesData.Currencies[currencyName] -= amount;
                
            DoCurrencyChangeEvent();
            SaveData();
            return true;


        }

        public int GetCurrencyAmount(string currencyName)
        {
            int currencyFound;

            CurrenciesData.Currencies.TryGetValue(currencyName, out currencyFound);
            
            return currencyFound;
        }

        public void AddCurrencyAmounts(string currencyName, int amount)
        {
            int currencyFound;

            CurrenciesData.Currencies.TryGetValue(currencyName, out currencyFound);

            if (CurrenciesData.Currencies.ContainsKey(currencyName))
            {
                CurrenciesData.Currencies[currencyName] += amount;
            }
            else
            {
                CurrenciesData.Currencies.Add(currencyName,amount);
            }
            
            DoCurrencyChangeEvent();
            SaveData();
        }

        #endregion
        
        #region EVENTS

        private void DoCurrencyChangeEvent()
        {
            if(OnCurrencyChange != null)
                OnCurrencyChange();
        }

        #endregion

    }
}