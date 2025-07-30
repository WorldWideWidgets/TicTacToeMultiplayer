using System;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;

public class GameVisualManager : NetworkBehaviour
{
    private const float GRID_SIZE = 3.1f;

    [SerializeField] private Transform crossPrefab;
    [SerializeField] private Transform circlePrefab;

    private void Start()
    {
        GameManager.Instance.OnClickedOnGridPosition += GameManager_OnClickedOnGridPosition;
    }

    private void GameManager_OnClickedOnGridPosition(object sender, GameManager.OnClickedOnGridPositionEventArgs e)
    {
        Debug.Log("GameManager_OnClickedOnGridPosition");
        SpawnObjectRpc(e.x, e.y, e.playerType);

    }

    [Rpc(SendTo.Server)]
    private void SpawnObjectRpc(int x, int y, GameManager.PlayerType playerType)
    {
        Transform prefab;
        Debug.Log("PlayerType: " + playerType);
        switch (playerType)
        {
            default:
            case GameManager.PlayerType.Cross:
                Debug.Log("Spawn Cross");
                prefab = crossPrefab;
                break;
            case GameManager.PlayerType.Circle:
                Debug.Log("Spawn Circle");
                prefab = circlePrefab;
                break;

        }
        Transform spawnedCrossTransform = Instantiate(prefab, GetGridWorldPosition(x, y), Quaternion.identity);

        spawnedCrossTransform.GetComponent<NetworkObject>().Spawn(true);
    }


    private Vector2 GetGridWorldPosition(int x, int y)
    {
        return new Vector2(-GRID_SIZE + (x * GRID_SIZE), -GRID_SIZE + (y * GRID_SIZE));
    }
}
