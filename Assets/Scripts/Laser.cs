using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
    private LineRenderer lineRenderer;
    private float counter;
    private float dist;
    private float laserWidth = 10.0f;
    public float maxLength = 50.0f;

    public Transform origin;
    public Transform destination;
    private Transform myTransform;
    private Vector3 offset;
    private Vector3[] position;
    private int length;
    public float noise = 1.0f;

    public float lineDrawSpeed = 6f;


    // Use this for initialization
    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        //lineRenderer.SetPosition(0, origin.position);
        lineRenderer.SetWidth(laserWidth, laserWidth);
        myTransform = transform;
        offset = new Vector3(0, 0, 0);

        //dist = Vector3.Distance(origin.position, destination.position);
    }

    void Update() {
        UpdateLength();

        for (int i = 0; i < length; i++) {
            //Set the position here to the current location and project it in the forward direction of the object it is attached to
            offset.x = myTransform.position.x + i * myTransform.forward.x + Random.Range(-noise, noise);
            offset.z = i * myTransform.forward.z + Random.Range(-noise, noise) + myTransform.position.z;
            position[i] = offset;
            position[0] = myTransform.position;

            lineRenderer.SetPosition(i, position[i]);

        }
        /*
        if (counter < dist) {
            lineRenderer.SetWidth(laserWidth, laserWidth);
            counter += .1f / lineDrawSpeed;

            float x = Mathf.Lerp(0, 20, counter);

            Vector3 pointA = origin.position;
            Vector3 pointB = destination.position;

            Vector3 pointALongLine = x * Vector3.Normalize(pointB - pointA) + pointA;
        }*/
    }

    void UpdateLength() {
        //Raycast from the location of the cube forwards
        RaycastHit[] hit;
        hit = Physics.RaycastAll(myTransform.position, myTransform.forward, maxLength);
        int i = 0;
        while (i < hit.Length) {
            //Check to make sure we aren't hitting triggers but colliders
            if (!hit[i].collider.isTrigger) {
                length = (int)Mathf.Round(hit[i].distance) + 2;
                position = new Vector3[length];
                //Move our End Effect particle system to the hit point and start playing it
                lineRenderer.SetVertexCount(length);
                return;
            }
            i++;
        }
        length = (int)maxLength;
        position = new Vector3[length];
        lineRenderer.SetVertexCount(length);
    }
}
