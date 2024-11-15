using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using GameManagement;

namespace Player
{
    /// <summary>
    /// Controls player movement, cursor interactions, and pathfinding based on user input. 
    /// Manages raycasting to detect ground blocks and updates player’s position accordingly.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        #region Properties

        // Player Manager Reference
        [SerializeField] private PlayerManager _playerManager;

        public PlayerManager PlayerManagerRef
        {
            get => _playerManager;
            private set => _playerManager = value;
        }

        // Path to Move
        [Space, Header("Path To Move")]
        [SerializeField] private List<Transform> _finalPath;

        public List<Transform> FinalPath
        {
            get => _finalPath;
            private set => _finalPath = value;
        }

        // Action Events
        public Action<int> OnMoveCursor;
        public Action OnSelectBlock;

        #endregion

        #region Methods

        private void Awake()
        {
            RegisterEvents();
        }

        private void Start()
        {
            PerformInitialRayCast();
        }

        private void Update()
        {
            UpdateRayCast();
            UpdateCursorParentBasedOnGround();
        }

        // Cursor-related methods

        #region Cursor

        /// <summary>
        /// Selects a block for the player to move toward, setting up pathfinding and animations.
        /// </summary>
        private void SelectBlock()
        {
            if (PlayerManagerRef.IsWalking || PlayerManagerRef.IsDefineTargetBlock || !PlayerManagerRef.CanWalk) return;

            PlayerManagerRef.IsDefineTargetBlock = true;
            PlayerManagerRef.ClickedCube = PlayerManagerRef.SelectedBlock;

            DOTween.Kill(transform);
            FinalPath.Clear();
            FindPath();

            PositionIndicatorOnSelectedBlock();
            PlayIndicatorAnimation();
        }

        /// <summary>
        /// Positions the indicator on the currently selected block.
        /// </summary>
        private void PositionIndicatorOnSelectedBlock()
        {
            PlayerManagerRef.Indicator.position = PlayerManagerRef.SelectedBlock.GetWalkPoint();
        }

        /// <summary>
        /// Plays an animation on the block selection indicator.
        /// </summary>
        private void PlayIndicatorAnimation()
        {
            Sequence s = DOTween.Sequence();
            var indicatorRenderer = PlayerManagerRef.Indicator.GetComponent<Renderer>().material;

            s.AppendCallback(() => PlayerManagerRef.Indicator.GetComponentInChildren<ParticleSystem>().Play());
            s.Append(indicatorRenderer.DOColor(Color.white, 0.1f));
            s.Append(indicatorRenderer.DOColor(Color.black, 0.3f).SetDelay(0.2f));
            s.Append(indicatorRenderer.DOColor(Color.clear, 0.3f));
        }

        /// <summary>
        /// Moves the cursor to a new target block based on the given index of possible paths.
        /// </summary>
        /// <param name="index">Index of the target path in the current block's possible paths.</param>
        private void MoveCursor(int index)
        {
            if (PlayerManagerRef.IsMovingCursor || !PlayerManagerRef.CanMoveCursor) return;

            PlayerManagerRef.IsMovingCursor = true;
            StartCoroutine(ResetCursorTimer());

            var targetBlock = PlayerManagerRef.SelectedBlock.PossiblePaths[index].Target;
            if (targetBlock != null)
            {
                UpdateSelectedBlock(targetBlock);
            }
            else
            {
                Debug.Log("There is no Block Controller at this position");
            }
        }

        /// <summary>
        /// Updates the selected block to the specified target, repositioning the cursor.
        /// </summary>
        /// <param name="targetBlock">Target block to move the cursor to.</param>
        private void UpdateSelectedBlock(Transform targetBlock)
        {
            PlayerManagerRef.SelectedBlock = targetBlock.GetComponent<BlockController>();
            PlayerManagerRef.Cursor.transform.position = new Vector3(
                PlayerManagerRef.SelectedBlock.transform.position.x,
                PlayerManagerRef.SelectedBlock.transform.position.y + PlayerManagerRef.SelectedBlock.WalkPointOffset,
                PlayerManagerRef.SelectedBlock.transform.position.z
            );
        }

        /// <summary>
        /// Resets the cursor movement status after a short delay.
        /// </summary>
        private IEnumerator ResetCursorTimer()
        {
            yield return new WaitForSeconds(0.1f);
            PlayerManagerRef.IsMovingCursor = false;
        }

        #endregion

        // Pathfinding methods

        #region PathFinding

        /// <summary>
        /// Initiates pathfinding to the selected block by exploring available paths.
        /// </summary>
        private void FindPath()
        {
            List<Transform> nextCubes = GetNextCubes();
            List<Transform> visitedCubes = new List<Transform> { PlayerManagerRef.CurrentBlock.transform };

            ExploreCube(nextCubes, visitedCubes);
            BuildPath();
        }

        /// <summary>
        /// Retrieves a list of the next reachable cubes based on active paths.
        /// </summary>
        /// <returns>List of next target cubes.</returns>
        private List<Transform> GetNextCubes()
        {
            var nextCubes = new List<Transform>();

            foreach (WalkPath path in PlayerManagerRef.CurrentBlock.PossiblePaths)
            {
                if (path.Target != null && path.Active)
                {
                    nextCubes.Add(path.Target);
                    path.Target.GetComponent<BlockController>().PreviousBlock = PlayerManagerRef.CurrentBlock.transform;
                }
            }

            return nextCubes;
        }

