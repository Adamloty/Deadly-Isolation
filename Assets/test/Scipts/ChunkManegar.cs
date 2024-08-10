using UnityEngine;
using System.Collections.Generic;

public class ChunkManegar : MonoBehaviour
{
    public GameObject chunkPrefab; // Prefab للـ Chunk
    public int chunkSize = 16; // حجم الـ Chunk
    public int viewDistance = 3; // عدد الـ Chunks التي يجب تفعيلها حول اللاعب

    private Dictionary<Vector2Int, GameObject> chunks = new Dictionary<Vector2Int, GameObject>();
    private Transform player;

    void Start()
    {
        player = Camera.main.transform; // استخدام الكاميرا كلاعب
        UpdateChunks();
    }

    void Update()
    {
        UpdateChunks();
    }

    void UpdateChunks()
    {
        Vector2Int playerChunkCoord = GetChunkCoordinate(player.position);

        // تفعيل أو إخفاء الـ Chunks بناءً على موقع اللاعب
        foreach (var chunkCoord in chunks.Keys)
        {
            GameObject chunk = chunks[chunkCoord];
            bool shouldBeActive = ShouldChunkBeActive(playerChunkCoord, chunkCoord);
            chunk.SetActive(shouldBeActive);
        }

        // توليد الـ Chunks الجديدة في نطاق الرؤية
        for (int x = -viewDistance; x <= viewDistance; x++)
        {
            for (int z = -viewDistance; z <= viewDistance; z++)
            {
                Vector2Int newChunkCoord = new Vector2Int(playerChunkCoord.x + x, playerChunkCoord.y + z);
                if (!chunks.ContainsKey(newChunkCoord))
                {
                    CreateChunk(newChunkCoord);
                }
            }
        }
    }

    Vector2Int GetChunkCoordinate(Vector3 position)
    {
        int x = Mathf.FloorToInt(position.x / chunkSize);
        int z = Mathf.FloorToInt(position.z / chunkSize);
        return new Vector2Int(x, z);
    }

    bool ShouldChunkBeActive(Vector2Int playerChunkCoord, Vector2Int chunkCoord)
    {
        int distanceX = Mathf.Abs(playerChunkCoord.x - chunkCoord.x);
        int distanceZ = Mathf.Abs(playerChunkCoord.y - chunkCoord.y);
        return distanceX <= viewDistance && distanceZ <= viewDistance;
    }

    void CreateChunk(Vector2Int coord)
    {
        Vector3 position = new Vector3(coord.x * chunkSize, 0, coord.y * chunkSize);
        GameObject newChunk = Instantiate(chunkPrefab, position, Quaternion.identity);
        newChunk.GetComponent<Chunk>().Initialize(coord);
        chunks.Add(coord, newChunk);
    }
}
