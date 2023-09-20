using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Interface
{
    public interface ICard
    {
        /// <summary>
        /// set target position for the card.
        /// </summary>
        void SetPosition(Vector2 position);

        /// <summary>
        /// returns Card name
        /// </summary>
        string CardName { get; }

        /// <summary>
        /// return card transform
        /// </summary>
        Transform Transform { get; }

        /// <summary>
        /// click event for the card
        /// </summary>
        Action<ICard> TileClicked { get; set; }

        /// <summary>
        /// enabling front card
        /// </summary>
        void EnableCard();

        /// <summary>
        /// disabling front card
        /// </summary>
        void DisableCard();
    }
}