using UnityEngine;
using System.Collections;

public class MenuEventController : MonoBehaviour {

    public void clickPlay()
    {
        Debug.Log("test");
    }

    public void clickExit()
    {
        Application.Quit();
    }
}
