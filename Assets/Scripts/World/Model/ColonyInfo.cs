using Assets.Scripts.Model;
using System;

[Serializable]
public class ColonyInfo
{
    public Colony Colony { get; }
    public int Idx { get; }
    public ColonyInfo(Colony colony, int idx)
    {
        Colony = colony;
        Idx = idx;
    }
}