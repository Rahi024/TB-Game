using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public Transform target; // The object or point the camera will orbit around
    public float speed = 10f; // Speed of the camera's rotation

    void Update()
    {
        // Rotate the camera around the target
        if (target != null)
        {
            transform.RotateAround(target.position, Vector3.up, speed * Time.deltaTime);
            transform.LookAt(target); // Keep the camera looking at the target
        }
    }
}
