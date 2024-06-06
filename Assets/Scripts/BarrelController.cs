using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
    private Transform barrelT;
    private Rigidbody barrelRB;
    private Vector3 direction;

    private int lastFloorID;
    private bool flying;

    public float speed;
    public float explosionForce;
    public ParticleSystem particleOnImpact;
    public GameObject audioSourceGO;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        barrelT = gameObject.transform;
        barrelRB = gameObject.GetComponent<Rigidbody>();

        audioSource = Instantiate(audioSourceGO).GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.flying)
        {
            barrelRB.AddForce(Time.deltaTime * this.speed * direction, ForceMode.Force);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        float collisionY = collision.GetContact(0).point.y;
        if (collision.collider.CompareTag("Floor") && collisionY < barrelT.position.y)
        {
            this.direction = collision.transform.forward;
            if (Vector3.Dot(direction, Vector3.up) > 0)
            {
                this.direction *= -1;
            }
            this.lastFloorID = collision.collider.GetInstanceID();
            this.flying = false;
        } else if (collision.collider.CompareTag("Player"))
        {
            Vector3 pushDirection = (collision.transform.position - barrelT.transform.position);
            pushDirection.y = 2;
            pushDirection.Normalize();
            collision.gameObject.GetComponent<Rigidbody>().AddForce(explosionForce * pushDirection, ForceMode.Impulse);
            collision.gameObject.GetComponent<PlayerController>().Stun();
            //particleOnImpact.transform.position = transform.position;
            //particleOnImpact.transform.rotation = Quaternion.identity;
            Instantiate(particleOnImpact, transform.position, Quaternion.identity);
            audioSource.Play();

            ObjectPooler.Instance.Destroy("Barrel", gameObject);
        } 
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.GetInstanceID() == this.lastFloorID)
        {
            this.flying = true;
        }
    }
}
