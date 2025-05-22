using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Controls the main gameplay
/// </summary>
public class GameController : MonoBehaviour
{
    [Tooltip("A reference to the tile we want to spawn")]
    public Transform tile;

    [Tooltip("A reference to the obstacle we want to spawn")]
    public Transform obstacle;

    [Tooltip("Where the first tile should be placed")]
    public Vector3 startPoint = new Vector3(0, 0, -5);

    [Tooltip("How many tiles should be created in advance")]
    [Range(1, 15)]
    public int initSpawnNum = 10;

    [Tooltip("How many tiles to spawn initially with no obstacles")]
    public int initNoObstacles = 4;

    /// <summary>
    /// Where the next tile should be spawned
    /// </summary>
    private Vector3 _nextTileLocation;

    /// <summary>
    /// How the next tile should be rotated
    /// </summary>
    private Quaternion _nextTileRotation;

    void Start()
    {
        // Set our starting point
        _nextTileLocation = startPoint;
        _nextTileRotation = Quaternion.identity;
        
        for(int i = 0; i < initSpawnNum; i++)
        {
            SpawnNextTile(i > initNoObstacles);
        }
    }
    
    /// <summary>
    /// Will spawn a tile at a certain location and set up the next position
    /// </summary>
    /// <param name="spawnObstacles">If we should spawn an obstacle</param>
    public void SpawnNextTile(bool spawnObstacles = true)
    {
        var newTile = Instantiate(tile, _nextTileLocation, _nextTileRotation);

        // Figure out where we should spawn the next item
        var nextTile = newTile.Find("Next Spawn Point");
        _nextTileLocation = nextTile.position;
        _nextTileRotation = nextTile.rotation;

        if (spawnObstacles)
        {
            SpawnObstacle(newTile);
        }
    }

    private void SpawnObstacle(Transform newTile)
    {
        // Get possible places to spawn obstacle
        var obstacleSpawnPoints = new List<GameObject>();
        
        foreach(Transform child in newTile)
        {
            if (child.CompareTag("ObstacleSpawn"))
            {
                obstacleSpawnPoints.Add(child.gameObject);
            }
        }

        if(obstacleSpawnPoints.Count > 0)
        {
            // Get a random object from the list
            var spawnPoint = obstacleSpawnPoints[Random.Range(0, obstacleSpawnPoints.Count)];

            // Store its position
            var spawnPos = spawnPoint.transform.position;

            // Create our obstacle
            var newObstacle = Instantiate(obstacle, spawnPos, Quaternion.identity);

            // Parent to the tile
            newObstacle.SetParent(spawnPoint.transform);
        }
    }
}
