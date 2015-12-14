using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelEventController : MonoBehaviour {

    private bool createBuildMenu;

    void OnGUI()
    {
        if (createBuildMenu)
        {
            createBuildMenu = false;
            Debug.Log("TEST");

            var panel = GameObject.Find("BuildMenu");
            GameObject buttonTest = new GameObject("ButtonTest");

            var image = buttonTest.AddComponent<Image>();
            image.transform.parent = panel.transform;
            image.rectTransform.sizeDelta = new Vector2(180, 50);
            image.rectTransform.anchoredPosition = Vector3.zero;
            image.color = new Color(1f, .3f, .3f, .5f);

            var button = buttonTest.AddComponent<Button>();
            button.targetGraphic = image;
            button.onClick.AddListener(() => Debug.Log(Time.time));
        }
    }

	public void clickLevel(string level)
    {
        switch(level)
        {
            case "Level 1":
                Debug.Log("Case 1");
                createBuildMenu = true;
                break;
        }
    }
}
