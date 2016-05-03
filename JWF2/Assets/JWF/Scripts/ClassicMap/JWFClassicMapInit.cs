using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace JWF.ClassicMap
{
	public class JWFClassicMapInit : MonoBehaviour
	{
		private static int TWO_PLAYERS = 2;
		private static int FOUR_PLAYERS = 4;

		private const int PLAYER_1 = 1;
		private const int PLAYER_2 = 2;
		private const int PLAYER_3 = 3;
		private const int PLAYER_4 = 4;

		// Prefab used to spawn the player into the game.
		public GameObject PlayerPrefab;
		public Material BluePrimary;
		public Material BlueSecondary;
		public Material RedPrimary;
		public Material RedSecondary;

		public Text BlueScoreText = null;
		public Text RedScoreText = null;
		public Text BlueScoreTextLarge = null;
		public Text RedScoreTextLarge = null;

		public GameObject IntroText = null;
		public Text IntroTextChild = null;
		public GameObject WinText = null;
		public Text WinTextChild = null;

		public GameObject PauseMenuQuit = null;
		public GameObject PauseMenuResume = null;

		public GameObject[] PlayerBottomUI = null;

		public JWFClassicMapCamera CameraManager;
		private JWFClassicMapInitSounds _MapInitSounds;

		private GameObject _Ball;
		public GameObject Ball { get { return _Ball; } }

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

		void Start()
		{
			_MapInitSounds = gameObject.GetComponent<JWFClassicMapInitSounds>();
			_Ball = GameObject.FindWithTag( JWFStatics.BALL_TAG );
			IntroText.SetActive( true );
			WinText.SetActive( false );
			JWFClassicMapScoreManager.Get.Init( this );
		}

		// Called from the HUD class once all players have joined.
		public void StartPreGame()
		{
			TimerManager.Get.SetTimer( _IntroSeq1Handle, IntroSeq1, _IntroSeq1Delay );
		}

		// Happens after 1 <See: _IntroSeq1Delay> second.
		void IntroSeq1()
		{
			TimerManager.Get.SetTimer( _CountdownHandle, IntroSeqCountdown, _CountdownHandleDelay, true );
		}

		// 3..2..1 counter happens every 1 second interval.
		private int _IntroSeqCountdownFucntionCounter = -1;
		void IntroSeqCountdown()
		{
			++_IntroSeqCountdownFucntionCounter;
			if ( _IntroSeqCountdownFucntionCounter == _CountdownMax )
			{
				TimerManager.Get.ClearTimer( _CountdownHandle );
				StartGame();
				return;
			}
			_MapInitSounds.PlayCountdown();
			int time =  _CountdownMax - _IntroSeqCountdownFucntionCounter;
			IntroTextChild.text = time.ToString();
		}

		void StartGame()
		{
			_MapInitSounds.PlayWhistle();
			IntroText.SetActive( false );
			PlayerSpawner();
			_Ball.GetComponent<Rigidbody>().isKinematic = false;
		}

		public void PlayerSpawner()
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
				SpawnPlayer( JWFPlayerManager.Get.GetPlayerWithID( PLAYER_2 ), _SpawnLocations[2] );
				SpawnPlayer( JWFPlayerManager.Get.GetPlayerWithID( PLAYER_3 ), _SpawnLocations[1] );
				SpawnPlayer( JWFPlayerManager.Get.GetPlayerWithID( PLAYER_4 ), _SpawnLocations[3] );
			}
		}

		public void SpawnPlayer(JWFPlayerData player, Vector3 position)
		{
			GameObject clone = Instantiate(PlayerPrefab, position, PlayerPrefab.transform.rotation) as GameObject;
			JWFClassicMapPlayerController controller = clone.GetComponent<JWFClassicMapPlayerController>();
			controller.PlayerData = player;
			HACK_SwitchColor( player.ID, clone, controller );
			controller.SetSpawnLocation( position );
		}

		void HACK_SwitchColor(int playerID, GameObject player, JWFClassicMapPlayerController controller)
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
