using System;
using System.Collections.Immutable;

namespace Assets.Scripts.Model
{
    public static class StructureRegistry
    {
        public static StructureInfo Housing { get; private set; } = BuildHousing();
        public static StructureInfo Farm { get; private set; } = BuildFarm();
        public static StructureInfo WaterCollectionPlant { get; private set; } = BuildWaterCollectionPlant();
        public static StructureInfo IronMine { get; private set; } = BuildIronMine();
        public static StructureInfo EnergyPlant { get; private set; } = BuildEnergyPlant();
        public static StructureInfo ElectrolysisPlant { get; private set; } = BuildElectrolysisPlant();
        public static StructureInfo SteelSmelter { get; private set; } = BuildSteelSmelter();
        public static StructureInfo MylarPlant { get; private set; } = BuildMylarPlant();
        public static StructureInfo CarbonCollector { get; private set; } = BuildCarbonCollector();
        public static StructureInfo DoctorsOffices { get; private set; } = BuildDoctorsOffices();
        public static StructureInfo AlcoholMaker { get; private set; } = BuildAlcoholMaker();
        public static StructureInfo SiliconCollector { get; private set; } = BuildSiliconMine();
        public static StructureInfo ChipMaker { get; private set; } = BuildChipMaker();
        public static StructureInfo School { get; private set; } = BuildSchool();
        public static StructureInfo WindowMaker { get; private set; } = BuildWindowMaker();
        public static StructureInfo GlassBlower { get; private set; } = BuildGlassBlower();
        public static StructureInfo CopperMine { get; private set; } = BuildCopperMine();
        private static StructureInfo BuildCopperMine()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.Steel, 1);
            costBuilder.Add(EGood.Chips, 3);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Copper, 2);
            flowBuilder.Add(EGood.Energy, -1);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Copper Mine", cost, resourceCost, flow, service, EResource.Copper, 0, "CM");
        }
        private static StructureInfo BuildElectrolysisPlant()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.Chips, 2);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            resourceCostBuilder.Add(EResource.Land, 1);
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Water, -2);
            flowBuilder.Add(EGood.Energy, -1);
            flowBuilder.Add(EGood.Hydrogen, 1);
            flowBuilder.Add(EGood.Oxygen, 2);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Electrolysis Plant", cost, resourceCost, flow, service, EResource.Land, 0, "EL");
        }
        private static StructureInfo BuildMylarPlant()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.Chips, 5);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            resourceCostBuilder.Add(EResource.Land, 1);
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Mylar, 1);
            flowBuilder.Add(EGood.Carbon, -1);
            flowBuilder.Add(EGood.Hydrogen, -1);
            flowBuilder.Add(EGood.Oxygen, -1);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Mylar Plant", cost, resourceCost, flow, service, EResource.Land, 1, "MY");
        }
        private static StructureInfo BuildEnergyPlant()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.Chips, 5);
            costBuilder.Add(EGood.Silicon, 5);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Energy, 25);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Energy Plant", cost, resourceCost, flow, service, EResource.Land, 0, "E");
        }
        private static StructureInfo BuildIronMine()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.Steel, 1);
            costBuilder.Add(EGood.Chips, 3);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Iron, 2);
            flowBuilder.Add(EGood.Energy, -1);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Iron Mine", cost, resourceCost, flow, service, EResource.Iron, 0, "IM");
        }
        private static StructureInfo BuildWaterCollectionPlant()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.Chips, 3);
            costBuilder.Add(EGood.Steel, 1);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Water, 10);
            flowBuilder.Add(EGood.Energy, -1);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Water Collection Plant", cost, resourceCost, flow, service, EResource.Water, 0, "WC");
        }
        private static StructureInfo BuildFarm()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.Chips, 3);
            costBuilder.Add(EGood.Steel, 3);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Water, -1);
            flowBuilder.Add(EGood.Food, 2);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Farm", cost, resourceCost, flow, service, EResource.Land, 0, "F");
        }
        private static StructureInfo BuildHousing()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.Steel, 5);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            serviceBuilder.Add(EService.Housing, 50);
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Housing", cost, resourceCost, flow, service, EResource.Land, 0, "H");
        }
        private static StructureInfo BuildSteelSmelter()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.Iron, 1);
            costBuilder.Add(EGood.Chips, 1);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            resourceCostBuilder.Add(EResource.Land, 1);
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Iron, -2);
            flowBuilder.Add(EGood.Energy, -2);
            flowBuilder.Add(EGood.Steel, 1);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Steel Smelter", cost, resourceCost, flow, service, EResource.Land, 0, "ST");
        }
        private static StructureInfo BuildCarbonCollector()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.Chips, 2);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            resourceCostBuilder.Add(EResource.Land, 1);
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Energy, -1);
            flowBuilder.Add(EGood.Carbon, 1);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Carbon Collector", cost, resourceCost, flow, service, EResource.Land, 0, "CC");
        }
        private static StructureInfo BuildDoctorsOffices()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.Chips, 2);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            resourceCostBuilder.Add(EResource.Land, 1);
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Energy, -1);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            serviceBuilder.Add(EService.Healthcare, 100);
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Doctor's Offices", cost, resourceCost, flow, service, EResource.Land, 2, "D");
        }
        private static StructureInfo BuildAlcoholMaker()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.Chips, 1);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            resourceCostBuilder.Add(EResource.Land, 1);
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Energy, -1);
            flowBuilder.Add(EGood.Food, -1);
            flowBuilder.Add(EGood.Alcohol, 1);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Alcohol Maker", cost, resourceCost, flow, service, EResource.Land, 0, "A");
        }
        private static StructureInfo BuildSiliconMine()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.Steel, 1);
            costBuilder.Add(EGood.Chips, 3);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            resourceCostBuilder.Add(EResource.Land, 1);
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Energy, -1);
            flowBuilder.Add(EGood.Silicon, 1);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Silicon Mine", cost, resourceCost, flow, service, EResource.Silicon, 0, "SM");
        }
        private static StructureInfo BuildChipMaker()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.Steel, 1);
            costBuilder.Add(EGood.Chips, 1);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Energy, -5);
            flowBuilder.Add(EGood.Silicon, -1);
            flowBuilder.Add(EGood.Copper, -1);
            flowBuilder.Add(EGood.Chips, 1);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Chip Maker", cost, resourceCost, flow, service, EResource.Land, 0, "CH");
        }
        private static StructureInfo BuildSchool()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.Chips, 2);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            resourceCostBuilder.Add(EResource.Land, 1);
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Energy, -1);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            serviceBuilder.Add(EService.Education, 100);
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("School", cost, resourceCost, flow, service, EResource.Land, 1, "SC");
        }
        private static StructureInfo BuildGlassBlower()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.Chips, 1);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            resourceCostBuilder.Add(EResource.Land, 1);
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Silicon, -2);
            flowBuilder.Add(EGood.Glass, 1);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Glass Blower", cost, resourceCost, flow, service, EResource.Land, 0, "G");
        }
        private static StructureInfo BuildWindowMaker()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.Chips, 3);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            resourceCostBuilder.Add(EResource.Land, 1);
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Glass, -1);
            flowBuilder.Add(EGood.Windows, 1);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Window Maker", cost, resourceCost, flow, service, EResource.Land, 1, "WI");
        }
    }
}
