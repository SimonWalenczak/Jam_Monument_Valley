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

        [SerializeField] private int PivotRotationIndex;
        [SerializeField] private List<ChangingBlock> ChangingBlocks;


        [SerializeField] private int _rotateTimer;

        public Action<int> OnMovePivot;

        public bool CanRotate;

        public bool IsRotating;

        private int valueRotation;

        List<WalkPath> previousWalkPaths = new List<WalkPath>();

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
            if (!CanRotate) return;

            CanRotate = false;
            IsRotating = true;

            valueRotation = value;
            PivotRotationIndex += value;

            CheckPivotIndex();
            RotatePivot();
        }

        private void CheckPivotIndex()
        {
            if (PivotRotationIndex > 3)
            {
                PivotRotationIndex = 0;
            }

            if (PivotRotationIndex < 0)
            {
                PivotRotationIndex = 3;
            }
        }

        private void UpdateBlocksRotation()
        {
            foreach (var changingBlock in ChangingBlocks)
            {
                changingBlock.Block.PossiblePaths = changingBlock.ChangingPaths[PivotRotationIndex].PossiblePaths;
            }
        }

        private void UpdatePivotBlocksRotation()
        {
            foreach (var pivotBlock in PivotBlocks)
            {
                previousWalkPaths = pivotBlock.PossiblePaths;
                
                for (int i = 0; i < pivotBlock.PossiblePaths.Count; i++)
                {
                    previousWalkPaths[i].Target = pivotBlock.PossiblePaths[i].Target;
                    previousWalkPaths[i].Active = pivotBlock.PossiblePaths[i].Active;
                }

                if (valueRotation == 1)
                {
                    pivotBlock.PossiblePaths[2].Target = previousWalkPaths[0].Target;
                    pivotBlock.PossiblePaths[3].Target = previousWalkPaths[1].Target;
                    pivotBlock.PossiblePaths[1].Target = previousWalkPaths[2].Target;
                    pivotBlock.PossiblePaths[0].Target = previousWalkPaths[3].Target;
                }
                else if (valueRotation == -1)
                {
                    pivotBlock.PossiblePaths[0] = previousWalkPaths[2];
                    pivotBlock.PossiblePaths[1] = previousWalkPaths[3];
                    pivotBlock.PossiblePaths[2] = previousWalkPaths[1];
                    pivotBlock.PossiblePaths[3] = previousWalkPaths[0];
                }
            }
        }

        private void RotatePivot()
        {
            transform.DORotate(new Vector3(0, 90 * PivotRotationIndex, 0), _rotateTimer).SetEase(Ease.Linear);
            StartCoroutine(WaitingForRotateTimer());
        }

        IEnumerator WaitingForRotateTimer()
        {
            yield return new WaitForSeconds(_rotateTimer);
            UpdateBlocksRotation();
            UpdatePivotBlocksRotation();
            IsRotating = false;
            CanRotate = true;
        }

        #endregion
    }
}