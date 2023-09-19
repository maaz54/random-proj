using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class MenuUi : MonoBehaviour
    {
        [SerializeField] Button RestartButton;
        public Action OnRestart;

        private void Start()
        {
            ButtonEvents();
        }

        private void ButtonEvents()
        {
            RestartButton.onClick.AddListener(() => OnRestart?.Invoke());
        }

    }
}
