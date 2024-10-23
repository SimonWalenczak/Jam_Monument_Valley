using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Properties

    public static GameManager Instance;

    [field: SerializeField] public PlayerController Player { get; private set; }
    [field: SerializeField] public List<PathCondition> PathConditions = new List<PathCondition>();
    [field: SerializeField] public List<Transform> Pivots;

    [field: SerializeField] public Transform[] ObjectsToHide;

    #endregion

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        foreach (PathCondition pc in PathConditions)
        {
            int count = 0;
            for (int i = 0; i < pc.conditions.Count; i++)
            {
                if (pc.conditions[i].conditionObject.eulerAngles == pc.conditions[i].eulerAngle)
                {
                    count++;
                }
            }

            foreach (SinglePath sp in pc.paths)
                sp.block.PossiblePaths[sp.index].active = (count == pc.conditions.Count);
        }

        if (Player.Walking)
            return;

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int multiplier = Input.GetKey(KeyCode.RightArrow) ? 1 : -1;
            Pivots[0].DOComplete();
            Pivots[0].DORotate(new Vector3(0, 90 * multiplier, 0), .6f, RotateMode.WorldAxisAdd).SetEase(Ease.OutBack);
        }

        foreach (Transform t in ObjectsToHide)
        {
            t.gameObject.SetActive(Pivots[0].eulerAngles.y > 45 && Pivots[0].eulerAngles.y < 90 + 45);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
    }

    public void RotateRightPivot()
    {
        Pivots[1].DOComplete();
        Pivots[1].DORotate(new Vector3(0, 0, 90), .6f).SetEase(Ease.OutBack);
    }
}

[System.Serializable]
public class PathCondition
{
    public string pathConditionName;
    public List<Condition> conditions;
    public List<SinglePath> paths;
}

[System.Serializable]
public class Condition
{
    public Transform conditionObject;
    public Vector3 eulerAngle;
}

[System.Serializable]
public class SinglePath
{
    public BlockController block;
    public int index;
}