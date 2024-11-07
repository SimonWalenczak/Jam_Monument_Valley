using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class SpawnPlayerSetupMenu : MonoBehaviour
{
    #region Properties

    [field: SerializeField] public GameObject PlayerSetupMenuPrefab { get; private set; }
    [field: SerializeField] public PlayerInput input { get; private set; }

    #endregion

    #region Methods

    private void Awake()
    {
        var rootMenu = GameObject.Find("MainLayout");
        if (rootMenu != null)
        {
            var menu = Instantiate(PlayerSetupMenuPrefab, rootMenu.transform);
            input.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();

            menu.GetComponent<SelectCharacter>().SetPlayerIndex(input.playerIndex);
        }
    }

    #endregion
}