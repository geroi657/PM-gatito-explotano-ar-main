using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalPipeScript : MonoBehaviour
{
    private bool started = false;

    void Start()
    {
        if (GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true;
        }
    }

    void Update()
    {
        if(transform.position.y <= -0.5f)
        {
            if (!started)
            {
                StartCoroutine(PerformAction());
                started = true;
            }
            return;
        }

        transform.Translate(new Vector3(0, -0.05f, 0));
    }

    IEnumerator PerformAction()
    {
        GetComponent<AudioSource>().Play(0);

        yield return new WaitForSeconds(0.5f);

        foreach (GatitoScript gatito in FindObjectsOfType<GatitoScript>())
        {
            Destroy(gatito.gameObject);
        }

        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
