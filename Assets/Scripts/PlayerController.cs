using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform playerT;
    private Rigidbody playerRB;
    private Vector3 spawnPosition;
    private Animator playerAnimator;

    private GameObject currentFloor;
    private bool isStunned = false;
    private bool canJump = false;

    [SerializeField] private float speed = 15;
    [SerializeField] public float jumpForce = 300;
    [SerializeField] private float gravityForce = 20000;

    // Start is called before the first frame update
    void Start()
    {
        playerT = this.gameObject.transform;
        playerRB = this.gameObject.GetComponent<Rigidbody>();
        spawnPosition = playerT.position;
        playerAnimator =  GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStunned)
        {
            Vector3 forwardDirection;
            if (this.currentFloor != null)
            {
                forwardDirection = currentFloor.transform.forward;
            }
            else
            {
                forwardDirection = playerT.forward;
            }

            if (Vector3.Dot(forwardDirection, Vector3.forward) < 0)
            {
                forwardDirection *= -1;
            }

            float hAxis = Input.GetAxis("Horizontal");
            if (hAxis > 0)
            {
                playerT.LookAt(playerT.position + 15 * Vector3.forward);
            }
            else if (hAxis < -0)
            {
                playerT.LookAt(playerT.position + 15 * Vector3.back);
            }
            playerT.position += Time.deltaTime * speed * hAxis * forwardDirection;
            playerAnimator.SetBool("Run", Mathf.Abs(hAxis) > 0.2 && canJump);

            if (Input.GetKeyDown(KeyCode.Space) && this.canJump)
            {
                playerRB.AddForce(playerT.up * jumpForce, ForceMode.Impulse);
                this.canJump = false;
                playerAnimator.Play("Jump");
                playerAnimator.SetBool("Land", false);
            }

        }
        if (playerT.position.y < -10)
        {
            this.Respawn();
        }

        if (!this.currentFloor)
        {
            playerRB.AddForce(-this.gravityForce * Time.deltaTime * playerT.up, ForceMode.Force);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        float collisionY = collision.GetContact(0).point.y;
        if (collision.collider.CompareTag("Floor") && collisionY < playerT.position.y)
        {
            currentFloor = collision.collider.gameObject;
            playerRB.velocity = Vector3.zero;
            isStunned = false;
            playerAnimator.SetBool("Stun", false);
        }
        if (collisionY < playerT.position.y)
        {
            canJump = true;
            playerAnimator.SetBool("Land", true);
        }
    }

    public void Respawn()
    {
        playerT.position = spawnPosition;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Floor") && this.currentFloor && this.currentFloor.GetInstanceID() == collision.collider.gameObject.GetInstanceID())
        {
            currentFloor = null;
        }
    }

    public void Stun()
    {
        this.isStunned = true;
        playerAnimator.SetBool("Stun", true);
        playerAnimator.Play("Fall");
    }
}
