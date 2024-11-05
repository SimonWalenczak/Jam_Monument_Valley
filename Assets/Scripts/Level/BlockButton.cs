using UnityEngine;
using DG.Tweening;
using Level;

public class BlockButton : MonoBehaviour
{
    [SerializeField] private GameObject _groupBlock;
    [SerializeField] private Vector3 _movePosition;
    [SerializeField] private float _moveDuration;

    public bool IsActive;

    public void Active()
    {
        IsActive = true;

        _groupBlock.transform.DOMove(_movePosition, _moveDuration).SetEase(Ease.Linear);
        _groupBlock.GetComponent<Pivot>().UpdatePivotBlocksRotationAfterMoved();
    }
}