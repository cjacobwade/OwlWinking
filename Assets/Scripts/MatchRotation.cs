using UnityEngine;
using System.Collections;

public class MatchRotation : MonoBehaviour 
{
	public AnimationCurve winkTimeCurve = new AnimationCurve();
    public Transform target;
    public Camera cam;

    private bool looking = false;

	void Update () 
    {
       if (!looking)
       {
          transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime * 6);
       }
	}

    public void LookAtCamera()
    {
        if (!looking)
		{
         	StartCoroutine( iLookAtCamera() );
			StartCoroutine( iLookTimeDilation() );
		}
    }

	IEnumerator iLookTimeDilation()
	{
		float timer = 0f;
		float time = 0.45f;

		while( timer < time )
		{
			Time.timeScale = 1f - winkTimeCurve.Evaluate( timer/time ) * 0.5f;
			timer += Time.deltaTime;

			yield return null;
		}
	}

    IEnumerator iLookAtCamera()
    {
         looking = true;
         float targetTime = 0.25f;
         float theTime = 0;
         while (theTime < targetTime)
         {
            theTime += Time.deltaTime;
			// CHANGED DOUBLE LERP TO SINGLE LERP
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(cam.transform.position - this.transform.position, Vector3.up), theTime / targetTime);
            yield return null;
         }

         float nextTime = 0;
         while (nextTime < 0.2f)
         {
            nextTime += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(cam.transform.position - this.transform.position, Vector3.up), Time.deltaTime * 6);
         }

         yield return null;
         looking = false;
    }
}
