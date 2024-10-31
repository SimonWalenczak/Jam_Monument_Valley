using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Character;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    #region Properties

    [field: Space, SerializeField] public Transform CurrentCube { get; private set; }
    [field: SerializeField] public Transform ClickedCube { get; private set; }
    [field: SerializeField] public GameObject Cursor { get; private set; }
    [field: SerializeField] public Transform Indicator { get; private set; }
    [field: Space, SerializeField] public List<Transform> FinalPath { get; private set; }

    private BlockController _selectedBlock;

    [SerializeField] private Animator _animator;

    public Action<int> OnMoveCursor;
    public Action OnSelectBlock;

    private bool _isStarted;
    private bool _isMovingCursor;
    private bool _isDefineTargetBlock;
    [SerializeField] private bool _isWalking;

    #endregion

    #region Methods

    private void Awake()
    {
        OnSelectBlock += SelectBlock;
        OnMoveCursor += MoveCursor;
    }

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();

        RayCastDown();

        Indicator.transform.parent = null;
        Cursor.transform.parent = null;
    }

    private void Update()
    {
        RayCastDown();

        transform.parent = CurrentCube.GetComponent<BlockController>().MovingGround ? CurrentCube.parent : null;

        _animator.SetBool("IsWalking", _isWalking);
    }

    private void SelectBlock()
    {
        if (_isWalking || _isDefineTargetBlock) return;

        _isDefineTargetBlock = true;

        ClickedCube = _selectedBlock.transform;
        DOTween.Kill(gameObject.transform);
        FinalPath.Clear();
        FindPath();

        Indicator.position = _selectedBlock.GetWalkPoint();
        Sequence s = DOTween.Sequence();
        s.AppendCallback(() => Indicator.GetComponentInChildren<ParticleSystem>().Play());
        s.Append(Indicator.GetComponent<Renderer>().material.DOColor(Color.white, .1f));
        s.Append(Indicator.GetComponent<Renderer>().material.DOColor(Color.black, .3f).SetDelay(.2f));
        s.Append(Indicator.GetComponent<Renderer>().material.DOColor(Color.clear, .3f));
    }

    private void MoveCursor(int index)
    {
        if (_isMovingCursor) return;

        _isMovingCursor = true;
        StartCoroutine(ResetCursorTimer());

        if (_selectedBlock.PossiblePaths[index].target != null)
        {
            _selectedBlock = _selectedBlock.PossiblePaths[index].target.GetComponent<BlockController>();
            Cursor.transform.position = new Vector3(_selectedBlock.transform.position.x, _selectedBlock.transform.position.y + _selectedBlock.walkPointOffset,
                _selectedBlock.transform.position.z);
        }
        else
        {
            Debug.Log("There is no Block Controller at this position");
        }
    }

    IEnumerator ResetCursorTimer()
    {
        yield return new WaitForSeconds(0.2f);
        _isMovingCursor = false;
    }

    #region PathFinding

    private void FindPath()
    {
        List<Transform> nextCubes = new List<Transform>();
        List<Transform> pastCubes = new List<Transform>();

        foreach (WalkPath path in CurrentCube.GetComponent<BlockController>().PossiblePaths)
        {
            if (path.target != null)
            {
                if (path.active)
                {
                    nextCubes.Add(path.target);
                    path.target.GetComponent<BlockController>().PreviousBlock = CurrentCube;
                }
            }
        }

        pastCubes.Add(CurrentCube);

        ExploreCube(nextCubes, pastCubes);
        BuildPath();
    }

    private void ExploreCube(List<Transform> nextCubes, List<Transform> visitedCubes)
    {
        Transform current = nextCubes.First();
        nextCubes.Remove(current);

        if (current == ClickedCube)
        {
            return;
        }

        foreach (WalkPath path in current.GetComponent<BlockController>().PossiblePaths)
        {
            if (path.target != null)
            {
                if (!visitedCubes.Contains(path.target) && path.active)
                {
                    nextCubes.Add(path.target);
                    path.target.GetComponent<BlockController>().PreviousBlock = current;
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
        Transform cube = ClickedCube;
        while (cube != CurrentCube)
        {
            FinalPath.Add(cube);
            if (cube.GetComponent<BlockController>().PreviousBlock != null)
                cube = cube.GetComponent<BlockController>().PreviousBlock;
            else
                return;
        }

        FinalPath.Insert(0, ClickedCube);

        FollowPath();
    }

    private void FollowPath()
    {
        Sequence s = DOTween.Sequence();

        _isWalking = true;

        for (int i = FinalPath.Count - 1; i > 0; i--)
        {
            float time = FinalPath[i].GetComponent<BlockController>().IsStair ? 1.5f : 1;

            s.Append(transform.DOMove(FinalPath[i].GetComponent<BlockController>().GetWalkPoint(), .2f * time).SetEase(Ease.Linear));

            if (!FinalPath[i].GetComponent<BlockController>().DontRotate)
            {
                s.Join(transform.DOLookAt(FinalPath[i].position, .1f, AxisConstraint.Y, Vector3.up));
            }
        }

        if (ClickedCube.GetComponent<BlockController>().IsButton)
        {
            s.AppendCallback(() => GameManager.Instance.RotateRightPivot());
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
        _isWalking = false;
        _isDefineTargetBlock = false;
    }

    #endregion

    private void RayCastDown()
    {
        Ray playerRay = new Ray(transform.GetChild(0).position, -transform.up);
        RaycastHit playerHit;

        if (Physics.Raycast(playerRay, out playerHit))
        {
            if (playerHit.transform.GetComponent<BlockController>() != null)
            {
                CurrentCube = playerHit.transform;

                if (_isStarted == false)
                {
                    _selectedBlock = CurrentCube.GetComponent<BlockController>();
                    _isStarted = true;
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
}