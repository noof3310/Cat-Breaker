﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float minPaddlePosInUnits = 1f;
    [SerializeField] float maxPaddlePosInUnits = 15f;

    GameSession gameSession;
    Ball ball;

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), minPaddlePosInUnits, maxPaddlePosInUnits);
        transform.position = paddlePos;
    }

    private float GetXPos()
    {
        if (gameSession.IsAutoPlayEnabled()) return ball.transform.position.x;
        else return Input.mousePosition.x / Screen.width * screenWidthInUnits;
    }
}
