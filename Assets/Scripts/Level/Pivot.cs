using System;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    public class Pivot : MonoBehaviour
    {
        #region Properties

        [SerializeField] private int _pivotRotationIndex;
        [SerializeField] private List<ChangingBlock> ChangingBlocks;

        public Action OnMovePivot;
        
        #endregion

        #region Methods

        private void Awake()
        {
            OnMovePivot += CheckPivotIndex;
            OnMovePivot += UpdateBlocksRotation;
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
    
        private void UpdateBlocksRotation()
        {
            foreach (var changingBlock in ChangingBlocks)
            {
                changingBlock.Block.PossiblePaths = changingBlock.ChangingPaths[_pivotRotationIndex].PossiblePaths;
            }
        }

        #endregion
    }
}