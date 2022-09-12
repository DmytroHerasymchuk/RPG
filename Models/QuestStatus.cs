using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Models
{
    public class QuestStatus : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Quest PlayerQuest { get; }
        public bool IsCompleted { get; set; }

        public QuestStatus(Quest playerQuest)
        {
            PlayerQuest = playerQuest;
            IsCompleted = false;
        }
    }
}
