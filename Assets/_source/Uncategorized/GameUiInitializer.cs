using UnityEngine;

namespace Game.Uncategorized
{
    public class GameUiInitializer : MonoBehaviour
    {
        [SerializeField] private RestartScreen _restartScreen;


        private void Awake()
        {
            GameUi.RestartScreen = _restartScreen;
        }
    }
}
