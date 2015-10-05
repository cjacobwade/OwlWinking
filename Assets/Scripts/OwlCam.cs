using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class OwlCam : MonoBehaviour 
{
    public OwlTech owl;
    public MatchRotation matchRotation;

    private float moveSpeed = 12;
    private float lookSpeed = 12;

    private Quaternion lookRotation;
    private Vector3 targetPosition; 

    private Camera cam;
    void Awake()
    {
       cam = GetComponent<Camera>();

       targetPosition = transform.position;
       lookRotation = transform.rotation;
    }

	void FixedUpdate () 
    {
       targetPosition = Vector3.Lerp(targetPosition,new Vector3(owl.transform.position.x, transform.position.y, owl.transform.position.z - 6), Time.deltaTime * moveSpeed);
       transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed / 2);

	   lookRotation = Quaternion.LookRotation(owl.transform.position - transform.position, Vector3.up);
       transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * lookSpeed);

       float distance = Mathf.Clamp(Vector3.Distance(transform.position, owl.transform.position), 6, 15);
       distance = (distance - 6) / 9;

       if (distance < 0.2f)
       {
          owl.Wink();
          matchRotation.LookAtCamera();
       }

       cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, Mathf.Lerp(35.0f, 60.0f, distance), Time.deltaTime * 1.5f);
	}
}
