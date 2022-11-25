namespace Game.Uncategorized
{
    public class DestroyOnDeath : Mortal
    {
        protected override void Die()
        {
            Destroy(gameObject);
        }
    }
}
