using UnityEngine;

namespace Game.Uncategorized
{
    public abstract class Mortal : MonoBehaviour, IMortal
    {
        public void Kill()
        {
            Die();
        }

        protected abstract void Die();
    }
}
