using System.Collections.Generic;

namespace KRPGLib.Fantasy.Feats
{
    public class FeatNode
    {
        public string Code;
        public string Name;
        public string Description;
        public List<string> Parents = new List<string>();

        public bool Unlocked;

        public FeatNode(string code, string name, string desc, params string[] parents)
        {
            Code = code;
            Name = name;
            Description = desc;
            Parents.AddRange(parents);
        }
    }
}