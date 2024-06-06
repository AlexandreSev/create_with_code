using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUpgrader : MonoBehaviour
{
    public GameObject upgradeObj;
    private ShopUpgrade upgrade;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        upgrade = upgradeObj.GetComponent<ShopUpgrade>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            upgrade.Upgrade();
            audioSource.Play();
            other.GetComponent<PlayerController>().Respawn();
            return;
        }
    }

}
