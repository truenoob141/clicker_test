using ClickerTest.Controllers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ClickerTest.Behaviours
{
    [RequireComponent(typeof(Toggle))]
    public class AutoCollectionToggleWidget : MonoBehaviour
    {
        [Inject]
        private readonly ClickerController _clickerController;
        
        private Toggle _toggle;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
        }

        private void OnEnable()
        {
            _toggle.isOn = _clickerController.IsAutoCollectionActive;
            _toggle.onValueChanged.AddListener(OnValueChangedHandler);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(OnValueChangedHandler);
        }

        private void OnValueChangedHandler(bool value)
        {
            _clickerController.ToggleAutoCollection(value);
        }
    }
}