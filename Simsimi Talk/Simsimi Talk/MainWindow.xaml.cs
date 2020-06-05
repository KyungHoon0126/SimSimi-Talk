using RestSharp;
using SimSimi_Talk.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SimSimi_Talk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = App.simsimiViewModel;
        }

        private void btnSendMsg_Click(object sender, RoutedEventArgs e)
        {
            App.simsimiViewModel.GetSimsimiMessage(tbUserMsg.Text);

            // [ 사용 X ] -> ScrollToBottom() 으로 대체
            //ScrollToLast(lvUserMessageList);
            //ScrollToLast(lvSimSimiMessageList);

            // Message 창 ScrollViewer 자동 맞춤
            ScrollToBottom(lvUserMessageList);
            ScrollToBottom(lvSimSimiMessageList);

            tbUserMsg.Text = string.Empty;
        }

        // Enter Key Event
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return)
            {
                App.simsimiViewModel.GetSimsimiMessage(tbUserMsg.Text);

                // [ 사용 X ] -> ScrollToBottom() 으로 대체
                //ScrollToLast(lvUserMessageList);
                ScrollToLast(lvSimSimiMessageList);

                // Message 창 ScrollViewer 자동 맞춤
                ScrollToBottom(lvUserMessageList);
                ScrollToBottom(lvSimSimiMessageList);

                tbUserMsg.Text = string.Empty;
            }
        }

        public void ScrollToBottom(ListView listView)
        { 
            if (VisualTreeHelper.GetChildrenCount(listView) > 0)
            {
                Border border = (Border)VisualTreeHelper.GetChild(listView, 0);
                ScrollViewer scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
                scrollViewer.ScrollToBottom();
            }
        }

        #region Find Scroll To LastItem 
        public void ScrollToLast(ListView listView)
        {
            Debug.WriteLine(listView.Name);

            if (listView.Items.Count > 0)
            {
                if(listView.Name == "lvUserMessageList")
                {
                    ScrollToLastItem<Model.User>(listView, App.simsimiViewModel.UserMsgItems);
                }
                else if(listView.Name == "lvSimSimiMessageList")
                {
                    ScrollToLastItem<Model.SimSimi>(listView, App.simsimiViewModel.SimSimiMsgItems);
                }
            }
        }

        public void ScrollToLastItem<T>(ListView listView, ObservableCollection<T> collection) where T : class
        {
            listView.SelectedItem = listView.Items.GetItemAt(collection.Count - 1);
            listView.ScrollIntoView(listView.SelectedItem);

            // Total Count
            var simSimiMsgCnt = App.simsimiViewModel.SimSimiMsgItems.Count;

            // Message List의 마지막 index에서 끝까지, 즉 마지막 메시지일 때
            for (int i = simSimiMsgCnt - 1; i < App.simsimiViewModel.SimSimiMsgItems.Count; i++)
            {
                if(i == 0)
                {
                    App.simsimiViewModel.TbMsgHeight = Convert.ToDouble(App.simsimiViewModel.SimSimiMsgItems[i].SimSimiMessage.Length);
                    Debug.WriteLine(App.simsimiViewModel.TbMsgHeight);
                }
                if (i == simSimiMsgCnt)
                {
                    App.simsimiViewModel.TbMsgHeight = Convert.ToDouble(App.simsimiViewModel.SimSimiMsgItems[i].SimSimiMessage.Length);
                    Debug.WriteLine(App.simsimiViewModel.TbMsgHeight);
                }
            }
            

            // 마지막 메시지의 Height를 구함.
            // App.simsimiViewModel.TbMsgHeight = (listView.SelectedItem as ListView).Height;

            // ListViewItem item = listView.ItemContainerGenerator.ContainerFromItem(listView.SelectedItem) as ListViewItem;

            //if(item != null)
            //{
            //    item.Focus();
            //}
        }
        #endregion
    }
}
