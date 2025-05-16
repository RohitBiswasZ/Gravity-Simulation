using System.Collections.Generic;
using UnityEngine;

public class ProcedrualPlanet : MonoBehaviour
{
    [Header("Mesh Config")]
    
    [Range(1f, 20f)] public int resulation;
    [Range(1f, 10f)] public int radius;
    
    private MeshData mData;
    
    [Header("Debug")]
    
    [Range(0.01f,1f)] public float vertexRadius;
    
    public void Start()
    {
        Init();
    }

    public void Init()
    {
        mData = new MeshData();
        mData.mesh = new Mesh();

        mData.CreateCubicFace(resulation, radius, Vector3.zero);
        mData.UpdateMesh();
    }

    public void OnValidate()
    {
        if (!Application.isPlaying) return;
        mData = new MeshData();
        mData.mesh = new Mesh();

        mData.CreateCubicFace(resulation, radius, Vector3.zero);
        mData.UpdateMesh();
    }

    public void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        if (mData.vertex.Count > 0 && mData.vertex != null && mData != null)
        {
            float cordStep = (float)radius / resulation;
            Gizmos.color = Color.black;
            
            for (int i = 0; i < mData.vertex.Count; i++)
            {
                Vector3 position = mData.vertex[i];
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