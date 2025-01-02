using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CannoBall : MonoBehaviour
{
    public GameObject CannonExplosinEffect;

    public void OnCollisionEnter(Collision collision)
    {
        if (CannonExplosinEffect != null)
        {
            GameObject effect = Instantiate(CannonExplosinEffect, transform.position, quaternion.identity);

            Destroy(effect, 1f);
        }
        Destroy(gameObject);
    }
}
