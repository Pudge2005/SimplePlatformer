using System;
using UnityEngine;

namespace Game.Economics
{
    public class Inventory : MonoBehaviour, IWallet
    {
        private RubleWallet _wallet;


        public int Precision => ((IWallet)_wallet).Precision;
        public float Balance => ((IWallet)_wallet).Balance;



        public event Action<IWallet, float> OnBalanceChanged
        {
            add
            {
                ((IWallet)_wallet).OnBalanceChanged += value;
            }

            remove
            {
                ((IWallet)_wallet).OnBalanceChanged -= value;
            }
        }


        public void ChangeBalance(float delta)
        {
            ((IWallet)_wallet).ChangeBalance(delta);
        }

        public void SetBalance(float newBalance)
        {
            ((IWallet)_wallet).SetBalance(newBalance);
        }


        private void Awake()
        {
            _wallet = new RubleWallet();
        }
    }
}
