using UnityEngine;
using System.Collections.Generic;

namespace JWF.Menu
{

	public class JWFMenuOptionsCreditsScroller : MonoBehaviour
	{

		private List<GameObject> children = new List<GameObject>();
		private int elementIndex = 0;
		private float _ScrollRate = 1.5f;

		private float _CurrentTimer = 0;

		void OnEnable()
		{
			_CurrentTimer = _ScrollRate;
			elementIndex = 0;
			foreach ( Transform child in transform )
			{
				children.Add( child.gameObject );
			}
		}

		void NextElement()
		{
			children[elementIndex].SetActive( false );
			++elementIndex;
			if (elementIndex >= children.Count)
			{
				elementIndex = 0;
			}
			children[elementIndex].SetActive( true );
		}

		void OnDisable()
		{
			children.Clear();
		}

		void Update()
		{
			if (_CurrentTimer > 0)
			{
				_CurrentTimer -= Time.deltaTime;
			}
			if (_CurrentTimer <= 0)
			{
				_CurrentTimer = _ScrollRate;
				NextElement();
			}
		}
	}

}