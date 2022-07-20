using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRb;
    public KeyCode jumpKey;
    public float jumpForce, gravityModifier, doubleJumpForce;
    private bool isOnGround, gameOver, doubleJumpUsed, doubleSpeed;
    private Animator playerAnimator;
    public ParticleSystem explosionParticleSystem,dirtParticleSystem;
    public AudioClip jumpSound, crashSound;
    private AudioSource playerAudio;
    
    private void Awake()
    {
        isOnGround = true;
        gameOver = false;
        doubleJumpUsed = false;
        doubleSpeed = false;
        playerRb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(jumpKey) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAudio.PlayOneShot(jumpSound,1.0f);
            isOnGround = false;
            playerAnimator.SetTrigger("Jump_trig");
            dirtParticleSystem.Stop();
            doubleJumpUsed = false;
        }
        else if (Input.GetKeyDown(jumpKey) && !isOnGround && !doubleJumpUsed)
        {
            doubleJumpUsed = true;
            playerRb.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
            playerAnimator.Play("Running_Jump",3,0f);
            playerAudio.PlayOneShot(jumpSound,1.0f);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            float speedValue = doubleSpeed ? 1.0f : 2.0f;
            playerAnimator.SetFloat("Speed_Multiplier", speedValue);
            doubleSpeed = !doubleSpeed;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collisionGameObject = collision.gameObject;
        
        if (collisionGameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            
            dirtParticleSystem.Play();
        }
        
        else if (collisionGameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            
            playerAnimator.SetBool("Death_b",true);
            playerAnimator.SetInteger("DeathType_int",1);
            
            playerAudio.PlayOneShot(crashSound,1.0f);
            
            explosionParticleSystem.Play();
            dirtParticleSystem.Stop();
            
            Debug.Log("Game Over!");
        }
    }

    public Animator PlayerAnimator => playerAnimator;

    public bool GameOver
    {
        get => gameOver;
        set => gameOver = value;
    }

    public bool DoubleSpeed => doubleSpeed;
}
