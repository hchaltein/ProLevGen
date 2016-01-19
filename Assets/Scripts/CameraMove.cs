using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    Transform MyTransform;
    float MoveStep;
	
    // Use this for initialization
	void Start ()
    {
        MyTransform = transform;

        MoveStep = 0.1f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.W))
            MyTransform.position += new Vector3(0.0f, MoveStep);
        if (Input.GetKey(KeyCode.S))
            MyTransform.position -= new Vector3(0.0f, MoveStep);
        if (Input.GetKey(KeyCode.D))
            MyTransform.position += new Vector3(MoveStep, 0.0f);
        if (Input.GetKey(KeyCode.A))
            MyTransform.position -= new Vector3(MoveStep, 0.0f);
        if (Input.GetKey(KeyCode.E))
            MyTransform.position += new Vector3(0.0f, 0.0f, MoveStep);
        if (Input.GetKey(KeyCode.Q))
            MyTransform.position -= new Vector3(0.0f, 0.0f, MoveStep);

    }
}
