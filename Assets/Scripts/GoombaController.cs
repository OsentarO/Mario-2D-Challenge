using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GoombaController : MonoBehaviour {


    private Animator anim;

    public float speed;

   // private bool facingRight = false;

    public LayerMask isGround;
    public LayerMask isPlayer;
    public Transform wallHitBox;
    public Transform playerHitBox;

    private bool wallHit;
    private bool playerhit;
    public float wallHitHeight;
    public float wallHitWidth;
    public float playerHitHeight;
    public float playerHitWidth;

    private AudioSource source;
    public AudioClip deathClip;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;



    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isDead", false);
        playerhit = false;
    }

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);

        wallHit = Physics2D.OverlapBox(wallHitBox.position, new Vector2(wallHitWidth, wallHitHeight), 0, isGround);

        if (wallHit == true)
        {
            speed = speed * -1;
        }

       /* if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
         {
             anim.SetBool("isDead", true);
             speed = 0;
         }
         else
         {
             transform.Translate(speed * Time.deltaTime, 0, 0);
             anim.SetBool("IsDead", false);

             wallHit = Physics2D.OverlapBox(wallHitBox.position, new Vector2(wallHitWidth, wallHitHeight), 0, isGround);
             if (wallHit == true)
             {
                 speed = speed * -1;
             }

             playerhit = Physics2D.OverlapBox(playerHitBox.position, new Vector2(playerHitWidth, playerHitHeight), 0, isPlayer);
             Debug.Log("playerhit is" + playerhit);
         }*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && playerhit == true)
        {
            source.PlayOneShot(deathClip);
            anim.SetBool("isDead", true);
            //facingRight = !facingRight;
            //speed = speed * -1;
            Debug.Log("goomba dead");
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(wallHitBox.position, new Vector3(wallHitWidth, wallHitHeight, 1));
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(playerHitBox.position, new Vector3(playerHitWidth, playerHitHeight, 1));
    }
}