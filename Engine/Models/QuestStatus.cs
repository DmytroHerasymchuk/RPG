using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class QuestStatus : BaseNotificationClass
    {
        private bool _isCompleted;
        public Quest PlayerQuest { get; }
        public bool IsCompleted 
        {
            get 
            { 
                return _isCompleted; 
            }
            set 
            { 
                _isCompleted = value;
                OnPropertyChanged();
            }
        }

        public QuestStatus(Quest playerQuest)
        {
            PlayerQuest = playerQuest;
            IsCompleted = false;
        }
    }
}
