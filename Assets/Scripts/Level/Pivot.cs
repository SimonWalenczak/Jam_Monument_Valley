using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Level
{
    public class Pivot : MonoBehaviour
    {
        #region Properties

        [SerializeField] private List<BlockController> PivotBlocks;
        List<WalkPath> previousWalkPaths = new List<WalkPath>();

        [SerializeField] private List<ChangingBlock> ChangingBlocks;
        [SerializeField] private List<ChangingBlock> ChangingBlocksAfterMoved;
        
        [SerializeField] private int _pivotRotationIndex;
        [SerializeField] private int _rotateTimer;

        private int _valueRotation;

        [HideInInspector] public PlayerController playerController;
        public bool CanRotate;
        public bool IsRotating;
        public bool HasMoved;

        public Action<int> OnMovePivot;

        #endregion

        #region Methods

        private void Awake()
        {
            OnMovePivot += ChangePivotValue;
        }

        private void Start()
        {
            CanRotate = true;
        }

        private void ChangePivotValue(int value)
        {
            if (CanRotate == false || playerController.IsWalking || playerController.CurrentCube.GetComponent<BlockController>().MovingGround) return;

            CanRotate = false;
            IsRotating = true;

            _valueRotation = value;
            _pivotRotationIndex += value;

            CheckPivotIndex();
            RotatePivot();
            playerController.ResetCursorPosition();
        }

        private void CheckPivotIndex()
        {
            if (_pivotRotationIndex > 3)
            {
                _pivotRotationIndex = 0;
            }

            if (_pivotRotationIndex < 0)
            {
                _pivotRotationIndex = 3;
            }
        }

        //Pour les blocks connectés à un block de pivot ET pour les block extrémitées du pivot
        private void UpdateBlocksRotation()
        {
            foreach (var changingBlock in ChangingBlocks)
            {
                changingBlock.Block.PossiblePaths = changingBlock.ChangingPaths[_pivotRotationIndex].PossiblePaths;
            }
        }

        //Pour tous les blocks du pivot sauf les extrémitées
        private void UpdatePivotBlocksRotation()
        {
            foreach (var pivotBlock in PivotBlocks)
            {
                previousWalkPaths = new List<WalkPath>();

                foreach (var path in pivotBlock.PossiblePaths)
                {
                    previousWalkPaths.Add(new WalkPath(path.Target, path.Active));
                }

                switch (_valueRotation)
                {
                    case 1:
                        pivotBlock.PossiblePaths[2].Target = previousWalkPaths[0].Target;
                        pivotBlock.PossiblePaths[3].Target = previousWalkPaths[1].Target;
                        pivotBlock.PossiblePaths[1].Target = previousWalkPaths[2].Target;
                        pivotBlock.PossiblePaths[0].Target = previousWalkPaths[3].Target;
                        break;
                    case -1:
                        pivotBlock.PossiblePaths[0] = previousWalkPaths[2];
                        pivotBlock.PossiblePaths[1] = previousWalkPaths[3];
                        pivotBlock.PossiblePaths[2] = previousWalkPaths[1];
                        pivotBlock.PossiblePaths[3] = previousWalkPaths[0];
                        break;
                }
            }
        }

        public void UpdatePivotBlocksRotationAfterMoved()
        {
            HasMoved = true;

            foreach (var changingBlock in ChangingBlocksAfterMoved)
            {
                changingBlock.Block.PossiblePaths = changingBlock.ChangingPaths[_pivotRotationIndex].PossiblePaths;
            }
        }

        private void RotatePivot()
        {
            transform.DORotate(new Vector3(0, 90 * _pivotRotationIndex, 0), _rotateTimer).SetEase(Ease.Linear);
            StartCoroutine(WaitingForRotateTimer());
        }

        IEnumerator WaitingForRotateTimer()
        {
            yield return new WaitForSeconds(_rotateTimer);
            UpdatePivotBlocksRotation();
            if (HasMoved == false)
            {
                UpdateBlocksRotation();
            }
            else
            {
                UpdatePivotBlocksRotationAfterMoved();
            }

            IsRotating = false;
            CanRotate = true;
        }

        #endregion
    }
}