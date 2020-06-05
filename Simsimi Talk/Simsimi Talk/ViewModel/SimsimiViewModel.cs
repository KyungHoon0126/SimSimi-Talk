using Newtonsoft.Json.Linq;
using Prism.Mvvm;
using SimSimi_Talk.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SimSimi_Talk.ViewModel
{
    public class SimsimiViewModel : BindableBase
    {
        private const string API_URL = "https://wsapi.simsimi.com/190410/talk";

        #region Properties
        private ObservableCollection<User> _userMsgItems = new ObservableCollection<User>();
        public ObservableCollection<User> UserMsgItems
        {
            get => _userMsgItems;
            set
            {
                SetProperty(ref _userMsgItems, value);
            }
        }

        private ObservableCollection<SimSimi> _simSimiMsgItems = new ObservableCollection<SimSimi>();
        public ObservableCollection<SimSimi> SimSimiMsgItems
        {
            get => _simSimiMsgItems;
            set
            {
                SetProperty(ref _simSimiMsgItems, value);
            }
        }

        private double _tbMsgHeight;
        public double TbMsgHeight
        {
            get => _tbMsgHeight;
            set
            {
                SetProperty(ref _tbMsgHeight, value);
            }
        }
        #endregion

        // 기존 API CURL 요청 예시

        //curl -X POST https://wsapi.simsimi.com/190410/talk \
        //     -H "Content-Type: application/json" \
        //     -H "x-api-key: PASTE_YOUR_PROJECT_KEY_HERE" \
        //     -d '{
        //            "utext": "안녕", 
        //            "lang": "ko" 
        //     }' 

        // Request
        public async void GetSimsimiMessage(string userMsg)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), API_URL))
                {
                    request.Headers.TryAddWithoutValidation
                    (
                        "x-api-key", "kUTVvDbFzyG1VfGbSD.t.eWaQzFX~QujrTyz-f-i"
                    );

                    request.Content = new StringContent("{\n \"utext\": \"" + userMsg + "\", \n\"lang\": \"ko\" \n}");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    string res = await response.Content.ReadAsStringAsync();

                    JToken jToken = JToken.Parse(res);
                    
                    string sMsg = jToken["atext"].ToString();
                    if(sMsg.Equals(userMsg))
                    {
                        sMsg.Replace(userMsg, "");
                    }

                    User user = new User();
                    SimSimi simSimi = new SimSimi();

                    user.UserMessage = userMsg;
                    simSimi.SimSimiMessage = sMsg;

                    // Property Value Add
                    UserMsgItems.Add(user);
                    SimSimiMsgItems.Add(simSimi);
                }
            }
        }
    }
}
