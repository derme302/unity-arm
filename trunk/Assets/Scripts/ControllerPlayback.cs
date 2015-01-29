using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CsvReader))]
public class ControllerPlayback : MonoBehaviour {

    CsvReader csvReader;

	// Use this for initialization
    void Start() {
        csvReader = gameObject.GetComponent<CsvReader>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
