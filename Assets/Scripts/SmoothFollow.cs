using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour
{
    public bool shouldRotate = true;

    // The target we are following
    public Transform target;
    // The distance in the x-z plane to the target
    public float distance = 10.0f;
    // the height we want the camera to be above the target
    public float height = 5.0f;
    // How much we
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;
    float wantedRotationAngle;
    float wantedHeight;
    float currentRotationAngle;
    float currentHeight;
    Quaternion currentRotation;

    void FixedUpdate()
    {
        if (target)
        {
            wantedRotationAngle = target.eulerAngles.y;
            wantedHeight = target.position.y + height;
            currentRotationAngle = transform.eulerAngles.y;
            currentHeight = transform.position.y;
            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
            currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
            transform.position = target.position;
            transform.position -= currentRotation * Vector3.forward * distance;
            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
            if (shouldRotate)
            {
                transform.LookAt(target);
            }
        }

    }
}