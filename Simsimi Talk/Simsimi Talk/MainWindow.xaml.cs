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
        delegate void ScrollToLastItemDelegate();

        ListView lvUser;
        ListView lvSimSimi;

        ObservableCollection<Model.User> users;
        ObservableCollection<Model.SimSimi> simSimis;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = App.simsimiViewModel;

            // ListView
            lvUser = lvUserMessageList;
            lvSimSimi = lvSimSimiMessageList;

            // ObservableCollection
            users = App.simsimiViewModel.UserMsgItems;
            simSimis = App.simsimiViewModel.SimSimiMsgItems;
        }

        // SendButton Click Event
        private void btnSendMsg_Click(object sender, RoutedEventArgs e)
        {
            App.simsimiViewModel.GetSimsimiMessage(tbUserMsg.Text);

            ScrollToLast(lvUserMessageList);
            ScrollToLast(lvSimSimiMessageList);

            tbUserMsg.Text = string.Empty;
        }

        // Enter Key Event
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return)
            {
                App.simsimiViewModel.GetSimsimiMessage(tbUserMsg.Text);

                ScrollToLast(lvUserMessageList);
                ScrollToLast(lvSimSimiMessageList);

                tbUserMsg.Text = string.Empty;

                // App.simsimiViewModel.TbMsgHeight = 

                FindMyStuff();
            }
        }

        private void FindMyStuff()
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(lvUserMessageList); i++)
            {
                Visual visual = VisualTreeHelper.GetChild(lvUserMessageList, i) as Visual;
                Debug.WriteLine(visual);
            }

            // 찾는 Control 요소
            //var stuff = this.GetChildren(this.lvUserMessageList);

            // 요소 확인
            //foreach (var item in stuff)
            //{
            //    Debug.WriteLine(item);
            //}
        }

        // Control안의 Control 접근을 위한 시각적 트리 탐색
        private List<FrameworkElement> GetChildren(DependencyObject parent)
        {
            List<FrameworkElement> controls = new List<FrameworkElement>();

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); ++i)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is FrameworkElement)
                {
                    controls.Add(child as FrameworkElement);
                }
                controls.AddRange(this.GetChildren(child));
            }

            return controls;
        }

        // Scroll To Last Method
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

        // Find Scroll To LastItem 
        public void ScrollToLastItem<T>(ListView listView, ObservableCollection<T> collection) where T : class
        {
            listView.SelectedItem = listView.Items.GetItemAt(collection.Count - 1);
            listView.ScrollIntoView(listView.SelectedItem);
            
            ListViewItem item = listView.ItemContainerGenerator.ContainerFromItem(listView.SelectedItem) as ListViewItem;
            
            if(item != null)
            {
                item.Focus();
            }
        }
    }
}
