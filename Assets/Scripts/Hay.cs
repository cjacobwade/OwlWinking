using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]

public class Hay : MonoBehaviour 
{
    private bool isBouncing = false;
    public void Bounce()
    {
        if (!isBouncing)
        {
            StartCoroutine(iBounce());
        }
    }

    IEnumerator iBounce()
    {
        isBouncing = true;
        
        float theTime = 0;
        while(theTime < 0.10f)
        {
            theTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(new Vector3(4.0f,2.0f,2.0f), new Vector3(3.5f, 1.5f, 0.5f), theTime / 0.10f);
            yield return null;
        }

        theTime = 0;
        while (theTime < 0.10f)
        {
            theTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(new Vector3(3.5f, 1.5f, 0.5f), new Vector3(5.0f, 3.0f, 3.0f), theTime / 0.10f);
            yield return null;
        }

        theTime = 0;
        while (theTime < 0.2f)
        {
            theTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(new Vector3(5.0f, 3.0f, 3.0f), new Vector3(4.0f, 2.0f, 2.0f), theTime / 0.2f);
            yield return null;
        }

        theTime = 0;

        isBouncing = false;
    }
}
