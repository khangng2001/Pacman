using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum GhostNodeStateEnum {
        
        respawning,
        leftNode,
        rightNode,
        startNode,
        movingInNodes,
        centerNode
    }

    public GhostNodeStateEnum ghostNodeState;
    public enum GhostType
    {
        red, blue, pink, orange
    }

    public GhostType ghostType;

    public GameObject ghostNodeLeft;
    public GameObject ghostNodeRight;
    public GameObject ghostNodeCenter;
    public GameObject ghostNodeStart;

    public MovementController movementController;

    public GameObject startingNode;

    public bool readytoLeaveHome = false;

    public GameManager gameManager;
     
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        movementController = GetComponent<MovementController>();
        if (ghostType == GhostType.red)
        {
            ghostNodeState = GhostNodeStateEnum.startNode;
            startingNode = ghostNodeStart;
            readytoLeaveHome = true;
        }
        else if (ghostType == GhostType.pink)
        {
            ghostNodeState = GhostNodeStateEnum.centerNode;
            startingNode = ghostNodeCenter;
        }
        else if (ghostType == GhostType.blue)
        {
            ghostNodeState = GhostNodeStateEnum.leftNode;
            startingNode = ghostNodeLeft;
        }
        else if (ghostType == GhostType.orange)
        {
            ghostNodeState = GhostNodeStateEnum.rightNode;
            startingNode = ghostNodeRight;
        }
        movementController.currentNode = startingNode;
        transform.position = startingNode.transform.position;
    }

    
    void Update()
    {
        
    }

    public void ReachedCenterOfNode(NodeController nodeController)
    {
        if (ghostNodeState == GhostNodeStateEnum.movingInNodes)
        {
            //Determine next gameNode to go to
            if (ghostType == GhostType.red)
                DitermineRedGhostDirection(); 
        }
        else if (ghostNodeState == GhostNodeStateEnum.respawning)
        {
            //Determine quickest direction to home
        }
        else
        {
            // if we're able to leave home
            if (readytoLeaveHome)
            {
                // Even we're left or right, we go to center 
                if (ghostNodeState == GhostNodeStateEnum.leftNode)
                {
                    ghostNodeState = GhostNodeStateEnum.centerNode;
                    movementController.SetDirection("right");
                }
                else if (ghostNodeState == GhostNodeStateEnum.rightNode)
                {
                    ghostNodeState = GhostNodeStateEnum.centerNode;
                    movementController.SetDirection("left");
                }
                //if in center, go to startNode
                else if (ghostNodeState == GhostNodeStateEnum.centerNode)
                {
                    ghostNodeState = GhostNodeStateEnum.startNode;
                    movementController.SetDirection("up");
                }
                // if in startNode, determine next gameNode to go
                else if (ghostNodeState == GhostNodeStateEnum.startNode)
                {
                    ghostNodeState = GhostNodeStateEnum.movingInNodes;
                    movementController.SetDirection("left");
                }
            }
        }
    }

    private void DitermineRedGhostDirection()
    {
        string direction = GetClosetDirection(gameManager.pacman.transform.position);
        movementController.SetDirection(direction);
    }
    private void DiterminePinkGhostDirection()
    {

    }
    private void DitermineBlueGhostDirection()
    {

    }
    private void DitermineOrangeGhostDirection()
    {

    }

    string  GetClosetDirection(Vector2 target)
    {
        float shortestDistance = 0;
        string lastMovingDirection = movementController.lastMovingDirrection;
        string newDirection = "";
        NodeController nodeController = movementController.currentNode.GetComponent<NodeController>();

        //If we can move up, and not reversing
        if (nodeController.canMoveUp && lastMovingDirection != "down")
        {
            //Get the node above us
            GameObject nodeUp = nodeController.nodeUp;
            //Get the distance between top node, and pacman
            float distance = Vector2.Distance(nodeUp.transform.position, target);
            if (distance < shortestDistance || shortestDistance ==0)
            {
                shortestDistance = distance;
                newDirection = "up";
            }
        }

        //If we can move down, and not reversing
        if (nodeController.canMoveDown && lastMovingDirection != "up")
        {
            //Get the node below us
            GameObject nodeDown = nodeController.nodeDown;
            //Get the distance between above node, and pacman
            float distance = Vector2.Distance(nodeDown.transform.position, target);
            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "down";
            }
        }

        //If we can move left, and not reversing
        if (nodeController.canMoveLeft && lastMovingDirection != "right")
        {
            //Get the node left to us
            GameObject nodeLeft = nodeController.nodeLeft;
            //Get the distance between left node, and pacman
            float distance = Vector2.Distance(nodeLeft.transform.position, target);
            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "left";
            }
        }

        //If we can move right, and not reversing
        if (nodeController.canMoveRight && lastMovingDirection != "left")
        {
            //Get the node right to us
            GameObject nodeRight = nodeController.nodeRight;
            //Get the distance between right node, and pacman
            float distance = Vector2.Distance(nodeRight.transform.position, target);
            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "right";
            }
        }

        return newDirection;
    }
}
