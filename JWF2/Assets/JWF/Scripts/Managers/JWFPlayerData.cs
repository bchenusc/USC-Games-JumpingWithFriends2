using UnityEngine;
using InControl;

namespace JWF
{
	public enum PlayerTeam
	{
		Red,
		Blue
	}

	public class JWFPlayerData
	{
		private int _Id = 0;
		private PlayerActionSet _Actions = null;
		private PlayerTeam _Team;
		private Material _Primary;
		private Material _Secondary;
		private int _IsKeyboard = 0; // Keyboard 1 is WASD, Keyboard 2 is Arrowkeys.

		public int ID
		{
			get { return _Id; }
			private set { _Id = value; }
		}

		public PlayerActionSet Actions
		{
			get { return _Actions; }
			private set { _Actions = value; }
		}

		public PlayerTeam Team
		{
			get { return _Team; }
			set { _Team = value; }
		}

		public Material Primary
		{
			get { return _Primary; }
			set { _Primary = value; }
		}

		public Material Secondary
		{
			get { return _Secondary; }
			set { _Secondary = value; }
		}

		public int IsKeyboard
		{
			get { return _IsKeyboard; }
		}

		public JWFPlayerData(int id, PlayerActionSet action, PlayerTeam team, int isKeyboard)
		{
			ID = id;
			Actions = action;
			Team = team;
			_IsKeyboard = isKeyboard;
		}

		public void Reset()
		{
			Actions = null;
		}

		public void RemoveActions()
		{
			_Actions = null;
		}
	};
}