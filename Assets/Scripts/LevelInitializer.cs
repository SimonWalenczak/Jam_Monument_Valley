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

            Vector3 eulerAngles = new Vector3(0, 90, 0);
            Quaternion quaternion = Quaternion.Euler(eulerAngles);
            
            GameObject characterPlayer = Instantiate(playerConfigs[i].MeshPlayer, placeForCharacterPlayer.position, quaternion,
                placeForCharacterPlayer.transform);
        }
    }
}