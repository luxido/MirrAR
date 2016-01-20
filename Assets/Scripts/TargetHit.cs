using UnityEngine;
using System.Collections;

public class TargetHit : MonoBehaviour {

    void OnCollisionEnter(Collision collision) {
        Color whateverColor = new Color(0, 255, 0, 1);
        Debug.Log("hitColor");
        MeshRenderer gameObjectRenderer = gameObject.GetComponent<MeshRenderer>();

        Material newMaterial = new Material(Resources.Load("TargetHitColor") as Material);

        newMaterial.color = whateverColor;
        gameObjectRenderer.material = newMaterial;

    }
}
