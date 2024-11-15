using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using TMPro;
using GameManagement;

namespace MultiplayerSystem
{
    /// <summary>
    /// Manages the player configurations, including player joining, setting meshes, and transitioning to the game.
    /// </summary>
    public class PlayerConfigurationManager : MonoBehaviour
    {
        #region Properties

        public static PlayerConfigurationManager Instance { get; private set; }

        [SerializeField] private Fader _fader;

        [SerializeField] private int _maxPlayer = 2;
        [SerializeField] private bool _selecteionFinish;
        [SerializeField] private TextMeshProUGUI _textExplain;
        [SerializeField] private GameObject _explainPanel;

        private List<PlayerConfiguration> _playerConfigs;

        #endregion

        #region Methods

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There is already another PlayerConfigurationManager in this scene !");
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(Instance);
                _playerConfigs = new List<PlayerConfiguration>();
            }
        }

        private void Update()
        {
            if (_selecteionFinish == false)
            {
                if (_playerConfigs.Count >= _maxPlayer)
                {
                    _explainPanel.SetActive(false);
                    GetComponent<PlayerInputManager>().playerPrefab = null;
                    _selecteionFinish = true;
                }
                else
                {
                    _textExplain.SetText((_maxPlayer - _playerConfigs.Count).ToString() +
                                         " controllers left to connect");
                }
            }
        }
        
        /// <summary>
        /// Returns the list of player configurations.
        /// </summary>
        /// <returns>A list of PlayerConfiguration objects.</returns>
        public List<PlayerConfiguration> GetPlayerConfigs()
        {
            return _playerConfigs;
        }

        /// <summary>
        /// Sets the player mesh for a specific player.
        /// </summary>
        /// <param name="index">The index of the player.</param>
        /// <param name="mesh">The new mesh GameObject for the player.</param>
        /// <param name="intMesh">The index of the selected mesh.</param>
        public void SetPlayerMesh(int index, GameObject mesh, int intMesh)
        {
            _playerConfigs[index].MeshPlayer = mesh;
            _playerConfigs[index].MeshIndex = intMesh;
            _playerConfigs[index].NumPlayer = _playerConfigs[index].PlayerIndex;
        }

        /// <summary>
        /// Marks a player as ready and checks if all players are ready to transition to the next scene.
        /// </summary>
        /// <param name="index">The index of the player to mark as ready.</param>
        public void ReadyPlayer(int index)
        {
            _playerConfigs[index].IsReady = true;
            if (_playerConfigs.Count == _maxPlayer && _playerConfigs.All(p => p.IsReady == true))
            {
                StartCoroutine(Transition());
            }
        }

        /// <summary>
        /// Handles a player joining the game, adding them to the configuration list if there is space.
        /// </summary>
        /// <param name="pi">The PlayerInput of the joining player.</param>
        public void HandlePlayerJoin(PlayerInput pi)
        {
            if (_playerConfigs.Count < _maxPlayer)
            {
                Debug.Log("Player Joined " + pi.playerIndex);

                if (_playerConfigs.All(p => p.PlayerIndex != pi.playerIndex))
                {
                    pi.transform.SetParent(transform);
                    _playerConfigs.Add(new PlayerConfiguration(pi));
                }
            }
            else
            {
                Debug.Log("Max Player");
            }
        }

        /// <summary>
        /// Handles the transition effect, fading out the music and transitioning to the next scene.
        /// </summary>
        /// <returns>An IEnumerator for the coroutine.</returns>
        IEnumerator Transition()
        {
            MusicPlayer.Instance.AudioSource.DOFade(0, 3);
            yield return new WaitForSeconds(1);
            _fader.LaunchFadeOutLobby();
        }

        #endregion
    }

    /// <summary>
    /// Represents a player's configuration, including their input, mesh, and readiness status.
    /// </summary>
    public class PlayerConfiguration
    {
        public PlayerConfiguration(PlayerInput pi)
        {
            PlayerIndex = pi.playerIndex;
            Input = pi;
        }

        public PlayerInput Input { get; set; }
        public int PlayerIndex { get; set; }
        public bool IsReady { get; set; }
        public GameObject MeshPlayer { get; set; }
        public int MeshIndex { get; set; }
        public int NumPlayer { get; set; }
    }
}