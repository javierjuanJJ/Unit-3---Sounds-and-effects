using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPosition;
    private float repeatWidth;
    private SpriteRenderer backgroundSpriteRenderer;
    public Sprite[] listBackgrounds;

    private void Awake()
    {
        startPosition = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
        backgroundSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < startPosition.x - repeatWidth)
        {
            transform.position = startPosition;
            backgroundSpriteRenderer.sprite = newBackground();
        }
    }
    
    private Sprite newBackground()
    {
        int obstacleIndex = Random.Range(0, listBackgrounds.Length);
        return listBackgrounds[obstacleIndex];
    }
}
