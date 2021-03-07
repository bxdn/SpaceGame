using Assets.Scripts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    [System.Serializable]
    public class Player
    {
        public Domain Domain { get; } = new Domain();
        public ISet<Colony> Colonies { get; } = new HashSet<Colony>();
    }
}
