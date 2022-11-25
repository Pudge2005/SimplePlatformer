using System;
using Game.Uncategorized;
using UnityEngine;

namespace Game
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private TmpCharacterController2D _controller2D;


        private void Awake()
        {
            _controller2D.State.OnStateUpdated += HandleStateUpdated;
        }

        private void HandleStateUpdated(ICharacterController2DState state)
        {
            var span = state.Triggers.Span;

            foreach (var col in span)
            {
                //alive check

                if(!col.isTrigger)
                {
                    continue;
                }

                if(col.TryGetComponent<Interactable2D>(out var interactable))
                {
                    interactable.Interact(gameObject);
                }
            }
        }
    }
}