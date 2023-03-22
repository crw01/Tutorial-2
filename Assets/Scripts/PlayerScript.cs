using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text lives;
    public Text win;
    private int scoreValue = 0;
    private int livesValue = 3;
    private int winValue = 3;

    //audio shtuff
    public AudioSource musicSource;
    public AudioClip backgroundMusic;
    public AudioClip loserMusic;
    public AudioClip winnerMusic;


    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        lives.text = "Lives: " + livesValue.ToString();
        win.text = " ";

        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    // FixedUpdate is update but with anything having to do with physics
    void FixedUpdate()
    {
        //controller settings (up/down arrows or awsd)
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");
   
        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            Destroy(collision.collider.gameObject);
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
        }
 
        if(scoreValue == 9)   
        {
            win.text = "You Win! Game Created by Riley Whitfield <" + winValue.ToString();
            musicSource.clip = winnerMusic;
            musicSource.Play();
        }

        if(collision.collider.tag == "Enemy")
        {
            livesValue = livesValue -1;
            lives.text = "Lives: " + livesValue.ToString();
        }

        if(livesValue == 0)
        {
            win.text = "You Lose </" + winValue.ToString();
            musicSource.clip = backgroundMusic;
            musicSource.Stop();
            musicSource.clip = loserMusic;
            musicSource.Play();
        }
  
        if(scoreValue == 4)
        {
            transform.position = new Vector3(30.0f, 0.0f, 0.0f);
            livesValue = 3;
            lives.text = "Lives: " + livesValue.ToString();
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

} 