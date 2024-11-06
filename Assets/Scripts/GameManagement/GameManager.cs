using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagement
{
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

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
            }
        }

        #endregion
    }
}