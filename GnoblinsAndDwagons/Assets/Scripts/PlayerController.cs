using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float scale;
    private bool isMoving = false;

    private Vector2 input;
    private Animator animator;
    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;

    [SerializeField] GameStateMemory gameState;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Vector3 startPos;

        if (gameState.inDungeon)
        {
            startPos = new Vector3(0f, 0f, 0f);
        }
        else if (gameState.leaveDungeon)
        {
            startPos = new Vector3(14f, 3.6f, 0f);
        }
        else if (gameState.leaveShop)
        {
            startPos = new Vector3(-4.7f,-2.8f,0f);
        }
        else
        {
            startPos = new Vector3(-16f, 5.5f, 0f);
        }
        transform.position = startPos;
    }

    public void HandleUpdate()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);
                var targetPos = transform.position;
                targetPos.x += input.x * scale;
                targetPos.y += input.y * scale;
                
                if (isWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
            }
        }

        animator.SetBool("isMoving", isMoving);

        if(Input.GetKeyDown(KeyCode.E)) 
        {
            interact();
        }
        
    }

    void interact()
    {
        Vector3 faceDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + 2*faceDir*scale;
        
        Debug.DrawLine(transform.position, interactPos, Color.red, 0.5f);

        var collider = Physics2D.OverlapCircle(interactPos, scale, interactableLayer);
        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }

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
