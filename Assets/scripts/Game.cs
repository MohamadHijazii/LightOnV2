using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Cryptography;
using System;
public class Game
{
    public Sprite onBulb;
    public Sprite offBulb;
    public static List<Level> levels;
    public string hash_path = "/hash.brain";
    public string levels_path = "/levels.brain";

    public Game()
    {
        levels = readContext();

    }

    public List<Level> readContext()
    {
        List<Level> levels = new List<Level>();
        string h_path = Application.persistentDataPath + hash_path;
        using (StreamReader sr = File.OpenText(h_path))
        {
            int lv_number = 0;
            while (!sr.EndOfStream)
            {
                Level level;
                List<Bulb> bulbs = new List<Bulb>();
                string dec = Security.Decrypt(sr.ReadLine(), "89ABCDEF01234567");
                string[] s = dec.Split('/');
                int n = Convert.ToInt16(s[0]);
                //char type = s[1][0];
                for(int i = 1; i < s.Length; i++)
                {
                    string[] ef = s[i].Split(' ');
                    List<int> eff = new List<int>();
                    for(int j = 0; j < ef.Length; j++)
                    {
                        eff.Add(Convert.ToInt16(ef[j]));                        
                    }
                    Bulb b = new Bulb(i - 1, false, eff);
                    bulbs.Add(b);
                }
                level = new Level(lv_number++,bulbs);
                levels.Add(level);

            }
        }
        return levels;
    }

    public void writeContext()
    {
            string h_path = Application.persistentDataPath + hash_path;
            var Data = getData().FindAll();
            string lvl_path = Application.persistentDataPath + levels_path;
        
        using (StreamWriter sl = File.AppendText(lvl_path),sh = File.AppendText(h_path))
        {
            foreach (var data in Data)
            {
                string oneLevel = "";
                oneLevel += data["number"];
                int n = data["number"].AsInt32;
                oneLevel += "/";
                //oneLevel += data["type"];
                for(int i = 0; i < n; i++)
                {
                    string nn = "n";
                    nn += i;
                    oneLevel += data[nn];
                    if(i!=n-1)
                        oneLevel += '/';
                }
                string enc = Security.Encrypt(oneLevel, "89ABCDEF01234567");
                Debug.Log(enc);
                sh.WriteLine(enc);
                sl.WriteLine(oneLevel);
            }
        }

    }

    public MongoCollection<BsonDocument> getData()
    {
        MongoClient client = new MongoClient("mongodb://127.0.0.1:27017");
        var server = client.GetServer();
        var db = server.GetDatabase("Light");
        return db.GetCollection<BsonDocument>("levels");
    }

    public bool AssertSec()
    {
        string op = "iBxrHYrn0jWMsPqynyeRG0fMlpu+tLpwE0lJqrXAzGHGYEJTckKQJQCXbdfUvK4YQL9TeZqsSA5Ad+BAAcIYxqmtTaJ9QAZWvILgUIWbt1DugsBSU7PlkAym9EG7MVrwSbBFxH8m1M+OULnj8YIsqeySijzDkAt+T++tl73zu5wyaQ8rHTy+VEeaOBptGvpK2IqfZwk9w1lkM4aqrkJr0HrxL2ZSCDe3jyNdkrq/+XSfNvEwvXb/TR7HZuF6iWsFjCpwrKOIuBG1RVWACSRbMNp9OM8SblgPV//NiNIQRp4Ey7KH8DDpUKvCFwmCXvgSU2dN/m1B1b9Uk3sNy33ooT7SjmDLtw4aJ0oCGS5eYiySbJVGHrLjZuzBuaplxfnbzJmyHEPLZrSF19xLiDUeOfmXFNVgINLLNXpgU5Eymk+cO9Hm/Ykl+qenlcahUZ97Sw8FnII+K6VLrYhA0gGWwq9PEB5QvHWD/W08Hoxnd5EnN2aRy3/CgWvypaDlQvnd9G1GhSdxe4q4qH4Df4z0vy3iY93FgIR8G4lU/LNk/skOUMl5SSUlYMyqHv410S97xxrSpl2y7UKUHWMgqRCO+SDTZltc77Geuxdbr+sQ4PbJZyoLPMUlf7SFKi/tEffIhZwrSdwb9iw7ur0Px5iqyUbAb1JbJX9zI/85KKHwy+YDHSrFZdJems8A0P9Ej35+Pl4gkeuVXIQcotx5A+RVKH72Hin55b+u";
        string h_path = Application.persistentDataPath + hash_path;
        using (StreamReader sr = File.OpenText(h_path))
        {
            string all = sr.ReadToEnd();
            string enc = Security.Encrypt(all, "FEDCBA9876543210");
            sr.Close();
            return enc.Equals(op);
        }
    }
}
