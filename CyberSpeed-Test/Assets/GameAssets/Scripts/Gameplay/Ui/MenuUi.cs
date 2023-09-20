using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class MenuUi : MonoBehaviour
    {
        /// <summary>
        /// Restart button in the menu UI
        /// </summary>
        [SerializeField] Button restartButton;

        /// <summary>
        /// Home button in the menu UI.
        /// </summary>
        [SerializeField] Button homeButton;

        /// <summary>
        /// Play button in the menu UI.
        /// </summary>
        [SerializeField] Button playButton;

        /// <summary>
        /// gameplay UI panel
        /// </summary>
        [SerializeField] GameObject gameplay;

        /// <summary>
        /// menu UI panel
        /// </summary>
        [SerializeField] GameObject menu;

        /// <summary>
        /// event when the "Restart" button is clicked
        /// </summary>
        public Action OnRestartButton;

        /// <summary>
        /// event when the Home button is clicked
        /// </summary>
        public Action OnHomeButton;

        /// <summary>
        /// event when the Play button is clicked
        /// </summary>
        public Action OnPlayButton;

        private void Start()
        {
            ButtonEvents();
            OpenMenu();
        }

        /// <summary>
        /// sets up button click event 
        /// </summary>
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

        /// <summary>
        /// Enabling Gameplay Panel
        /// </summary>
        private void OpenGameplay()
        {
            gameplay.SetActive(true);
            menu.SetActive(false);
        }

        /// <summary>
        /// Enabling Menu Panel
        /// </summary>
        private void OpenMenu()
        {
            menu.SetActive(true);
            gameplay.SetActive(false);
        }

    }
}
