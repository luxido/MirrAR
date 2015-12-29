using UnityEngine;
using System.Collections;

public class TurnMirror : MonoBehaviour {
    //selectMirror ist der Zustand, wo der Spiegel ausge
    public bool buttonDown, selectMirror = false, mirrorSelected;
    private GameObject mirror;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        /*
        if (Input.GetMouseButtonDown(0) && !selectMirror && !mirrorSelected && !buttonDown) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //if ((Physics.Raycast(ray, out hit) && hit.collider.gameObject.name == "Mirror")) {
            if ((Physics.Raycast(ray, out hit) && hit.collider.tag == "Mirror")) {
                buttonDown = true;
                selectMirror = true;
                Debug.Log("Select a mirror");
                Mirror.turnButton.SetActive(true);
            } else {
                turnMirrorOff();
            }
        }*/

        if (!mirrorSelected && Input.GetMouseButtonUp(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if ((Physics.Raycast(ray, out hit) && hit.collider.tag == "Mirror")) {
                mirror = hit.transform.gameObject;
                mirrorSelected = true;
                buttonDown = true;
                Debug.Log("Mirror selected");
                Mirror.turnButton.SetActive(true);
            } else {
                turnMirrorOff();
            }
        }

        /*tonDown = false;
        }

        if (mirrorSelected && Input.GetMouseButtonUp(0)) {
            buttonDown = false;
        }*/
    }

    void turnMirrorOff() {
        selectMirror = false;
        mirrorSelected = false;
        buttonDown = false;
        Mirror.turnButton.SetActive(false);
    }

    public void turnMirror() {
        if (mirrorSelected) {
            Debug.Log("Mirror Turned");
            mirror.transform.rotation *= Quaternion.Euler(0, 45.0f, 0);
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if ((Physics.Raycast(ray, out hit) && (hit.collider.gameObject.name == "Ground"))) {
            Debug.Log(hit.collider.gameObject.name);
            Debug.Log("TURN OFF");
            turnMirrorOff();
        }
    }
}

