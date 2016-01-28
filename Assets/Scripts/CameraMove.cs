using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    Transform MyTransform;
    [SerializeField]
    [Range(1.0f,10.0f)]
    float MoveStep;
	
    // Use this for initialization
	void Start ()
    {
        MyTransform = transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Basic hard-coded camera movement control:
        
        // Vertical Movement
        if (Input.GetKey(KeyCode.W))
            MyTransform.position += new Vector3(0.0f, MoveStep);
        if (Input.GetKey(KeyCode.S))
            MyTransform.position -= new Vector3(0.0f, MoveStep);
        // Horizontal Movement
        if (Input.GetKey(KeyCode.D))
            MyTransform.position += new Vector3(MoveStep, 0.0f);
        if (Input.GetKey(KeyCode.A))
            MyTransform.position -= new Vector3(MoveStep, 0.0f);

        // Zoom In and Zoom Out controls
        if (Input.GetKey(KeyCode.E))
            MyTransform.position += new Vector3(0.0f, 0.0f, 2*MoveStep);
        if (Input.GetKey(KeyCode.Q))
            MyTransform.position -= new Vector3(0.0f, 0.0f, 2*MoveStep);

    }
}
