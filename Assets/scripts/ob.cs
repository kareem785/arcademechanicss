using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ob : MonoBehaviour
{
    [SerializeField] private GameObject particle;
    [SerializeField] private GameObject explosion;
    [SerializeField] private string tag;
    public GameObject player;
    void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.tag == tag)
        {
            explosion = Instantiate(particle, transform.position, transform.rotation);
            Destroy(explosion, 2f);

            if (tag == "Object")
            {
                Destroy(player);
            }
        }

    }
}