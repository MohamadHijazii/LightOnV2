using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class MenuLevels : MonoBehaviour
{ 
    public Transform panCenter;
    Vector3 next_pos;
    Stack<Vector3> next_stack;
    Stack<Vector3> prev_stack;
    public Vector3 current_panel_position;
    public GameObject panel;
    public Button l;
    Button []lvls;
    int N;   //number of levels
    int panel_index;
    public Button nextbtn;
    public Button prevbtn;
    private void Start()
    {
        Game game = new Game();        
        N = Game.levels.Count;
        lvls = new Button[N];
        panel_index = 0;
        iniPanels();
        int n =( N / 8 )+1;
        current_panel_position = new Vector3(78.2f, 192.6f);
        //next_pos = panCenter.position;
        next_stack = new Stack<Vector3>();
        prev_stack = new Stack<Vector3>();
        float hop = 0;
        while (n-- !=0)
        {
            Vector3 temp = new Vector3(0,0,0);
            hop += 300;
            next_pos.Set(next_pos.x - 300, 192.6f, 0);
            temp = next_pos;
            prev_stack.Push(temp);
            
        }
        while (prev_stack.Count != 0)
        {
            next_stack.Push(prev_stack.Pop());
        }
        check();
    }


    public void goNext()
    {
        next_pos = next_stack.Pop();
        panCenter.transform.position = Vector3.Lerp(current_panel_position, next_pos, 0);
        prev_stack.Push(current_panel_position);
        current_panel_position = next_pos;
        check();
        Debug.Log(panCenter.position);
        
    }

    public void goPrev()
    {
        next_pos = prev_stack.Pop();
        panCenter.transform.position = Vector3.Lerp(current_panel_position, next_pos, 0);
        next_stack.Push(current_panel_position);
        current_panel_position = next_pos;
        check();
    }

    private void iniButton(Transform target)
    {
        int n = panel_index;
        for (int i = n; i < n + 8 && i < N; i++)
        {
            int c = i;
            Button newbtn = Instantiate<Button>(l, target);
            lvls[i] = newbtn;
            lvls[i].GetComponentInChildren<TextMeshProUGUI>().text = (i + 1) + "";// + "\n"+getN(i);
            lvls[i].onClick.AddListener(() => goToLevel(c));
        }
        panel_index += 8;
    }

    public void iniPanels()
    {
        //Vector3 v = new Vector3(0, 0);
        //panCenter.SetPositionAndRotation(v, Quaternion.identity);
        Transform toAdd = panCenter;
        for (int i = 0; i <= N / 8; i++)
        {
            var brain = Instantiate(panel, toAdd);
            var brain1 = brain;
            iniButton(brain1.transform);
        }

    }

    public void check()
    {
        if (next_stack.Count == 0)
            nextbtn.interactable = false;
        if (next_stack.Count != 0)
            nextbtn.interactable = true;

        if (prev_stack.Count == 0)
            prevbtn.interactable = false;
        if (prev_stack.Count != 0)
            prevbtn.interactable = true;
    }

    public void goToLevel(int n)
    {
        GameManager.current_level = Game.levels[n];
        SceneManager.LoadScene("Game");
    }

}
