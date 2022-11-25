using UnityEngine;

namespace Game.Uncategorized
{
    public abstract class Interactable2D : MonoBehaviour
    {
        [SerializeField] private bool _destroyCollider = true;
        [SerializeField] private bool _destroyGameObject;


        public void Interact(GameObject interactor)
        {
            if (TryHandleInteraction(interactor))
            {
                if (_destroyGameObject)
                {
                    Destroy(gameObject);
                }
                else if (_destroyCollider)
                {
                    Destroy(GetComponent<Collider2D>());
                }
            }
        }

        protected abstract bool TryHandleInteraction(GameObject interactor);
    }
}
