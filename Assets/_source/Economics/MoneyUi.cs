using TMPro;
using UnityEngine;

namespace Game.Economics
{
    public class MoneyUi : MonoBehaviour
    {
        [SerializeField] private Inventory _trackingInventory;
        [SerializeField] private TextMeshProUGUI _balanceText;


        private void Start()
        {
            _trackingInventory.OnBalanceChanged += HandleBalanceChanged;
            HandleBalanceChanged(_trackingInventory, _trackingInventory.Balance);
        }

        private void HandleBalanceChanged(IWallet sender, float balance)
        {
            _balanceText.text = balance.ToString("N2");
        }
    }
}
