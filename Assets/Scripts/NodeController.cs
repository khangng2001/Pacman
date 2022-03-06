using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    public bool canMoveLeft = false;
    public bool canMoveRight = false;
    public bool canMoveUp = false;
    public bool canMoveDown = false;

    public GameObject nodeLeft;
    public GameObject nodeRight;
    public GameObject nodeUp;
    public GameObject nodeDown;

    public bool isWarpRightNode = false;
    public bool isWarpLeftNode = false;

    //If the node contains a pellet when the game starts
    public bool isPelletNode = false;
    //If the node still has a pellet
    public bool hasPellet = false;

    public SpriteRenderer pellet;
    public GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        pellet = GetComponentInChildren<SpriteRenderer>();
        if (transform.childCount > 0)
        {
            isPelletNode = true;
            hasPellet = true;
        }
        // shoot raycast line going down
        RaycastHit2D[] hitsDown = Physics2D.RaycastAll(transform.position,Vector2.down);

        //Loop through all of the gameobjects that the raycast hits
        foreach (RaycastHit2D hit in hitsDown)
        {
            float distance = Mathf.Abs(hit.point.y - transform.position.y);
            if (distance<0.4f && hit.collider.tag =="Node")
            {
                canMoveDown = true;
                nodeDown = hit.collider.gameObject;
            }
        }

        // shoot raycast line going up
        RaycastHit2D[] hitsUp = Physics2D.RaycastAll(transform.position, Vector2.up);

        //Loop through all of the gameobjects that the raycast hits
        foreach (RaycastHit2D hit in hitsUp)
        {
            float distance = Mathf.Abs(hit.point.y - transform.position.y);
            if (distance < 0.4f && hit.collider.tag == "Node")
            {
                canMoveUp = true;
                nodeUp = hit.collider.gameObject;
            }
        }

        // shoot raycast line going right
        RaycastHit2D[] hitsRight = Physics2D.RaycastAll(transform.position, Vector2.right);

        //Loop through all of the gameobjects that the raycast hits
        foreach (RaycastHit2D hit in hitsRight)
        {
            float distance = Mathf.Abs(hit.point.x - transform.position.x);
            if (distance < 0.4f && hit.collider.tag == "Node")
            {
                canMoveRight = true;
                nodeRight = hit.collider.gameObject;
            }
        }

        // shoot raycast line going left
        RaycastHit2D[] hitsLeft = Physics2D.RaycastAll(transform.position, Vector2.left);

        //Loop through all of the gameobjects that the raycast hits
        foreach (RaycastHit2D hit in hitsLeft)
        {
            float distance = Mathf.Abs(hit.point.x - transform.position.x);
            if (distance < 0.4f && hit.collider.tag == "Node")
            {
                canMoveLeft = true;
                nodeLeft = hit.collider.gameObject;
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetNodeFromDirection(string direction)
    {
        if(direction == "left" && canMoveLeft)
        {
            return nodeLeft;
        }
        else if(direction == "right" && canMoveRight)
        {
            return nodeRight;
        }
        else if(direction == "down" && canMoveDown)
        {
            return nodeDown;
        }
        else if(direction == "up" && canMoveUp)
        {
            return nodeUp;
        }
        else
        {
            return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && hasPellet)
        {
            hasPellet = false;
            pellet.enabled = false;
            gameManager.CollectedPellet(this);
        }
    }
}
