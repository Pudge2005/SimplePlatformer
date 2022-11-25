using Game.Economics;
using UnityEngine;

namespace Game.Uncategorized
{
    public class TmpCollectableRuble : Interactable2D
    {
        [SerializeField] private float _value;

        protected override bool TryHandleInteraction(GameObject interactor)
        {
            if(interactor.TryGetComponent<Inventory>(out var inventory))
            {
                inventory.ChangeBalance(_value);
                return true;
            }

            return false;
        }
    }
}
