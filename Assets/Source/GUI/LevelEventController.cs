using UnityEngine;
using UnityEngine.UI;

public class LevelEventController : MonoBehaviour {

    public static int currentLevel;
    GameObject go;
    GameObject laser;

    public void Start()
    {
        go = GameObject.Find("Ground");
        laser = GameObject.Find("LaserGeneratorPrefab"); //ggf. sollte das Laser.cs ebenfalls in Ground rein damit das einheitlich ist, oder?
    }

	public void clickLevel(string level)
    {
        bool foundLevel = false;
        int targetAmount = 0;

        switch(level)
        {
            case "1":
                Debug.Log("Level 1");
                foundLevel = true;
                int.TryParse(level, out currentLevel);
                Mirror.totalMirrorAmount = 5;
                targetAmount = 1;
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

            //Sets the amount of targets
            Laser laserClass = (Laser)laser.GetComponent(typeof(Laser));
            laserClass.initTargets(targetAmount);
        }

        //To make sure everything has been reset
        TurnMirror eventClass = (TurnMirror)go.GetComponent(typeof(TurnMirror));
        eventClass.turnMirrorOff();
    }
}
