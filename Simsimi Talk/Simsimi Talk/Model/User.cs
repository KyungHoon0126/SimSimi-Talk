using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simsimi_Talk.Model
{
    public class User : BindableBase
    {
        private string _userMessage;
        public string UserMessage
        {
            get => _userMessage;
            set
            {
                SetProperty(ref _userMessage, value);
            }
        }
    }
}
