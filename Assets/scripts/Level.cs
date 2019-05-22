using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level 
{
    public int level_number;
    public static int crrent_level_number = 0;
    public List<Bulb> bulbs;
    public int difficulty;
    public bool winned;

    public Level(int level_number)
    {
        this.level_number = level_number;
    }

    public Level(int lv,List<Bulb> b)
    {
        level_number = lv;
        bulbs = b;
    }

    public override string ToString()
    {
        string ret = "";
        ret += "level number: " + level_number+"\n";
        foreach(Bulb b in bulbs)
        {
            ret += b.ToString();
        }
        return ret;
    }

    public bool won()
    {
        foreach(var b in bulbs)
        {
            if (!b.state)
                return false;
        }
        return true;
    }


}
