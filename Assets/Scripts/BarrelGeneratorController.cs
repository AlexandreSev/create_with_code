using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelGeneratorController : MonoBehaviour
{

    public GameObject barrel;

    private Vector3 spawnPosition;
    private Quaternion spawnRotation;
    public float spawnInterval = 10.0f;

    public float barrelSpeed;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position + 2 * Vector3.down;
        spawnRotation = barrel.transform.rotation;
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(SpawnObject());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnObject()
    {
        while (true)
        {
            GameObject spawned = ObjectPooler.Instance.SpawnFromPool("Barrel", spawnPosition, spawnRotation);
            audioSource.Play();

            Rigidbody spawnedrb = spawned.GetComponent<Rigidbody>();
            spawnedrb.velocity = Vector3.zero;
            spawnedrb.angularVelocity = Vector3.zero;
            spawnedrb.GetComponent<BarrelController>().speed = barrelSpeed;


            // Wait for the specified interval before the next spawn
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void MoreBarrel()
    {
        this.spawnInterval *= 0.9f;
    }

    public void SpeedUp()
    {
        this.barrelSpeed *= 1.1f;
    }

}
