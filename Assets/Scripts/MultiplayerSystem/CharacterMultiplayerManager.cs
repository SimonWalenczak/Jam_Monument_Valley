using UnityEngine;

public class CharacterMultiplayerManager : MonoBehaviour
{
    #region Properties

    [field: SerializeField] public CharacterManager CharacterManager { get; private set; }
    [field: SerializeField] public InputManagement InputManagement { get; private set; }

    #endregion
}