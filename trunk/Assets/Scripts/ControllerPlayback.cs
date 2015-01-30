using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class ControllerPlayback : MonoBehaviour {

    // Bone Positions
    public Transform bonePrefab;
    Transform[] bones;

    // Joint Positions
    public Transform[] point;
    int pointLength;

    // UI to show current frame
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

        pointLength = point.Length;

        bones = new Transform[pointLength];

        for (int i = 0; i < (pointLength - 1); i++) {
            bones[i] = Instantiate(bonePrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity) as Transform;
        }

        Setup();
    }
	
	// Update is called once per frame
	void Update() {       
        point[0].position = Vector3.Lerp(point[0].position, Vector3.zero, interpolation * Time.deltaTime);
        point[1].position = Vector3.Lerp(point[1].position, shoulderPosition, interpolation * Time.deltaTime); 
        point[2].position = Vector3.Lerp(point[2].position, elbowPosition, interpolation * Time.deltaTime);

        Transform cylinderRef;
        Transform spawn;
        Transform target;

        /* Update positions of Bones */
        for (int i = 1; i < pointLength; i++) {

            if (bones[i] == null)
                Debug.Log("No bone at:" + i.ToString());

            cylinderRef = bones[i];

            int li = i - 1; // Previous i value

            spawn = point[i];
            target = point[li];

            // Find the distance between 2 points
            Vector3 newScale = cylinderRef.localScale;
            newScale.z = Vector3.Distance(spawn.position, target.position) / 2;

            cylinderRef.localScale = newScale;
            cylinderRef.position = spawn.position;        // place bond here
            cylinderRef.LookAt(target);                   // aim bond at positiion
        }
	}

    public void Setup() {
        shoulderData = CsvReader.Read(shoulderDataFilename);
        elbowData = CsvReader.Read(elbowDataFilename);

        /* Verify data
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
         * */


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

        Playback();
    }
}
