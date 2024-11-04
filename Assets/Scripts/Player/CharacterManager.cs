using System;
using Level;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    #region Properties

    [field: SerializeField] public InputManagement InputManagementProperty { get; private set; }
    [field: SerializeField] public PlayerController PlayerControllerProperty { get; private set; }

    [field: SerializeField] public Pivot PivotProperty { get; private set; }

    #endregion

    #region Methods

    private void Start()
    {
        PivotProperty = GameManager.Instance.Pivots[InputManagementProperty.PlayerConfig.NumPlayer].GetComponent<Pivot>();
    }

    private void Update()
    {
        HandleCursorMovement();
    }

    private void HandleCursorMovement()
    {
        if (InputManagementProperty.Inputs.MoveNorth)
        {
            PlayerControllerProperty.OnMoveCursor.Invoke(0);
        }
        else if (InputManagementProperty.Inputs.MoveSouth)
        {
            PlayerControllerProperty.OnMoveCursor.Invoke(1);
        }
        else if (InputManagementProperty.Inputs.MoveEast)
        {
            PlayerControllerProperty.OnMoveCursor.Invoke(2);
        }
        else if (InputManagementProperty.Inputs.MoveWest)
        {
            PlayerControllerProperty.OnMoveCursor.Invoke(3);
        }
        else if (InputManagementProperty.Inputs.RotateLeft)
        {
            PivotProperty.OnMovePivot(-1);
        }
        else if (InputManagementProperty.Inputs.RotateRight)
        {
            PivotProperty.OnMovePivot(1);
        }

        if (InputManagementProperty.Inputs.ClickBlock)
        {
            PlayerControllerProperty.OnSelectBlock.Invoke();
        }
    }

    #endregion
}