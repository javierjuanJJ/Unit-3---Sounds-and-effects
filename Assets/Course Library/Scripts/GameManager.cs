using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    private float score;
    private PlayerController playerControllerScript;
    
    public Transform startingPoint;
    public float lerpSpeed;
    
    public float Score => score;

    // Start is called before the first frame update

    private void Start()
    {
        
    }

    void Awake()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        score = 0;
        playerControllerScript.GameOver = true;
        StartCoroutine(PlayIntro());
    }

    private IEnumerator PlayIntro()
    {
        Vector3 startPos = playerControllerScript.transform.position;
        Vector3 endPos = startingPoint.position;
        Animator animator = playerControllerScript.GetComponent<Animator>();
        
        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;
        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;
        
        animator.SetFloat("Speed_Multiplier", 0.5f);
        
        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            fractionOfJourney = distanceCovered / journeyLength;
            
            playerControllerScript.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            
            yield return null;
        }
       
        animator.SetFloat("Speed_Multiplier", 1.0f);
        playerControllerScript.GameOver = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerControllerScript.GameOver)
        {
            score += !playerControllerScript.DoubleSpeed ? 2 : 1;
            Debug.Log("Score: " + score);
        }
    }
}
