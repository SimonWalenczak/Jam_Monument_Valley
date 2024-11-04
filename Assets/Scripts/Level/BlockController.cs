using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    #region Properties

    [field: SerializeField] public List<WalkPath> PossiblePaths { get; set; }
    [field: Space, SerializeField] public Transform PreviousBlock { get; set; }
    [field: Header("Booleans"), SerializeField] public bool IsStair { get; private set; }
    [field: SerializeField] public bool MovingGround { get; private set; }
    [field: SerializeField] public bool IsButton { get; private set; }
    [field: SerializeField] public bool DontRotate { get; private set; }

    [field: Space, Header("Offsets"), SerializeField] public float WalkPointOffset { get; private set; }
    [field: SerializeField] public float StairOffset { get; private set; }
  
    #endregion

    #region Methods

    public Vector3 GetWalkPoint()
    {
        float stair = IsStair ? StairOffset : 0;
        return transform.position + transform.up * WalkPointOffset - transform.up * stair;
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
            if (p.Target == null)
                return;
            Gizmos.color = p.Active ? Color.black : Color.clear;
            Gizmos.DrawLine(GetWalkPoint(), p.Target.GetComponent<BlockController>().GetWalkPoint());
        }
    }

    #endregion
}

[Serializable]
public class WalkPath
{
    public Transform Target;
    public bool Active;

    public WalkPath(Transform targetPos, bool active)
    {
        Target = targetPos.transform;
        Active = true;
    }
}

[Serializable]
public class ChangingBlock
{
	public BlockController Block;
	public List<ChangingPath> ChangingPaths;

    public ChangingBlock(BlockController block, List<ChangingPath> possiblePaths)
    {
        Block = block;
        ChangingPaths = possiblePaths;
    }
}

[Serializable]
public class ChangingPath
{
    public List<WalkPath> PossiblePaths;

    public ChangingPath(List<WalkPath> possiblePaths)
    {
        PossiblePaths = possiblePaths;
    }
}