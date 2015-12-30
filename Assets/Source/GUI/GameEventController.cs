﻿using UnityEngine;

public class GameEventController : MonoBehaviour {

    public static GameObject buildButton;
    private TurnMirror eventClass;

    void Start()
    {
        GameObject go = GameObject.Find("Ground");
        eventClass = (TurnMirror)go.GetComponent(typeof(TurnMirror));
    }

	public void menuClick()
    {
        Debug.Log("Menu Click");
    }

    public void buildClick()
    {
        Mirror.createMirror = true;
        Mirror.buttonDown = true;
    }

    public void turnClick()
    {
        Debug.Log("Turn Click");
        eventClass.turnMirror();
    }

    public void moveClick()
    {
        Debug.Log("Move Click");
        eventClass.enableMoveState();
    }
}
