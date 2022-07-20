using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{

    public GameObject[] listObstacles;

    private Vector3 obstaclePosition;

    public float startDelay, repeatDelay;
    
    public PlayerController playerControllerScript;

    private void Awake()
    {
        obstaclePosition = new Vector3(25, 0, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateObstacle",startDelay,repeatDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateObstacle()
    {
        if (!playerControllerScript.GameOver)
        {
            int obstacleIndex = Random.Range(0, listObstacles.Length);
            GameObject obstacle = listObstacles[obstacleIndex];
            Instantiate(obstacle, obstaclePosition, obstacle.transform.rotation);
        }
    }
}
