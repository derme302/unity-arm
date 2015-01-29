using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(CsvReader))]
public class ControllerPlayback : MonoBehaviour {

    public string shoulderDataFilename;
    public string elbowDataFilename;

	// Use this for initialization
    void Start() {

    }
	
	// Update is called once per frame
	void Update() {
	
	}

    public void Setup() {
        List<Dictionary<string, object>> shoulderData = CsvReader.Read(shoulderDataFilename);
        List<Dictionary<string, object>> elbowData = CsvReader.Read(elbowDataFilename);

    }

    public void Play() {

    }

    public void Stop() {

    }
}
