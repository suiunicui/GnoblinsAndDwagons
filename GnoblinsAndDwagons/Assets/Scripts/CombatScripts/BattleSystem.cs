using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BattleSystem : MonoBehaviour
{
	public event Action StartCombat;

	public static BattleSystem instance { get; private set;}
	
	public enum BattleState { Start, PlayerTurn, EnemyTurn, Won, Lost, Fled }

	private enum PlayerAction
	{
		NormalAttack,
		HeavyAttack,
		QuickAttack,
		Flee
	}
	[SerializeField] public Dialog victorydialog;
	[SerializeField] public Dialog defeatdialog;
	[SerializeField] public Dialog fleddialog;
	
	public GameObject playerPrefab;
	public GameObject enemyPrefab;
	
	public Transform playerBattleStation;
	public Transform enemyBattleStation;

	private CombatUnit playerUnit;
	private CombatUnit enemyUnit;

	public Slider playerHealthSlider;
	public Slider enemyHealthSlider;
	
	public Text enemyName;
	public Text playerName;
	
	public BattleState state;


	private void Awake()
	{
		instance = this;
	}
	// Start is called before the first frame update
	private void Start()
	{
		Debug.Log("hi");
		
		state = BattleState.Start;
		StartCombat?.Invoke();
		StartCoroutine(SetupBattle());
	}

	private IEnumerator SetupBattle()
	{
		var playerObject = Instantiate(playerPrefab, playerBattleStation);
		playerUnit = playerObject.GetComponent<CombatUnit>();
		playerName.text = playerUnit.unitName;
	    
		var enemyObject = Instantiate(enemyPrefab, enemyBattleStation);
		enemyUnit = enemyObject.GetComponent<CombatUnit>();
		enemyName.text = enemyUnit.unitName;

		yield return new WaitForSeconds(3f);
	    
		state = BattleState.PlayerTurn;
		PlayerTurn();
	}

	private IEnumerator PerformPlayerAction(PlayerAction action)
	{
		if (state != BattleState.PlayerTurn)
			yield break;
		
		var damageDealt = 0;
		
		switch (action)
		{
			case PlayerAction.NormalAttack:
				damageDealt = CalculateDamage(playerUnit.stats.Strength, enemyUnit.stats.Toughness);
				Debug.Log("Player used Normal Attack and dealt " + damageDealt + " damage to the enemy.");
				break;

			case PlayerAction.HeavyAttack:
				damageDealt = CalculateDamage(playerUnit.stats.Strength * 2, enemyUnit.stats.Toughness);
				Debug.Log("Player used Heavy Attack and dealt " + damageDealt + " damage to the enemy.");
				break;

			case PlayerAction.QuickAttack:
				damageDealt = CalculateDamage(playerUnit.stats.Agility, enemyUnit.stats.Toughness);
				Debug.Log("Player used Quick Attack and dealt " + damageDealt + " damage to the enemy.");
				break;

			case PlayerAction.Flee:
				Debug.Log("Player chose to Flee.");
				var fleeChance = CalculateFleeChance(playerUnit.stats.Agility);
				var randomValue = Random.Range(0f, 1f);

				if (randomValue <= fleeChance)
				{
					Debug.Log("Player chose to Flee and successfully escaped!");
					state = BattleState.Fled;

					yield return new WaitForSeconds(2f);
					
					EndBattle();
				}
				else
				{
					Debug.Log("Player chose to Flee but failed to escape.");
					state = BattleState.EnemyTurn;
					StartCoroutine(EnemyTurn());
				}
				break;
		}

		if (damageDealt == 0)
			yield break;
		
		var isDead = enemyUnit.TakeDamage(damageDealt);
		
		SetHealthSlider(playerHealthSlider, playerUnit.currentHP, playerUnit.maxHP);
		SetHealthSlider(enemyHealthSlider, enemyUnit.currentHP, enemyUnit.maxHP);
		
		yield return new WaitForSeconds(2f);

		if (isDead)
		{
			state = BattleState.Won;
			EndBattle();
		}
		else
		{
			state = BattleState.EnemyTurn;
			StartCoroutine(EnemyTurn());
		}
	}

	private IEnumerator EnemyTurn()
	{
		// TODO: Enemy Turn UI changes
		
		var damageDealt = CalculateDamage(enemyUnit.stats.Strength, playerUnit.stats.Toughness);

		yield return new WaitForSeconds(1f);
		var isDead = playerUnit.TakeDamage(damageDealt);

		yield return new WaitForSeconds(1f);
		
		SetHealthSlider(playerHealthSlider, playerUnit.currentHP, playerUnit.maxHP);
		SetHealthSlider(enemyHealthSlider, enemyUnit.currentHP, enemyUnit.maxHP);

		if (isDead)
		{
			state = BattleState.Lost;
			EndBattle();
		}
		else
		{
			state = BattleState.PlayerTurn;
			PlayerTurn();
		}
	}

	private void EndBattle()
	{
		switch (state)
		{
			case BattleState.Won:
				DialogManager.instance.showDialog(victorydialog,true,"RandomDungeon");
				break;
			case BattleState.Lost:
				DialogManager.instance.showDialog(defeatdialog,true,"Camp");
				break;
			case BattleState.Fled:
				DialogManager.instance.showDialog(fleddialog,true,"Camp");
				break;
		}
	}

	private void PlayerTurn()
	{
		// TODO: Player Turn UI changes
	}
	
	public void OnNormalAttackButtonClicked()
	{
		StartCoroutine(PerformPlayerAction(PlayerAction.NormalAttack));
	}

	public void OnHeavyAttackButtonClicked()
	{
		StartCoroutine(PerformPlayerAction(PlayerAction.HeavyAttack));
	}

	public void OnQuickAttackButtonClicked()
	{
		StartCoroutine(PerformPlayerAction(PlayerAction.QuickAttack));
	}
	
	public void OnFleeButtonClicked()
	{
		StartCoroutine(PerformPlayerAction(PlayerAction.Flee));
	}
	
	private static int CalculateDamage(int attackerStat, int defenderStat)
	{
		return Mathf.Max(attackerStat - defenderStat, 0);
	}
	
	private static float CalculateFleeChance(int playerAgility)
	{
		// Calculate flee chance based on player's agility.
		// You can customize the formula as needed.
		return playerAgility * 0.01f; // 1% chance per agility point.
	}
	
	private static void SetHealthSlider(Slider slider, int currentHealth, int maxHealth)
	{
		var healthPercent = Mathf.Clamp01((float)currentHealth / maxHealth);
		slider.value = healthPercent;
	}
}
