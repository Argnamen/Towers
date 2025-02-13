using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class PlayerResult
{
    public int levelNumber;
    public int movesCount;

    public PlayerResult(int level, int moves)
    {
        levelNumber = level;
        movesCount = moves;
    }
}

