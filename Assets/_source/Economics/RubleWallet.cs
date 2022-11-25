namespace Game.Economics
{
    public class RubleWallet : Wallet
    {
        public RubleWallet() : base(2) { }

        public RubleWallet(float balance) : base(2, balance) { }
    }
}
