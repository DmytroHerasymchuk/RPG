using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Dialog
    {
        public string ShortDialog { get; set; }
        public string AnswerDialog { get; set; }
        public Dialog(string shortDialog, string answerDialog)
        { 
            ShortDialog = shortDialog;
            AnswerDialog = answerDialog;
        }
    }
}
