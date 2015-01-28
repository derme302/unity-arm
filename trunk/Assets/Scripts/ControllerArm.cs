using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControllerArm : MonoBehaviour {

    public Transform boneHumerus;
    public Transform boneRadius;

    public Slider boneAngle;

    float angleCurrent;

	// Use this for initialization
	void Start () {
        boneAngle.onValueChanged.AddListener(delegate { UpdateAngle(boneAngle.value); });
	}
	
	// Update is called once per frame
	void Update () {
        boneRadius.localRotation = Quaternion.Euler(angleCurrent, 90, 0);
	}

    public void UpdateAngle(float value) {
        angleCurrent = value;
    }
}
