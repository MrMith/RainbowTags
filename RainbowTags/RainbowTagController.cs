//CREDIT TO https://github.com/KoukoCocoa/RainbowTags and https://github.com/FruitBoi/RainbowTags


using System.Collections.Generic;
using UnityEngine;

namespace RainbowTags
{
	public class RainbowTagController : MonoBehaviour
	{
		private ServerRoles Roles;
		private string OriginalColor;

		private int Position = 0;
		private float NextCycle = 0f;

		public List<string> Colors;

		public float Interval { get; set; }

		public void Awake()
		{
			Roles = GetComponent<ServerRoles>();
			NextCycle = Time.time;
			OriginalColor = Roles.NetworkMyColor;
		}

		public void OnDestroy()
		{
			Roles.NetworkMyColor = OriginalColor;
		}

		public void Update()
		{
			if (Time.time >= NextCycle)
			{
				NextCycle += Interval;
				Roles.NetworkMyColor = Colors[Position];

				if (++Position >= Colors.Count)
					Position = 0;
			}
		}
	}
}