using GameManagement;
using MultiplayerSystem;
using Player;
using UnityEngine;

namespace Level
{
    /// <summary>
    /// Handles the initialization of the game level, including spawning players at specified locations 
    /// and assigning the appropriate player configurations and character meshes.
    /// </summary>
    public class InitializeLevel : MonoBehaviour
    {
        #region Properties

        [SerializeField] private Transform[] _playerSpawn;
        [SerializeField] private CharacterMultiplayerManager _playerPrefab;

        #endregion

        #region Methods

        private void Start()
        {
            InitializePlayers();
        }

        /// <summary>
        /// Initializes the level by instantiating players at the specified spawn points and assigning player configurations.
        /// </summary>
        private void InitializePlayers()
        {
            var playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();

            // Instantiate each player at the appropriate spawn point
            for (int i = 0; i < playerConfigs.Length; i++)
            {
                // Instantiate the player object
                CharacterMultiplayerManager player = Instantiate(
                    _playerPrefab,
                    _playerSpawn[i].position,
                    _playerSpawn[i].rotation,
                    gameObject.transform
                );

                // Initialize player input management with the player's configuration
                player.GetComponentInChildren<InputManagement>().InitializePlayer(playerConfigs[i]);

                Transform placeForCharacterPlayer = player.transform;

                // Instantiate the player's character mesh based on their configuration
                GameObject characterPlayer = Instantiate(
                    playerConfigs[i].MeshPlayer,
                    placeForCharacterPlayer.position,
                    Quaternion.identity,
                    placeForCharacterPlayer.transform
                );
                
                GameManager.Instance.Players.Add(characterPlayer);
            }
            
            GameManager.Instance.AssignPlayersToCameras();
        }

        #endregion
    }
}