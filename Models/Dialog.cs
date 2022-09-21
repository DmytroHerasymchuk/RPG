using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Dialog
    {
        public int IDShort { get; set; }
        public int IDAnswer { get; set; }
        public string ShortDialog { get; set; }
        public string AnswerDialog { get; set; }
        public Dialog(string shortDialog, string answerDialog, int iDShort)
        {
            ShortDialog = shortDialog;
            AnswerDialog = answerDialog;
            IDShort = iDShort;
            IDAnswer = iDShort;
        }
    }
}
