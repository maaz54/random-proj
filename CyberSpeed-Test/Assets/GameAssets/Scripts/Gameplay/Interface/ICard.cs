using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Interface
{
    public interface ICard
    {
        void SetPosition(Vector2 position);
        string CardName { get; }
        Transform Transform { get; }
        Action<ICard> TileClicked { get; set; }
        void EnableCard();
        void DisableCard();
    }
}