using UnityEngine;
using UnityEngine.EventSystems;

public class GameEventController : MonoBehaviour {

    public static GameObject buildButton;
    public static bool turnButtonClicked;

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
        turnButtonClicked = true;
        Debug.Log("Turn Click");
        GameObject go = GameObject.Find("Ground");
        TurnMirror other = (TurnMirror)go.GetComponent(typeof(TurnMirror));
        other.turnMirror();
        turnButtonClicked = false;
    }
}
