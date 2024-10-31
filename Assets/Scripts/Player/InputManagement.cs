using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagement : MonoBehaviour
{
    #region Properties

    [field: SerializeField] public InputsEnum Inputs { get; private set; }

    [SerializeField] private float deadzoneJoystick = 0.3f;
    [SerializeField] private float deadzoneJoystickTrigger = 0.3f;

    private CharacterMultiplayerManager _characterMultiplayerManager;
    private GameplayInputs _gameplayInputs;
    private PlayerConfiguration _playerConfig;

    #endregion

    #region Methods

    private void Awake()
    {
        _gameplayInputs = new GameplayInputs();
        _gameplayInputs.Enable();
    }

    public void InitializePlayer(PlayerConfiguration pc)
    {
        _playerConfig = pc;
        _playerConfig.Input.onActionTriggered += GatherInputs;
    }

    private void GatherInputs(InputAction.CallbackContext context)
    {
        InputsEnum inputsEnum = Inputs;

        if (context.action.name == _gameplayInputs.Player.MoveNorth.name)
            inputsEnum.MoveNorth = context.ReadValue<float>() > deadzoneJoystickTrigger;

        if (context.action.name == _gameplayInputs.Player.MoveSouth.name)
            inputsEnum.MoveSouth = context.ReadValue<float>() > deadzoneJoystickTrigger;

        if (context.action.name == _gameplayInputs.Player.MoveEast.name)
            inputsEnum.MoveEast = context.ReadValue<float>() > deadzoneJoystickTrigger;

        if (context.action.name == _gameplayInputs.Player.MoveWest.name)
            inputsEnum.MoveWest = context.ReadValue<float>() > deadzoneJoystickTrigger;

        if (context.action.name == _gameplayInputs.Player.ClickBlock.name)
            inputsEnum.ClickBlock = context.ReadValue<float>() > deadzoneJoystickTrigger;

        inputsEnum.Deadzone = deadzoneJoystick;

        Inputs = inputsEnum;
    }

    #endregion
}

[Serializable]
public struct InputsEnum
{
    public bool MoveNorth;
    public bool MoveSouth;
    public bool MoveEast;
    public bool MoveWest;

    public bool ClickBlock;

    public float Deadzone;
}