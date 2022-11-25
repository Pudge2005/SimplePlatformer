using UnityEngine;

namespace Game.Uncategorized
{
    public abstract class Interactable2D : MonoBehaviour
    {
        [SerializeField] private bool _destroyCollider = true;


        public void Interact(GameObject interactor)
        {
            if (_destroyCollider)
            {
                Destroy(GetComponent<Collider2D>());
            }

            HandleInteraction(interactor);
        }

        protected abstract void HandleInteraction(GameObject interactor);
    }
}
