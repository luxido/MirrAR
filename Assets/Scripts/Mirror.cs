using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Mirror : MonoBehaviour {
    public static bool 
        createMirror = false, 
        buttonDown = false;
    public static GameObject turnButton, amountLabel, moveButton, buildButton, winPanel, levelPanel, nextLevelButton; 
        //Idee: eventuell sollten wir eine Main-Class schreiben, die alle static vars hält, oder was sagst du dazu? Diese müsste dann mit als erstes geladen werden.
    public static int totalMirrorAmount = 0, leftMirrorAmount = 0;
    private GameObject lastCreatedMirror;

    void Start() {
        turnButton = GameObject.Find("Game_MirrorTurnButton");
        turnButton.SetActive(false);
        moveButton = GameObject.Find("Game_MoveButton");
        moveButton.SetActive(false);
        buildButton = GameObject.Find("Game_BuildButton");
        amountLabel = GameObject.Find("Game_MirrorDisplayAmountLabel");
        GameObject.Find("Game").SetActive(false);
        nextLevelButton = GameObject.Find("GameEnd_ButtonNext");
        winPanel = GameObject.Find("GameEnd");
        winPanel.SetActive(false);
        levelPanel = GameObject.Find("LevelMenu");
        levelPanel.SetActive(false);
    }

    //vermutlich sollten wir das in eine onMouseClick umwandeln um die Performance zu verbessern. Diese Funktion ist ja nur relevant
    //wenn die Maustaste auch wirklich geklickt wurde
    void Update() {
        if (!buttonDown && createMirror && Input.GetMouseButtonDown(0) && leftMirrorAmount > 0) {
            Debug.Log("createMirror");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                Transform targetTransform = GameObject.Find("Ground").transform;
                Transform generator = GameObject.Find("LaserGeneratorPrefab").transform;
                //Vector3 position = hit.point + new Vector3(0, 100, 0);/
                Vector3 position = new Vector3(hit.point.x, generator.position.y, hit.point.z);
                GameObject obj = Instantiate(Resources.Load("MirrorPrefab"), position, transform.rotation) as GameObject;
                
                obj.transform.SetParent(generator, true);
                //obj.transform.localScale = new Vector3(1.0f, 1.1f, 1.02f);
                lastCreatedMirror = obj;

                //Laser aktualisieren
                GameObject go = GameObject.Find("LaserGeneratorPrefab");
                Laser other = (Laser)go.GetComponent(typeof(Laser));
                other.Start();
                other.drawLaserLine();

                createMirror = false;

                buildButton.SetActive(true);
                GameObject.Find("Game_BuildPressedButton").SetActive(false);

                leftMirrorAmount--;
                GameObject.Find("Game_MirrorDisplayAmountLabel").GetComponent<Text>().text = leftMirrorAmount + "";
            }
        }

        if(createMirror && Input.GetMouseButtonUp(0)) {
            buttonDown = false;
        }
    }

    //Funktioniert leider zurzeit nicht. Diese Methode soll den letzten erstellten Spiegel zerstören. Aktuelles Problem: 
    //Spiegel darf nur erstellt werden, wenn die Maus nicht über der GUI ist.
    public void destroyLastMirror()
    {
        Debug.Log("Destroy Mirror");
        Destroy(lastCreatedMirror);
        createMirror = false;
        buttonDown = false;
    }
}
