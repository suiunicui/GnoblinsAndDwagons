using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, Interactable, updatable
{
    [SerializeField] Dialog dialog;

    public float speed;
    public float moveDist;
    private bool isMoving = false;

    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;

    private Vector3 startPos;

    public void Interact()
    {
        DialogManager.instance.showDialog(dialog);
    }

    void Start()
    {
        startPos = transform.position;
    }

    public void HandleUpdate()
    {
        if (!isMoving)
        {
            Vector3Int direction = (Vector3Int)getRandomDirection();
            Vector3 targetPos = transform.position + direction;

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
