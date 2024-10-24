using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    #region Properties

    [field: SerializeField] public List<WalkPath> PossiblePaths { get; private set; }
    [field: Space, SerializeField] public Transform PreviousBlock { get; set; }
    [field: Header("Booleans"), SerializeField] public bool IsStair { get; private set; }
    [field: SerializeField] public bool MovingGround { get; private set; }
    [field: SerializeField] public bool IsButton { get; private set; }
    [field: SerializeField] public bool DontRotate { get; private set; }

    [field: Space, Header("Offsets"), SerializeField] public float walkPointOffset { get; private set; }
    [field: SerializeField] public float stairOffset { get; private set; }
  
    #endregion

    #region Methods

    public Vector3 GetWalkPoint()
    {
        float stair = IsStair ? stairOffset : 0;
        return transform.position + transform.up * walkPointOffset - transform.up * stair;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        float stair = IsStair ? .4f : 0;
        Gizmos.DrawSphere(GetWalkPoint(), .1f);

        if (PossiblePaths == null)
            return;

        foreach (WalkPath p in PossiblePaths)
        {
            if (p.target == null)
                return;
            Gizmos.color = p.active ? Color.black : Color.clear;
            Gizmos.DrawLine(GetWalkPoint(), p.target.GetComponent<BlockController>().GetWalkPoint());
        }
    }

    #endregion
}

[Serializable]
public class WalkPath
{
    public Transform target;
    public bool active = true;

    public WalkPath(Transform targetPos, bool active)
    {
        this.target = targetPos.transform;
        this.active = true;
    }
}