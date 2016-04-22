using UnityEngine;
using UnityEngine.UI;


namespace JWF
{
	public class JWFScoreManager : Singleton<JWFScoreManager>
	{
		private const int _MaxScore = 5;
		private const string WIN_STRING = " Team Wins!";
		private const string RED_STRING = "Red";
		private const string BLUE_STRING = "Blue";
		private const string WIN_TEXT_CHILD_NAME = "Text";

		int _P1Score = 0;
		int _P2Score = 0;
		int _P3Score = 0;
		int _P4Score = 0;

		int _BlueScore = 0;
		int _RedScore = 0;

		private Text _BlueScoreText = null;
		private Text _RedScoreText = null;
		private GameObject _WinText = null;
		private GameObject _Ball = null;

		private float _EndOfGameDelayToMenu = 1.0f;
		private float _RespawnBallDelay = 1.5f;

		private TimerHandle _ReturnToMenuHandle = new TimerHandle();
		private TimerHandle _ReturnFromSlowMoHandle = new TimerHandle();

		public override void Init()
		{
			Debug.LogError( "Do not use Init() in JWFScoreManager without parameters." );
		}

		public void Init(JWFClassicMapInit initScript)
		{
			_BlueScoreText = initScript.BlueScoreText;
			_RedScoreText = initScript.RedScoreText;
			_WinText = initScript.WinText;
			_Ball = initScript._Ball;

			_BlueScore = _MaxScore;
			_RedScore = _MaxScore;
			UpdateScore();
		}

		public void AddScore(int playerId, PlayerTeam team)
		{
			switch ( team )
			{
				case PlayerTeam.Red:
				--_RedScore;
				break;

				case PlayerTeam.Blue:
				--_BlueScore;
				break;
			}

			if ( JWFPlayerManager.Get.GetPlayerTeam( playerId ) == team )
			{
				switch ( playerId )
				{
					case 1:
					--_P1Score;
					break;

					case 2:
					--_P2Score;
					break;

					case 3:
					--_P3Score;
					break;

					case 4:
					--_P4Score;
					break;
				}
			}
			UpdateScore();
			CheckIfGameOver();
			SlowMo();
		}

		public void SlowMo()
		{
			TimerManager.Get.SetTimer( _ReturnFromSlowMoHandle, ReturnTimeScale, _RespawnBallDelay );
			Time.timeScale = 0.3f;
		}
		
		public void ReturnTimeScale()
		{
			Time.timeScale = 1.0f;
		}

		public int GetScore(PlayerTeam team)
		{
			switch ( team )
			{
				case PlayerTeam.Blue:
				return _BlueScore;

				case PlayerTeam.Red:
				return _RedScore;
			}
			return -1;
		}

		private void UpdateScore()
		{
			_BlueScoreText.text = _BlueScore.ToString();
			_RedScoreText.text = _RedScore.ToString();
		}

		private void CheckIfGameOver()
		{
			if ( _BlueScore == 0 )
			{
				GameOver( PlayerTeam.Blue );
			}
			else if ( _RedScore == 0 )
			{
				GameOver( PlayerTeam.Red );
			}
		}

		private void GameOver(PlayerTeam winner)
		{
			string winningTeam = winner == PlayerTeam.Red ? RED_STRING : BLUE_STRING;
			Color teamColor = winner == PlayerTeam.Red? Color.red : Color.blue;
			_WinText.transform.FindChild( WIN_TEXT_CHILD_NAME ).GetComponent<Text>().text = winningTeam + WIN_STRING;
			_WinText.transform.GetComponent<Image>().color = teamColor;
			_WinText.SetActive( true );
			_Ball.SetActive( false );
			TimerManager.Get.SetTimer( _ReturnToMenuHandle, ReturnToMenu, _EndOfGameDelayToMenu );
		}

		private void ReturnToMenu()
		{
			Time.timeScale = 1.0f;
			JWFSceneManager.LoadLevel( Scenes.MENU );
		}

		protected override bool ShouldDestroyOnLoad()
		{
			return true;
		}
	}

}
