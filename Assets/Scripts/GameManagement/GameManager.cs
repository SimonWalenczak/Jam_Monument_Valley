using System.Collections.Generic;
using UnityEngine;

namespace GameManagement
{
    /// <summary>
    /// This class follows the Singleton pattern to ensure there is only one GameManager in the scene.
    /// It manages a list of pivots, which are essential for the gameplay in order to assign each pivot to players.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region Properties

        public static GameManager Instance;
        [field: SerializeField] public List<Transform> Pivots { get; private set; }

        #endregion

        #region Methods

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("There is another Game Manager in this scene !");
            }
        }
        
        #endregion
    }
}