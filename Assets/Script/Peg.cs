using System.Collections.Generic;
public class Peg
{
    public List<Ring> Rings { get; private set; } = new List<Ring>();

    public void AddRing(Ring ring)
    {
        Rings.Add(ring);
        ring.CurrentPeg = this;
    }

    public void RemoveRing(Ring ring)
    {
        Rings.Remove(ring);
    }
}
