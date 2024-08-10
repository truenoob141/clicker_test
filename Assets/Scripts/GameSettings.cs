using System;
using UnityEngine;

namespace ClickerTest
{
    [Serializable]
    public class GameSettings
    {
        [Header("Tap Values")]
        public int _tapBaseValue = 100;
        public int _tapModifier = 1;
        public int _tapEnergyCost = 5;

        [Header("Tap Energy")]
        public int _tapEnergyStartValue = 50;
        public int _tapEnergyMaxValue = 100;
        public int _tapEnergyRegenValue = 5;
        public int _tapEnergyRegenTime = 5;
        public int _tapEnergyRegenDelimiter = 1;
        
        [Header("Auto Collection")]
        public int _autoCollectValue = 100;
        public int _autoCollectTime = 5;
        [Range(0, 1)]
        public float _autoCollectAdditionalTapPercentage = 0.1f;
    }
}