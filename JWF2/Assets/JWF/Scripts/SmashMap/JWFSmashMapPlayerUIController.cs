using UnityEngine;

namespace JWF.SmashMap
{
	[RequireComponent( typeof( JWFSmashMapPlayerController) )]
	public class JWFSmashMapPlayerUIController : MonoBehaviour
	{
		private float _ButtonScaleDown = 0.5f;
		private float _ButtonScaleUp = 0.9f;
		private float _ButtonScaleTime = 0.5f;

		JWFSmashMapPlayerController _Controller;
		JWFSmashMapInit _SceneInit;

		private enum UIState
		{
			None,
			Resume,
			Quit
		}
		private static UIState _UIState = UIState.None;
		private static JWFSmashMapPlayerController _PlayerThatSummonedPauseMenu;

		void Start()
		{
			_Controller = GetComponent<JWFSmashMapPlayerController>();
			_SceneInit = GameObject.FindWithTag( GameStatics.SCENE_INIT_TAG ).GetComponent<JWFSmashMapInit>();
		}

		void Update()
		{
			JWFSmashMapPlayerActions Actions = (JWFSmashMapPlayerActions) _Controller.PlayerData.Actions;

			if ( _PlayerThatSummonedPauseMenu == null || _PlayerThatSummonedPauseMenu == _Controller )
			{
				if ( Actions.StartCommand.WasPressed )
				{
					// If UI is already up, then close it. If UI is not up, then open it.
					if ( _UIState == UIState.None )
					{
						ChangeStateTo( UIState.Resume );
					}
					else
					{
						ChangeStateTo( UIState.None );
					}
				}

				if ( Actions.Accept.WasPressed )
				{
					if ( _UIState == UIState.Quit )
					{
						JWFSceneManager.LoadLevel( Scenes.MENU.First );
					}
					else if ( _UIState == UIState.Resume )
					{
						ChangeStateTo( UIState.None );
					}
				}

				if (Actions.Left.WasPressed)
				{
					if (_UIState == UIState.Quit)
					{
						ChangeStateTo( UIState.Resume );
					}
				}
				
				if (Actions.Right.WasPressed)
				{
					if (_UIState == UIState.Resume)
					{
						ChangeStateTo( UIState.Quit );
					}
				}
			}
		}

		void ChangeStateTo(UIState state)
		{
			_UIState = state;
			switch ( state)
			{
				case UIState.None:
				SetActiveParent( _SceneInit.PauseMenuResume, false );
				_PlayerThatSummonedPauseMenu = null;
				break;

				case UIState.Quit:
				iTween.ScaleTo( _SceneInit.PauseMenuQuit, Vector3.one * _ButtonScaleUp, _ButtonScaleTime );
				iTween.ScaleTo( _SceneInit.PauseMenuResume, Vector3.one * _ButtonScaleDown, _ButtonScaleTime );
				break;

				case UIState.Resume:
				if (_PlayerThatSummonedPauseMenu == null || _PlayerThatSummonedPauseMenu == _Controller)
				{
					_PlayerThatSummonedPauseMenu = _Controller;
					SetActiveParent( _SceneInit.PauseMenuResume, true );
					iTween.ScaleTo( _SceneInit.PauseMenuQuit, Vector3.one * _ButtonScaleDown, _ButtonScaleTime );
					iTween.ScaleTo( _SceneInit.PauseMenuResume, Vector3.one * _ButtonScaleUp, _ButtonScaleTime );

				}
				break;
			}
		}

		static void SetActiveParent(GameObject child, bool isActive)
		{
			child.transform.parent.gameObject.SetActive( isActive );
		}

	}
}
