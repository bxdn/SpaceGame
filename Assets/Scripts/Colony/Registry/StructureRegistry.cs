using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class StructureRegistry
    {
        private readonly IDictionary<String, StructureInfo> structures = new Dictionary<String, StructureInfo>();
        public StructureRegistry()
        {
            foreach (var structure in Resources.Load<TextAsset>("Structures").text.Split(new string[] { "\r\n" }, StringSplitOptions.None))
                AddStructure(structure);
        }
        private void AddStructure(string structure)
        {
            var data = structure.Split('|');
            var name = data[0];
            var cost = GetMap(data[1]);
            var goodFlow = GetMap(data[2]);
            var serviceFlow = GetMap(data[3]);
            var prereq = data[4];
            var workerLevel = int.Parse(data[5]);
            var code = data[6];
            structures.Add(name, new StructureInfo(name, cost, goodFlow, serviceFlow, prereq, workerLevel, code));
        }
        private ImmutableDictionary<string, float> GetMap(string data)
        {
            var builder = ImmutableDictionary.CreateBuilder<string, float>();
            if (string.IsNullOrEmpty(data))
                return builder.ToImmutable();
            foreach (var item in data.Split(','))
            {
                var info = item.Split('=');
                builder.Add(info[0], float.Parse(info[1]));
            }
           return builder.ToImmutable();
        }
        public StructureInfo GetStructure(string name)
        {
            if (structures.ContainsKey(name))
                return structures[name];
            return null;
        }
        public IEnumerable<StructureInfo> GetAllStructures()
        {
            return structures.Values;
        }
    }
}
