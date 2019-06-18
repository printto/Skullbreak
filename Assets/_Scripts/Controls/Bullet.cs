using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{

    public float ExplosionRadius = 20f;
    public float ExlposionForce = 600f;
    private Rigidbody rb;

    //Audio sources
    public AudioClip[] ShootSounds;
    AudioSource audio;
    bool dieSoundPlayed = false;

    ParticleSystem emit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ParticleSystem system = GetComponent<ParticleSystem>();
        audio = GetComponent<AudioSource>();
        if(ShootSounds.Length != 0)
        {
            int randomed = Random.Range(0, ShootSounds.Length);
            audio.PlayOneShot(ShootSounds[randomed]);
        }
        Invoke("destroyBullet", 3);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("PlayerFace"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>(), true);
        }
        if (collision.gameObject.tag.Equals("Monster"))
        {
            destroyBullet();
        }
    }

    private void destroyBullet()
    {
        gameObject.transform.position = new Vector3(0, -99999, 0);
        Destroy(gameObject, 1.5f);
    }

}