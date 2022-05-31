using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteAlways,RequireComponent(typeof(BoxCollider))]
public class GenLPG : MonoBehaviour
{
    public LightProbeGroup lpg;
    public float f;

    private void OnDrawGizmos()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();

        Bounds localBounds = new Bounds(boxCollider.center, boxCollider.size);

        const float radius = 0.1f;


        //Gizmos.color = Color.green;
        //Gizmos.DrawSphere(transform.TransformPoint(new Vector3(localBounds.min.x, localBounds.min.y, localBounds.min.z)), radius);
        //Gizmos.color = Color.red;
        //Gizmos.DrawSphere(transform.TransformPoint(new Vector3(localBounds.min.x, localBounds.min.y, localBounds.max.z)), radius);
        //Gizmos.color = Color.blue;
        //Gizmos.DrawSphere(transform.TransformPoint(new Vector3(localBounds.max.x, localBounds.min.y, localBounds.max.z)), radius);
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(transform.TransformPoint(new Vector3(localBounds.max.x, localBounds.min.y, localBounds.min.z)), radius);

        //Gizmos.DrawSphere(transform.TransformPoint(new Vector3(localBounds.min.x, -localBounds.min.y, localBounds.min.z)), radius);
        //Gizmos.DrawSphere(transform.TransformPoint(new Vector3(localBounds.min.x, -localBounds.min.y, localBounds.max.z)), radius);
        //Gizmos.DrawSphere(transform.TransformPoint(new Vector3(localBounds.max.x, -localBounds.min.y, localBounds.max.z)), radius);
        //Gizmos.DrawSphere(transform.TransformPoint(new Vector3(localBounds.max.x, -localBounds.min.y, localBounds.min.z)), radius);

        for (int x = 0; x < boxCollider.size.x + 0.5f; x++)
        {
            for (int y = 0; y < boxCollider.size.y + 0.5f; y++)
            {
                for (int z = 0; z < boxCollider.size.z + 0.5f; z++)
                {
                    Gizmos.DrawSphere(new Vector3(
                        x + transform.TransformPoint(new Vector3(localBounds.min.x, localBounds.min.y, localBounds.min.z)).x,
                        y + transform.TransformPoint(new Vector3(localBounds.min.x, localBounds.min.y, localBounds.min.z)).y,
                        z + transform.TransformPoint(new Vector3(localBounds.min.x, localBounds.min.y, localBounds.min.z)).z
                        ), radius);
                }
            }
           
        }

    }




    public void Gen()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();

        Bounds localBounds = new Bounds(boxCollider.center, boxCollider.size);
        List<Vector3> position = new List<Vector3>();


        for (int x = 0; x < boxCollider.size.x + 0.5f; x++)
        {
            for (int y = 0; y < boxCollider.size.y + 0.5f; y++)
            {
                for (int z = 0; z < boxCollider.size.z + 0.5f; z++)
                {
                    position.Add(new Vector3(
                        x + transform.TransformPoint(new Vector3(localBounds.min.x, localBounds.min.y, localBounds.min.z)).x,
                        y + transform.TransformPoint(new Vector3(localBounds.min.x, localBounds.min.y, localBounds.min.z)).y - transform.position.y,
                        z + transform.TransformPoint(new Vector3(localBounds.min.x, localBounds.min.y, localBounds.min.z)).z
                        ));
                }
            }

        }

        lpg.probePositions = position.ToArray();

    }
}
[CustomEditor(typeof(GenLPG))]
public class GenLPGEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GenLPG gen = (GenLPG)target;
        if (GUILayout.Button("Gen"))
        {
            gen.Gen();
        }
    }
}
