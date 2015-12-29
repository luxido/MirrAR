using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Laser : MonoBehaviour {
    LineRenderer lineRenderer;
    GameObject target;
    GameObject generator;
    public float linewidth = 10.0f;
    public Vector3 directionToGo = new Vector3(0, 0, 100);


    void Start() {
        target = GameObject.Find("TargetPrefab");
        generator = GameObject.Find("LaserGeneratorPrefab");
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetVertexCount(2);
        lineRenderer.SetWidth(linewidth, linewidth);

    }

    void Update() {
        
        RaycastHit hit;
        Vector3 direction = transform.TransformDirection(directionToGo);
        if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity)){
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);
        }

        /*
            if (!(hit.transform.gameObject.layer == LayerMask.NameToLayer("Mirror"))) {

        }*/
    }
}

