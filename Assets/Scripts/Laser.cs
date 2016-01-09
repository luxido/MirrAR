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

    public void Start() {
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
        targetAmount = 2; /////jetzt nur testweise
        targetHit = new bool[targetAmount];
        for (int j = 0; j < targetHit.Length; j++) {
            targetHit[j] = false;
        }

    }

    public void Update() {
        if (laserHitEnd()) {
            //Debug.Log("all targets were hit");//
            Mirror.winPanel.SetActive(true);
        } else {
            //Debug.Log("not all targets were hit");
            Start();
            drawLaserLine();
        }
        
    }

    public void drawLaserLine() {
        RaycastHit hit;
        Vector3 direction = transform.TransformDirection(directionToGo);
        //Erstmal abfragen, ob es nur ein Laserstrahl gibt, damit die positions nicht immer neu gesetzt werden
        if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity) && lastLaserPoint == 1) {
            lineRenderer[0].SetWidth(linewidth, linewidth);
            positions[1] = hit.point;
            positions[0] = transform.position;
            lineRenderer[0].SetPosition(0, positions[0]);
            lineRenderer[0].SetPosition(1, positions[1]);
            
            //Wurde das Ziel erreicht?
            if (hit.collider.tag == "Target" || hit.collider.tag == "Mirror") {
                lastLaserPoint++;
                for (int i = 1; i < lastLaserPoint; i++) {
                    ////Debug.Log("position " + i + ": " + positions[i]);
                    
                    if (hit.collider.tag == "Target") {
                        direction = hit.transform.TransformDirection(directionToGo);

                        //prüft ob gameobject mit dem Namen "TargetPrefab" + j" gehittet wurde
                        for (int j = 0; j < targetHit.Length; j++) {
                            if(hit.collider.gameObject.name == "Targetprefab" + j) {
                                targetHit[j] = true;
                                Debug.Log("Target Hit"+j);
                            }
                        }
                    } else if (hit.collider.tag == "Mirror") {
                        directionToGo = Vector3.Reflect((positions[i] - positions[i - 1]).normalized, hit.normal);
                        direction = transform.TransformDirection(directionToGo);
                    }
      
                    if (Physics.Raycast(positions[i], direction, out hit, Mathf.Infinity)) {
                        positions[i + 1] = hit.point;
                        lineRenderer[i].SetPosition(0, positions[i]);
                        lineRenderer[i].SetPosition(1, positions[i+1]);
                        //Wurde das Ziel getroffen?
                        if (hit.collider.tag == "Target") {
                            Debug.Log("You Win");
                            lastLaserPoint++;
                        }
                        //Wenn ein Mirror getroffen wird, gibt es ein Laserstrahl mehr
                        else if ((hit.collider.tag == "Mirror")) {
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
        for (int i=0; i < targetHit.Length; i++) {
            if (targetHit[i] == false) {
                allHit = false;
                break;
            }
        }
        return allHit;
    }
}