        /// <summary>
        /// Recursively explores cubes to build a path, tracking visited cubes.
        /// </summary>
        /// <param name="nextCubes">List of reachable cubes to explore.</param>
        /// <param name="visitedCubes">List of already visited cubes.</param>
        private void ExploreCube(List<Transform> nextCubes, List<Transform> visitedCubes)
        {
            while (nextCubes.Any())
            {
                Transform current = nextCubes.First();
                nextCubes.Remove(current);

                if (current == PlayerManagerRef.ClickedCube.transform) return;

                foreach (WalkPath path in current.GetComponent<BlockController>().PossiblePaths)
                {
                    if (path.Target != null && path.Active && !visitedCubes.Contains(path.Target))
                    {
                        nextCubes.Add(path.Target);
                        path.Target.GetComponent<BlockController>().PreviousBlock = current;
                    }
                }

                visitedCubes.Add(current);
            }
        }

        /// <summary>
        /// Builds the final path from the current block to the selected target block.
        /// </summary>
        private void BuildPath()
        {
            Transform cube = PlayerManagerRef.ClickedCube.transform;

            while (cube != PlayerManagerRef.CurrentBlock.transform)
            {
                FinalPath.Add(cube);
                if (cube.GetComponent<BlockController>().PreviousBlock != null)
                {
                    cube = cube.GetComponent<BlockController>().PreviousBlock;
                }
                else
                {
                    return;
                }
            }

            FinalPath.Insert(0, PlayerManagerRef.ClickedCube.transform);
            FollowPath();
        }

        /// <summary>
        /// Moves the player along the calculated path to the target block.
        /// </summary>
        private void FollowPath()
        {
            Sequence s = DOTween.Sequence();
            PlayerManagerRef.IsWalking = true;

            for (int i = FinalPath.Count - 1; i >= 0; i--)
            {
                float time = FinalPath[i].GetComponent<BlockController>().IsStair ? 1.5f : 1;
                s.Append(transform.parent.DOMove(FinalPath[i].GetComponent<BlockController>().GetWalkPoint(), 0.2f * time).SetEase(Ease.Linear));

                if (!FinalPath[i].GetComponent<BlockController>().DontRotate)
                {
                    s.Join(transform.parent.DOLookAt(FinalPath[i].position, 0.1f, AxisConstraint.Y, Vector3.up));
                }
            }

            s.AppendCallback(ClearPath);
        }

        /// <summary>
        /// Clears the path data after reaching the target block, resetting all temporary path properties.
        /// </summary>
        private void ClearPath()
        {
            if (PlayerManagerRef.ClickedCube.IsFinalBlock)
            {
                PlayerManagerRef.CanWalk = false;
                GameManager.Instance.AddFinishedPlayer();
            }
            
            foreach (Transform t in FinalPath)
            {
                t.GetComponent<BlockController>().PreviousBlock = null;
            }

            FinalPath.Clear();
            PlayerManagerRef.IsWalking = false;
            PlayerManagerRef.IsDefineTargetBlock = false;
        }

        #endregion

        // Ground detection methods

        #region GroundRayDetect

        /// <summary>
        /// Casts a ray downward to detect the ground block below the player, updating the current block reference.
        /// </summary>
        private void RayCastDown()
        {
            Ray playerRay = new Ray(transform.GetChild(0).position, -transform.up);
            if (Physics.Raycast(playerRay, out RaycastHit playerHit))
            {
                var blockController = playerHit.transform.GetComponent<BlockController>();
                if (blockController != null)
                {
                    PlayerManagerRef.CurrentBlock = blockController;

                    if (!PlayerManagerRef.IsStarted)
                    {
                        PlayerManagerRef.SelectedBlock = PlayerManagerRef.CurrentBlock;
                        PlayerManagerRef.IsStarted = true;
                    }
                }
            }
        }

        /// <summary>
        /// Draws a visual representation of the downward ray in the Unity editor.
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(new Ray(transform.GetChild(0).position, -transform.up));
        }

        #endregion

        // Utilities Methods

        #region Utilities Methods

        /// <summary>
        /// Registers event handlers for selecting blocks and moving the cursor.
        /// </summary>
        private void RegisterEvents()
        {
            OnSelectBlock += SelectBlock;
            OnMoveCursor += MoveCursor;
        }

        /// <summary>
        /// Initializes a raycast to detect the initial ground block below the player.
        /// </summary>
        private void PerformInitialRayCast()
        {
            RayCastDown();
        }

        /// <summary>
        /// Continuously updates the raycast to check the ground below the player.
        /// </summary>
        private void UpdateRayCast()
        {
            RayCastDown();
        }

        /// <summary>
        /// Updates the player's parent object based on the ground status (moving or static).
        /// </summary>
        private void UpdateCursorParentBasedOnGround()
        {
            transform.parent.parent = PlayerManagerRef.CurrentBlock.MovingGround
                ? PlayerManagerRef.CurrentBlock.transform.parent
                : null;
        }

        #endregion

        #endregion
    }
}