using System;
using System.Collections.Generic;
using MultiplayerSystem;
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

        [field: SerializeField] public List<DynamicCamera> Cameras { get; private set; }
        [field: SerializeField] public List<GameObject> Players { get; set; }

        [SerializeField] private int _nbPlayerFinished = 0;

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

        public void AssignPlayersToCameras()
        {
            foreach (var camera in Cameras)
            {
                foreach (var player in Players)
                {
                    camera.Players.Add(player.transform);
                }
            }
        }

        public void AddFinishedPlayer()
        {
            _nbPlayerFinished++;
        }

        #endregion
    }
}