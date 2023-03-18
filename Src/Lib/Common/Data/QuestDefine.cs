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
    public class QuestDefine
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int LimitLevel { get; set;}
        public int LimitClass { get; set; }
        public int PreQuest { get; set; }
        public string Type { get; set; }
        public int AcceptNPC { get; set; }
        public int SubmitNPC { get; set; }
        public string Overview { get; set; }
        public string Dialog { get; set; }
        public string DialogAccept { get; set; }
        public string DialogDeny { get; set; }
        public string DialogFinish { get; set; }
        public string Target1 { get; set; }
        public int Target1ID { get; set; }
        public int Target1Num { get; set; }
        public string Target2 { get; set; }
        public int Target2ID { get; set; }
        public int Target2Num { get; set; }
        public string Target3 { get; set; }
        public int Target3ID { get; set; }
        public int Target3Num { get; set; }
        public int RewardGold { get; set; }
        public int RewardExp { get; set; }
        public int RewardItem1 { get; set; }
        public int RewardItem1Count { get; set; }

    }
}
