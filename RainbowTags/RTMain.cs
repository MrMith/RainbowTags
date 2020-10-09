//CREDIT TO https://github.com/KoukoCocoa/RainbowTags and https://github.com/FruitBoi/RainbowTags


using Smod2;
using Smod2.Attributes;
using Smod2.Config;

namespace RainbowTags
{
	[PluginDetails(
		author = "Mith",
		name = "RainbowTags",
		description = "Rainbow Tags",
		id = "mith.RainbowTags",
		version = "1.0.0",
		configPrefix = "rt",
		SmodMajor = 3,
		SmodMinor = 7,
		SmodRevision = 0
		)]
	class RTMain : Plugin
	{
		[ConfigOption]
		public readonly bool disable = false;

		[ConfigOption]
		public readonly string[] active_groups = new string[] { "owner", "admin", "moderator" };

		[ConfigOption]
		public readonly bool use_custom = false;

		[ConfigOption]
		public readonly string[] custom_sequence = new string[] { "red", "orange", "yellow", "green", "blue_green", "magenta" };

		[ConfigOption]
		public readonly float interval = 0.5f;

		public override void OnDisable()
		{
			this.Info($"{this.Details.name}(Version:{this.Details.version}) has been disabled.");
		}

		public override void OnEnable()
		{
			this.Info($"{this.Details.name}(Version:{this.Details.version}) has been enabled.");
		}

		public override void Register()
		{
			this.AddCommand(this.Details.configPrefix + "_version", new RTVersion(this));
			this.AddCommand(this.Details.configPrefix + "_disable", new RTDisable(this));
			this.AddEventHandlers(new RTEventHandler(this));
		}
	}
}