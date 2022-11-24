using UnityEngine.InputSystem;

namespace Game
{
    public interface IInputActionsProvider<TControls> where TControls : IInputActionCollection
    {
        TControls InputActions { get; }
    }
}