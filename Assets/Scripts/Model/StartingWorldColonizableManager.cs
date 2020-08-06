using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model
{
    public class StartingWorldColonizableManager : IColonizableManager
    {
        public Domain Owner { get; set; }

        public StartingWorldColonizableManager()
        {
            Owner = Player.Domain;
        }
    }
}
