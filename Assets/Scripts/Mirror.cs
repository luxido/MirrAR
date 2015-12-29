using UnityEngine;

public class Mirror : MonoBehaviour {
    public static bool 
        createMirror = false, 
        buttonDown = false;
    public static GameObject turnButton;
    public static int totalMirrorAmount, leftMirrorAmount;

    void Start() {
        turnButton = GameObject.Find("Game_MirrorTurnButton");
        turnButton.SetActive(false);
        GameObject.Find("Game_MirrorDisplayPanel_AmountLabel").GetComponent<GUIText>().text = Mirror.leftMirrorAmount + "";
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

                leftMirrorAmount--;
                GameObject.Find("Game_MirrorDisplayPanel_AmountLabel").GetComponent<GUIText>().text = Mirror.leftMirrorAmount + "";
            }
        }

        if(createMirror && Input.GetMouseButtonUp(0)) {
            buttonDown = false;
        }
    }
}
