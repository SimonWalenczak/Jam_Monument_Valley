using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

namespace MultiplayerSystem
{
    /// <summary>
    /// Manages the setup menu for player configuration, including spawning the setup menu and assigning the correct player input module.
    /// </summary>
    public class SpawnPlayerSetupMenu : MonoBehaviour
    {
        #region Properties

        [field: SerializeField] public GameObject PlayerSetupMenuPrefab { get; private set; }
        [field: SerializeField] public PlayerInput Input { get; private set; }

        #endregion

        #region Methods

        private void Awake()
        {
            var rootMenu = GameObject.Find("MainLayout");
            if (rootMenu == null) return;
            
            var menu = Instantiate(PlayerSetupMenuPrefab, rootMenu.transform);
            Input.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();

            menu.GetComponent<SelectCharacter>().SetPlayerIndex(Input.playerIndex);
        }

        #endregion
    }
}