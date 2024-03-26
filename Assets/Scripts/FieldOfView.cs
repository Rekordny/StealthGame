using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public Material VisionMaterial;
    public float VisionRange;
    public float VisionAngle;
    public LayerMask VisionObstructingLayer; //Layer of obstacles blocking vision
    public int VisionConeResolution = 120; //triangles shapes make up the vision cone, higer resolution, pretier mesh will be
    Mesh VisionConeMesh;
    MeshFilter VisionConeFilter;

    // Start is called before the first frame update
    void Start()
    {
        transform.AddComponent<MeshRenderer>().material = VisionMaterial;
        VisionConeFilter = transform.AddComponent<MeshFilter>();
        VisionConeMesh = new Mesh();
        VisionAngle *= Mathf.Deg2Rad;
    }

    // Update is called once per frame
    void Update()
    {
        DrawVisionCone();
    }

    void DrawVisionCone()//this method creates the vision cone mesh
    {
        int[] triangles = new int[(VisionConeResolution - 1) * 3];
        Vector3[] Vertices = new Vector3[VisionConeResolution + 1];
        Vertices[0] = Vector3.zero;
        float Currentangle = -VisionAngle / 2;
        float angleIcrement = VisionAngle / (VisionConeResolution - 1);
        float Sine;
        float Cosine;

        for (int i = 0; i < VisionConeResolution; i++)
        {
            Sine = Mathf.Sin(Currentangle);
            Cosine = Mathf.Cos(Currentangle);
            Vector3 RaycastDirection = (transform.forward * Cosine) + (transform.right * Sine);
            Vector3 VertForward = (Vector3.forward * Cosine) + (Vector3.right * Sine);
            if (Physics.Raycast(transform.position, RaycastDirection, out RaycastHit hit, VisionRange, VisionObstructingLayer))
            {
                Vertices[i + 1] = VertForward * hit.distance;
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    Debug.Log("FOUND YA!");
                    transform.parent.GetComponent<EnemyController>().ChasePlayer();
                }
            }
            else
            {
                Vertices[i + 1] = VertForward * VisionRange;
            }

            Currentangle += angleIcrement;
        }
        for (int i = 0, j = 0; i < triangles.Length; i += 3, j++)
        {
            triangles[i] = 0;
            triangles[i + 1] = j + 1;
            triangles[i + 2] = j + 2;
        }
        VisionConeMesh.Clear();
        VisionConeMesh.vertices = Vertices;
        VisionConeMesh.triangles = triangles;
        VisionConeFilter.mesh = VisionConeMesh;
    }
}
