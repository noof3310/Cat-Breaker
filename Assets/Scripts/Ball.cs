using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Paddle paddle;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;

    Vector2 paddleToBallV;

    bool hasStarted = false;

    AudioSource myAudioSource;
    Rigidbody2D myRigidbody2D;

    float initialVelocitySqr;
    float velocitySqr;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallV = transform.position - paddle.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
        else
        {
            velocitySqr = myRigidbody2D.velocity.sqrMagnitude;
            if (velocitySqr < initialVelocitySqr)
            {
                myRigidbody2D.velocity
                    = new Vector2(myRigidbody2D.velocity.x * 1.1f, myRigidbody2D.velocity.y * 1.1f);

            }else if (velocitySqr > initialVelocitySqr * 1.5f)
            {
                myRigidbody2D.velocity
                    = new Vector2(myRigidbody2D.velocity.x * 0.9f, myRigidbody2D.velocity.y * 0.9f);
            }
        }
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            myRigidbody2D.velocity = new Vector2(xPush, yPush);
            initialVelocitySqr = myRigidbody2D.velocity.sqrMagnitude;
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePos + paddleToBallV;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2
            (Random.Range(0, randomFactor),
            Random.Range(0, randomFactor));

        AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
        myAudioSource.PlayOneShot(clip);

        myRigidbody2D.velocity += velocityTweak;
    }
}
