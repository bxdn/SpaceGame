using Assets.Scripts;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using System;

public class RockyPlanet : Planet, IArable
{
    public override string Type => "Rocky Planet";
    protected sealed override int MaxSize => 50;
    protected sealed override int MinSize => 25;
    protected sealed override int MaxOrbitals => 4;
    public IColonizableManager ColonizableManager { get; private set; }
    public RockyPlanet(SolarSystem sol, char id) : base(sol, id)
    {
        ColonizableManager = new ColonizableManager(this);
    }

    public void DesignateStartingWorld()
    {
        //ColonizableManager = new StartingWorldColonizableManager();
        ColonizableManager = new ColonizableManager(this);
        ColonizableManager.Owner = Player.Domain;
    }

    public void RenderColony()
    {
        WorldGeneration.ClearGUI();
        CameraController.Reset();
        new ColonyGUI(this);
    }
}
