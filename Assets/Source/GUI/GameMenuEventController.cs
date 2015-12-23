using UnityEngine;
using System.Collections;

public class GameMenuEventController : MonoBehaviour {

	public void back()
    {
        Debug.Log("Back");
    }

    public void backToMenu()
    {
        Debug.Log("Back to menu.");
    }

    public void exit()
    {
        Application.Quit();
    }
}
