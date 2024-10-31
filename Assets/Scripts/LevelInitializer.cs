using UnityEngine;
using Character;

public class InitializeLevel : MonoBehaviour
{
    [SerializeField] private Transform[] _playerSpawn;
    [SerializeField] private CharacterMultiplayerManager _playerPrefab;

    private void Start()
    {
        var playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            CharacterMultiplayerManager player = Instantiate(_playerPrefab, _playerSpawn[i].position, _playerSpawn[i].rotation,
                gameObject.transform);
            player.GetComponentInChildren<InputManagement>().InitializePlayer(playerConfigs[i]);

            Transform placeForCharacterPlayer = player.transform;
            
            GameObject characterPlayer = Instantiate(playerConfigs[i].MeshPlayer, placeForCharacterPlayer.position, Quaternion.identity,
                placeForCharacterPlayer.transform);
        }
    }
}