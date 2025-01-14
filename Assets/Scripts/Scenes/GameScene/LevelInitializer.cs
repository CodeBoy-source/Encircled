using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
  [SerializeField]
  private List<Transform> PlayerSpawns;

  [SerializeField]
  private GameObject playerPrefab;
  // Start is called before the first frame update
  void Start()
  {
    var playerConfigurationManager = PlayerConfigurationManager.Instance;
    if (playerConfigurationManager){
      var playerConfigs = playerConfigurationManager.GetPlayerConfigs().ToArray();
      var Count = PlayerSpawns.Count; 
      for (int i = 0; i < playerConfigs.Length; i++)
      {
        var index = i % Count;
        var player = Instantiate(playerPrefab, PlayerSpawns[index].position, PlayerSpawns[index].rotation, gameObject.transform);
        playerConfigs[index].InitializePlayer(player);
        player.GetComponent<PlayerMovement>().InitializePlayer(playerConfigs[index]);
      }
    }

  }
}
