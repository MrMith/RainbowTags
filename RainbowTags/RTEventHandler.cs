//CREDIT TO https://github.com/KoukoCocoa/RainbowTags and https://github.com/FruitBoi/RainbowTags


using Smod2;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace RainbowTags
{
	internal class RTEventHandler : IEventHandlerWaitingForPlayers, IEventHandlerPlayerJoin
	{
		public float Interval;

		public List<string> ValidColors;
		public List<string> ValidGroups;

		public bool Enabled = true;
		public bool CustomSeq = false;

		private readonly Plugin plugin;
		public RTEventHandler(Plugin plugin)
		{
			this.plugin = plugin;
		}

		public void OnPlayerJoin(PlayerJoinEvent ev)
		{
			if (!Enabled) return;
			AddRainbowController(ReferenceHub.GetHub((GameObject)ev.Player.GetGameObject()));
		}

		public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
		{
			Enabled = !plugin.GetConfigBool(plugin.Details.configPrefix + "_disable");
			if (!Enabled)
			{
				plugin.PluginManager.DisablePlugin(plugin);
			}

			Interval = plugin.GetConfigFloat(plugin.Details.configPrefix + "_interval");
			CustomSeq = plugin.GetConfigBool(plugin.Details.configPrefix + "_use_custom");

			if (CustomSeq)
			{
				ValidColors = plugin.GetConfigList(plugin.Details.configPrefix + "_custom_sequence").ToList();
			}
			else
			{
				ValidColors = new List<string>
				{
					"pink",
					"red",
					"brown",
					"silver",
					"light_green",
					"crimson",
					"cyan",
					"aqua",
					"deep_pink",
					"tomato",
					"yellow",
					"magenta",
					"blue_green",
					"orange",
					"lime",
					"green",
					"emerald",
					"carmine",
					"nickel",
					"mint",
					"army_green",
					"pumpkin"
				};
			}

			ValidGroups = plugin.GetConfigList(plugin.Details.configPrefix + "_active_groups").ToList();

			foreach (var ply in ReferenceHub.GetAllHubs().Values)
			{
				AddRainbowController(ply);
			}
		}

		public void AddRainbowController(ReferenceHub Ply)
		{
			if (Ply.TryGetComponent(out RainbowTagController RainbowTagCtrl))
				return;

			if (Ply == ReferenceHub.HostHub ||! ValidGroups.Contains(ServerStatic.GetPermissionsHandler().GetUserGroup(Ply.characterClassManager.UserId).Name)) return;

			var RainbowTagComp = Ply.gameObject.AddComponent<RainbowTagController>();

			RainbowTagComp.Colors = ValidColors;
			RainbowTagComp.Interval = Interval;
		}
	}
}