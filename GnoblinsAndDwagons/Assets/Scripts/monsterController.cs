using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;

public class monsterController : MonoBehaviour, updatable
{
    public float speed;
    public float moveDist;
    private bool isMoving = false;

    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;

    public GameObject player;

    private Vector3 startPos;

    [SerializeField]
    public string monsterName;
    [SerializeField]
    public string monsterType;

    [SerializeField]
    public CombatStats enemyStats;

    [SerializeField]
    public int Agility;

    [SerializeField]
    public int Strength;

    [SerializeField]
    public int Toughness;

    [SerializeField]
    public int Dexterity;

    [SerializeField]
    GameStateMemory gameStateMemory;

    [SerializeField]
    public Dialog dialog;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        startPos = transform.position;
    }

    public void HandleUpdate()
    {
        if (!isMoving)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < 1)
            {
                triggerCombat();
            }
            else if(Vector3.Distance(player.transform.position, transform.position) <= 3 && Vector3.Distance(transform.position, startPos) <= moveDist)
            {
                if(isWalkable((player.transform.position + transform.position) / 2))
                {
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                }
                else 
                { 
                    randomWalk(); 
                }
            }
            else
            {
                randomWalk();
            }
        }
        
    }

    private void randomWalk()
    {
        Vector3Int direction = (Vector3Int)getRandomDirection();
        animator.SetFloat("moveX", direction.x);
        Vector3 targetPos = transform.position + direction;
        if (isWalkable(targetPos) && Vector3.Distance(targetPos, startPos) <= moveDist)
        {
            StartCoroutine(Move(targetPos));
        }
    }

    public void setPlayerObject(GameObject player)
    {
        this.player = player;
    }

    private void triggerCombat()
    {
        enemyStats.unitName = monsterName;
        enemyStats.type = monsterType;
        enemyStats.Agility = Agility * gameStateMemory.dungeonLevel;
        enemyStats.Strength = Strength * gameStateMemory.dungeonLevel;
        enemyStats.Toughness = Toughness * gameStateMemory.dungeonLevel;
        enemyStats.Dexterity = Dexterity * gameStateMemory.dungeonLevel;

        gameStateMemory.inDungeon = false;
        gameStateMemory.inCombat = true;
        gameStateMemory.leaveCombat = false;
        DialogManager.instance.showDialog(dialog, true, "combat");
        Destroy(gameObject);
        
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
