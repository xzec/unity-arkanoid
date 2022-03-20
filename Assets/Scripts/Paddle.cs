using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float screenWidthInUnits = 16f;
    [SerializeField] private float minX = 1.25f;
    [SerializeField] private float maxX = 14.75f;
    private GameStatus _theGameStatus;
    private Ball _theBall;

    private void Start()
    {
        _theGameStatus = FindObjectOfType<GameStatus>();
        _theBall = FindObjectOfType<Ball>();
    }

    private void Update()
    {
        var mousePosInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;
        mousePosInUnits = Mathf.Clamp(mousePosInUnits, minX, maxX);
        var paddlePos = new Vector2(mousePosInUnits, transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);
        transform.position = paddlePos;
    }

    private float GetXPos()
    {
        if (_theGameStatus.IsAutoPlayEnabled()) return _theBall.transform.position.x;
        return Input.mousePosition.x / Screen.width * screenWidthInUnits;
    }
}