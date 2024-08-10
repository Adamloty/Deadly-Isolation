using UnityEngine;

public class Chunk : MonoBehaviour
{
    public Vector2Int chunkCoordinate; // إحداثيات الـ chunk
    public bool isActive; // للتحقق من تفعيل الـ chunk أو تعطيله

    [SerializeField]
    private Terrain terrain; // نجعل المتغير private للحفاظ على الإخفاء، لكن يمكن الوصول إليه من Unity Editor

    // القيم القابلة للتعديل من Unity Editor
    [Header("Terrain Settings")]
    public int chunkSize = 16; // حجم الـ chunk
    public float terrainHeight = 100f; // ارتفاع التضاريس
    public float noiseScale = 5f; // مقياس Noise

    [Header("Feature Settings")]
    public float mountainThreshold = 0.6f; // عتبة لتحديد الجبال
    public float seaLevel = 0.1f; // عتبة لتحديد مستوى البحر
    public GameObject treePrefab; // Prefab للشجرة
    public int forestDensity = 10; // كثافة الغابات (عدد الأشجار لكل chunk)

    public void Initialize(Vector2Int coordinate)
    {
        chunkCoordinate = coordinate;
        gameObject.name = $"Chunk ({coordinate.x}, {coordinate.y})";

        CreateTerrain();
        AddFeatures();
    }

    private void CreateTerrain()
    {
        TerrainData terrainData = new TerrainData();
        terrainData.size = new Vector3(chunkSize, terrainHeight, chunkSize); // تعيين حجم الـ Terrain

        terrain = Terrain.CreateTerrainGameObject(terrainData).GetComponent<Terrain>();
        terrain.transform.SetParent(transform);
        terrain.transform.localPosition = Vector3.zero;

        // توليد التضاريس باستخدام القيم القابلة للتعديل
        GenerateTerrainData(terrainData);
    }

    private void GenerateTerrainData(TerrainData terrainData)
    {
        int resolution = 129;
        terrainData.heightmapResolution = resolution;

        float[,] heights = new float[resolution, resolution];
        for (int x = 0; x < resolution; x++)
        {
            for (int z = 0; z < resolution; z++)
            {
                float xCoord = (float)x / resolution * noiseScale;
                float zCoord = (float)z / resolution * noiseScale;

                float height = Mathf.PerlinNoise(xCoord, zCoord) * 0.2f;

                // إضافة جبال بناءً على عتبة معينة
                if (height > mountainThreshold)
                {
                    height += Mathf.PerlinNoise(xCoord * 0.5f, zCoord * 0.5f) * 0.3f;
                }

                // إضافة بحر بناءً على عتبة منخفضة
                if (height < seaLevel)
                {
                    height = seaLevel;
                }

                heights[x, z] = height;
            }
        }

        terrainData.SetHeights(0, 0, heights);
    }

    private void AddFeatures()
    {
        // إضافة غابات عشوائية
        AddForests();
    }

    private void AddForests()
    {
        if (treePrefab == null) return;

        TerrainData terrainData = terrain.terrainData;
        float[,] heights = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);

        for (int i = 0; i < forestDensity; i++)
        {
            float x = Random.Range(0, terrainData.heightmapResolution);
            float z = Random.Range(0, terrainData.heightmapResolution);
            float height = heights[(int)x, (int)z];

            // تحديد ما إذا كانت الشجرة يجب أن توضع بناءً على مستوى الارتفاع
            if (height > seaLevel)
            {
                GameObject tree = Instantiate(treePrefab);
                tree.transform.position = new Vector3(x, height * terrainHeight, z);
                tree.transform.SetParent(terrain.transform);
            }
        }
    }

    public void SetActiveChunk(bool active)
    {
        isActive = active;
        gameObject.SetActive(active);
    }
}
