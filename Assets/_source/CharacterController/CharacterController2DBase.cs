using System;
using UnityEngine;

namespace Game
{
    public abstract class CharacterController2DBase : MonoBehaviour, ICharacterController2D
    {
        public abstract ICharacterController2DState State { get; }


        public abstract void Move(Vector2 move);
    }

}