using UnityEngine;
using System.Collections.Generic;

public class ProceduralMesh : MonoBehaviour
{
    public int pointsPerChunk = 100; // عدد النقاط لكل Chunk
    public int chunkSize = 16; // حجم الـ Chunk
    public float maxHeight = 5f; // أقصى ارتفاع للنقاط
    public Vector2Int chunkCoordinate; // إحداثيات الـع Chunk

    private Mesh mesh;
    private List<Vector3> vertices;
    private List<int> triangles;

    public void Initialize(Vector2Int coord)
    {
        chunkCoordinate = coord;
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GenerateMesh();
    }

    void GenerateMesh()
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        // توليد النقاط بشكل عشوائي
        for (int i = 0; i < pointsPerChunk; i++)
        {
            float x = Random.Range(0, chunkSize);
            float z = Random.Range(0, chunkSize);
            float y = Random.Range(0, maxHeight);

            vertices.Add(new Vector3(x, y, z));
        }

        // توليد المثلثات
        for (int i = 0; i < pointsPerChunk - 2; i += 3)
        {
            triangles.Add(i);
            triangles.Add(i + 1);
            triangles.Add(i + 2);
        }

        // تطبيق النقاط والمثلثات على الـ Mesh
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals(); // إعادة حساب الاتجاهات العادية للحصول على إضاءة صحيحة
    }

    void OnDrawGizmos()
    {
        if (vertices == null) return;

        Gizmos.color = Color.red;
        foreach (var vertex in vertices)
        {
            Gizmos.DrawSphere(transform.position + vertex, 0.1f);
        }
    }
}
