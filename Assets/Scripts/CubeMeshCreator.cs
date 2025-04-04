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
            vertices[i] = RotateX(vertices[i], 0.03f);
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
        vertices = new Vector3[]
        {
            new Vector3(-0.5f, 0.5f, -0.5f), //0
            new Vector3(0.5f, 0.5f, -0.5f),//1
            new Vector3(-0.5f, -0.5f, -0.5f),//2
            new Vector3(0.5f, -0.5f, -0.5f),//3
            new Vector3(0.5f, 0.5f, 0.5f),//4
            new Vector3(0.5f, -0.5f, 0.5f),//5
            new Vector3(-0.5f, 0.5f, 0.5f),//6
            new Vector3(-0.5f, -0.5f, 0.5f),//7
        };

        triangles = new int[]
        {
            0, 1, 2,
            1,3,2,
            1,4,3,
            4,5,3,
            0,6,4,
            0,4,1,
            0,7,6,
            0,2,7,
            6,7,4,
            7,5,4,
            2,3,5,
            2,5,7
        };
    }

    void UpdateMesh()
    {
        mesh.Clear();
        
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        
        mesh.RecalculateNormals();
    }
}
