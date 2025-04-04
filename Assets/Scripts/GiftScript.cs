using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftScript : MonoBehaviour
{
    public GameObject metalPipe;

    void OnTriggerEnter(Collider bullet)
    {
        Instantiate(metalPipe, new Vector3(0f, 4f, 5f), Quaternion.Euler(0, 90, 0));

        Destroy(this.gameObject);
        Destroy(bullet.gameObject);
    }
}
