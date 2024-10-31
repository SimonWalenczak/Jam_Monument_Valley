using UnityEngine;
using Character;

public class CharacterNavigation : MonoBehaviour
{
    private CharacterManager _characterManagerRef;
    private InputManagement _inputs;

    private PlayerController _playerController;
    
    #region Constructor

    public CharacterNavigation(CharacterMultiplayerManager characterMultiplayerManager)
    {
        _characterManagerRef = characterMultiplayerManager.CharacterManager;
        _inputs = characterMultiplayerManager.InputManagement;

        _playerController = characterMultiplayerManager.CharacterManager.PlayerController;
    }

    #endregion

    private void Update()
    {
        HandleCursorMovement();
    }

    private void HandleCursorMovement()
    {
        if (_inputs.Inputs.MoveNorth)
        {
            _playerController.OnMoveCursor.Invoke(0);
        }
        else if (_inputs.Inputs.MoveSouth)
        {
            _playerController.OnMoveCursor.Invoke(1);

        }
        else if (_inputs.Inputs.MoveEast)
        {
            _playerController.OnMoveCursor.Invoke(2);

        }
        else if (_inputs.Inputs.MoveWest)
        {
            _playerController.OnMoveCursor.Invoke(3);

        }
    }
}
