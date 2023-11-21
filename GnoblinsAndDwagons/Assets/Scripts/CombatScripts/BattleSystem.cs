using System;
using System.Collections;
using Unity.VisualScripting;
using CombatScripts;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

public class BattleSystem : MonoBehaviour
{
	[SerializeField] private GameStateMemory _gameStateMemory;
	[SerializeField] private PlayerInventory _playerInventory;
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
	
	public CombatLog combatLog;


	private void Awake()
	{
		instance = this;
	}
	// Start is called before the first frame update
	private void Start()
	{
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

		yield return new WaitForSeconds(1f);
	    
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
				combatLog.AddLogMessage($"Player used Normal Attack and dealt {damageDealt} damage to the enemy.");
				Debug.Log("Player used Normal Attack and dealt " + damageDealt + " damage to the enemy.");
				break;

			case PlayerAction.HeavyAttack:
				damageDealt = CalculateDamage(playerUnit.stats.Strength * 2, enemyUnit.stats.Toughness + enemyUnit.stats.Dexterity);
				combatLog.AddLogMessage($"Player used Heavy Attack and dealt {damageDealt} damage to the enemy.");
				Debug.Log("Player used Heavy Attack and dealt " + damageDealt + " damage to the enemy.");
				break;

			case PlayerAction.QuickAttack:
				damageDealt = CalculateDamage(playerUnit.stats.Agility, enemyUnit.stats.Toughness);
				combatLog.AddLogMessage($"Player used Quick Attack and dealt {damageDealt} damage to the enemy.");
				Debug.Log("Player used Quick Attack and dealt " + damageDealt + " damage to the enemy.");
				break;

			case PlayerAction.Flee:
				combatLog.AddLogMessage("Player chose to Flee.");
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

		var isDead = enemyUnit.TakeDamage(damageDealt);
		
		SetHealthSlider(playerHealthSlider, playerUnit.currentHp, playerUnit.maxHp);
		SetHealthSlider(enemyHealthSlider, enemyUnit.currentHp, enemyUnit.maxHp);

		if (isDead)
		{
			state = BattleState.Won;
			EndBattle();
		}
		else if(state != BattleState.EnemyTurn)
		{
			state = BattleState.EnemyTurn;
			yield return new WaitForSeconds(0f);
			StartCoroutine(EnemyTurn());
		}
	}

	private IEnumerator EnemyTurn()
	{
		var damageDealt = CalculateDamage(enemyUnit.stats.Strength, playerUnit.stats.Toughness);

		yield return new WaitForSeconds(0f);
		var isDead = playerUnit.TakeDamage(damageDealt);
		combatLog.AddLogMessage($"Enemy dealt {damageDealt} damage to the player.");

		SetHealthSlider(playerHealthSlider, playerUnit.currentHp, playerUnit.maxHp);
		SetHealthSlider(enemyHealthSlider, enemyUnit.currentHp, enemyUnit.maxHp);

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
                _gameStateMemory.leaveCombat = true;
				_gameStateMemory.inDungeon = true;
				DialogManager.instance.showDialog(victorydialog,true,"RandomDungeon");
				break;
			case BattleState.Lost:
				_gameStateMemory.clearGameState();
				_gameStateMemory.leaveDungeon = true;
                _gameStateMemory.leaveCombat = true;
				_playerInventory.gold = 0;
				_playerInventory.inventory = new List<ItemThings.Item>(35);
				_playerInventory.equippedItems = new ItemThings.PlayerEquippedItems();
                DialogManager.instance.showDialog(defeatdialog,true,"Camp");
				break;
			case BattleState.Fled:
                _gameStateMemory.clearGameState();
                _gameStateMemory.leaveDungeon = true;
                _gameStateMemory.leaveCombat = true;

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
		return playerAgility * 0.01f; // 1% chance per agility point.
	}
	
	private static void SetHealthSlider(Slider slider, int currentHealth, int maxHealth)
	{
		var healthPercent = Mathf.Clamp01((float)currentHealth / maxHealth);
		slider.value = healthPercent;
	}
}
