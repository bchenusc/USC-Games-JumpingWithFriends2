using UnityEngine;

namespace JWF.ClassicMap
{
	[RequireComponent( typeof( JWFClassicMapPlayerController) )]
	public class JWFClassicMapPlayerUIController : MonoBehaviour
	{
		private float _ButtonScaleDown = 0.5f;
		private float _ButtonScaleUp = 0.9f;
		private float _ButtonScaleTime = 0.5f;

		JWFClassicMapPlayerController _Controller;
		JWFClassicMapInit _SceneInit;

		private enum UIState
		{
			None,
			Resume,
			Quit
		}
		private static UIState _UIState = UIState.None;
		private static JWFClassicMapPlayerController _PlayerThatSummonedPauseMenu;

		void Start()
		{
			_Controller = GetComponent<JWFClassicMapPlayerController>();
			_SceneInit = GameObject.FindWithTag( GameStatics.SCENE_INIT_TAG ).GetComponent<JWFClassicMapInit>();
		}

		void Update()
		{
			JWFClassicMapPlayerActions Actions = (JWFClassicMapPlayerActions) _Controller.PlayerData.Actions;

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
