using UnityEngine;
using System.Collections.Generic;

namespace JWF
{

	public class JWFClassicMapInit : MonoBehaviour
	{
		// Prefab used to spawn the player into the game.
		public GameObject PlayerPrefab;
		public Material BluePrimary;
		public Material BlueSecondary;
		public Material RedPrimary;
		public Material RedSecondary;


		private static int CLASSIC_MAP = 1;
		private static int TWO_PLAYERS = 2;
		private static int FOUR_PLAYERS = 4;

		private const int PLAYER_1 = 1;
		private const int PLAYER_2 = 2;
		private const int PLAYER_3 = 3;
		private const int PLAYER_4 = 4;

		private List<Vector3> _SpawnLocations = new List<Vector3>()
		{
			new Vector3(-6, 3, 0),
			new Vector3(-3, 3, 0),
			new Vector3(3, 3, 0),
			new Vector3(6, 3, 0)
		};

		void OnLevelWasLoaded(int level)
		{
			if ( level == CLASSIC_MAP )
			{
				PlayerSpawner();
			}
		}

		void PlayerSpawner()
		{
			int num_players = JWFPlayerManager.Get.GetPlayerCount();

			// If there are only 2 players 
			if (num_players == TWO_PLAYERS)
			{
				SpawnPlayer( JWFPlayerManager.Get.GetPlayerWithID( PLAYER_1 ), _SpawnLocations[0] );
				SpawnPlayer( JWFPlayerManager.Get.GetPlayerWithID( PLAYER_1 ), _SpawnLocations[1] );
				SpawnPlayer( JWFPlayerManager.Get.GetPlayerWithID( PLAYER_2 ), _SpawnLocations[2] );
				SpawnPlayer( JWFPlayerManager.Get.GetPlayerWithID( PLAYER_2 ), _SpawnLocations[3] );
			}
			else if (num_players == FOUR_PLAYERS)
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
			HACK_SwitchColor( player.ID, clone );
			clone.GetComponent<JWFPlayerController>().PlayerData = player;
		}

		void HACK_SwitchColor(int playerID, GameObject player)
		{
			MeshRenderer renderer = player.GetComponent<MeshRenderer>();
			Material[] mats = renderer.materials;
			switch (playerID)
			{
				case PLAYER_1:
				mats[0] = BluePrimary;
				mats[1] = BlueSecondary;
				break;
				
				case PLAYER_2:
				mats[0] = RedPrimary;
				mats[1] = RedSecondary;
				break;

				case PLAYER_3:
				mats[0] = BlueSecondary;
				mats[1] = BluePrimary;
				break;

				case PLAYER_4:
				mats[0] = RedSecondary;
				mats[1] = RedPrimary;
				break;
			}
			renderer.materials = mats;
		}
	}
}
