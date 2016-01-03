using UnityEngine;
using UnityEngine.UI;

public class Laser : MonoBehaviour {
    LineRenderer [] lineRenderer;
    GameObject generator; //würde es nicht Sinn machen diese evtl. auch in einen Array zu speichern um damit möglichst dynamisch zu bleiben?
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

    //Erstellt das Target, zurzeit stimmt aber leider nicht die y-Achse. Aus irgendeinen Grund nimmt diese einen anderen Wert an
    //als vorgegeben. An welcher Stelle muss man dem Objekt noch die Zuweisung geben, sodass es auch wirklich die "0.1" animmt?
    public void initTargets(int amount)
    {
        targetAmount = amount;
        target = new GameObject[amount];
        GameObject parent = GameObject.Find("ImageTarget");

        for (int i = 0; i < target.Length; i++)
        {
            if (GameObject.Find("Target " + i) != null)
            {
                DestroyImmediate(GameObject.Find("Target " + i));
            }
            target[i] = Instantiate(Resources.Load("TargetPrefab"), new Vector3(0, 0.1f, 0), transform.rotation) as GameObject;
            target[i].name = "Target " + i;
            target[i].transform.SetParent(parent.transform, true);
            target[i].transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        }
    }

    private void laserHitEnd()
    {
        //Überprüfen ob der Laser alle Targets trifft.
        //Überprüfen ob es ein weiteres Level gibt. Wenn nicht muss der "Next Level" Button deaktiviert werden.
        
        //Mirror.nextLevelButton.GetComponent<Button>().interactable = false; um den Next Level Button zu deaktivieren
    }
}

