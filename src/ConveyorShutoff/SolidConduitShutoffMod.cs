﻿using System;
using System.Collections.Generic;
using System.Linq;
using Harmony;

namespace ConveyorShutoff
{
	public class SolidConduitShutoffMod
	{
		[HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
		public class SolidConduitShutoffBuildingsPatch
		{
			private static void Prefix()
			{
				Strings.Add("STRINGS.BUILDINGS.PREFABS.SOLIDCONDUITSHUTOFF.NAME", "Conveyor Rail Shutoff");
				Strings.Add("STRINGS.BUILDINGS.PREFABS.SOLIDCONDUITSHUTOFF.DESC", "Your items won't go anywhere unless you let them.");
				Strings.Add("STRINGS.BUILDINGS.PREFABS.SOLIDCONDUITSHUTOFF.EFFECT", "Automatically turns flow of objects on the Conveyor Rail on or off using Automation technology.");

				ModUtil.AddBuildingToPlanScreen("Conveyance", SolidConduitShutoffConfig.ID);
			}
		}

		[HarmonyPatch(typeof(Db), "Initialize")]
		public class SolidConduitShutoffDbPatch
		{
			private static void Prefix()
			{
				List<string> ls = new List<string>(Database.Techs.TECH_GROUPING["SolidTransport"]) { SolidConduitShutoffConfig.ID };
				Database.Techs.TECH_GROUPING["SolidTransport"] = ls.ToArray();
			}
		}

		[HarmonyPatch(typeof(KSerialization.Manager), "GetType", new Type[] { typeof(string) })]
		public static class SolidConduitShutoffSerializationPatch
		{
			[HarmonyPostfix]
			public static void GetType(string type_name, ref Type __result)
			{
				if (type_name == "ConveyorShutoff.SolidConduitShutoff")
				{
					__result = typeof(SolidConduitShutoff);
				}
			}
		}
	}
}