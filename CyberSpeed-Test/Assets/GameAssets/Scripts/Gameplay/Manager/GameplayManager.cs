using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
using ObjectPool;
using ObjectPool.Interface;
using Gameplay.Interface;
using Gameplay.UI;
using Gameplay.SFX;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gameplay
{
    public class GameplayManager : Singleton<GameplayManager>
    {
        /// <summary>
        /// dimensions (length) of the card grid.
        /// </summary>
        [SerializeField] Vector2 cardsLenght;

        /// <summary>
        /// object pooler component for managing object pooling in the game.
        /// </summary>
        [SerializeField] ObjectPooler objectPooler;

        /// <summary>
        /// score component for keeping track of the player's score.
        /// </summary>
        [SerializeField] Score score;

        /// <summary>
        /// user interface component for displaying the player's score.
        /// </summary>
        [SerializeField] ScoreUi scoreUI;

        /// <summary>
        /// Return object pooler component 
        /// </summary>
        public IObjectPooler ObjectPooler => objectPooler;

        /// <summary>
        /// component responsible for managing the grid of cards.
        /// </summary>
        [SerializeField] CardsGrid cardsGrid;

        /// <summary>
        /// triggered when cards are generated
        /// </summary>
        public Action<ICard[,]> OnCardsGenerated;

        /// <summary>
        /// user interface component for managing in-game menus.
        /// </summary>
        [SerializeField] MenuUi menuUi;

        /// <summary>
        /// SFX player component for managing sound effects.
        /// </summary>
        [SerializeField] SFXPlayer sFXPlayer;

        /// <summary>
        /// Return Sfx player component
        /// </summary>
        public ISFXPlayer SFXPlayer => sFXPlayer;

        /// <summary>
        /// store the current grid of card objects.
        /// </summary>
        ICard[,] currentcardsGrid;

        /// <summary>
        /// previously selected card
        /// </summary>
        ICard prevCardSelected = null;

        /// <summary>
        /// total number of cards remaining in the game
        /// </summary>
        private int totalCardsRemainig;

        /// <summary>
        /// CancellationTokenSource for managing asynchronous tasks
        /// </summary>
        private CancellationTokenSource startGameCancellationTokenSource;

        private void Start()
        {
            cardsGrid.CardsGenerated += CardsGenerated;
            menuUi.OnRestartButton += OnRestartGame;
            menuUi.OnPlayButton += OnPlayGame;
            menuUi.OnHomeButton += OnHomeButton;
            score.OnUpdateScore += OnUpdateScore;
            score.OnUpdateNoOfTurns += OnUpdateNoOfTurns;
            score.UpdateScore(score.GetScore);
        }

        /// <summary>
        /// Updates the score UI text
        /// </summary>
        private void OnUpdateScore(int score)
        {
            scoreUI.UpdateScoreText(score);
        }

        /// <summary>
        /// Updates the No of Turns UI text
        /// </summary>
        private void OnUpdateNoOfTurns(int score)
        {
            scoreUI.UpdateNoOfTurnsText(score);
        }

        /// <summary>
        /// triggered when cards are generated,
        /// </summary>
        private void CardsGenerated(ICard[,] cards)
        {
            currentcardsGrid = cards;
            OnCardsGenerated?.Invoke(cards);
            StartGame(cards);

            foreach (var card in cards)
            {
                card.TileClicked = OnCardClick;
            }
            totalCardsRemainig = cards.Length;
        }

        /// <summary>
        /// triggerd when home button click
        /// </summary>
        private void OnHomeButton()
        {
            ResetScore();
            cardsGrid.DestroyCards();
        }

        /// <summary>
        /// Regenerates a new set of cards on game end
        /// </summary>
        private void RegenerateCards()
        {
            prevCardSelected = null;
            cardsGrid.DestroyCards();
            cardsGrid.GenerateCards((int)cardsLenght.x, (int)cardsLenght.y);
            sFXPlayer.CompleteSfx();
        }

        /// <summary>
        /// triggerd when Play button click
        /// </summary>
        private void OnPlayGame()
        {
            cardsGrid.GenerateCards((int)cardsLenght.x, (int)cardsLenght.y);
        }

        /// <summary>
        /// Resets the game score and the previous card selected , preparing for a new game.
        /// </summary>
        private void ResetScore()
        {
            score.UpdateScore(0);
            score.UpdateNoOfTurns(0);
            prevCardSelected = null;
        }

        /// <summary>
        /// triggerd when restart button click
        /// </summary>
        private void OnRestartGame()
        {
            ResetScore();
            cardsGrid.DestroyCards();
            cardsGrid.GenerateCards((int)cardsLenght.x, (int)cardsLenght.y);
        }

        /// <summary>
        /// Start game when cards generated
        /// </summary>
        private void StartGame(ICard[,] cards)
        {
            if (startGameCancellationTokenSource != null)
            {
                StopTask(startGameCancellationTokenSource);
            }

            startGameCancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = startGameCancellationTokenSource.Token;

            StartGameTask(cards, cancellationToken);
        }

        /// <summary>
        /// enabling cards, delaying for a short duration, and then disabling the cards.
        /// </summary>
        private async Task StartGameTask(ICard[,] cards, CancellationToken cancellationToken)
        {
            foreach (var card in cards)
            {
                card.EnableCard();
            }

            await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);

            foreach (var card in cards)
            {
                card.DisableCard();
            }
        }

        /// <summary>
        /// triggerd when user click on card
        /// </summary>
        private void OnCardClick(ICard card)
        {
            _ = OnCardClickTask(card);
        }

        /// <summary>
        /// enabling the card
        ///  checking for matches
        ///  updating the score
        ///  and managing card visibility.
        /// </summary>
        private async Task OnCardClickTask(ICard card)
        {
            card.EnableCard();
            totalCardsRemainig--;
            if (prevCardSelected == null)
            {
                prevCardSelected = card;
            }
            else
            {
                ICard prevCard = prevCardSelected;
                if (card.CardName.Contains(prevCardSelected.CardName))
                {
                    score.UpdateScore(score.GetScore + 1);
                    sFXPlayer.MatchedSfx();
                }
                else
                {
                    sFXPlayer.MissedSfx();
                }
                score.UpdateNoOfTurns(score.GetNoOfTurns + 1);

                prevCardSelected = null;
                await Task.Delay(TimeSpan.FromSeconds(.25));
                card.Transform.gameObject.SetActive(false);
                prevCard.Transform.gameObject.SetActive(false);
            }

            if (totalCardsRemainig <= 0)
            {
                RegenerateCards();
            }
        }

        /// <summary>
        /// Cancels a specified CancellationTokenSource, effectively stopping an asynchronous task.
        /// </summary>
        private void StopTask(CancellationTokenSource cancellationTokenSource)
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
            }
        }

    }
}
