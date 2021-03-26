using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript: MonoBehaviour
{
    public float speed; //the players speed
    private int count; //scores the value to be displayed in score
    public Text countText;
    public Text winText; //flashes when we win
    public Text loseText; //flashes when we lose
    public GameObject reset; //this is the reset button in the canvas
    public GameObject nextlevel; //this is the next level button in the canvas
    public Text TimerText; //displays time remaining
    public float timeLeft; //the time left
    public float totalTime; //alloted time for the round
    public bool gameWon; //check to see if user has achieved goal
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        gameWon = false; //user starts the game in a losing stance
        totalTime = 21; //user has 21 seconds to complete the level
        SetCountText();
        winText.text = ""; //empty win text at start
        loseText.text = ""; //empty lose text at start
        TimerText.text = ""; //empty timer text at start
        timeLeft = totalTime; //sets the value of remaining time to the time we have left
        reset.SetActive(false); //at the beginning of the game, the reset button is invisible
        nextlevel.SetActive(false); //at the beginning of the game, the next level button is invisible
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");  //the movement for left and right
        float moveVertical = Input.GetAxis("Vertical");      //the movement for up and down

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);   //sets each direction to correct direction

        GetComponent<Rigidbody>().AddForce(movement * speed * Time.deltaTime); //use rigid body to get player moving
        TimerText.text = "Time Remaining: " + (int)timeLeft; //cpmverts time remaining to a string and stores it as timer text
        if (gameWon == false)
        {
            timeLeft -= Time.deltaTime; //increments the timer down
            
            if (timeLeft <= 0)
            {
                winText.text = "Oh Noes, You Lose";
                imdead();
            }
            if (count >= 3)
            {
                winText.text = "Congrats, You Win!";
                nextlevel.SetActive(true);
                gameWon = true;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pickup"))
        {
            other.gameObject.SetActive(false);   // Destroy(pickup.gameObject);
            count = count + 1;
            SetCountText();
        }
    }
    void SetCountText()
    {
        countText.text = "Score: " + count.ToString();
      
    }
    public void imdead()
    {
        gameObject.SetActive(false);
        reset.SetActive(true);   //this makes the button visible
    }
}
