using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelDataBase : ScriptableObject
{
    public Level[] level;

    public int levelCount
    {
        get
        {
            return level.Length;
        }
    }

    public Level GetLevel(int index)
    {
        return level[index];
    }
}
