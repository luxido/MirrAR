using UnityEngine;
using UnityEngine.UI;

public class Mirror : MonoBehaviour {
    public static bool 
        createMirror = false, 
        buttonDown = false;
    public static GameObject turnButton, amountLabel, moveButton, buildButton;
    public static int totalMirrorAmount = 0, leftMirrorAmount = 0;

    void Start() {
        turnButton = GameObject.Find("Game_MirrorTurnButton");
        turnButton.SetActive(false);
        moveButton = GameObject.Find("Game_MoveButton");
        moveButton.SetActive(false);
        buildButton = GameObject.Find("Game_BuildButton");
        amountLabel = GameObject.Find("Game_MirrorDisplayAmountLabel");
        GameObject.Find("Game").SetActive(false);
    }

    void Update() {
        if (!buttonDown && createMirror && Input.GetMouseButtonDown(0)) {
            Debug.Log("createMirror");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                Transform targetTransform = GameObject.Find("ImageTarget").transform;
                Transform generator = GameObject.Find("LaserGeneratorPrefab").transform;
                //Vector3 position = hit.point + new Vector3(0, 100, 0);/
                Vector3 position = new Vector3(hit.point.x, generator.position.y, hit.point.z);
                GameObject obj = Instantiate(Resources.Load("MirrorPrefab"), position, transform.rotation) as GameObject;
                
                obj.transform.SetParent(targetTransform, true);
                obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.02f);

                //Laser aktualisieren
                GameObject go = GameObject.Find("LaserGeneratorPrefab");
                Laser other = (Laser)go.GetComponent(typeof(Laser));
                other.Start();
                other.drawLaserLine();

                createMirror = false;

                buildButton.SetActive(true);
                GameObject.Find("Game_BuildPressedButton").SetActive(false);

                leftMirrorAmount--;
                GameObject.Find("Game_MirrorDisplayAmountLabel").GetComponent<Text>().text = Mirror.leftMirrorAmount + "";
            }
        }

        if(createMirror && Input.GetMouseButtonUp(0)) {
            buttonDown = false;
        }
    }
}
