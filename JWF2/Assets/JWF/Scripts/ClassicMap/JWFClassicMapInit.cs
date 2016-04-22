using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace JWF
{

	public class JWFClassicMapInit : MonoBehaviour
	{
		private const string INTRO_TEXT_CHILD_NAME = "Text";
		private const string BALL_TAG = "Ball";

		// Prefab used to spawn the player into the game.
		public GameObject PlayerPrefab;
		public Material BluePrimary;
		public Material BlueSecondary;
		public Material RedPrimary;
		public Material RedSecondary;

		public Text BlueScoreText = null;
		public Text RedScoreText = null;
		public GameObject IntroText = null;
		private Text _IntroText = null;
		public GameObject WinText = null;
		public GameObject _Ball;


		private static int CLASSIC_MAP = 1;
		private static int TWO_PLAYERS = 2;
		private static int FOUR_PLAYERS = 4;

		private const int PLAYER_1 = 1;
		private const int PLAYER_2 = 2;
		private const int PLAYER_3 = 3;
		private const int PLAYER_4 = 4;

		private List<Vector3> _SpawnLocations = new List<Vector3>()
		{
			new Vector3(-6, 10, 0),
			new Vector3(-3, 10, 0),
			new Vector3(3, 10, 0),
			new Vector3(6, 10, 0)
		};

		private const float _IntroSeq1Delay = 0.7f;
		private const float _CountdownHandleDelay = 1.0f;
		private const int _CountdownMax = 3;

		private TimerHandle _IntroSeq1Handle = new TimerHandle();
		private TimerHandle _CountdownHandle = new TimerHandle();

		void OnLevelWasLoaded(int level)
		{
			if ( level == CLASSIC_MAP )
			{
				_IntroText = IntroText.transform.FindChild( INTRO_TEXT_CHILD_NAME ).GetComponent<Text>();
				_Ball = GameObject.FindWithTag( BALL_TAG );
				JWFScoreManager.Get.Init( this );
				IntroText.SetActive( true );
				WinText.SetActive( false );
				TimerManager.Get.SetTimer( _IntroSeq1Handle, IntroSeq1, _IntroSeq1Delay );
			}
		}

		// Happens after 1 <See: _IntroSeq1Delay> second.
		void IntroSeq1()
		{
			TimerManager.Get.SetTimer( _CountdownHandle, IntroSeqCountdown, _CountdownHandleDelay, true );
		}

		// Happens every second with a countdown of 3.
		private int _IntroSeqCountdownFucntionCounter = -1;
		void IntroSeqCountdown()
		{
			++_IntroSeqCountdownFucntionCounter;
			if ( _IntroSeqCountdownFucntionCounter == _CountdownMax )
			{
				TimerManager.Get.ClearTimer( _CountdownHandle );
				StartGame();
			}
			int time =  _CountdownMax - _IntroSeqCountdownFucntionCounter;
			_IntroText.text = time.ToString();
		}

		void StartGame()
		{
			IntroText.SetActive( false );
			PlayerSpawner();
			_Ball.GetComponent<Rigidbody>().isKinematic = false;
		}

		void PlayerSpawner()
		{
			int num_players = JWFPlayerManager.Get.GetPlayerCount();

			// If there are only 2 players 
			if ( num_players == TWO_PLAYERS )
			{
				SpawnPlayer( JWFPlayerManager.Get.GetPlayerWithID( PLAYER_1 ), _SpawnLocations[0] );
				SpawnPlayer( JWFPlayerManager.Get.GetPlayerWithID( PLAYER_1 ), _SpawnLocations[1] );
				SpawnPlayer( JWFPlayerManager.Get.GetPlayerWithID( PLAYER_2 ), _SpawnLocations[2] );
				SpawnPlayer( JWFPlayerManager.Get.GetPlayerWithID( PLAYER_2 ), _SpawnLocations[3] );
			}
			else if ( num_players == FOUR_PLAYERS )
			{
				SpawnPlayer( JWFPlayerManager.Get.GetPlayerWithID( PLAYER_1 ), _SpawnLocations[0] );
				SpawnPlayer( JWFPlayerManager.Get.GetPlayerWithID( PLAYER_2 ), _SpawnLocations[1] );
				SpawnPlayer( JWFPlayerManager.Get.GetPlayerWithID( PLAYER_3 ), _SpawnLocations[2] );
				SpawnPlayer( JWFPlayerManager.Get.GetPlayerWithID( PLAYER_4 ), _SpawnLocations[3] );
			}
		}

		void SpawnPlayer(JWFPlayerData player, Vector3 position)
		{
			GameObject clone = Instantiate(PlayerPrefab, position, PlayerPrefab.transform.rotation) as GameObject;
			JWFPlayerController controller = clone.GetComponent<JWFPlayerController>();
			controller.PlayerData = player;
			HACK_SwitchColor( player.ID, clone, controller );
			controller.SetSpawnLocation( position );
		}

		void HACK_SwitchColor(int playerID, GameObject player, JWFPlayerController controller)
		{
			MeshRenderer renderer = player.GetComponent<MeshRenderer>();
			Material[] mats = renderer.materials;
			switch ( playerID )
			{
				case PLAYER_1:
				mats[0] = BluePrimary;
				mats[1] = BlueSecondary;
				controller.PlayerData.Team = PlayerTeam.Blue;
				break;

				case PLAYER_2:
				mats[0] = RedPrimary;
				mats[1] = RedSecondary;
				controller.PlayerData.Team = PlayerTeam.Red;
				break;

				case PLAYER_3:
				mats[0] = BlueSecondary;
				mats[1] = BluePrimary;
				controller.PlayerData.Team = PlayerTeam.Blue;
				break;

				case PLAYER_4:
				mats[0] = RedSecondary;
				mats[1] = RedPrimary;
				controller.PlayerData.Team = PlayerTeam.Red;
				break;
			}
			renderer.materials = mats;
		}
	}
}
