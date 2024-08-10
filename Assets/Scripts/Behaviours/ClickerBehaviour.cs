using System.Collections.Generic;
using ClickerTest.Behaviours.Pools;
using ClickerTest.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Random = UnityEngine.Random;

namespace ClickerTest.Behaviours
{
    // Replace animation to dotween?
    public class ClickerBehaviour : MonoBehaviour, IPointerDownHandler
    {
        [Inject]
        private readonly ClickerController _clickerController;

        [SerializeField]
        private SimpleLabelPool _labelPool;
        [Header("Animations"), SerializeField]
        private float _animationOffsetY = 1f;
        [SerializeField]
        private float _animationLifeTime = 1f;
        [SerializeField, Range(0, 1)]
        private float _animationFadeTimePercent = 0.1f;

        private RectTransform _rectTransform;
        private readonly List<Animation> _animations = new();
        private readonly List<Animation> _animationsToRemove = new();

        private void Awake()
        {
            _rectTransform = (RectTransform) transform;
        }

        private void Update()
        {
            // Icon animation/Find expired icons
            foreach (var anim in _animations)
            {
                float progress = Mathf.Clamp01((Time.time - anim.CreateTime) / _animationLifeTime);
                var rect = anim.RectTransform;
                rect.anchoredPosition = new Vector2(
                    rect.anchoredPosition.x,
                    rect.anchoredPosition.y + _animationOffsetY * Time.deltaTime);

                var color = anim.Label.color;
                color.a = Mathf.Clamp01((1 - progress) / _animationFadeTimePercent);
                anim.Label.color = color;

                if (progress >= 1)
                    _animationsToRemove.Add(anim);
            }

            // Remove expired icons
            if (_animationsToRemove.Count == 0)
                return;

            foreach (var anim in _animationsToRemove)
            {
                _labelPool.Despawn(anim.Label);
                _animations.Remove(anim);
            }

            _animationsToRemove.Clear();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            int added = _clickerController.TryAddTapCurrency();
            if (added > 0)
                CreateBubbleIcon(added);
        }

        private void CreateBubbleIcon(int value)
        {
            var point = new Vector3(
                Random.Range(_rectTransform.offsetMin.x, _rectTransform.offsetMax.x),
                Random.Range(_rectTransform.offsetMin.y, _rectTransform.offsetMax.y));

            var item = _labelPool.SpawnItem(value.ToString("+0;-0;+0"));
            var trans = (RectTransform) item.transform;
            trans.anchoredPosition = point;

            _animations.Add(new Animation(trans, item, Time.time));
        }

        private class Animation
        {
            public RectTransform RectTransform { get; }
            public TMP_Text Label { get; }
            public float CreateTime { get; }

            public Animation(RectTransform rectTransform, TMP_Text label, float createTime)
            {
                RectTransform = rectTransform;
                Label = label;
                CreateTime = createTime;
            }
        }
    }
}