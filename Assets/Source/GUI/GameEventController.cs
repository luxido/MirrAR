using UnityEngine;
using System.Collections;

public class GameEventController : MonoBehaviour {

    public static GameObject buildButton;

	public void menuClick()
    {
        Debug.Log("Menu Click");
    }

    public void buildClick()
    {
        Mirror.createMirror = true;
        Mirror.buttonDown = true;
        buildButton = GameObject.Find("Game_BuildButton");
    }

    public void turnClick()
    {
        Debug.Log("Turn Click");
    }
}
