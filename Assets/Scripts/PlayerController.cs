using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

[SelectionBase]
public class PlayerController : MonoBehaviour
{
    #region Properties

    [field: SerializeField] public bool Walking { get; private set; }
    [field: Space, SerializeField] public Transform CurrentCube { get; private set; }
    [field: SerializeField] public Transform ClickedCube { get; private set; }
    [field: SerializeField] public Transform Indicator { get; private set; }
    [field: Space, SerializeField] public List<Transform> FinalPath { get; private set; }

    #endregion

    #region Methods

    private void Start()
    {
        RayCastDown();
    }

    private void Update()
    {
        RayCastDown();

        if (CurrentCube.GetComponent<BlockController>().MovingGround)
        {
            transform.parent = CurrentCube.parent;
        }
        else
        {
            transform.parent = null;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;

            if (Physics.Raycast(mouseRay, out mouseHit))
            {
                if (mouseHit.transform.GetComponent<BlockController>() != null)
                {
                    ClickedCube = mouseHit.transform;
                    DOTween.Kill(gameObject.transform);
                    FinalPath.Clear();
                    FindPath();

                    Indicator.position = mouseHit.transform.GetComponent<BlockController>().GetWalkPoint();
                    Sequence s = DOTween.Sequence();
                    s.AppendCallback(() => Indicator.GetComponentInChildren<ParticleSystem>().Play());
                    s.Append(Indicator.GetComponent<Renderer>().material.DOColor(Color.white, .1f));
                    s.Append(Indicator.GetComponent<Renderer>().material.DOColor(Color.black, .3f).SetDelay(.2f));
                    s.Append(Indicator.GetComponent<Renderer>().material.DOColor(Color.clear, .3f));
                }
            }
        }
    }

    private void FindPath()
    {
        List<Transform> nextCubes = new List<Transform>();
        List<Transform> pastCubes = new List<Transform>();

        foreach (WalkPath path in CurrentCube.GetComponent<BlockController>().PossiblePaths)
        {
            if (path.active)
            {
                nextCubes.Add(path.target);
                path.target.GetComponent<BlockController>().PreviousBlock = CurrentCube;
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
            if (!visitedCubes.Contains(path.target) && path.active)
            {
                nextCubes.Add(path.target);
                path.target.GetComponent<BlockController>().PreviousBlock = current;
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

        Walking = true;

        for (int i = FinalPath.Count - 1; i > 0; i--)
        {
            float time = FinalPath[i].GetComponent<BlockController>().IsStair ? 1.5f : 1;

            s.Append(transform.DOMove(FinalPath[i].GetComponent<BlockController>().GetWalkPoint(), .2f * time).SetEase(Ease.Linear));

            if (!FinalPath[i].GetComponent<BlockController>().DontRotate)
                s.Join(transform.DOLookAt(FinalPath[i].position, .1f, AxisConstraint.Y, Vector3.up));
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
        Walking = false;
    }

    private void RayCastDown()
    {
        Ray playerRay = new Ray(transform.GetChild(0).position, -transform.up);
        RaycastHit playerHit;

        if (Physics.Raycast(playerRay, out playerHit))
        {
            if (playerHit.transform.GetComponent<BlockController>() != null)
            {
                CurrentCube = playerHit.transform;
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