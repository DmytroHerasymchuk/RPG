using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Models.Actions
{
    public interface IAction
    {
        event EventHandler<string> OnActionPerformed;
        void Execute(LivingEntity actor, LivingEntity target);
    }
}
