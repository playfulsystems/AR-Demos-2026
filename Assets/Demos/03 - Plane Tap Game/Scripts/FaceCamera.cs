using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // get camera
        Vector3 lookTarget = mainCamera.transform.position;
        lookTarget.y = transform.position.y; // ignore vertical difference

        // look at camera and then rotate
        transform.LookAt(lookTarget);
        transform.Rotate(0, 180, 0); // flip to face camera
    }
}
