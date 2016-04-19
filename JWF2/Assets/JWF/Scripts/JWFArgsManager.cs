using UnityEngine;
using System.Collections;
using System;

namespace JWF
{
	public class JWFArgsManager : Singleton<JWFArgsManager>
	{
		private string _Arguments = "";

		public void SetArguments(string args)
		{
			_Arguments = args;
		}

		public string GetArguments()
		{
			return _Arguments;
		}

		protected override bool ShouldDestroyOnLoad()
		{
			return false;
		}
	}
}
