using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using GameManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

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

    public List<PlayerConfiguration> GetPlayerConfigs()
    {
        return _playerConfigs;
    }

    public void SetPlayerMesh(int index, GameObject mesh, int intMesh)
    {
        _playerConfigs[index].MeshPlayer = mesh;
        _playerConfigs[index].MeshIndex = intMesh;
        _playerConfigs[index].NumPlayer = _playerConfigs[index].PlayerIndex;
    }

    public void ReadyPlayer(int index)
    {
        _playerConfigs[index].IsReady = true;
        if (_playerConfigs.Count == _maxPlayer && _playerConfigs.All(p => p.IsReady == true))
        {
            StartCoroutine(Transition());
        }
    }

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

    IEnumerator Transition()
    {
        MusicPlayer.Instance.AudioSource.DOFade(0, 3);
        yield return new WaitForSeconds(1);
        _fader.FadeOut();
    }

    #endregion
}

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