using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning
{
    public LineRenderer lineRenderer { get; set; }
    public LineRenderer lightRenderer { get; set; }

    public float SegmentLength { get; set; }
    public bool IsActive { get; private set; }

    public Lightning(float segmentLength)
    {
        SegmentLength = segmentLength;
    }

    public void Init(GameObject lineRendererPrefab, GameObject lightRendererPrefab,Transform parent)
    {
        lineRenderer = ((GameObject)ObjectPooling.Instance.GetGameObject(lineRendererPrefab, new Vector3(0,0,0), Quaternion.identity) as GameObject).GetComponent<LineRenderer>();
       // lineRenderer = (GameObject.Instantiate(lineRendererPrefab) as GameObject).GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lightRenderer = ((GameObject)ObjectPooling.Instance.GetGameObject(lightRendererPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject).GetComponent<LineRenderer>();
        //lightRenderer = (GameObject.Instantiate(lightRendererPrefab) as GameObject).GetComponent<LineRenderer>();
        lineRenderer.transform.parent = parent;
        lightRenderer.transform.parent = parent;
        IsActive = false;
    }

    public void Activate()
    {
        lineRenderer.enabled = true;
        lightRenderer.enabled = true;
        IsActive = true;
    }

    public void Deactivate()
    {
        lineRenderer.enabled = false;
        lightRenderer.enabled = false;
        IsActive = false;
    }
    public void DrawLightning(Vector2 source, RaycastHit2D hit)
    {
        //Calculated amount of Segments
        float distance = Vector2.Distance(source, hit.point);
        int segments = 5;
        segments = Mathf.FloorToInt(distance / SegmentLength) + 2;

        lineRenderer.positionCount = segments;
        //lineRenderer.SetVertexCount(segments);
        lineRenderer.SetPosition(0, source);
        Vector2 lastPosition = source;
        for (int j = 1; j < segments - 1; j++)
        {
            //Go linear from source to target
            Vector2 tmp = Vector2.Lerp(source, hit.point, (float)j / (float)segments);
            //Add randomness
            lastPosition = new Vector2(tmp.x + Random.Range(-0.1f, 0.1f), tmp.y + Random.Range(-0.1f, 0.1f));
            //Set the calculated position
            lineRenderer.SetPosition(j, lastPosition);

            lineRenderer.SetPosition(segments - 1, hit.point);

            //Set the points for the light
            lightRenderer.SetPosition(0, source);
            lightRenderer.SetPosition(1, hit.point);
            //Set the color of the light
            Color lightColor = new Color(0.5647f, 0.58823f, 1f, Random.Range(0.2f, 1f));
            lightRenderer.startColor = lightColor;
            lightRenderer.endColor = lightColor;
        }
    }
}
