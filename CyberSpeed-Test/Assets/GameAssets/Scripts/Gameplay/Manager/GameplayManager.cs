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
        [SerializeField] Vector2 cardsLenght;
        [SerializeField] ObjectPooler objectPooler;
        [SerializeField] Score score;
        [SerializeField] ScoreUi scoreUI;
        public IObjectPooler ObjectPooler => objectPooler;
        [SerializeField] CardsGrid cardsGrid;
        public Action<ICard[,]> OnCardsGenerated;
        [SerializeField] MenuUi menuUi;
        [SerializeField] SFXPlayer sFXPlayer;
        public ISFXPlayer SFXPlayer => sFXPlayer;
        ICard[,] currentcardsGrid;
        ICard prevCardSelected = null;
        private int totalCardsRemainig;
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

        private void OnUpdateScore(int score)
        {
            scoreUI.UpdateScoreText(score);
        }

        private void OnUpdateNoOfTurns(int score)
        {
            scoreUI.UpdateNoOfTurnsText(score);
        }

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

        private void OnHomeButton()
        {
            ResetScore();
            cardsGrid.DestroyCards();
        }

        private void RegenerateCards()
        {
            prevCardSelected = null;
            cardsGrid.DestroyCards();
            cardsGrid.GenerateCards((int)cardsLenght.x, (int)cardsLenght.y);
            sFXPlayer.CompleteSfx();
        }

        private void OnPlayGame()
        {
            cardsGrid.GenerateCards((int)cardsLenght.x, (int)cardsLenght.y);
        }

        private void ResetScore()
        {
            score.UpdateScore(0);
            score.UpdateNoOfTurns(0);
            prevCardSelected = null;
        }

        private void OnRestartGame()
        {
            ResetScore();
            cardsGrid.DestroyCards();
            cardsGrid.GenerateCards((int)cardsLenght.x, (int)cardsLenght.y);
        }

        private async Task StartGame(ICard[,] cards)
        {
            if (startGameCancellationTokenSource != null)
            {
                StopTask(startGameCancellationTokenSource);
            }

            startGameCancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = startGameCancellationTokenSource.Token;

            StartGameTask(cards, cancellationToken);
        }

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

        private void OnCardClick(ICard card)
        {
            _ = OnCardClickTask(card);
        }

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

        private void StopTask(CancellationTokenSource cancellationTokenSource)
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
            }
        }

    }
}
