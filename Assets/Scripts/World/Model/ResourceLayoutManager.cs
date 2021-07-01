using Assets.Scripts.Interfaces;
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
        public ICodable[] Fields { get; }
        public Dictionary<ICodable, int> FieldMap { get; } = new Dictionary<ICodable, int>();
        private readonly IList<Patch> patches = new List<Patch>();
        private static readonly int PATCH_MIN_DISTANCE = 1;
        private static readonly int PATCH_MAX_DISTANCE = 4;
        private readonly Random rand;
        private readonly int rowSize;
        private static readonly ResourceInfo[] allResources = RegistryUtil.Resources.GetAllResources().ToArray();
        private readonly int habitability;

        public ResourceLayoutManager(int size, int seed, int habitability)
        {
            Fields = new ICodable[size];
            rand = new Random(seed);
            this.habitability = habitability;
            rowSize = Utils.GetRowSize(size);
        }
        public void LayoutResources()
        {
            InitializeFields();
            CreatePatches();
            FillPatches();
        }
        private void InitializeFields()
        {
            for (int i = 0; i < Fields.Length; i++)
                InitializeField(i);
        }
        private void InitializeField(int idx)
        {
            var ableToBeDeveloped = rand.Next(100) < habitability;
            var land = RegistryUtil.Resources.Get("Land");
            if (ableToBeDeveloped)
                Fields[idx] = land;
        }
        private void CreatePatches()
        {

            for (int i = 0; i < Fields.Length; i++)
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
            AddResource(resource, idx);
        }
        private void FillPatches()
        {
            for (int i = 0; i < Fields.Length; i++)
                FillPatch(i);
        }
        private void FillPatch(int idx)
        {
            if (Fields[idx] == null)
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
                AddResource(patch.Resource, idx);
        }
        private void AddResource(ResourceInfo newResource, int idx)
        {
            var currentResource = Fields[idx];
            if (currentResource != null && !currentResource.Name.Equals("Land"))
                FieldMap[currentResource]--;
            Fields[idx] = newResource;
            FieldMap[newResource] = FieldMap.ContainsKey(newResource) ? FieldMap[newResource] + 1 : 1;
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
