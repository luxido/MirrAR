using UnityEngine;
using UnityEngine.UI;

public class Laser : MonoBehaviour {
    LineRenderer[] lineRenderer;
    GameObject generator;
    GameObject[] target;
    public static int targetAmount;
    private float linewidth = 5.0f;
    public Vector3 directionToGo;
    public int lastLaserPoint;
    public Vector3[] positions;
    public bool[] targetHit;
    public GameObject generatorTracker;
    public GameObject winPanel;
      
   
    public void Start() {
        winPanel.SetActive(false);
        generator = GameObject.Find("LaserGeneratorPrefab");
        generatorTracker = GameObject.Find("FrameMarker2 - Generator");
        lineRenderer = new LineRenderer[10];
        //Falls es noch alte Lines gibt, diese löschen
        for (int i = 0; i < lineRenderer.Length; i++) {
            if (GameObject.Find("Line " + i) != null) {
                DestroyImmediate(GameObject.Find("Line " + i));
            }
            lineRenderer[i] = new GameObject("Line " + i).AddComponent<LineRenderer>();
            GameObject.Find("Line " + i).transform.SetParent(generatorTracker.transform);
            lineRenderer[i].SetWidth(linewidth, linewidth);
            lineRenderer[i].SetPosition(0, new Vector3(0, 0, 0));
            lineRenderer[i].SetPosition(1, new Vector3(0, 0, 0));
        }
        directionToGo = new Vector3(0, 0, 100);
        lastLaserPoint = 1;
        positions = new Vector3[10];
        targetAmount = 4; /////jetzt nur testweise
        targetHit = new bool[targetAmount];
        for (int j = 0; j < targetHit.Length; j++) {
            targetHit[j] = false;
        }
    }

    public void Update() {
        if (laserHitEnd()) {
            //Debug.Log("all targets were hit");//
            winPanel.SetActive(true);
        } else {
            //Debug.Log("not all targets were hit");
            Start();
            drawLaserLine();
        }

    }

    public void drawLaserLine() {
        RaycastHit hit;
        Vector3 direction = transform.TransformDirection(directionToGo);
        LayerMask targetMask = ~(1 << 9);

        //Erstmal abfragen, ob es nur ein Laserstrahl gibt, damit die positions nicht immer neu gesetzt werden
        //Erstmal abfragen, ob ein Target getroffen wurde
        if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity) && lastLaserPoint == 1) {
            if (hit.collider.tag == "Target") {
                for (int j = 0; j < targetHit.Length; j++) {
                    if (hit.collider.gameObject.name == "Targetprefab" + j) {
                        //targetHit[j] = true;
                        //Debug.Log("Target Hit" + j);
                    }
                }
            }
        }

        //Dann die neue Position vom LineRenderer setzen
        if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity, targetMask) && lastLaserPoint == 1) {
            if (hit.collider.tag != "Target") {
                lineRenderer[0].SetWidth(linewidth, linewidth);
                //Debug.Log("direction0: " + direction);
                positions[1] = hit.point;
                positions[1].y = transform.position.y;
                positions[0] = transform.position;
                lineRenderer[0].SetPosition(0, positions[0]);
                lineRenderer[0].SetPosition(1, positions[1]);
            }

            //
            if (hit.collider.tag == "Mirror") {
                lastLaserPoint++;
                for (int i = 1; i < lastLaserPoint; i++) {
                    directionToGo = Vector3.Reflect((positions[i] - positions[i - 1]).normalized, hit.normal);
                    directionToGo.y = 0;
                    direction = transform.TransformDirection(directionToGo);
                    //Debug.Log("direction" + i +":" + direction);
                    if (Physics.Raycast(positions[i], direction, out hit, Mathf.Infinity)) {
                        if (hit.collider.tag == "Target") {
                            for (int j = 0; j < targetHit.Length; j++) {
                                if (hit.collider.gameObject.name == "Targetprefab" + j) {
                                    targetHit[j] = true;
                                    Debug.Log("Target Hit" + j);
                                }
                            }
                        }
                    }
                    if (Physics.Raycast(positions[i], direction, out hit, Mathf.Infinity, targetMask)) {
                        //Wenn ein Mirror getroffen wird, gibt es ein Laserstrahl mehr//
                        positions[i + 1] = hit.point;
                        positions[i + 1].y = transform.position.y;
                        lineRenderer[i].SetPosition(0, positions[i]);
                        lineRenderer[i].SetPosition(1, positions[i + 1]);
                        if ((hit.collider.tag == "Mirror")) {
                            lastLaserPoint++;
                        }
                    }
                }
            }
        }


    }

    public void initTargets(int amount) {
        targetAmount = amount;
        target = new GameObject[amount];
    }

    bool laserHitEnd() {
        bool allHit = true;
        for (int i = 0; i < targetHit.Length; i++) {
            Material newMaterial;
            MeshRenderer gameObjectRenderer = GameObject.Find("Circle" + i).GetComponent<MeshRenderer>();
            if (targetHit[i] == true) {
                newMaterial = new Material(Resources.Load("TargetHitColor") as Material);
                gameObjectRenderer.material = newMaterial;
            }
            else if (targetHit[i] == false) {
                
                newMaterial = new Material(Resources.Load("TargetMaterial 2") as Material);
                gameObjectRenderer.material = newMaterial;
                //break;
                allHit = false;//
            }
            
        }
        return allHit;
    }

}

