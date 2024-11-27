using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target; // what the camera following
    public float lerpAmount;
    Vector3 velocity = Vector3.zero;
    Vector3 cameraTargetPosition;
    Vector3 offset = new Vector3(0f,0f,-10f);
    // Start is called before the first frame update

    void Start()
    {
        transform.position = new Vector3(target.position.x, target.position.y, -10f);

    }

    // Update is called once per frame
    void Update()
    {
        cameraTargetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, cameraTargetPosition, ref velocity, lerpAmount);

    }
}
