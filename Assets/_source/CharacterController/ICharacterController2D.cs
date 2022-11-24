using System;
using UnityEngine;

namespace Game
{
    public interface ICollisionDetector2D
    {
        int Detect(Collider2D[] buffer);
    }

    public interface ICollisionResolver2D
    {
        Vector2 Resolve(ReadOnlyMemory<Collider2D> collisions);
    }
    public interface ICharacterController2DState
    {
        CollisionFlags CollisionFlags { get; }
        ReadOnlyMemory<Collider2D> Collisions { get; }


        event System.Action<ICharacterController2DState> OnStateUpdated;
    }


    public interface ICharacterController2D
    {
        ICharacterController2DState State { get; }


        void Move(Vector2 move);
    }
}