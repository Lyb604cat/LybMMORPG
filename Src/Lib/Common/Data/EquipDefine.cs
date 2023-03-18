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
    public class EquipDefine
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Slot { get; set;}
        public string Category { get; set; }
        public string Descript { get; set; }
        public float MaxMP { get; set; }
        public float STR { get; set; }
        public float INT { get; set; }
        public float DEX { get; set; }
        public float AD { get; set; }
        public float AP { get; set; }
        public float DEF { get; set; }
        public float MDEF { get; set; }
        public float SPD { get; set; }
        public float CRI { get; set; }
    }
}
