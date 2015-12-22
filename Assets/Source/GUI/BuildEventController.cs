using UnityEngine;

public class BuildEventController : MonoBehaviour {

    private bool builtModusActive;

	public void backClick()
    {
        Debug.Log("back");
        builtModusActive = false;
    }

    public void setMirrorPosition(string placement)
    {
        if(builtModusActive)
        {
            Debug.Log(placement);
        }
    }

    public void builtModus()
    {
        //hier müsste noch abgefragt werden ob überhaupt Spiegel verfügbar sind
        builtModusActive = true;
    }

    public void cancelBuiltModus()
    {
        builtModusActive = false;
    }
}
