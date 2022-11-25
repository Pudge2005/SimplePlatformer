namespace Game.Uncategorized
{
    public abstract class Mortal : IMortal
    {
        public void Kill()
        {
            Die();
        }

        protected abstract void Die();
    }
}
