using UnityEngine;
using System.Collections.Generic;

public class InfiniteWorld : MonoBehaviour
{
    [Header("Chunk Settings")]
    public GameObject chunkPrefab; // Prefab الخاص بالـ chunk
    public int chunkSize = 16; // حجم الـ chunk
    public int viewDistance = 2; // المسافة التي يمكن للاعب رؤيتها (بالـ chunks)

    private Dictionary<Vector2Int, Chunk> chunks = new Dictionary<Vector2Int, Chunk>();
    private Vector2Int currentChunkCoord;
    private Vector3 playerPosition;

    void Start()
    {
        UpdateChunks();
    }

    void Update()
    {
        playerPosition = transform.position;
        Vector2Int newChunkCoord = GetChunkCoordFromPosition(playerPosition);

        if (newChunkCoord != currentChunkCoord)
        {
            currentChunkCoord = newChunkCoord;
            UpdateChunks();
        }
    }

    Vector2Int GetChunkCoordFromPosition(Vector3 position)
    {
        int x = Mathf.FloorToInt(position.x / chunkSize);
        int z = Mathf.FloorToInt(position.z / chunkSize);
        return new Vector2Int(x, z);
    }

    void UpdateChunks()
    {
        List<Vector2Int> activeChunks = new List<Vector2Int>();

        for (int x = -viewDistance; x <= viewDistance; x++)
        {
            for (int z = -viewDistance; z <= viewDistance; z++)
            {
                Vector2Int chunkCoord = new Vector2Int(currentChunkCoord.x + x, currentChunkCoord.y + z);

                if (!chunks.ContainsKey(chunkCoord))
                {
                    CreateChunk(chunkCoord);
                }

                activeChunks.Add(chunkCoord);
            }
        }

        foreach (var chunk in chunks)
        {
            if (activeChunks.Contains(chunk.Key))
            {
                chunk.Value.SetActiveChunk(true);
            }
            else
            {
                chunk.Value.SetActiveChunk(false);
            }
        }
    }

    void CreateChunk(Vector2Int coord)
    {
        GameObject newChunkObject = Instantiate(chunkPrefab);
        newChunkObject.transform.position = new Vector3(coord.x * chunkSize, 0, coord.y * chunkSize);

        Chunk newChunk = newChunkObject.GetComponent<Chunk>();
        newChunk.Initialize(coord);

        chunks.Add(coord, newChunk);
    }
}
