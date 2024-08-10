using System;
using UnityEngine;

namespace ClickerTest
{
    public class Fps : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }
    }
}