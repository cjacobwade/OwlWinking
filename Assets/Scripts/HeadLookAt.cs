using UnityEngine;
using System.Collections;

public class HeadLookAt : MonoBehaviour 
{
    public Transform target;
    public float lookSpeed = 1;

    public float minX = 36.0f;
    public float maxX = 90.0f;
    public float minY = 615.0f;
    public float maxY = 645.0f;
    public float minZ = 240.0f;
    public float maxZ = 303.0f;
    
	void Update () 
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation((target.position - transform.position)), Time.deltaTime * lookSpeed);
        Vector3 euler = transform.localEulerAngles;

        euler.x = Mathf.Clamp(euler.x, minX, maxX);
        euler.y = Mathf.Clamp(euler.y, minY, maxY);
        euler.z = Mathf.Clamp(euler.z, minZ, maxZ);

        transform.localEulerAngles = euler;
	}
}
