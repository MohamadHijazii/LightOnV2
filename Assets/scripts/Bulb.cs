using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bulb
{
    public int index;
    public bool state;
    public List<int> effected_indexes;
    public Sprite sprite;
    public Button btn;
    static Image on;
    static Image off;

    public Bulb(int index,bool state,List<int> ef)
    {
        this.index = index;
        this.state = state;
        this.effected_indexes = ef;
    }

    private Bulb(int index) // waiting for a  reason to put it or remove it
    {
        this.index = index;
        this.state = false;
    }

    public bool switchState()
    {
        if (state)
            return turnOff();
        return turnOn();
    }

    public bool turnOn()
    {
        state = true;
        btn.GetComponent<Image>().sprite = on.sprite;
        return true;
    }

    public bool turnOff()
    {
        state = false;
        btn.GetComponent<Image>().sprite = on.sprite;
        return false;
    }

    public override string ToString()
    {
        string ret = "";
        ret += index + ": ";
        foreach(int e in effected_indexes)
        {
            ret += e + "  ";
        }
        ret += "\t";
        return ret;
    }

    public static void setImages(Image _on,Image _off)
    {
        on = _on;
        off = _off;
    }


}
