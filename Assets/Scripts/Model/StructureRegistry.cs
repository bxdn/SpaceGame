using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model
{
    public static class StructureRegistry
    {
        public static StructureInfo Housing { get; private set; } = BuildHousing();
        public static StructureInfo Farm { get; private set; } = BuildFarm();
        public static StructureInfo WaterCollectionPlant { get; private set; } = BuildWaterCollectionPlant();
        public static StructureInfo LumberMill { get; private set; }
        public static StructureInfo MetalMine { get; private set; }
        public static StructureInfo EnergyPlant { get; private set; }
        public static StructureInfo GasCollectionPlant { get; private set; }

        static StructureRegistry()
        {
            BuildHousing();
            BuildFarm();
            BuildWaterCollectionPlant();
            /*BuildLumberMill();
            BuildMetalMine();
            BuildEnergyPlant();
            BuildGasCollectionPlant();*/
        }

        private static void BuildGasCollectionPlant()
        {
            throw new NotImplementedException();
        }

        private static void BuildEnergyPlant()
        {
            throw new NotImplementedException();
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
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Water, 2);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Water Collection Plant", cost, flow, service, 10, 
                x => x.Resource == EResource.Water);
        }

        private static StructureInfo BuildFarm()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.BuildingMaterials, 10);
            var cost = costBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            flowBuilder.Add(EGood.Water, -1);
            flowBuilder.Add(EGood.Food, 2);
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Farm", cost, flow, service, 10, x => x.Arable);
        }

        private static StructureInfo BuildHousing()
        {
            var costBuilder = ImmutableDictionary.CreateBuilder<EGood, int>();
            costBuilder.Add(EGood.BuildingMaterials, 10);
            var cost = costBuilder.ToImmutable();
            var flowBuilder = ImmutableDictionary.CreateBuilder<EGood, float>();
            var flow = flowBuilder.ToImmutable();
            var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, float>();
            serviceBuilder.Add(EService.Housing, 10);
            var service = serviceBuilder.ToImmutable();
            return new StructureInfo("Housing", cost, flow, service, 0, x => true);
        }
    }
}
