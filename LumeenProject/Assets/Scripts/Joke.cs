using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


// Class with the only purpose of deserializing
    class Joke
    {
    public int id;
    public string type;
    public string setup;
    public string punchline;
    public static Joke CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Joke>(jsonString);
    }
}

