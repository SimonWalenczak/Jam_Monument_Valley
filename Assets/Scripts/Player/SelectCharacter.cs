using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    #region Properties

    [field: SerializeField] public Image CharacterImage { get; private set; }
    [field: SerializeField] public Sprite[] CharacterSprites { get; private set; }

    [SerializeField] private int selectedCharacterIndex = 0;

    [Header("Selection")]
    [SerializeField] private Image _titlePlayer;
    [SerializeField] private List<Sprite> _titlePlayerSprites;
    [SerializeField] private List<GameObject> _meshCharacterPlayers;
    [SerializeField] private GameObject _actualMesh;
    [SerializeField] private GameObject _readyPanel;
    [SerializeField] private GameObject _menuPanel;

    private int _playerIndex;

    #endregion

    #region Methods

    private void Start()
    {
        _actualMesh = _meshCharacterPlayers[0];
    }

    public void SetPlayerIndex(int pi)
    {
        _playerIndex = pi;
        _titlePlayer.sprite = _titlePlayerSprites[_playerIndex];
    }

    public void SelectNextCharacter()
    {
        if (selectedCharacterIndex < CharacterSprites.Length - 1)
            selectedCharacterIndex = selectedCharacterIndex + 1;
        else
            selectedCharacterIndex = 0;

        UpdateCharacterSprite();
    }

    public void SelectPreviousCharacter()
    {
        if (selectedCharacterIndex > 0)
            selectedCharacterIndex = selectedCharacterIndex - 1;
        else
            selectedCharacterIndex = CharacterSprites.Length - 1;

        UpdateCharacterSprite();
    }

    void UpdateCharacterSprite()
    {
        CharacterImage.sprite = CharacterSprites[selectedCharacterIndex];
        _actualMesh = _meshCharacterPlayers[selectedCharacterIndex];
    }

    public void SetMesh()
    {
        PlayerConfigurationManager.Instance.SetPlayerMesh(_playerIndex, _actualMesh, selectedCharacterIndex);
        PlayerConfigurationManager.Instance.ReadyPlayer(_playerIndex);
        _menuPanel.SetActive(false);
        _readyPanel.SetActive(true);
    }

    #endregion
}