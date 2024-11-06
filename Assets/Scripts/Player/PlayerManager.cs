using GameManagement;
using Level;
using UnityEngine;

namespace Player
{
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

        private void InitializeAnimator()
        {
            _animator = transform.parent.GetComponentInChildren<Animator>();
        }

        private void SetInitialState()
        {
            CanWalk = true;
        }

        private void DetachIndicatorAndCursor()
        {
            Indicator.transform.parent = null;
            Cursor.transform.parent = null;
        }

        private void SetPivotReference()
        {
            int playerIndex = InputManagementProperty.PlayerConfig.NumPlayer;
            PivotProperty = GameManager.Instance.Pivots[playerIndex].GetComponent<Pivot>();
            PivotProperty.PlayerManagerRef = this;
        }

        private void UpdateAnimatorState()
        {
            _animator.SetBool("IsWalking", IsWalking);
        }

        private void UpdateCursorMovementAvailability()
        {
            CanMoveCursor = !PivotProperty.IsRotating;
        }

        private void UpdateTargetBlockStatus()
        {
            if (IsStarted)
            {
                IsDefineTargetBlock = SelectedBlock.gameObject == CurrentBlock.gameObject;
            }
        }

        private void UpdateCursorParent()
        {
            if (!IsStarted) return;
            Cursor.transform.parent = CurrentBlock.MovingGround ? CurrentBlock.transform.parent : null;
            Cursor.transform.parent = SelectedBlock.MovingGround ? SelectedBlock.transform : null;
        }

        private void ActivateCurrentBlockButton()
        {
            if (CurrentBlock.IsButton)
            {
                var blockButton = CurrentBlock.GetComponent<BlockButton>();
                if (!blockButton.IsActive)
                {
                    blockButton.Active();
                }
            }
        }

        public void ResetCursorPosition()
        {
            SelectedBlock = CurrentBlock;
            Cursor.transform.position = new Vector3(
                SelectedBlock.transform.position.x,
                SelectedBlock.transform.position.y + SelectedBlock.WalkPointOffset,
                SelectedBlock.transform.position.z
            );
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
}