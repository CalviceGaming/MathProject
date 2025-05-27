using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMeshCreator : MonoBehaviour
{
    public Mesh mesh;

    [SerializeField] private float fuckedUpVariable;

    public Vector3[] vertices;
    public int[] triangles;

    public Vector3[] verticesInitialPos;
    public Vector3[] verticesEatInitialPos;
    
    
    
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
        for(int i = 0; i < verticesInitialPos.Length; i++)
        {
            //verticesInitialPos[i] = RotateZ(verticesInitialPos[i], 0.03f);
        }
        
        //EatApple();
        //SnakeLikeMovement();
        BezierCurveMovement();
        UpdateMesh();
                                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                //Fucked Up Statement
                                                                                                                                                                                                                                                                                                                                                                                                                                            }
    }
    
    Vector3 CubicBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 point = uuu * p0;
        point += 3 * uu * t * p1;
        point += 3 * u * tt * p2;
        point += ttt * p3;

        return point;
    }
    
    void BezierCurveMovement() 
    {
        Vector3 p0 = new Vector3(-0.5f, 0, 0); 
        Vector3 p1 = new Vector3(-1f, 0f, 0); 
        Vector3 p2 = new Vector3(1f, 0f, 0); 
        Vector3 p3 = new Vector3(0.5f, 0, 0); 

        float t = Time.time * 0.5f; 

        for (int i = 0; i < vertices.Length; i++) 
        { 
            int chunk = (i / 4) + 1; 
            float localT = Mathf.PingPong(t + chunk * 0.05f, 1); 
            Vector3 offset = CubicBezier(p0, p1, p2, p3, localT); 
            vertices[i] = verticesEatInitialPos[i] + new Vector3(offset.x, offset.y, 0); 
        }
    }

    Vector3 RotateX(Vector3 vertex, float angle)
    {
        float cosTheta = Mathf.Cos(angle);
        float sinTheta = Mathf.Sin(angle);
        
        float newY = vertex.y * cosTheta - vertex.z * sinTheta;
        float newZ = vertex.y * sinTheta + vertex.z * cosTheta;
        
        return new Vector3(vertex.x, newY, newZ);
    }
    
    Vector3 RotateZ(Vector3 vertex, float angle)
    {
        float cosTheta = Mathf.Cos(angle);
        float sinTheta = Mathf.Sin(angle);
        
        float newX = vertex.x * cosTheta - vertex.y * sinTheta;
        float newY = vertex.x * sinTheta + vertex.y * cosTheta;
        
        return new Vector3(newX, newY, vertex.z);
    }

    void EatApple()
    {
        for (int i = 0; i < verticesInitialPos.Length; i++)
        {
            int chunk = (i / 4) + 1 ;
            float newX = verticesInitialPos[i].x * (1 + Mathf.Abs(Mathf.Sin(chunk * 10 + Time.time * 30 * Mathf.Deg2Rad)));
            float newY = verticesInitialPos[i].y * (1 + Mathf.Abs(Mathf.Sin(chunk * 10 + Time.time * 30 * Mathf.Deg2Rad)));
            verticesEatInitialPos[i] = new Vector3(newX, newY, verticesInitialPos[i].z);
        }
    }

    void CreateMesh()
    {
        List<Vector3> startingVertices = new List<Vector3>();
        
        startingVertices.Add(new Vector3(-0.5f, 0.5f, -0.5f)); //0
        startingVertices.Add(new Vector3(0.5f, 0.5f, -0.5f));//1
        startingVertices.Add(new Vector3(-0.5f, -0.5f, -0.5f));//2
        startingVertices.Add(new Vector3(0.5f, -0.5f, -0.5f));//3
        startingVertices.Add(new Vector3(-0.5f, 0.5f, 0));//4
        startingVertices.Add(new Vector3(0.5f, 0.5f, 0));//5
        startingVertices.Add( new Vector3(-0.5f, -0.5f, 0));//6
        startingVertices.Add(new Vector3(0.5f, -0.5f, 0));//7

        List<int> startingTriangles = new List<int>
        {
            0, 1, 2,
            1, 3, 2,
            1, 5, 3,
            5, 7, 3,
            0, 4, 5,
            0, 5, 1,
            0, 6, 4,
            0, 2, 6,
            4, 6, 5,
            6, 7, 5,
            2, 3, 7,
            2, 7, 6 
        };

        for (int y = 0; y < 20; y++)
        {
            int amount = 8 + 4 * y;
            for (int i = amount - 4; i < amount; i++)
            {
                startingVertices.Add(startingVertices[i] + new Vector3( 0, 0, 0.5f)); //0.5f*Mathf.Cos(y*30 * Mathf.Deg2Rad)
            }
        
            int amount2 = 36 + 30 * y;
            for (int i = amount2 - 30; i < amount2; i++)
            {
                startingTriangles.Add(startingTriangles[i] + 4);
            }
        }
        
        vertices = startingVertices.ToArray();
        triangles = startingTriangles.ToArray();
        for (int i = 0; i < startingVertices.Count; i++)
        {
            
        }

        verticesInitialPos = vertices.Clone() as Vector3[];
        verticesEatInitialPos = vertices.Clone() as Vector3[];
    }

    void UpdateMesh()
    {
        mesh.Clear();
        
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        
        mesh.RecalculateNormals();
    }

    void SnakeLikeMovement()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            int chunk = (i / 4) + 1 ;
            float startCos = chunk * 30 * Mathf.Deg2Rad;
             float diramount = fuckedUpVariable * Mathf.Cos(startCos + (Time.time * 90 * Mathf.Deg2Rad));
            //float diramount = (Mathf.Cos(startCos)) * Mathf.Sin((Time.time * 90 * Mathf.Deg2Rad));
            vertices[i] = verticesEatInitialPos[i] + new Vector3(1*diramount, 0, 0);
        }
    }
}
