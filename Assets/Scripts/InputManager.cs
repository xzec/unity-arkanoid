using System.Collections;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private bool _shaking;
    private float _shakingOffset = 5f;
    private GameStatus _gameStatus;

    private void Start()
    {
        _gameStatus = FindObjectOfType<GameStatus>();
    }

    public void ReadStringInput(string value) => _gameStatus.NickName = value;

    private void Update()
    {
        if (_shaking) PerformShaking();
    }

    private void PerformShaking()
    {
        _shakingOffset *= -1;
        var t = transform;
        var oldPos = t.position;
        var newPos = new Vector3(oldPos.x + _shakingOffset, oldPos.y, oldPos.z);
        t.position = newPos;
    }

    public void Shake() => StartCoroutine(nameof(ShakeNow));

    private IEnumerator ShakeNow()
    {
        var originalPos = transform.position;

        if (_shaking == false) _shaking = true;

        yield return new WaitForSeconds(0.25f);

        _shaking = false;
        transform.position = originalPos;
    }
}

