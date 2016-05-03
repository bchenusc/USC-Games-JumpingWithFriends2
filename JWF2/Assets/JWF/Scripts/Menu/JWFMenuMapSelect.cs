using UnityEngine;
using System.Collections.Generic;

namespace JWF.Menu
{
	public class JWFMenuMapSelect : JWFMenuBase
	{
		public GameObject[] MapSelectButtons;

		private enum JWFMapSelectState
		{
			Classic = 0,
			Fight = 1
		}
		private List<JWFMenuBase> _MenuStates = new List<JWFMenuBase>();
		private JWFMapSelectState _State = JWFMapSelectState.Classic;

		void Start()
		{
			_MenuStates.Add( gameObject.GetComponent<JWFMenuOptions>() ); // 0 
			_MenuStates.Add( gameObject.GetComponent<JWFMenuMapSelect>() ); // 1
			iTween.ScaleTo( MapSelectButtons[(int) _State], Vector3.one * _ButtonScaleUp, _ButtonScaleTime );
		}

		public override Vector3 GetCameraPosition()
		{
			return new Vector3( 10, 1.5f, -10.5f );
		}

		public override JWFMenuState GetState()
		{
			return JWFMenuState.MenuMapSelect;
		}

		public override void AcceptPressed()
		{
			base.AcceptPressed();	
			if (_State == JWFMapSelectState.Classic)
			{
				JWFSceneManager.LoadLevel( Scenes.CLASSIC_MAP.First );
				return;
			}

			if (_State == JWFMapSelectState.Fight)
			{
				JWFSceneManager.LoadLevel( Scenes.SMASH_MAP.First );
				return;
			}
		}

		public override void LeftPressed()
		{
			base.LeftPressed();

			// Returns the button the left. If left most, then return itself.
			if ( _State == 0 ) return;
			iTween.ScaleTo( MapSelectButtons[(int) _State], Vector3.one * _ButtonScaleDown, _ButtonScaleTime );
			--_State;
			iTween.ScaleTo( MapSelectButtons[(int) _State], Vector3.one * _ButtonScaleUp, _ButtonScaleTime );
		}

		public override void RightPressed()
		{
			base.RightPressed();
			if ( (int) _State >= System.Enum.GetNames( typeof( JWFMapSelectState ) ).Length - 1 ) return;
			iTween.ScaleTo( MapSelectButtons[(int) _State], Vector3.one * _ButtonScaleDown, _ButtonScaleTime );
			++_State;
			iTween.ScaleTo( MapSelectButtons[(int) _State], Vector3.one * _ButtonScaleUp, _ButtonScaleTime );
		}
	}
}
