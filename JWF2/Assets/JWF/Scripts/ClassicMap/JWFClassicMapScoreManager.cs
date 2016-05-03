using UnityEngine;
using UnityEngine.UI;

namespace JWF.ClassicMap
{
	public class JWFClassicMapScoreManager : Singleton<JWFClassicMapScoreManager>
	{
		private const int _MaxScore = 5;
		private const string WIN_STRING = " Team Wins!";
		private const string RED_STRING = "Red";
		private const string BLUE_STRING = "Blue";
		private const string SCORED_STRING = " Scored!";
		private const string OWNGOAL_STRING = " Own Goal!";
		private const string PLAYER_STRING = "Player ";

		private const float _SlowMoSpeed = 0.27f;
		private const float _NormalTimeSpeed = 1.0f;

		private const float _EndOfGameDelayToMenu = 8.0f;
		private const float _RespawnBallDelay = 1.5f;

		int _P1Score = 0;
		int _P2Score = 0;
		int _P3Score = 0;
		int _P4Score = 0;

		int _BlueScore = 0;
		int _RedScore = 0;

		// Set in Init()
		private Text _BlueScoreText = null;
		private Text _RedScoreText = null;
		private Text _BlueScoreTextLarge = null;
		private Text _RedScoreTextLarge = null;
		private GameObject _WinText = null;
		private GameObject _Ball = null;
		private JWFClassicMapCamera _CameraManager = null;
		private Text _WinTextChild = null;

		private TimerHandle _ReturnToMenuHandle = new TimerHandle();
		private TimerHandle _ReturnFromSlowMoHandle = new TimerHandle();

		private bool _GameOver = false;

		public override void Init()
		{
			Debug.LogError( "Do not use Init() in JWFScoreManager without parameters." );
		}

		public void Init(JWFClassicMapInit initScript)
		{
			_BlueScoreText = initScript.BlueScoreText;
			_RedScoreText = initScript.RedScoreText;
			_BlueScoreTextLarge = initScript.BlueScoreTextLarge;
			_RedScoreTextLarge = initScript.RedScoreTextLarge;
			_WinText = initScript.WinText;
			_WinTextChild = initScript.WinTextChild;
			_Ball = initScript.Ball;
			_CameraManager = initScript.CameraManager;

			_BlueScore = 0;
			_RedScore = 0;
			UpdateScore();
		}

		public void AddScore(int playerId, PlayerTeam team)
		{
			switch ( team )
			{
				case PlayerTeam.Red:
				_CameraManager.GoalScoredCameraBM( PlayerTeam.Blue );
				++_RedScore;
				break;

				case PlayerTeam.Blue:
				_CameraManager.GoalScoredCameraBM( PlayerTeam.Red );
				++_BlueScore;
				break;
			}

			if ( JWFPlayerManager.Get.GetPlayerTeam( playerId ) == team )
			{
				switch ( playerId )
				{
					case 1:
					++_P1Score;
					break;

					case 2:
					++_P2Score;
					break;

					case 3:
					++_P3Score;
					break;

					case 4:
					++_P4Score;
					break;
				}
			}
			UpdateScore();
			_GameOver = CheckIfGameOver();
			if ( !_GameOver )
			{
				ShowWhoScored( playerId, team );
			}
			SlowMo();
		}

		public void ShowWhoScored(int playerId, PlayerTeam teamThatGotPoint)
		{
			PlayerTeam playersTeam = JWFPlayerManager.Get.GetPlayerTeam(playerId);
			bool isOwnGoal = playersTeam != teamThatGotPoint;
			Color bannerColor = playersTeam == PlayerTeam.Red ? Color.red : Color.blue;
			string goaledString = isOwnGoal == true ? OWNGOAL_STRING : SCORED_STRING;
			string output = PLAYER_STRING + playerId + goaledString;
			_WinTextChild.text = output;
			_WinText.transform.GetComponent<Image>().color = bannerColor;
			_WinText.SetActive( true );
		}

		public void SlowMo()
		{
			TimerManager.Get.SetTimer( _ReturnFromSlowMoHandle, ReturnTimeScale, _RespawnBallDelay );
			Time.timeScale = _SlowMoSpeed;
		}

		public void ReturnTimeScale()
		{
			if ( !_GameOver )
			{
				_WinText.SetActive( false );
			}
			Time.timeScale = _NormalTimeSpeed;
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

		private bool CheckIfGameOver()
		{
			if ( _BlueScore == _MaxScore )
			{
				GameOver( PlayerTeam.Blue );
				return true;
			}
			else if ( _RedScore == _MaxScore )
			{
				GameOver( PlayerTeam.Red );
				return true;
			}
			return false;
		}

		private void GameOver(PlayerTeam winner)
		{
			string winningTeam = winner == PlayerTeam.Red ? RED_STRING : BLUE_STRING;
			Color teamColor = winner == PlayerTeam.Red? Color.red : Color.blue;
			_WinTextChild.text = winningTeam + WIN_STRING;
			_WinText.transform.GetComponent<Image>().color = teamColor;
			_WinText.SetActive( true );
			_Ball.SetActive( false );
			_BlueScoreTextLarge.transform.parent.gameObject.SetActive( true );
			_RedScoreTextLarge.transform.parent.gameObject.SetActive( true );
			_BlueScoreTextLarge.text = _BlueScore.ToString();
			_RedScoreTextLarge.text = _RedScore.ToString();
			TimerManager.Get.SetTimer( _ReturnToMenuHandle, ReturnToMenu, _EndOfGameDelayToMenu );
		}

		private void ReturnToMenu()
		{
			ReturnTimeScale();
			JWFSceneManager.LoadLevel( Scenes.MENU.First );
		}

		protected override bool ShouldDestroyOnLoad()
		{
			return true;
		}
	}

}
