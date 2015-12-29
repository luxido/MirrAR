using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TurnMirror : MonoBehaviour
{
    //selectMirror ist der Zustand, wo der Spiegel ausge
    public bool mirrorSelected = false, moveMirrorState = false;
    private GameObject mirror;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!mirrorSelected && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if ((Physics.Raycast(ray, out hit) && hit.collider.tag == "Mirror"))
            {
                mirror = hit.transform.gameObject;
                mirrorSelected = true;
                Debug.Log("Mirror selected");
                Mirror.turnButton.SetActive(true);
                Mirror.moveButton.SetActive(true);
            }
            else
            {
                turnMirrorOff();
            }
        }
    }

    void turnMirrorOff()
    {
        mirrorSelected = false;
        Mirror.turnButton.SetActive(false);

        if (GameObject.Find("Game_MovePressedButton") != null)
        {
            GameObject.Find("Game_MovePressedButton").SetActive(false);
        }
    }

    public void turnMirror()
    {
        if (mirrorSelected)
        {
            Debug.Log("Mirror Turned");
            mirror.transform.rotation *= Quaternion.Euler(0, 45.0f, 0);


            ////////TESTTTTT
            GameObject go = GameObject.Find("LaserGeneratorPrefab");
            Laser other = (Laser)go.GetComponent(typeof(Laser));
            other.Start();
            other.drawLaserLine();

        }
    }

    public void enableMoveState()
    {
        moveMirrorState = true;
    }

    void moveMirror()
    {
        Debug.Log("Mirror Moved");
        Vector3 a = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        a.Set(a.x, a.y, mirror.transform.position.z);
        mirror.transform.position = Vector3.Lerp(mirror.transform.position, a, 0.01f); //dieser Teil funktioniert aktuell noch nicht
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (moveMirrorState)
        {
            moveMirror();
        }

        turnMirrorOff();
    }
}

