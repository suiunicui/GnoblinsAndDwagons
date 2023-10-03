using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public enum AttackType { NORMAL, HEAVY, QUICK }

public class BattleSystem : MonoBehaviour
{
	public GameObject playerPrefab;
	public GameObject enemyPrefab;
	
	public Transform playerBattleStation;
	public Transform enemyBattleStation;
	
	CombatUnit playerUnit;
	CombatUnit enemyUnit;
	
	public BattleState state;

	// Start is called before the first frame update
	void Start()
	{
		state = BattleState.START;
		StartCoroutine(SetupBattle());
	}

	IEnumerator SetupBattle()
	{
		var playerObject = Instantiate(playerPrefab, playerBattleStation);
		playerUnit = playerObject.GetComponent<CombatUnit>();
	    
		var enemyObject = Instantiate(enemyPrefab, enemyBattleStation);
		enemyUnit = enemyObject.GetComponent<CombatUnit>();

		yield return new WaitForSeconds(3f);
	    
		state = BattleState.PLAYERTURN;
		PlayerTurn();
	}

	IEnumerator PlayerAttack(AttackType type)
	{
		var isDead = enemyUnit.TakeDamage(playerUnit.damage);
		
		// TODO: Set UI to reflect damage to enemy
		
		yield return new WaitForSeconds(2f);

		if (isDead)
		{
			state = BattleState.WON;
			EndBattle();
		}
		else
		{
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}

	IEnumerator EnemyTurn()
	{
		// TODO: Enemy Turn UI changes
		
		yield return new WaitForSeconds(1f);
		var isDead = playerUnit.TakeDamage(enemyUnit.damage);
		
		// TODO: Set UI to reflect damage to player
		
		yield return new WaitForSeconds(1f);

		if (isDead)
		{
			state = BattleState.LOST;
			EndBattle();
		}
		else
		{
			state = BattleState.PLAYERTURN;
			PlayerTurn();
		}
	}

	void EndBattle()
	{
		if (state == BattleState.WON)
		{
			// TODO: Set UI to reflect victory
		} else if (state == BattleState.LOST)
		{
			// TODO: Set UI to reflect loss
		}
	}

	void PlayerTurn()
	{
		// TODO: Player Turn UI changes
	}

	public void OnNormalAttackButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerAttack(AttackType.NORMAL));
	}
}
