using System.Collections.Generic;
using MultiplayerSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    /// <summary>
    /// Manages the selection and customization of the player's character.
    /// Allows for selecting the next or previous character and setting the character's mesh for gameplay.
    /// </summary>
    public class SelectCharacter : MonoBehaviour
    {
        #region Properties

        [field: SerializeField] public Image CharacterImage { get; private set; }
        [field: SerializeField] public Sprite[] CharacterSprites { get; private set; }

        [SerializeField] private int _selectedCharacterIndex = 0;

        [Header("Selection")]
        [SerializeField] private Image _playerTitleImage;
        [SerializeField] private List<Sprite> _playerTitleSprites;
        [SerializeField] private List<GameObject> _characterMeshList;
        [SerializeField] private GameObject _currentCharacterMesh;
        [SerializeField] private GameObject _readyPanel;
        [SerializeField] private GameObject _menuPanel;

        private int _playerIndex;

        #endregion

        #region Methods

        private void Start()
        {
            _currentCharacterMesh = _characterMeshList[0];
        }

        /// <summary>
        /// Sets the player index and updates the player title image based on the index.
        /// </summary>
        /// <param name="playerIndex">The index of the player.</param>
        public void SetPlayerIndex(int pi)
        {
            _playerIndex = pi;
            _playerTitleImage.sprite = _playerTitleSprites[_playerIndex];
        }

        /// <summary>
        /// Selects the next character in the list. Loops back to the first character if the end is reached.
        /// </summary>
        public void SelectNextCharacter()
        {
            if (_selectedCharacterIndex < CharacterSprites.Length - 1)
                _selectedCharacterIndex = _selectedCharacterIndex + 1;
            else
                _selectedCharacterIndex = 0;

            UpdateCharacterSprite();
        }

        /// <summary>
        /// Selects the previous character in the list. Loops back to the last character if the beginning is reached.
        /// </summary>
        public void SelectPreviousCharacter()
        {
            if (_selectedCharacterIndex > 0)
                _selectedCharacterIndex = _selectedCharacterIndex - 1;
            else
                _selectedCharacterIndex = CharacterSprites.Length - 1;

            UpdateCharacterSprite();
        }

        /// <summary>
        /// Updates the character sprite and mesh based on the selected character index.
        /// </summary>
        void UpdateCharacterSprite()
        {
            CharacterImage.sprite = CharacterSprites[_selectedCharacterIndex];
            _currentCharacterMesh = _characterMeshList[_selectedCharacterIndex];
        }

        
        /// <summary>
        /// Sets the character mesh for the player and moves to the ready panel.
        /// </summary>
        public void SetMesh()
        {
            PlayerConfigurationManager.Instance.SetPlayerMesh(_playerIndex, _currentCharacterMesh, _selectedCharacterIndex);
            PlayerConfigurationManager.Instance.ReadyPlayer(_playerIndex);
            _menuPanel.SetActive(false);
            _readyPanel.SetActive(true);
        }

        #endregion
    }
}