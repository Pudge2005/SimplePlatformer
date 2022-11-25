using UnityEngine;

namespace Game.Uncategorized
{
    public class TmpKillerTrigger : Interactable2D
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _activationAnimParamName;


        private int _activationAnimParamID;


        private void Awake()
        {
            _activationAnimParamID = Animator.StringToHash(_activationAnimParamName);
        }

        public void KillPlayer(GameObject player)
        {
            _animator.SetTrigger(_activationAnimParamID);
            GameUi.RestartScreen.Show("Вы проигрыватель!", Color.red);

            if (player.TryGetComponent<Mortal>(out var mortal))
            {
                mortal.Kill();
            }
        }

        protected override void HandleInteraction(GameObject interactor)
        {
            KillPlayer(interactor);
        }
    }
}
