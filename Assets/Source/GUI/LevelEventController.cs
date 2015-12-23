using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelEventController : MonoBehaviour {

	public void clickLevel(string level)
    {
        switch(level)
        {
            case "Level 1":
                Debug.Log("Case 1");
                break;
        }
    }
}
