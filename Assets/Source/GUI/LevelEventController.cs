using UnityEngine;
using UnityEngine.UI;

public class LevelEventController : MonoBehaviour {

    public static int currentLevel;

	public void clickLevel(string level)
    {
        bool foundLevel = false;

        switch(level)
        {
            case "1":
                Debug.Log("Level 1");
                foundLevel = true;
                int.TryParse(level, out currentLevel);
                Mirror.totalMirrorAmount = 5;
                break;
            case "replayLevel":
                clickLevel(currentLevel + "");
                break;
            case "nextLevel":
                clickLevel((currentLevel + 1) + "");
                break;
            default:
                Mirror.winPanel.SetActive(false);
                Mirror.levelPanel.SetActive(true);
                break;
        }

        if(foundLevel)
        {
            Mirror.leftMirrorAmount = Mirror.totalMirrorAmount;
            Mirror.amountLabel.GetComponent<Text>().text = Mirror.leftMirrorAmount + "";
        }

        //To make sure everything has been reset
        GameObject go = GameObject.Find("Ground");
        TurnMirror eventClass = (TurnMirror)go.GetComponent(typeof(TurnMirror));
        eventClass.turnMirrorOff();
    }
}
