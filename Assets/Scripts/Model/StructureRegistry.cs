using System;
using System.Collections.Immutable;

namespace Assets.Scripts.Model
{
    public static class StructureRegistry
    {
        public static StructureInfo Housing { get; private set; } = BuildHousing();
        public static StructureInfo Farm { get; private set; } = BuildFarm();
        public static StructureInfo WaterCollectionPlant { get; private set; } = BuildWaterCollectionPlant();
        public static StructureInfo LumberMill { get; private set; }
        public static StructureInfo MetalMine { get; private set; }
        public static StructureInfo EnergyPlant { get; private set; } = BuildEnergyPlant();
        public static StructureInfo GasCollectionPlant { get; private set; }

        private static void BuildGasCollectionPlant()
        {
            throw new NotImplementedException();
        }

        private static StructureInfo BuildEnergyPlant()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.BuildingMaterials, 10);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            resourceCostBuilder.Add(EResource.Land, 1);
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Energy, 5);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Energy Plant", cost, resourceCost, flow, service, 10,
                x => x.Resources[EResource.EnergySource] > 0);
        }

        private static void BuildMetalMine()
        {
            throw new NotImplementedException();
        }

        private static void BuildLumberMill()
        {
            throw new NotImplementedException();
        }

        private static StructureInfo BuildWaterCollectionPlant()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.BuildingMaterials, 10);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            resourceCostBuilder.Add(EResource.Land, 1);
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Water, 2);
            flowBuilder.Add(EGood.Energy, -1);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Water Collection Plant", cost, resourceCost, flow, service, 10,
               x => x.Resources[EResource.Water] > 0);
        }

        private static StructureInfo BuildFarm()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.BuildingMaterials, 10);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            resourceCostBuilder.Add(EResource.Land, 1);
            resourceCostBuilder.Add(EResource.ArableLand, 1);
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Water, -1);
            flowBuilder.Add(EGood.Food, 2);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Farm", cost, resourceCost, flow, service, 10, x => x.Resources[EResource.ArableLand] > 0);
        }

        private static StructureInfo BuildHousing()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.BuildingMaterials, 10);
            var cost = costBuilder.ToImmutable();
            var resourceCostBuilder = ImmutableDictionary.CreateBuilder<EResource, int>();
            var resourceCost = resourceCostBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            serviceBuilder.Add(EService.Housing, 50);
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Housing", cost, resourceCost, flow, service, 0, x => true);
        }
    }
}
