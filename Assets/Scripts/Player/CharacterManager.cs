using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    #region Properties

    [field: SerializeField] public PlayerController PlayerController { get; private set; }
    [field: SerializeField] public InputManagement InputManagementProperty { get; private set; }

    #endregion

    #region Methods

    private void Update()
    {
        HandleCursorMovement();
    }

    private void HandleCursorMovement()
    {
        if (InputManagementProperty.Inputs.MoveNorth)
        {
            PlayerController.OnMoveCursor.Invoke(0);
        }
        else if (InputManagementProperty.Inputs.MoveSouth)
        {
            PlayerController.OnMoveCursor.Invoke(1);
        }
        else if (InputManagementProperty.Inputs.MoveEast)
        {
            PlayerController.OnMoveCursor.Invoke(2);
        }
        else if (InputManagementProperty.Inputs.MoveWest)
        {
            PlayerController.OnMoveCursor.Invoke(3);
        }

        if (InputManagementProperty.Inputs.ClickBlock)
        {
            PlayerController.OnSelectBlock.Invoke();
        }
    }

    #endregion
}