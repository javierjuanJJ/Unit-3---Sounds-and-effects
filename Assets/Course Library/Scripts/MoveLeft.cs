using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{

    public float speed = 30;
    private PlayerController playerControllerScript;
    public float leftBound = -15;

    private void Awake()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerControllerScript.GameOver)
        {

            if (!playerControllerScript.DoubleSpeed)
            {
                float speedMultiplier = playerControllerScript.PlayerAnimator.GetFloat("Speed_Multiplier");
                transform.Translate(Vector3.left * (speed * speedMultiplier * Time.deltaTime));
            }
            else
            {
                transform.Translate(Vector3.left * (speed * Time.deltaTime));
            }
            
            
        }

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
        
    }
}
