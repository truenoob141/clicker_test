using ClickerTest.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ClickerTest.Behaviours
{
    public class CurrencyWidget : MonoBehaviour
    {
        [Inject]
        private readonly CurrencyService _currencyService;
        
        [SerializeField]
        private string _currencyId;
        [SerializeField]
        private TMP_Text _amountLabel;
        [SerializeField]
        private Image _currencyIcon;

        private void Start()
        {
            // TODO Implement project resources management system
            var sprite = Resources.Load<Sprite>(_currencyId);
            if (sprite == null)
            {
                Debug.LogWarning("Failed to load currency icon: " + _currencyId);
                _currencyIcon.gameObject.SetActive(false);
                return;
            }

            _currencyIcon.sprite = sprite;
            _currencyIcon.gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            _currencyService.OnCurrencyChanged += OnCurrencyChangedHandler;

            long current = _currencyService.GetCurrency(_currencyId);
            OnCurrencyChangedHandler(_currencyId, current);
        }

        private void OnDisable()
        {
            _currencyService.OnCurrencyChanged -= OnCurrencyChangedHandler;
        }

        private void OnCurrencyChangedHandler(string currencyId, long amount)
        {
            if (currencyId != _currencyId)
                return;

            _amountLabel.text = amount.ToString();
        }
    }
}