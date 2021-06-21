using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody PlayerRB;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAuido;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver=false;
    
    void Start()
    {
        PlayerRB = GetComponent<Rigidbody>();
        playerAuido = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        playerAnim = GetComponent<Animator>();
        
        
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround&&!gameOver)
        {
            PlayerRB.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAuido.PlayOneShot(jumpSound, 1f);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAuido.PlayOneShot(crashSound, 1f);

                
        }
        
    }
}
