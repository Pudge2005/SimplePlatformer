using UnityEngine;

namespace Game
{
    public class TmpInputActionsConnector : MonoBehaviour
    {
        [SerializeField] private DefaultControlsProvider _controlsProvider;

        [SerializeField] private TmpInputActionsHandler _tmpInputActionsHandler;


        private void Start()
        {
            var def = _controlsProvider.InputActions.Default;
            def.Move.performed += _tmpInputActionsHandler.Move_performed;
            def.Jump.performed += _tmpInputActionsHandler.Jump_performed;
        }

        private void OnDestroy()
        {
            if (_controlsProvider == null
                || _controlsProvider.InputActions == null)
                return;

            if (_tmpInputActionsHandler == null)
                return;

            var def = _controlsProvider.InputActions.Default;
            def.Move.performed -= _tmpInputActionsHandler.Move_performed;
            def.Jump.performed -= _tmpInputActionsHandler.Jump_performed;
        }



    }
}