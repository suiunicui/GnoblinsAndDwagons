using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterController : MonoBehaviour, updatable
{
    public float speed;
    public float moveDist;
    private bool isMoving = false;

    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;

    public PlayerController player;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    public void HandleUpdate()
    {

        if (Vector3.Distance(player.transform.position, transform.position) < 1)
        {
            Debug.Log("Start combat");
        }
        else if (!isMoving)
        {
            Vector3 targetPos = new Vector3();
            if (Vector3.Distance(player.transform.position, transform.position) < 3)
            {
                targetPos = player.transform.position;
            }
            else
            {
                Vector3Int direction = (Vector3Int)getRandomDirection();
                targetPos = transform.position + direction;
            }
            if (isWalkable(targetPos) && Vector3.Distance(targetPos, startPos) <= moveDist)
            {
                StartCoroutine(Move(targetPos));
            }
        }
    }
    private static Vector2Int getRandomDirection()
    {
        return cardinalDirList[Random.Range(0, cardinalDirList.Count)];
    }

    private static List<Vector2Int> cardinalDirList = new List<Vector2Int>()
    {
        new Vector2Int(0,1), //up
        new Vector2Int(1,0), //right
        new Vector2Int(0,-1),//down
        new Vector2Int(-1,0), //left
    };

    private bool isWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) != null || Physics2D.OverlapCircle(targetPos, 0.2f, interactableLayer) != null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }

}
