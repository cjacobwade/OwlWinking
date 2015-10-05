using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class OwlTech : MonoBehaviour 
{
    public Transform owlBounce;
    public SkinnedMeshRenderer skinnedMeshRenderer;
	public HeadLookAt headLookAt;
    public ParticleSystem twinkleSystem;
    public AudioClip twinkleClip;

    private bool bouncing = false;

    private Vector3 inputVector;
    private float inputSpeed = 500.0f; // CHANGED TO 500
    private float drag = 0.75f;

    private float closeAmount = 0;

    private Rigidbody rigidbody;
    private Animator animator;
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
       if (Input.GetKeyDown(KeyCode.F5))
       {
          Application.LoadLevel(Application.loadedLevel);
       }

		// CHANGED THIS FROM += TO =
        inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime * inputSpeed;
    }

    void FixedUpdate()
    {
		// REMOVED DRAG REFERENCE
        rigidbody.AddForce(inputVector);

		// MOVED THIS STUFF INTO FIXED UPDATE FROM UPDATE

		// cloase the thang
		float hitDistance = 0;
		RaycastHit hit;
		Ray ray = new Ray(transform.position + new Vector3(0, -0.51f, 0), Vector3.down);
		
		if (Physics.Raycast(ray,out hit,3))
		{
			hitDistance = 1 - Mathf.Clamp01(Vector3.Distance(transform.position + new Vector3(0, -1f, 0), hit.point) / 3);
			if (hitDistance < 0.1)
			{
				hitDistance = 0;
			}
		}
		closeAmount = Mathf.Lerp(closeAmount, hitDistance, Time.deltaTime * 7);
		animator.Play("Close", 0, Mathf.Clamp01(closeAmount));
		animator.speed = 0;
    }

    void OnCollisionEnter(Collision collision)
    {
        Winston w = collision.gameObject.GetComponent<Winston>();
        Hay h = collision.gameObject.GetComponent<Hay>();

        if (w != null)
        {
            w.PlayAction();
            //rigidbody.AddForce(Vector3.up * ((12 - Mathf.Clamp(collision.relativeVelocity.y, 3.0f, 10.0f)) * 70));
            rigidbody.AddForce(Vector3.up * ((Mathf.Clamp(collision.relativeVelocity.y, 5.0f, 10.0f)) * 60));
            Debug.Log(collision.relativeVelocity.y);
        }
        else if (h != null)
        {
            h.Bounce();
            Debug.Log(collision.relativeVelocity.magnitude);
            rigidbody.AddForce(collision.contacts[0].normal * (Mathf.Clamp(collision.relativeVelocity.magnitude, 5.0f, 10.0f) * 40.0f));
        }
        else
        {
            rigidbody.AddForce(Vector3.up * ((Mathf.Clamp(collision.relativeVelocity.y, 0.0f, 15.0f)) * 15));
        }
    }

    private bool isWinking = false;
    public void Wink()
    {
       if(!isWinking)
         StartCoroutine(iWink());
    }

    IEnumerator iWink()
    {
	// ADDED HEAD LOOKAT
       isWinking = true;
	   headLookAt.enabled = true;
       yield return new WaitForSeconds(0.45f);
       float theTime = 0;
       while (theTime < 0.15f)
       {
          theTime += Time.deltaTime;
          skinnedMeshRenderer.SetBlendShapeWeight(0, Mathf.Lerp(0.0f,100.0f, theTime / 0.1f));
          yield return null;
       }

       // play sounds
       // play particle effect

       twinkleSystem.Emit(1);
       AudioSource.PlayClipAtPoint(twinkleClip, transform.position);

       yield return new WaitForSeconds(0.3f);
       theTime = 0;
       while (theTime < 0.1f)
       {
          theTime += Time.deltaTime;
          skinnedMeshRenderer.SetBlendShapeWeight(0, Mathf.Lerp(100.0f, 0.0f, theTime / 0.1f));
          yield return null;
       }

       yield return new WaitForSeconds(1.0f);
	   headLookAt.enabled = false;
       isWinking = false;
    }
}
