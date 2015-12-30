using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Laser : MonoBehaviour {
    LineRenderer lineRenderer;
    GameObject generator;
    private float linewidth = 10.0f;
    public Vector3 directionToGo;
    public int lastLaserNumber;
    public Vector3[] positions;

    public void Start() {
        generator = GameObject.Find("LaserGeneratorPrefab");
        lineRenderer = GetComponent<LineRenderer>();
        directionToGo = new Vector3(0, 0, 100);
        lastLaserNumber = 1;
        lineRenderer.SetWidth(linewidth, linewidth);
        positions = new Vector3[10];

    }

    public void Update() {
        drawLaserLine();

    }

    public void drawLaserLine() {
        RaycastHit hit;
        Vector3 direction = transform.TransformDirection(directionToGo);
        //Erstmal abfragen, ob es nur ein Laserstrahl gibt, damit die positions nicht immer neu gesetzt werden
        if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity) && lastLaserNumber == 1) {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);
            //Wenn ein Mirror getroffen wird, gibt es ein Laserstrahl mehr
            if (hit.collider.tag == "Mirror") {
                lastLaserNumber++;
                positions[1] = hit.point;
                positions[0] = transform.position;
                for (int i = 1; i < lastLaserNumber; i++) {
                    Debug.Log("position " + i + ": " + positions[i]);
                    directionToGo = Vector3.Reflect((positions[i] - positions[i - 1]).normalized, hit.normal);
                    direction = transform.TransformDirection(directionToGo);
                    if (Physics.Raycast(positions[i], direction, out hit, Mathf.Infinity)) {
                        lineRenderer.SetVertexCount(lastLaserNumber + 1);
                        lineRenderer.SetPosition(lastLaserNumber, hit.point);
                        //Wenn ein Mirror getroffen wird, gibt es ein Laserstrahl mehr
                        if ((hit.collider.tag == "Mirror")) {   
                            positions[lastLaserNumber] = hit.point;
                            lastLaserNumber++;

                        }

                    }
                }
            }

        }
    }
}

