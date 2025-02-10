using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring
{
    public int Size { get; private set; }
    public Peg CurrentPeg { get; set; }

    public Ring(int size)
    {
        Size = size;
    }
}
