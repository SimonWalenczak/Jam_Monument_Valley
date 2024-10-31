using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class InputManagement : MonoBehaviour
    {
        private CharacterMultiplayerManager _characterMultiplayerManager;

        private GameplayInputs _gameplayInputs;

        private PlayerConfiguration _playerConfig;

        public GameplayInputs GameplayInputs
        {
            get { return _gameplayInputs; }
            private set { _gameplayInputs = value; }
        }

        [SerializeField] float DeadzoneJoystick = 0.3f;
        [SerializeField] float DeadzoneJoystickTrigger = 0.3f;
        [field: SerializeField] public InputsEnum Inputs { get; private set; }

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
                inputsEnum.MoveNorth = context.ReadValue<float>() > DeadzoneJoystickTrigger;

            if (context.action.name == _gameplayInputs.Player.MoveSouth.name)
                inputsEnum.MoveSouth = context.ReadValue<float>() > DeadzoneJoystickTrigger;
            
            if (context.action.name == _gameplayInputs.Player.MoveEast.name)
                inputsEnum.MoveEast = context.ReadValue<float>() > DeadzoneJoystickTrigger;

            if (context.action.name == _gameplayInputs.Player.MoveWest.name)
                inputsEnum.MoveWest = context.ReadValue<float>() > DeadzoneJoystickTrigger;
            
            if (context.action.name == _gameplayInputs.Player.ClickBlock.name)
                inputsEnum.ClickBlock = context.ReadValue<float>() > DeadzoneJoystickTrigger;

            inputsEnum.Deadzone = DeadzoneJoystick;

            Inputs = inputsEnum;
        }
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
}