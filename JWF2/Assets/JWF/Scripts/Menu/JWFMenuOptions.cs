using UnityEngine;
using System.Collections.Generic;
using System;

namespace JWF.Menu
{
	public class JWFMenuOptions : JWFMenuBase
	{
		public GameObject[] OptionMenuButtons;

		private JWFMenuManager _MenuManager;
		private enum JWFOptionsMenuState
		{
			Back = 0
		}
		private List<JWFMenuBase> _OptionsMenuStates = new List<JWFMenuBase>();
		private JWFOptionsMenuState _State = JWFOptionsMenuState.Back;

		void Start()
		{
			_OptionsMenuStates.Add( gameObject.GetComponent<JWFMenuStart>() ); // 0
			_MenuManager = gameObject.GetComponent<JWFMenuManager>();
			iTween.ScaleTo( OptionMenuButtons[(int) _State], Vector3.one * _ButtonScaleUp, _ButtonScaleTime );
		}

		public override Vector3 GetCameraPosition()
		{
			return new Vector3( -10, 1.5f, -10.5f );
		}

		public override JWFMenuState GetState()
		{
			return JWFMenuState.MenuOptions;
		}

		public override void AcceptPressed()
		{
			_MenuManager.ChangeStateTo( _OptionsMenuStates[(int) _State] );
		}

		public override void LeftPressed()
		{
			// Returns the button the left. If left most, then return itself.
			if ( _State == 0 ) return;
			iTween.ScaleTo( OptionMenuButtons[(int) _State], Vector3.one * _ButtonScaleDown, _ButtonScaleTime );
			--_State;
			iTween.ScaleTo( OptionMenuButtons[(int) _State], Vector3.one * _ButtonScaleUp, _ButtonScaleTime );
		}

		public override void RightPressed()
		{
			if ( (int) _State >= System.Enum.GetNames( typeof( JWFOptionsMenuState ) ).Length - 1 ) return;
			iTween.ScaleTo( OptionMenuButtons[(int) _State], Vector3.one * _ButtonScaleDown, _ButtonScaleTime );
			++_State;
			iTween.ScaleTo( OptionMenuButtons[(int) _State], Vector3.one * _ButtonScaleUp, _ButtonScaleTime );
		}
	}
}
