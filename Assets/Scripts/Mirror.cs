using UnityEngine;

public class Mirror : MonoBehaviour {
    public static bool 
        createMirror = false, 
        buttonDown = false;

    void Start() {
    }

    void Update() {
        if (!buttonDown && createMirror && Input.GetMouseButtonDown(0)) {
            Debug.Log("createMirror");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                Vector3 position = hit.point + new Vector3(0, 100, 0);
                GameObject obj = Instantiate(Resources.Load("MirrorPrefab"), position, transform.rotation) as GameObject;
                Transform targetTransform = GameObject.Find("ImageTarget").transform;
                obj.transform.SetParent(targetTransform, true);
                obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.02f);
                createMirror = false;
                GameEventController.buildButton.SetActive(true);
                GameObject.Find("Game_BuildPressedButton").SetActive(false);
            }
        }

        if(createMirror && Input.GetMouseButtonUp(0)) {
            buttonDown = false;
        }
    }
}
