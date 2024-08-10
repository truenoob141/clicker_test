using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ClickerTest.Behaviours.Pools
{
    public class SimpleLabelPool : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _labelPrefab;

        private readonly Stack<TMP_Text> _items = new();

        private void Awake()
        {
            _labelPrefab.gameObject.SetActive(false);
        }

        public TMP_Text SpawnItem(string text)
        {
            TMP_Text item;
            if (!_items.TryPop(out item))
            {
                var parent = _labelPrefab.transform.parent;
                var go = Instantiate(_labelPrefab.gameObject, parent);
                item = go.GetComponent<TMP_Text>();
                
                var trans = go.transform;
                trans.localPosition = Vector3.zero;
                trans.rotation = Quaternion.identity;
                trans.localScale = Vector3.one;
            }

            item.text = text;
            item.gameObject.SetActive(true);
            
            return item;
        }

        public void Despawn(TMP_Text item)
        {
            item.gameObject.SetActive(false);
            _items.Push(item);
        }
    }
}