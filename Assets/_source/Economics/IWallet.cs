namespace Game.Economics
{
    public interface IWallet
    {
        int Precision { get; }
        float Balance { get; }


        event System.Action<IWallet, float> OnBalanceChanged;


        void SetBalance(float newBalance);
        void ChangeBalance(float delta);
    }
}
