using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject pacman;
    public GameObject leftWarpNode;
    public GameObject rightWarpNode;

    public AudioSource siren;
    public AudioSource munch_1;
    public AudioSource munch_2;
    public int currentMunch = 0;

    public int score;
    public Text scoreText;
    
    void Awake()
    {
        score = 0;
        currentMunch = 0;
        siren.Play(); 
    }
        
    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: "+ score.ToString();
    }

    public void CollectedPellet(NodeController nodeController)
    {
        if (currentMunch == 0)
        {
            munch_1.Play();
            currentMunch = 1;
        }
        else if (currentMunch ==1)
        {
            munch_2.Play();
            currentMunch = 0;
        }
        //Add to our score
        AddToScore(10);

        //Check if there are any pellets left

        //Check how many pellets were eaten
    }
}
