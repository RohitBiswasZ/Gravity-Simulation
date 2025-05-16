using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ProcedrualPlanet : MonoBehaviour
{
    [Header("Mesh Config")]
    
    [Range(1f, 20f)] public int resulation;
    [Range(1f, 10f)] public int radius;
    
    private MeshData mData;
    
    [Header("Debug")]
    
    [Range(0.01f,1f)] public float vertexRadius;
    [Min(0)] public float vertexAnimateTime;
    public Color vertexColor;
    [FormerlySerializedAs("BoxOutlineColor")] [FormerlySerializedAs("BoxOutline")] public Color boxOutlineColor;
    List<Vector3> aniVertex = new List<Vector3>();
    
    public void Start()
    {
        Init();
    }

    public void Init()
    {
        mData = new MeshData();
        mData.mesh = new Mesh();

        Vector3 calOffset = new Vector3(radius / 2f, radius / 2f, radius / 2f);
        mData.CreateCubicFace(resulation, radius, -calOffset);
        mData.UpdateMesh();
        
        StopAllCoroutines();
        StartCoroutine(UpdateAnimatedVertex(mData.vertex, vertexAnimateTime));
    }

    public void OnValidate()
    {
        if (!Application.isPlaying) return;
        mData = new MeshData();
        mData.mesh = new Mesh();

        Vector3 calOffset = new Vector3(radius / 2f, radius / 2f, radius / 2f);
        mData.CreateCubicFace(resulation, radius, -calOffset);
        
        aniVertex.Clear();
        StopAllCoroutines();
        StartCoroutine(UpdateAnimatedVertex(mData.vertex, vertexAnimateTime));
    }

    IEnumerator UpdateAnimatedVertex(List<Vector3> vertexList, float t)
    {
        for (int i = 0; i < vertexList.Count; i++)
        {
            Vector3 vertex = vertexList[i];
            aniVertex.Add(vertex);
            yield return new WaitForSeconds(t);
        }
    }

    public void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        if (mData.vertex.Count > 0 && mData.vertex != null && mData != null)
        {
            Gizmos.color = boxOutlineColor;
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one * radius);
            
            float cordStep = (float)radius / resulation;
            Gizmos.color = vertexColor;
            
            for (int i = 0; i < aniVertex.Count; i++)
            {
                Vector3 position = aniVertex[i];
                Gizmos.DrawSphere(position, vertexRadius * cordStep);
            }
            
        }
    }
}

public class MeshData
{
    public Mesh mesh;
    public List<Vector3> vertex;

    public MeshData()
    {
        vertex = new List<Vector3>();
    }

    public void UpdateMesh()
    {
        mesh = new Mesh();
        mesh.vertices = vertex.ToArray();
        
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }
    
    public void CreateFaceBack(int resulation, int radius, Vector3 offset)
    {
        float posStep = (float)radius / resulation;

        for (int y = 0; y <= resulation; y++)
        for (int x = 0; x <= resulation; x++)
        {
            float calculateX = x * posStep + offset.x;
            float calculateY = y * posStep + offset.y;
            float calculateZ = 0 + offset.z;
            
            Vector3 position = new Vector3(calculateX, calculateY, calculateZ);
            vertex.Add(position);
        }
    }

    public void CreateFaceFront(int resulation, int radius, Vector3 offset)
    {
        float posStep = (float)radius / resulation;

        for (int y = 0; y <= resulation; y++)
        for (int x = 0; x <= resulation; x++)
        {
            float calculateX = x * posStep + offset.x;
            float calculateY = y * posStep + offset.y;
            float calculateZ = radius + offset.z;
            
            Vector3 position = new Vector3(calculateX, calculateY, calculateZ);
            vertex.Add(position);
        }
    }
    
    public void CreateFaceUpper(int resulation, int radius, Vector3 offset)
    {
        float posStep = (float)radius / resulation;

        for (int z = 0; z <= resulation; z++)
        for (int x = 0; x <= resulation; x++)
        {
            float calculateX = x * posStep + offset.x;
            float calculateZ = z * posStep + offset.z;
            float calculateY = radius + offset.y;
            
            Vector3 position = new Vector3(calculateX, calculateY, calculateZ);
            vertex.Add(position);
        }
    }
    
    public void CreateFaceBottom(int resulation, int radius, Vector3 offset)
    {
        float posStep = (float)radius / resulation;

        for (int z = 0; z <= resulation; z++)
        for (int x = 0; x <= resulation; x++)
        {
            float calculateX = x * posStep + offset.x;
            float calculateZ = z * posStep + offset.z;
            float calculateY = 0 + offset.y;
            
            Vector3 position = new Vector3(calculateX, calculateY, calculateZ);
            vertex.Add(position);
        }
    }
    
    public void CreateFaceRight(int resulation, int radius, Vector3 offset)
    {
        float posStep = (float)radius / resulation;

        for (int y = 0; y <= resulation; y++)
        for (int z = 0; z <= resulation; z++)
        {
            float calculateZ = z * posStep + offset.z;
            float calculateY = y * posStep + offset.y;
            float calculateX = radius + offset.x;
            
            Vector3 position = new Vector3(calculateX, calculateY, calculateZ);
            vertex.Add(position);
        }
    }
    
    public void CreateFaceLeft(int resulation, int radius, Vector3 offset)
    {
        float posStep = (float)radius / resulation;

        for (int y = 0; y <= resulation; y++)
        for (int z = 0; z <= resulation; z++)
        {
            float calculateZ = z * posStep + offset.z;
            float calculateY = y * posStep + offset.y;
            float calculateX = 0 + offset.x;
            
            Vector3 position = new Vector3(calculateX, calculateY, calculateZ);
            vertex.Add(position);
        }
    }


    public void CreateCubicFace(int resulation, int radius, Vector3 offset)
    {
        if (vertex.Count > 0) vertex.Clear();
        
        CreateFaceUpper(resulation, radius, offset);
        CreateFaceBottom(resulation, radius, offset);
        CreateFaceRight(resulation, radius, offset);
        CreateFaceLeft(resulation, radius, offset);
        CreateFaceFront(resulation, radius, offset);
        CreateFaceBack(resulation, radius, offset);
    }
}