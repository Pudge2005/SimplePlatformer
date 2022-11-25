using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Uncategorized
{
    public class RestartScreen : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _buttonText;


        public TextMeshProUGUI Title => _title;
        public Button Button => _button;
        public TextMeshProUGUI ButtonText => _buttonText;


        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Show(string titleText)
        {
            _title.text = titleText;
            Show();
        }

        public void Show(string titleText, Color titleTextColor)
        {
            _title.color = titleTextColor;
            Show(titleText);
        }
    }
}
