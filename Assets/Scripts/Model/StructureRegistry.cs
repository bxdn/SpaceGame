using System;
using System.Collections.Immutable;

namespace Assets.Scripts.Model
{
    public static class StructureRegistry
    {
        public static StructureInfo Housing { get; private set; } = BuildHousing();
        public static StructureInfo Farm { get; private set; } = BuildFarm();
        public static StructureInfo WaterCollectionPlant { get; private set; } = BuildWaterCollectionPlant();
        public static StructureInfo LumberMill { get; private set; } = BuildLumberMill();
        public static StructureInfo IronMine { get; private set; } = BuildIronMine();
        public static StructureInfo EnergyPlant { get; private set; } = BuildEnergyPlant();
        public static StructureInfo ElectrolysisPlant { get; private set; } = BuildElectrolysisPlant();
        public static StructureInfo SteelSmelter { get; private set; } = BuildSteelSmelter();
        public static StructureInfo ConstructionManufacturer { get; private set; } = BuildConstructionManufacturer();
        public static StructureInfo MylarPlant { get; private set; } = BuildMylarPlant();
        public static StructureInfo CarbonCollector { get; private set; } = BuildCarbonCollector();
        public static StructureInfo DoctorsOffices { get; private set; } = BuildDoctorsOffices();
        public static StructureInfo AlcoholMaker { get; private set; } = BuildAlcoholMaker();
        public static StructureInfo SiliconCollector { get; private set; } = BuildSiliconCollector();
        public static StructureInfo ChipMaker { get; private set; } = BuildChipMaker();
        public static StructureInfo ScreenMaker { get; private set; } = BuildScreenMaker();
        public static StructureInfo School { get; private set; } = BuildSchool();
        public static StructureInfo WindowMaker { get; private set; } = BuildWindowMaker();
        public static StructureInfo GlassBlower { get; private set; } = BuildGlassBlower();
        private static StructureInfo BuildElectrolysisPlant()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.BuildingMaterials, 3);
            costBuilder.Add(EGood.Chips, 5);
            costBuilder.Add(EGood.Screens, 1);
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
            return new StructureInfo("Electrolysis Plant", cost, resourceCost, flow, service, 2, 0);
        }
        private static StructureInfo BuildMylarPlant()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.BuildingMaterials, 3);
            costBuilder.Add(EGood.Chips, 10);
            costBuilder.Add(EGood.Screens, 2);
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
            return new StructureInfo("Mylar Plant", cost, resourceCost, flow, service, 5, 1);
        }
        private static StructureInfo BuildEnergyPlant()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.BuildingMaterials, 3);
            costBuilder.Add(EGood.Chips, 5);
            costBuilder.Add(EGood.Screens, 5);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            resourceCostBuilder.Add(EResource.Land, 1);
            resourceCostBuilder.Add(EResource.EnergySource, 1);
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Energy, 10);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Energy Plant", cost, resourceCost, flow, service, 5, 0);
        }
        private static StructureInfo BuildIronMine()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.BuildingMaterials, 5);
            costBuilder.Add(EGood.Chips, 5);
            costBuilder.Add(EGood.Screens, 2);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            resourceCostBuilder.Add(EResource.Land, 1);
            resourceCostBuilder.Add(EResource.Iron, 1);
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Iron, 2);
            flowBuilder.Add(EGood.Energy, -1);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Iron Mine", cost, resourceCost, flow, service, 5, 0);
        }
        private static StructureInfo BuildLumberMill()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.BuildingMaterials, 5);
            costBuilder.Add(EGood.Chips, 5);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            resourceCostBuilder.Add(EResource.ArableLand, 4);
            resourceCostBuilder.Add(EResource.Land, 5);
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Wood, 2);
            flowBuilder.Add(EGood.Energy, -1);
            flowBuilder.Add(EGood.Water, -1);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Lumber Mill", cost, resourceCost, flow, service, 5, 0);
        }
        private static StructureInfo BuildWaterCollectionPlant()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.BuildingMaterials, 3);
            costBuilder.Add(EGood.Chips, 3);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            resourceCostBuilder.Add(EResource.Land, 1);
            resourceCostBuilder.Add(EResource.Water, 1);
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Water, 5);
            flowBuilder.Add(EGood.Energy, -1);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Water Collection Plant", cost, resourceCost, flow, service, 5, 0);
        }
        private static StructureInfo BuildFarm()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.BuildingMaterials, 5);
            costBuilder.Add(EGood.Chips, 5);
            costBuilder.Add(EGood.Screens, 1);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            resourceCostBuilder.Add(EResource.Land, 5);
            resourceCostBuilder.Add(EResource.ArableLand, 4);
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Water, -1);
            flowBuilder.Add(EGood.Food, 2);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Farm", cost, resourceCost, flow, service, 10, 0);
        }
        private static StructureInfo BuildHousing()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.BuildingMaterials, 5);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            resourceCostBuilder.Add(EResource.Land, 1);
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            serviceBuilder.Add(EService.Housing, 50);
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Housing", cost, resourceCost, flow, service, 0, 0);
        }
        private static StructureInfo BuildSteelSmelter()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.BuildingMaterials, 1);
            costBuilder.Add(EGood.Chips, 1);
            costBuilder.Add(EGood.Screens, 1);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            resourceCostBuilder.Add(EResource.Land, 1);
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Iron, -2);
            flowBuilder.Add(EGood.Steel, 1);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Steel Smelter", cost, resourceCost, flow, service, 5, 0);
        }
        private static StructureInfo BuildConstructionManufacturer()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.BuildingMaterials, 2);
            costBuilder.Add(EGood.Chips, 10);
            costBuilder.Add(EGood.Screens, 5);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            resourceCostBuilder.Add(EResource.Land, 1);
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Mylar, -1);
            flowBuilder.Add(EGood.Steel, -1);
            flowBuilder.Add(EGood.BuildingMaterials, 1);
            flowBuilder.Add(EGood.Energy, -1);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Construction Manufacturer", cost, resourceCost, flow, service, 5, 1);
        }
        private static StructureInfo BuildCarbonCollector()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.BuildingMaterials, 3);
            costBuilder.Add(EGood.Chips, 2);
            costBuilder.Add(EGood.Screens, 1);
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
            return new StructureInfo("Carbon Collector", cost, resourceCost, flow, service, 2, 0);
        }
        private static StructureInfo BuildDoctorsOffices()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.BuildingMaterials, 1);
            costBuilder.Add(EGood.Chips, 2);
            costBuilder.Add(EGood.Screens, 3);
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
            return new StructureInfo("Doctor's Offices", cost, resourceCost, flow, service, 1, 2);
        }
        private static StructureInfo BuildAlcoholMaker()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.BuildingMaterials, 2);
            costBuilder.Add(EGood.Chips, 1);
            costBuilder.Add(EGood.Screens, 1);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            resourceCostBuilder.Add(EResource.Land, 1);
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Energy, -1);
            flowBuilder.Add(EGood.Food, -2);
            flowBuilder.Add(EGood.Alcohol, 1);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Alcohol Maker", cost, resourceCost, flow, service, 3, 0);
        }
        private static StructureInfo BuildSiliconCollector()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.BuildingMaterials, 3);
            costBuilder.Add(EGood.Chips, 2);
            costBuilder.Add(EGood.Screens, 1);
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
            return new StructureInfo("Silicon Collector", cost, resourceCost, flow, service, 2, 0);
        }
        private static StructureInfo BuildChipMaker()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.BuildingMaterials, 3);
            costBuilder.Add(EGood.Chips, 10);
            costBuilder.Add(EGood.Screens, 5);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            resourceCostBuilder.Add(EResource.Land, 1);
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Energy, -5);
            flowBuilder.Add(EGood.Silicon, -1);
            flowBuilder.Add(EGood.Chips, 1);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Chip Maker", cost, resourceCost, flow, service, 5, 2);
        }
        private static StructureInfo BuildScreenMaker()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.BuildingMaterials, 3);
            costBuilder.Add(EGood.Chips, 10);
            costBuilder.Add(EGood.Screens, 5);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            resourceCostBuilder.Add(EResource.Land, 1);
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Energy, -3);
            flowBuilder.Add(EGood.Silicon, -1);
            flowBuilder.Add(EGood.Carbon, -1);
            flowBuilder.Add(EGood.Screens, 1);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Screen Maker", cost, resourceCost, flow, service, 5, 2);
        }
        private static StructureInfo BuildSchool()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.BuildingMaterials, 1);
            costBuilder.Add(EGood.Chips, 2);
            costBuilder.Add(EGood.Screens, 2);
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
            return new StructureInfo("School", cost, resourceCost, flow, service, 1, 1);
        }
        private static StructureInfo BuildGlassBlower()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.BuildingMaterials, 1);
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
            return new StructureInfo("Glass Blower", cost, resourceCost, flow, service, 5, 0);
        }
        private static StructureInfo BuildWindowMaker()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.BuildingMaterials, 1);
            costBuilder.Add(EGood.Chips, 3);
            costBuilder.Add(EGood.Screens, 3);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            resourceCostBuilder.Add(EResource.Land, 1);
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Glass, -2);
            flowBuilder.Add(EGood.Windows, 1);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Window Maker", cost, resourceCost, flow, service, 3, 1);
        }
    }
}
