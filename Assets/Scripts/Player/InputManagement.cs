using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    /// <summary>
    /// Manages player input settings and processing, including joystick deadzone adjustments and input gathering.
    /// </summary>
    public class InputManagement : MonoBehaviour
    {
        #region Properties

        [field: SerializeField] public InputsEnum Inputs { get; private set; }
        [HideInInspector] public PlayerConfiguration PlayerConfig;

        [SerializeField] private float _deadzoneJoystick = 0.3f;
        [SerializeField] private float _deadzoneJoystickTrigger = 0.3f;

        private CharacterMultiplayerManager _characterMultiplayerManager;
        private GameplayInputs _gameplayInputs;

        #endregion

        #region Methods

        private void Awake()
        {
            _gameplayInputs = new GameplayInputs();
            _gameplayInputs.Enable();
        }

        /// <summary>
        /// Sets up player-specific configurations and assigns the input action event listener.
        /// </summary>
        /// <param name="playerConfig">The player configuration instance to be assigned.</param>
        public void InitializePlayer(PlayerConfiguration pc)
        {
            PlayerConfig = pc;
            PlayerConfig.Input.onActionTriggered += GatherInputs;
        }

        /// <summary>
        /// Processes input actions and updates the <see cref="InputsEnum"/> structure based on defined deadzones.
        /// </summary>
        /// <param name="context">The context of the input action event.</param>
        private void GatherInputs(InputAction.CallbackContext context)
        {
            InputsEnum inputsEnum = Inputs;

            if (context.action.name == _gameplayInputs.Player.MoveNorth.name)
                inputsEnum.MoveNorth = context.ReadValue<float>() > _deadzoneJoystickTrigger;

            if (context.action.name == _gameplayInputs.Player.MoveSouth.name)
                inputsEnum.MoveSouth = context.ReadValue<float>() > _deadzoneJoystickTrigger;

            if (context.action.name == _gameplayInputs.Player.MoveEast.name)
                inputsEnum.MoveEast = context.ReadValue<float>() > _deadzoneJoystickTrigger;

            if (context.action.name == _gameplayInputs.Player.MoveWest.name)
                inputsEnum.MoveWest = context.ReadValue<float>() > _deadzoneJoystickTrigger;

            if (context.action.name == _gameplayInputs.Player.ClickBlock.name)
                inputsEnum.ClickBlock = context.ReadValue<float>() > _deadzoneJoystickTrigger;

            if (context.action.name == _gameplayInputs.Player.RotateLeft.name)
                inputsEnum.RotateLeft = context.ReadValue<float>() > _deadzoneJoystickTrigger;
        
            if (context.action.name == _gameplayInputs.Player.RotateRight.name)
                inputsEnum.RotateRight = context.ReadValue<float>() > _deadzoneJoystickTrigger;
            
            inputsEnum.Deadzone = _deadzoneJoystick;

            Inputs = inputsEnum;
        }

        #endregion
    }

    /// <summary>
    /// Structure representing various player input states and deadzone settings.
    /// </summary>
    [Serializable]
    public struct InputsEnum
    {
        public bool MoveNorth;
        public bool MoveSouth;
        public bool MoveEast;
        public bool MoveWest;

        public bool ClickBlock;

        public bool RotateLeft;
        public bool RotateRight;

        public float Deadzone;
    }
}