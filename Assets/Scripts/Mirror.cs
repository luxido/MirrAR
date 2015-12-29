using UnityEngine;
using System.Collections;

public class Mirror : MonoBehaviour {
    private bool createMirror = false, buttonDown = false;
    public GameObject test;
    GameObject obj;

    void Start() {
    }

    void Update() {
        if (Input.GetMouseButtonDown(0) && !createMirror) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if ((Physics.Raycast(ray, out hit) && hit.collider.gameObject.name == "Capsule")) {
                createMirror = true;
                buttonDown = true;
            }
        }

        if (!buttonDown && createMirror && Input.GetMouseButtonDown(0)) {
            Debug.Log("createMirror");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                Vector3 position = hit.point + new Vector3(0, 100, 0);
                obj = Instantiate(Resources.Load("MirrorPrefab"), position, transform.rotation) as GameObject;
                Transform targetTransform = GameObject.Find("ImageTarget").transform;
                obj.transform.SetParent(targetTransform, true);
                obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.02f);
                createMirror = false;
            }
        }

        if(createMirror && Input.GetMouseButtonUp(0)) {
            buttonDown = false;
        }
    }
}
