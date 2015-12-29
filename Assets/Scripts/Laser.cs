using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Laser : MonoBehaviour {
    LineRenderer lineRenderer;
    GameObject target;
    GameObject generator;
    private float linewidth = 10.0f;
    public Vector3 directionToGo;
    public int lastLaserNumber;
    public Vector3 startPos;

    public void Start() {
        target = GameObject.Find("TargetPrefab");
        generator = GameObject.Find("LaserGeneratorPrefab");
        lineRenderer = GetComponent<LineRenderer>();
        startPos = transform.position;
        directionToGo = new Vector3(0, 0, 100);
        lastLaserNumber = 1;
        lineRenderer.SetWidth(linewidth, linewidth);

    }

void Update() {
        drawLaserLine();

    }

    public void drawLaserLine() {
        RaycastHit hit;
        Vector3 direction = transform.TransformDirection(directionToGo);
        if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity)) {
            lineRenderer.SetPosition(lastLaserNumber - 1, startPos);
            lineRenderer.SetPosition(lastLaserNumber, hit.point);
            if ((hit.collider.tag == "Mirror")) {
                lastLaserNumber++;
                lineRenderer.SetVertexCount(lastLaserNumber + 1);
                directionToGo = Vector3.Reflect((hit.point - startPos).normalized, hit.normal);
                startPos = hit.point;
                drawLaserLine();
            }
        }
        /*
            if (!(hit.transform.gameObject.layer == LayerMask.NameToLayer("Mirror"))) {
            //
        }*/
    }
}

