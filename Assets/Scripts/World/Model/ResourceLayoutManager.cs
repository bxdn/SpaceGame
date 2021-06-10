using Assets.Scripts.Model;
using Assets.Scripts.Registry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.World.Model
{
    [Serializable]
    public class ResourceLayoutManager
    {
        private readonly Area[] fields;
        private readonly IList<Patch> patches = new List<Patch>();
        private static readonly int PATCH_MIN_DISTANCE = 1;
        private static readonly int PATCH_MAX_DISTANCE = 4;
        private readonly Random rand;
        private readonly int rowSize;
        private static readonly ResourceInfo[] allResources = RegistryUtil.Resources.GetAllResources().ToArray();
        private readonly int habitability;

        public ResourceLayoutManager(int size, int seed, int habitability)
        {
            fields = new Area[size];
            rand = new Random(seed);
            this.habitability = habitability;
            rowSize = Utils.GetRowSize(size);
        }
        public Area[] LayoutResources()
        {
            InitializeFields();
            CreatePatches();
            FillPatches();
            return fields;
        }
        private void InitializeFields()
        {
            for (int i = 0; i < fields.Length; i++)
                InitializeField(i);
        }
        private void InitializeField(int idx)
        {
            var ableToBeDeveloped = rand.Next(100) < habitability;
            var land = RegistryUtil.Resources.Get("Land");
            if (ableToBeDeveloped)
                fields[idx] = new Area(land);
        }
        private void CreatePatches()
        {

            for (int i = 0; i < fields.Length; i++)
                CreatePotentialPatch(i);
        }
        private void CreatePotentialPatch(int idx)
        {
            var placePatchHere = rand.Next(100) == 0;
            if (placePatchHere)
                CreatePatch(idx);
        }
        private void CreatePatch(int idx)
        {
            var resource = allResources[rand.Next(allResources.Count())];
            if (resource.Name.Equals("Land"))
            {
                CreatePatch(idx);
                return;
            }
            patches.Add(new Patch(idx, rand.Next(PATCH_MIN_DISTANCE, PATCH_MAX_DISTANCE), resource));
            fields[idx] = new Area(resource);
        }
        private void FillPatches()
        {
            for (int i = 0; i < fields.Length; i++)
                FillPatch(i);
        }
        private void FillPatch(int idx)
        {
            if (fields[idx] == null)
                return;
            foreach (var patch in patches)
                TryFillSquare(patch, idx);
        }
        private void TryFillSquare(Patch patch, int idx)
        {
            var dist = Utils.GetDistance(idx, patch.Idx, rowSize);
            var withinPatch = dist < patch.MaxDistance;
            var toPlaceInPatch = rand.NextDouble() > (dist / patch.MaxDistance);
            if (withinPatch && toPlaceInPatch)
                fields[idx] = new Area(patch.Resource);
        }
        [Serializable]
        private class Patch
        {
            public int Idx { get; }
            public int MaxDistance { get; }
            public ResourceInfo Resource { get; }
            public Patch(int idx, int maxDist, ResourceInfo resource)
            {
                Idx = idx;
                MaxDistance = maxDist;
                Resource = resource;
            }
        }
    }
}
