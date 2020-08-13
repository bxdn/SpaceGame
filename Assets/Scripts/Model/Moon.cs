using Assets.Scripts;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using System;
[System.Serializable]
public class Moon : Orbiter, IArable
{
    private static readonly int MIN_SIZE = 10;
    private static readonly int MAX_SIZE = 50;
    private static readonly int ORBITER_DELTA = 10;
    public override int Size { get; }
    public override string Name { get; set; }
    public override string Type => "Moon";
    public IColonizableManager ColonizableManager { get; private set; }
    public Moon(Planet parent, int id) : base(parent)
    {
        Name = "M" + parent.Name.Substring(1) + "-" + id;
        Size = ColonizerR.r.Next(MIN_SIZE, Math.Min(MAX_SIZE, parent.Size - ORBITER_DELTA));
        ColonizableManager = new ColonizableManager(this);
    }

    public bool DesignateStartingWorld()
    {
        return ((ColonizableManager = new StartingWorldColonizableManager(this)) as StartingWorldColonizableManager).DesignateStartingColony();
    }
}
