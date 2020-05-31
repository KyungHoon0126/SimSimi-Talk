using RestSharp;
using System;
using System.Collections.Generic;
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

namespace Simsimi_Talk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        delegate void ScrollToLastItemDelegate();

        ListView lvUser;
        ListView lvSimSimi;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = App.simsimiViewModel;
            lvUser = lvUserMessageList;
            lvSimSimi = lvSimSimiMessageList;
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
            var ch = this.GetChildren(this.lvUserMessageList);
        }

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

            if (listView.Items.Count > 0 && listView.Name == "lvUserMessageList")
            {
                ScrollToLastItem_User();
                // listView.Dispatcher.BeginInvoke(DispatcherPriority.Background, new ScrollToLastItemDelegate(ScrollToLastItem));
            }
            else if (listView.Items.Count > 0 && listView.Name == "lvSimSimiMessageList")
            {
                ScrollToLastItem_SimSimi();
                // listView.Dispatcher.BeginInvoke(DispatcherPriority.Background, new ScrollToLastItemDelegate(ScrollToLastItem));
            }
        }

        // Find ScrollToLastItem User
        public void ScrollToLastItem_User()
        {
            lvUserMessageList.SelectedItem = lvUserMessageList.Items.GetItemAt(App.simsimiViewModel.UserMsgItems.Count - 1);
            lvUserMessageList.ScrollIntoView(lvUserMessageList.SelectedItem);
            
            ListViewItem item = lvUserMessageList.ItemContainerGenerator.ContainerFromItem(lvUserMessageList.SelectedItem) as ListViewItem;
            
            if(item != null)
            {
                item.Focus();
            }
        }

        // Find ScrollToLastItem SimSimi
        public void ScrollToLastItem_SimSimi()
        {
            lvSimSimiMessageList.SelectedItem = lvSimSimiMessageList.Items.GetItemAt(App.simsimiViewModel.SimSimiMsgItems.Count - 1);
            lvSimSimiMessageList.ScrollIntoView(lvSimSimiMessageList.SelectedItem);
            
            ListViewItem item = lvUserMessageList.ItemContainerGenerator.ContainerFromItem(lvSimSimiMessageList.SelectedItem) as ListViewItem;

            if (item != null)
            {
                item.Focus();
            }
        }
    }
}
