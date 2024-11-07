using GameManagement;
using Level;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// Manages the player's state, movement, cursor handling, and interactions with the game environment.
    /// Coordinates with other components like InputManagement, PlayerController, and Pivot for player actions.
    /// </summary>
    public class PlayerManager : MonoBehaviour
    {
        #region Properties

        // Player Properties References
        [Header("Player Properties References")]
        [SerializeField] private InputManagement _inputManagement;

        public InputManagement InputManagementProperty
        {
            get => _inputManagement;
            private set => _inputManagement = value;
        }

        [SerializeField] private PlayerController _playerController;

        public PlayerController PlayerControllerProperty
        {
            get => _playerController;
            private set => _playerController = value;
        }

        [SerializeField] private Pivot PivotProperty;

        // Animator
        private Animator _animator;

        // Other Properties
        [field: Header("Other")]
        public bool IsWalking { get; set; }

        public bool CanMoveCursor { get; set; }
        public bool CanWalk { get; set; }
        public bool IsStarted { get; set; }
        public bool IsMovingCursor { get; set; }
        public bool IsDefineTargetBlock { get; set; }

        public BlockController CurrentBlock { get; set; }
        public BlockController ClickedCube { get; set; }
        public BlockController SelectedBlock { get; set; }

        // Path Finding Object
        [Header("Path Finding Object")]
        [SerializeField] private Transform _indicator;

        public Transform Indicator
        {
            get => _indicator;
            private set => _indicator = value;
        }

        [SerializeField] private GameObject _cursor;

        public GameObject Cursor
        {
            get => _cursor;
            private set => _cursor = value;
        }

        #endregion

        #region Methods

        private void Start()
        {
            InitializeAnimator();
            SetInitialState();
            DetachIndicatorAndCursor();
            SetPivotReference();
        }

        private void Update()
        {
            UpdateAnimatorState();
            UpdateCursorMovementAvailability();
            UpdateTargetBlockStatus();
            UpdateCursorParent();
            ActivateCurrentBlockButton();
            HandleCursorMovement();
        }

        /// <summary>
        /// Sets up the Animator component by finding it in the player's parent object.
        /// </summary>
        private void InitializeAnimator()
        {
            _animator = transform.parent.GetComponentInChildren<Animator>();
        }

        /// <summary>
        /// Sets the initial state of player properties, such as enabling walking.
        /// </summary>
        private void SetInitialState()
        {
            CanWalk = true;
        }

        /// <summary>
        /// Detaches the indicator and cursor objects from the player's transform for independent control.
        /// </summary>
        private void DetachIndicatorAndCursor()
        {
            Indicator.transform.parent = null;
            Cursor.transform.parent = null;
        }

        /// <summary>
        /// Sets the reference for the Pivot object based on the player index, and assigns the PlayerManager reference to the Pivot.
        /// </summary>
        private void SetPivotReference()
        {
            int playerIndex = InputManagementProperty.PlayerConfig.NumPlayer;
            PivotProperty = GameManager.Instance.Pivots[playerIndex].GetComponent<Pivot>();
            PivotProperty.PlayerManagerRef = this;
        }

        /// <summary>
        /// Updates the animator's "IsWalking" parameter based on the player's walking state.
        /// </summary>
        private void UpdateAnimatorState()
        {
            _animator.SetBool("IsWalking", IsWalking);
        }

        /// <summary>
        /// Sets the availability of cursor movement based on the rotation state of the Pivot object.
        /// </summary>
        private void UpdateCursorMovementAvailability()
        {
            CanMoveCursor = !PivotProperty.IsRotating;
        }

        /// <summary>
        /// Checks if the selected block is the current block, updating the target block status.
        /// </summary>
        private void UpdateTargetBlockStatus()
        {
            if (IsStarted)
            {
                IsDefineTargetBlock = SelectedBlock.gameObject == CurrentBlock.gameObject;
            }
        }

        /// <summary>
        /// Updates the cursor's parent transform based on the ground's movement status for both current and selected blocks.
        /// </summary>
        private void UpdateCursorParent()
        {
            if (!IsStarted) return;
            Cursor.transform.parent = CurrentBlock.MovingGround ? CurrentBlock.transform.parent : null;
            Cursor.transform.parent = SelectedBlock.MovingGround ? SelectedBlock.transform : null;
        }

        /// <summary>
        /// Activates any button on the current block, if present, allowing for interaction.
        /// </summary>
        private void ActivateCurrentBlockButton()
        {
            if (IsStarted == false) return;

            if (CurrentBlock.IsButton)
            {
                var blockButton = CurrentBlock.GetComponent<BlockButton>();
                if (!blockButton.IsActive)
                {
                    blockButton.Activate();
                }
            }
        }

        /// <summary>
        /// Resets the cursor's position to match the current block, ensuring it aligns with the block's walk point offset.
        /// </summary>
        public void ResetCursorPosition()
        {
            SelectedBlock = CurrentBlock;
            Cursor.transform.position = new Vector3(
                SelectedBlock.transform.position.x,
                SelectedBlock.transform.position.y + SelectedBlock.WalkPointOffset,
                SelectedBlock.transform.position.z
            );
        }

        /// <summary>
        /// Handles cursor movement and block selection based on user input, invoking appropriate actions or rotations.
        /// </summary>
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
}