using UnityEngine;

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
		private JWFPlayerActions _Actions = null;
		private PlayerTeam _Team;
		private Material _Primary;
		private Material _Secondary;

		public int ID
		{
			get { return _Id; }
			private set { _Id = value; }
		}

		public JWFPlayerActions Actions
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

		public JWFPlayerData(int id, JWFPlayerActions action, PlayerTeam team)
		{
			ID = id;
			Actions = action;
			Team = team;
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