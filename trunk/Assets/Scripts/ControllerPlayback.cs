using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class ControllerPlayback : MonoBehaviour {

    public Transform point1;
    public Transform point2;
    public Transform point3;

    public Text frameCounter;

    public string shoulderDataFilename;
    public string elbowDataFilename;
    public int fps;
    public float interpolation;

    int animationLength;

    Vector3 shoulderPosition = Vector3.zero;
    Vector3 elbowPosition = Vector3.zero;

    List<Dictionary<string, object>> shoulderData;
    List<Dictionary<string, object>> elbowData;

    float frameLength;
    int frame;

	// Use this for initialization
    void Start() {
        frameLength = 1.0f / fps;

        Setup();
    }
	
	// Update is called once per frame
	void Update() {       
        point1.position = Vector3.Lerp(point1.position, Vector3.zero, interpolation * Time.deltaTime);
        point2.position = Vector3.Lerp(point2.position, shoulderPosition, interpolation * Time.deltaTime); 
        point3.position = Vector3.Lerp(point3.position, elbowPosition, interpolation * Time.deltaTime); 
	}

    public void Setup() {
        shoulderData = CsvReader.Read(shoulderDataFilename);
        elbowData = CsvReader.Read(elbowDataFilename);

        Debug.Log("Shoulder Data");
        for(var i=0; i < shoulderData.Count; i++) {
            print("x " + shoulderData[i]["x"] + " " +
                   "y " + shoulderData[i]["y"] + " " +
                   "z " + shoulderData[i]["z"]); 
		}

        Debug.Log("Elbow Data");
        for (var i = 0; i < elbowData.Count; i++) {
            print("x " + elbowData[i]["x"] + " " +
                   "y " + elbowData[i]["y"] + " " +
                   "z " + elbowData[i]["z"]);
        } 


        animationLength = shoulderData.Count;
        Debug.Log("Animation: " + animationLength.ToString());
    }

    public void Play() {
        frame = 0;

        InvokeRepeating("Playback", frameLength, frameLength);

        frameCounter.text = frame.ToString() + "/" + animationLength.ToString();
        Debug.Log("Animation Started");
    }

    void Playback() {
        Debug.Log("Playing Frame: " + frame.ToString());
        if (frame >= animationLength)
            frame = 0;

        shoulderPosition.x = Convert.ToSingle(shoulderData[frame]["x"]);
        shoulderPosition.y = Convert.ToSingle(shoulderData[frame]["y"]);
        shoulderPosition.z = Convert.ToSingle(shoulderData[frame]["z"]);

        elbowPosition.x = Convert.ToSingle(elbowData[frame]["x"]);
        elbowPosition.y = Convert.ToSingle(elbowData[frame]["y"]);
        elbowPosition.z = Convert.ToSingle(elbowData[frame]["z"]);

        frameCounter.text = frame.ToString() + "/" + animationLength.ToString();
        frame ++;
    }

    public void Stop() {
        CancelInvoke();
        Debug.Log("Animation Stopped");
    }

    public void UpdateFrame(int value) {
        frame = value;
    }
}
