using UnityEngine;
using UnityEngine.UI;

public class GameEndEventController : MonoBehaviour {

	public void clickReplay()
    {
        Debug.Log("Click Replay");
        Mirror.nextLevelButton.GetComponent<Button>().interactable = true;
    }

    public void clickNext()
    {
        Debug.Log("Click Next");
    }

    public void clickMenu()
    {
        Debug.Log("Click Menu");
        Mirror.nextLevelButton.GetComponent<Button>().interactable = true;
    }
}
