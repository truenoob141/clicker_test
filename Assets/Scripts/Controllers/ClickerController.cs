using System;
using ClickerTest.Services;
using UnityEngine;
using Zenject;

namespace ClickerTest.Controllers
{
    public class ClickerController : IInitializable, ITickable
    {
        [Inject]
        private readonly CurrencyService _currencyService;
        [Inject]
        private readonly GameSettings _gameSettings;

        public bool IsAutoCollectionActive { get; private set; }

        private float _lastEnergyRegenTime;
        private float _lastCollectTime = float.MinValue;

        public void Initialize()
        {
            _lastEnergyRegenTime = Time.time + _gameSettings._tapEnergyRegenTime;
            _currencyService.SetCurrency(CurrencyIds.tapEnergy, _gameSettings._tapEnergyStartValue);
        }

        public int TryAddTapCurrency()
        {
            var settings = _gameSettings;

            int cost = settings._tapEnergyCost;
            long energy = _currencyService.GetCurrency(CurrencyIds.tapEnergy);
            if (energy < cost)
                return 0;

            int value = settings._tapBaseValue * settings._tapModifier;
            if (IsAutoCollectionActive)
                value += (int) (settings._autoCollectValue * settings._autoCollectAdditionalTapPercentage);

            _currencyService.TakeCurrency(CurrencyIds.tapEnergy, cost);
            _currencyService.AddCurrency(CurrencyIds.soft, value);
            return value;
        }

        public void ToggleAutoCollection(bool active)
        {
            IsAutoCollectionActive = active;
            _lastCollectTime = active ? Time.time : float.MaxValue;
        }

        public void Tick()
        {
            var settings = _gameSettings;

            // Energy regeneration
            if (Time.time - _lastEnergyRegenTime > settings._tapEnergyRegenTime)
            {
                _lastEnergyRegenTime = Time.time;

                long currentAmount = _currencyService.GetCurrency(CurrencyIds.tapEnergy);
                _currencyService.SetCurrency(CurrencyIds.tapEnergy,
                    Math.Min(settings._tapEnergyMaxValue,
                        currentAmount + settings._tapEnergyRegenValue / settings._tapEnergyRegenDelimiter));
            }

            // Auto Collection
            if (!IsAutoCollectionActive)
                return;

            if (Time.time - _lastCollectTime < settings._autoCollectTime)
                return;

            _lastCollectTime = Time.time;
            _currencyService.AddCurrency(CurrencyIds.soft, settings._autoCollectValue);
        }
    }
}