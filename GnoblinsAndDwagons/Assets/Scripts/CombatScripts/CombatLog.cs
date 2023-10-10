using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CombatScripts
{
	public class CombatLog : MonoBehaviour
	{
		public Text combatLogText;
		private readonly Queue<string> _logMessages = new();
		private const int MaxMessages = 4;

		public void AddLogMessage(string message)
		{
			_logMessages.Enqueue(message);
			if (_logMessages.Count > MaxMessages)
			{
				_logMessages.Dequeue();
			}
			
			UpdateCombatText();
		}

		private void UpdateCombatText()
		{
			combatLogText.text = string.Join("\n", _logMessages.ToArray());
		}
	}
}
