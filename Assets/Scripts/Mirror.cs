using UnityEngine;
using System.Collections;

public class Mirror : MonoBehaviour {
    private bool createMirror = false;
    public GameObject test;
    GameObject obj;

    void Start() {
        //Input.simulateMouseWithTouches = true;
        
    }
    void Update() {
        if (Input.GetMouseButtonDown(0) && !createMirror) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //Vector3 touchPositionForPlacing = 
            if ((Physics.Raycast(ray, out hit) && hit.collider.gameObject.name == "Capsule")) {
                //Debug.Log("get capsule");
                createMirror = true;
            }
        }

        if (Input.GetMouseButtonUp(0) && createMirror ) {
            Debug.Log("createMirror");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                obj = Instantiate(Resources.Load("MirrorPrefab"), hit.point, transform.rotation) as GameObject;
                Transform targetTransform = GameObject.Find("ImageTarget").transform;
                obj.transform.SetParent(targetTransform, true);
                obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.02f);
                createMirror = false;
                
            }
        }
    }
}
