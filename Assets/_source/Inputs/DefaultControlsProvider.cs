using Game.Inputs;
using UnityEngine;

namespace Game
{
    public class DefaultControlsProvider : MonoBehaviour, IInputActionsProvider<DefaultControls>
    {
        private DefaultControls _controlsInst;


        public DefaultControls InputActions => _controlsInst;


        private void Awake()
        {
            _controlsInst = new();
            _controlsInst.Enable();
        }

        private void OnDestroy()
        {
            _controlsInst.Dispose();
            _controlsInst = null;
        }
    }
}