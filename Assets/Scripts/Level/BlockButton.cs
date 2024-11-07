using UnityEngine;
using DG.Tweening;
using Level;

/// <summary>
/// Handles the activation and movement of a group of blocks, including animations and updating rotations.
/// </summary>
public class BlockButton : MonoBehaviour
{
    #region Properties

    [field: SerializeField] public Ease Ease { get; private set; }
    public bool IsActive { get; private set; }

    [SerializeField] private GameObject _groupBlock;
    [SerializeField] private Vector3 _movePosition;
    [SerializeField] private float _moveDuration;

    #endregion

    #region Methods
    
    /// <summary>
    /// Activates the block button, triggering the block's movement and rotation update.
    /// </summary>
    public void Activate()
    {
        IsActive = true;
        MoveGroupBlock();
        UpdatePivotRotation();
    }

    /// <summary>
    /// Moves the group block to the specified position over the configured duration with an easing effect.
    /// </summary>
    private void MoveGroupBlock()
    {
        _groupBlock.transform.DOMove(_movePosition, _moveDuration).SetEase(Ease);
    }

    /// <summary>
    /// Updates the rotation of the blocks associated with the Pivot component after the block group has moved.
    /// </summary>
    private void UpdatePivotRotation()
    {
        _groupBlock.GetComponent<Pivot>().UpdatePivotBlocksRotationAfterMoved();
    }
    
    #endregion
}