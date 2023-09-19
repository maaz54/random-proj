using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class MenuUi : MonoBehaviour
    {
        [SerializeField] Button restartButton;
        [SerializeField] Button homeButton;
        [SerializeField] Button playButton;
        [SerializeField] GameObject gameplay;
        [SerializeField] GameObject menu;
        public Action OnRestartButton;
        public Action OnHomeButton;
        public Action OnPlayButton;

        private void Start()
        {
            ButtonEvents();
            OpenMenu();
        }

        private void ButtonEvents()
        {
            restartButton.onClick.AddListener(() => OnRestartButton?.Invoke());
            homeButton.onClick.AddListener(() =>
            {
                OpenMenu();
                OnHomeButton?.Invoke();
            });
            playButton.onClick.AddListener(() =>
            {
                OpenGameplay();
                OnPlayButton?.Invoke();
            });
        }

        private void OpenGameplay()
        {
            gameplay.SetActive(true);
            menu.SetActive(false);
        }

        private void OpenMenu()
        {
            menu.SetActive(true);
            gameplay.SetActive(false);
        }

    }
}
