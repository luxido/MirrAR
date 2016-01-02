using UnityEngine;
using UnityEngine.UI;

public class Laser : MonoBehaviour {
    LineRenderer [] lineRenderer;
    GameObject generator;
    GameObject[] target;
    public static int targetAmount;
    private float linewidth = 10.0f;
    public Vector3 directionToGo;
    public int lastLaserNumber;
    public Vector3[] positions;
    GameObject[] newLine;// 

    public void Start() {
        generator = GameObject.Find("LaserGeneratorPrefab");
        lineRenderer = new LineRenderer[10];
        //newLine = new GameObject[10];
        for (int i = 0; i < lineRenderer.Length; i++) {
            if (GameObject.Find("Line " + i) !=null) {
                DestroyImmediate(GameObject.Find("Line " + i));
            }//
           // newLine[i] = new GameObject("Line " + i);
            lineRenderer[i] = new GameObject("Line " + i).AddComponent<LineRenderer>();
            lineRenderer[i].SetWidth(linewidth, linewidth);
            lineRenderer[i].SetPosition(0, new Vector3(0, 0, 0));
            lineRenderer[i].SetPosition(1, new Vector3(0, 0, 0));
        }
        //lineRenderer[0] = GetComponent<LineRenderer>();
        directionToGo = new Vector3(0, 0, 100);
        lastLaserNumber = 1;
        positions = new Vector3[10];

    }

    public void Update() {
        drawLaserLine();

    }

    public void drawLaserLine() {
        RaycastHit hit;
        Vector3 direction = transform.TransformDirection(directionToGo);
        //Erstmal abfragen, ob es nur ein Laserstrahl gibt, damit die positions nicht immer neu gesetzt werden
        if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity) && lastLaserNumber == 1) {
            lineRenderer[0].SetWidth(linewidth, linewidth); 
            lineRenderer[0].SetPosition(0, transform.position);
            lineRenderer[0].SetPosition(1, hit.point);
            //Wenn ein Mirror getroffen wird, gibt es ein Laserstrahl mehr
            if (hit.collider.tag == "Mirror") {
                lastLaserNumber++;
                positions[1] = hit.point;
                positions[0] = transform.position;
                for (int i = 1; i < lastLaserNumber; i++) {
                    Debug.Log("position " + i + ": " + positions[i]);
                    directionToGo = Vector3.Reflect((positions[i] - positions[i - 1]).normalized, hit.normal);
                    direction = transform.TransformDirection(directionToGo);
                    if (Physics.Raycast(positions[i], direction, out hit, Mathf.Infinity)) {
                        lineRenderer[i].SetPosition(0, positions[i]);
                        lineRenderer[i].SetPosition(1, hit.point);
                        //Wenn ein Mirror getroffen wird, gibt es ein Laserstrahl mehr
                        if ((hit.collider.tag == "Mirror")) {   
                            positions[lastLaserNumber] = hit.point;
                            lastLaserNumber++;
                        }
                    }
                }
            }
        }
    }

    public void initTargets(int amount)
    {
        targetAmount = amount;
        target = new GameObject[amount];

        for (int i = 0; i < target.Length; i++)
        {
            if (GameObject.Find("Target " + i) != null)
            {
                DestroyImmediate(GameObject.Find("Target " + i));
            }
            target[i] = (GameObject)Instantiate(Resources.Load("TargetPrefab"), Vector3.zero, Quaternion.identity);
            target[i].name = "Target " + i;
        }
    }

    private void laserHitEnd()
    {
        //Überprüfen ob der Laser alle Targets trifft.
        //Überprüfen ob es ein weiteres Level gibt. Wenn nicht muss der "Next Level" Button deaktiviert werden.
        
        //Mirror.nextLevelButton.GetComponent<Button>().interactable = false; um den Next Level Button zu deaktivieren
    }
}

