using System;
using UnityEngine;

namespace Game.Uncategorized
{
    public class TmpWinTrigger : Interactable2D
    {
        protected override void HandleInteraction(GameObject interactor)
        {
            ShowWinScreen();
            DestroyPlayerComponents(interactor);
        }

        private void DestroyPlayerComponents(GameObject hypotheticalPlayerGameObject)
        {
            Type[] compsToDestroy = new Type[]
            {
                typeof(Collider2D),
                typeof(TmpCharacterController2D),
                typeof(DefaultControlsProvider),
                typeof(TmpInputActionsConnector),
                typeof(TmpInputActionsHandler),
                typeof(Interactor),
                typeof(Mortal),
            };

            foreach (var cmpType in compsToDestroy)
            {
                if (hypotheticalPlayerGameObject.TryGetComponent(cmpType, out var cmpInst))
                {
                    Destroy(cmpInst);
                }
            }
        }

        public void ShowWinScreen()
        {
            Application.OpenURL("https://youtu.be/e9oGl5jXtWU?t=2009");
            GameUi.RestartScreen.Show("Поздравления, ты подебил, EAT <style=\"Title\">S</style>!", Color.green);
        }

    }
}
