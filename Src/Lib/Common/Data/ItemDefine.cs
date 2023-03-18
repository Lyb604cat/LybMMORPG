using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillBridge.Message;

namespace Common.Data
{
    /// <summary>
    /// lyb add
    /// </summary>
    public class ItemDefine
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set;}
        public string Type { get; set;}
        public string Category { get; set; }
        public int Level { get; set; }
        public bool CanUse { get; set; }
        public float UseCD { get; set; }
        public int Price { get; set; }
        public int SellPrice { get; set; }
        public int StackLimit { get; set; }
        public string Icon { get; set; }
        public string Function { get; set; }
        public int Param { get; set; }
    }
}
