using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Paddle paddle1;
    [SerializeField] private float xPush = 2f;
    [SerializeField] private float yPush = 15f;
    [SerializeField] private float randomFactor = 1f;
    private Vector2 _paddleToBallVector;
    private bool _hasStarted;
    private Rigidbody2D _myRigidBody2D;
    private AudioSource _myAudioSource;

    private void Start()
    {
        _paddleToBallVector = transform.position - paddle1.transform.position;
        _myRigidBody2D = GetComponent<Rigidbody2D>();
        _myAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_hasStarted) return;
        LockBallToPaddle();
        LaunchOnMouseClick();
    }

    private void LaunchOnMouseClick()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        _myRigidBody2D.velocity = new Vector2(xPush, yPush);
        _hasStarted = true;
    }

    private void LockBallToPaddle()
    {
        var currPaddlePos = paddle1.transform.position;
        var newPaddlePos = new Vector2(currPaddlePos.x, currPaddlePos.y);
        transform.position = newPaddlePos + _paddleToBallVector;
    }

    private void OnCollisionEnter2D()
    {
        var velocityTweak = new Vector2(UnityEngine.Random.Range(0f, randomFactor),
            UnityEngine.Random.Range(0f, randomFactor));
        if (!_hasStarted) return;
        _myAudioSource.Play();
        _myRigidBody2D.velocity += velocityTweak;
    }
}