using Assets.Scripts.Controllers;
using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
namespace Assets.Scripts.Model
{
    public class Colony
    {
        private readonly IDictionary<EGood, float> goods = new Dictionary<EGood, float>();
        private readonly IDictionary<EService, float> services = new Dictionary<EService, float>();
        private readonly ISet<LandUnit> units = new HashSet<LandUnit>();
        public float Influence { get; private set; } = 50;
        public int Workers { get; set; } = 100;
        public int Population { get; private set; } = 150;
        public IDictionary<EGood, float> Goods
        {
            get => new Dictionary<EGood, float>(goods);
        }
        public IDictionary<EService, float> Services
        {
            get => new Dictionary<EService, float>(services);
        }
        public LevelInfo CurrentLevel { get; private set; } = LevelInfo.FirstLevel;
        public Colony()
        {
            ColonyUpdater.AddColony(this);
            goods[EGood.Food] = 100;
            goods[EGood.Water] = 100;
            goods[EGood.BuildingMaterials] = 100;
            goods[EGood.Energy] = 100;
        }
        public void IncrementGood(EGood good, float amount)
        {
            goods[good] = goods.ContainsKey(good) ? goods[good] + amount : amount;
            if (goods[good] < 0)
                LoseInfluence(good);
        }
        private void LoseInfluence(EGood good)
        {
            Influence += goods[good];
            goods[good] = 0;
        }
        public void IncrementService(EService service, float amount)
        {
            services[service] = services.ContainsKey(service) ? services[service] + amount : amount;
            if (services[service] < 0)
                services[service] = 0;
        }
        public void AddLandUnitWorked(LandUnit u)
        {
            units.Add(u);
        }
        public void TickForward()
        {
            foreach (LandUnit unit in units)
                WorkLand(unit);
            ProcessDemand();
            if (services.ContainsKey(EService.Housing) 
                && services[EService.Housing] > Population * CurrentLevel.ServicesPerPop[EService.Housing])
                IncrementPop();
            // Update the dialog if this colony is selected
            if (Selection.CurrentSelection is ISelectable s
                && s.ModelObject is IColonizable col
                && col.ColonizableManager is IColonizableManager m
                && m.Colony == this)
                ColonyDialogController.Update(col);
        }
        private void IncrementPop()
        {
            int amountToIncrease = (int) Math.Min(.01 * Population, services[EService.Housing] - Population * CurrentLevel.ServicesPerPop[EService.Housing]);
            Population += amountToIncrease;
            for (int i = 0; i < amountToIncrease; i++)
                IncrementWorkers();
        }
        private void IncrementWorkers()
        {
            if (ColonizerR.r.Next(100) > 33)
                Workers++;
        }
        private void WorkLand(LandUnit unit)
        {
            var structure = unit.Structure;
            if (structure != null)
                WorkStructure(structure);
        }
        private void WorkStructure(StructureInfo structure)
        {
            foreach (var goodInfo in structure.Flow)
                IncrementGood(goodInfo.Key, goodInfo.Value);
        }
        private void ProcessDemand()
        {
            var goodRequirements = CurrentLevel.GoodsPerPop;
            foreach (var goodPair in goodRequirements)
                IncrementGood(goodPair.Key, (-goodPair.Value/100f) * Population);
        }
    }
}
