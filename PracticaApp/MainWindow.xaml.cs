using System;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;
using PracticaWpfApp.Helpers;
using PracticaWpfApp.Models;

namespace PracticaWpfApp
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Topic> topics;

        public MainWindow()
        {
            InitializeComponent();
            topics = new ObservableCollection<Topic>();
            dgTopics.ItemsSource = topics;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            topics.Clear();
            var list = DatabaseHelper.GetTopics();
            foreach (var topic in list)
                topics.Add(topic);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new EditWindow();
            if (editWindow.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgTopics.SelectedItem is Topic selectedTopic)
            {
                var editWindow = new EditWindow(selectedTopic);
                if (editWindow.ShowDialog() == true)
                {
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Выберите тему для редактирования.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgTopics.SelectedItem is Topic selectedTopic)
            {
                var result = MessageBox.Show($"Удалить тему \"{selectedTopic.Title}\"?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    DatabaseHelper.DeleteTopic(selectedTopic.Id);
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Выберите тему для удаления.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
