using UnityEngine;
using System.Collections;

public class GroundResize : MonoBehaviour {
	GameObject ground;

	void Awake(){

	}
	// Use this for initialization
	void Start () {
		float scrHeight = Camera.main.orthographicSize * 2;
		float scrWidth = scrHeight * Screen.width/ Screen.height;
		ground = (GameObject) Instantiate(Resources.Load("groundplane"), new Vector3(0, 0, 0), Quaternion.identity);
		Vector3 temp = ground.localScale;
		temp.x = scrHeight;
		temp.y = scrWidth;
		ground.transform.localScale = temp;
	}
	
	// Update is called once per frame
	void Update () {
		Resize ();
	}

	void Resize(){

	}
}
