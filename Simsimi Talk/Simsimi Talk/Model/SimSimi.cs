using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simsimi_Talk.Model
{
    public class SimSimi : BindableBase
    {
        private string _simSimiMessage;
        public string SimSimiMessage
        {
            get => _simSimiMessage;
            set
            {
                SetProperty(ref _simSimiMessage, value);
            }
        }
    }
}
