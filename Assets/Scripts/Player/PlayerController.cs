using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Player
{
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
            OnSelectBlock += SelectBlock;
            OnMoveCursor += MoveCursor;
        }

        private void Start()
        {
            RayCastDown();
        }

        private void Update()
        {
            RayCastDown();

            //Setting parent depends to ground nature
            transform.parent.transform.parent = PlayerManagerRef.CurrentBlock.MovingGround ? PlayerManagerRef.CurrentBlock.transform.parent : null;
        }

        #region Cursor

        private void SelectBlock()
        {
            if (PlayerManagerRef.IsWalking || PlayerManagerRef.IsDefineTargetBlock || PlayerManagerRef.CanWalk == false) return;

            PlayerManagerRef.IsDefineTargetBlock = true;

            PlayerManagerRef.ClickedCube = PlayerManagerRef.SelectedBlock;
            DOTween.Kill(gameObject.transform);
            FinalPath.Clear();
            FindPath();

            PlayerManagerRef.Indicator.position = PlayerManagerRef.SelectedBlock.GetWalkPoint();
            Sequence s = DOTween.Sequence();
            s.AppendCallback(() => PlayerManagerRef.Indicator.GetComponentInChildren<ParticleSystem>().Play());
            s.Append(PlayerManagerRef.Indicator.GetComponent<Renderer>().material.DOColor(Color.white, .1f));
            s.Append(PlayerManagerRef.Indicator.GetComponent<Renderer>().material.DOColor(Color.black, .3f).SetDelay(.2f));
            s.Append(PlayerManagerRef.Indicator.GetComponent<Renderer>().material.DOColor(Color.clear, .3f));
        }

        private void MoveCursor(int index)
        {
            if (PlayerManagerRef.IsMovingCursor || PlayerManagerRef.CanMoveCursor == false) return;

            PlayerManagerRef.IsMovingCursor = true;
            StartCoroutine(ResetCursorTimer());

            if (PlayerManagerRef.SelectedBlock.PossiblePaths[index].Target != null)
            {
                PlayerManagerRef.SelectedBlock = PlayerManagerRef.SelectedBlock.PossiblePaths[index].Target.GetComponent<BlockController>();
                PlayerManagerRef.Cursor.transform.position = new Vector3(PlayerManagerRef.SelectedBlock.transform.position.x,
                    PlayerManagerRef.SelectedBlock.transform.position.y + PlayerManagerRef.SelectedBlock.WalkPointOffset,
                    PlayerManagerRef.SelectedBlock.transform.position.z);
            }
            else
            {
                Debug.Log("There is no Block Controller at this position");
            }
        }

        IEnumerator ResetCursorTimer()
        {
            yield return new WaitForSeconds(0.1f);
            PlayerManagerRef.IsMovingCursor = false;
        }

        #endregion

        #region PathFinding

        private void FindPath()
        {
            List<Transform> nextCubes = new List<Transform>();
            List<Transform> pastCubes = new List<Transform>();

            foreach (WalkPath path in PlayerManagerRef.CurrentBlock.PossiblePaths)
            {
                if (path.Target != null)
                {
                    if (path.Active)
                    {
                        nextCubes.Add(path.Target);
                        path.Target.GetComponent<BlockController>().PreviousBlock = PlayerManagerRef.CurrentBlock.transform;
                    }
                }
            }

            pastCubes.Add(PlayerManagerRef.CurrentBlock.transform);

            ExploreCube(nextCubes, pastCubes);
            BuildPath();
        }

        private void ExploreCube(List<Transform> nextCubes, List<Transform> visitedCubes)
        {
            Transform current = nextCubes.First();
            nextCubes.Remove(current);

            if (current == PlayerManagerRef.ClickedCube.transform)
            {
                return;
            }

            foreach (WalkPath path in current.GetComponent<BlockController>().PossiblePaths)
            {
                if (path.Target != null)
                {
                    if (!visitedCubes.Contains(path.Target) && path.Active)
                    {
                        nextCubes.Add(path.Target);
                        path.Target.GetComponent<BlockController>().PreviousBlock = current;
                    }
                }
            }

            visitedCubes.Add(current);

            if (nextCubes.Any())
            {
                ExploreCube(nextCubes, visitedCubes);
            }
        }

        private void BuildPath()
        {
            Transform cube = PlayerManagerRef.ClickedCube.transform;
            while (cube != PlayerManagerRef.CurrentBlock.transform)
            {
                FinalPath.Add(cube);
                if (cube.GetComponent<BlockController>().PreviousBlock != null)
                    cube = cube.GetComponent<BlockController>().PreviousBlock;
                else
                    return;
            }

            FinalPath.Insert(0, PlayerManagerRef.ClickedCube.transform);

            FollowPath();
        }

        private void FollowPath()
        {
            Sequence s = DOTween.Sequence();

            PlayerManagerRef.IsWalking = true;

            for (int i = FinalPath.Count - 1; i > 0; i--)
            {
                float time = FinalPath[i].GetComponent<BlockController>().IsStair ? 1.5f : 1;

                s.Append(transform.parent.transform.DOMove(FinalPath[i].GetComponent<BlockController>().GetWalkPoint(), .2f * time).SetEase(Ease.Linear));

                if (!FinalPath[i].GetComponent<BlockController>().DontRotate)
                {
                    s.Join(transform.parent.transform.DOLookAt(FinalPath[i].position, .1f, AxisConstraint.Y, Vector3.up));
                }
            }

            s.AppendCallback(() => Clear());
        }

        private void Clear()
        {
            foreach (Transform t in FinalPath)
            {
                t.GetComponent<BlockController>().PreviousBlock = null;
            }

            FinalPath.Clear();
            PlayerManagerRef.IsWalking = false;
            PlayerManagerRef.IsDefineTargetBlock = false;
        }

        #endregion

        #region GroundRayDetect

        private void RayCastDown()
        {
            Ray playerRay = new Ray(transform.GetChild(0).position, -gameObject.transform.up);
            RaycastHit playerHit;

            if (Physics.Raycast(playerRay, out playerHit))
            {
                if (playerHit.transform.GetComponent<BlockController>() != null)
                {
                    PlayerManagerRef.CurrentBlock = playerHit.transform.GetComponent<BlockController>();

                    if (PlayerManagerRef.IsStarted == false)
                    {
                        PlayerManagerRef.SelectedBlock = PlayerManagerRef.CurrentBlock;
                        PlayerManagerRef.IsStarted = true;
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Ray ray = new Ray(transform.GetChild(0).position, -transform.up);
            Gizmos.DrawRay(ray);
        }

        #endregion

        #endregion
    }
}