using UnityEngine;
using Player;

namespace MultiplayerSystem
{
    /// <summary>
    /// Manages the multiplayer character, including player manager and input management.
    /// </summary>
    public class CharacterMultiplayerManager : MonoBehaviour
    {
        #region Properties

        [field: SerializeField] public PlayerManager PlayerManager { get; private set; }
        [field: SerializeField] public InputManagement InputManagement { get; private set; }

        #endregion
    }
}