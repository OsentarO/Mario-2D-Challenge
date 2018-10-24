using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private bool facingRight = true;
    private int count;
    public int numberofCoins;
   

    public float speed;
    public float jumpforce;
    public Text countText;
    public AudioClip jumpClip;
    public AudioClip pickupClip;

    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    //private float jumpTimeCounter;
    //public float jumpTime;
    //private bool isJumping;


    private AudioSource source;

 
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        SetCountText();
    }


    void Awake()
    {

       source = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");

        rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        Debug.Log(isOnGround);

        if (facingRight == false && moveHorizontal > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveHorizontal < 0)
        {
            Flip();
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)


        {
            if (Input.GetKey(KeyCode.UpArrow))
               
            {
                rb2d.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
                rb2d.velocity = Vector2.up * jumpforce;
                source.PlayOneShot(jumpClip);
            }
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("coin"))
        {
            other.gameObject.SetActive(false);
            count = count + 10;
            SetCountText();
            source.PlayOneShot(pickupClip);
        }

        if ((other.gameObject.CompareTag("CoinBox")) && (numberofCoins > 0))
        {
            numberofCoins = numberofCoins - 1;
            count = count + 10;
            SetCountText();
            source.PlayOneShot(pickupClip);
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }


}
