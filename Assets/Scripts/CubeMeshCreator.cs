using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMeshCreator : MonoBehaviour
{
    public Mesh mesh;

    public Vector3[] vertices;
    public int[] triangles;
    
    
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateMesh();
        UpdateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < vertices.Length; i++)
        {
            //vertices[i] = RotateX(vertices[i], 0.03f);
        }
        UpdateMesh();
    }

    Vector3 RotateX(Vector3 vertex, float angle)
    {
        float cosTheta = Mathf.Cos(angle);
        float sinTheta = Mathf.Sin(angle);
        
        float newY = vertex.y * cosTheta - vertex.z * sinTheta;
        float newZ = vertex.y * sinTheta + vertex.z * cosTheta;
        
        return new Vector3(vertex.x, newY, newZ);
    }

    void CreateMesh()
    {
        List<Vector3> startingVertices = new List<Vector3>();
        
        startingVertices.Add(new Vector3(-0.5f, 0.5f, -0.5f)); //0
        startingVertices.Add(new Vector3(0.5f, 0.5f, -0.5f));//1
        startingVertices.Add(new Vector3(-0.5f, -0.5f, -0.5f));//2
        startingVertices.Add(new Vector3(0.5f, -0.5f, -0.5f));//3
        startingVertices.Add(new Vector3(0.5f, 0.5f, 0.5f));//4
        startingVertices.Add(new Vector3(0.5f, -0.5f, 0.5f));//5
        startingVertices.Add( new Vector3(-0.5f, 0.5f, 0.5f));//6
        startingVertices.Add(new Vector3(-0.5f, -0.5f, 0.5f));//7

        List<int> startingTriangles = new List<int>
        {
            0, 1, 2,
            1, 3, 2,
            1, 4, 3,
            4, 5, 3,
            0, 6, 4,
            0, 4, 1,
            0, 7, 6,
            0, 2, 7,
            6, 7, 4,
            7, 5, 4,
            2, 3, 5,
            2, 5, 7
        };

        for (int y = 1; y <= 5; y++)
        {
            int amount = 8 * y;
            for (int i = amount - 8; i < amount; i++)
            {
                startingVertices.Add(startingVertices[i] + new Vector3(0, 0, 1));
            }
        
            int amount2 = 36 * y;
            for (int i = amount2 - 36; i < amount2; i++)
            {
                startingTriangles.Add(startingTriangles[i] + 8);
            }
        }

        
        vertices = startingVertices.ToArray();
        triangles = startingTriangles.ToArray();
        for (int i = 0; i < startingVertices.Count; i++)
        {
            
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        
        mesh.RecalculateNormals();
    }
}
