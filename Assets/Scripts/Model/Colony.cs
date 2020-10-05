using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model
{
    public class Colony
    {
        private IDictionary<EGood, float> goods = new Dictionary<EGood, float>();
        public IDictionary<EGood, float> Goods
        {
            get => new Dictionary<EGood, float>(goods);
        }
        public Colony()
        {
            goods[EGood.Food] = 100;
            goods[EGood.Water] = 100;
            goods[EGood.BuildingMaterials] = 100;
        }
    }
}
