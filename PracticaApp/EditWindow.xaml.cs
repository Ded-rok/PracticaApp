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
using System.Windows.Shapes;
using PracticaWpfApp.Helpers;
using PracticaWpfApp.Models;

namespace PracticaWpfApp
{
    public partial class EditWindow : Window
    {
        private Topic currentTopic;

        // Конструктор для добавления
        public EditWindow()
        {
            InitializeComponent();
            currentTopic = new Topic { CreatedAt = DateTime.Now };
            DataContext = currentTopic;
        }

        // Конструктор для редактирования
        public EditWindow(Topic topic)
        {
            InitializeComponent();
            currentTopic = new Topic
            {
                Id = topic.Id,
                Title = topic.Title,
                Description = topic.Description,
                Teacher = topic.Teacher,
                Student = topic.Student,
                CreatedAt = topic.CreatedAt
            };
            DataContext = currentTopic;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Введите название темы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtTeacher.Text))
            {
                MessageBox.Show("Введите преподавателя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Явно копируем данные из полей в объект
            currentTopic.Title = txtTitle.Text;
            currentTopic.Description = txtDescription.Text;
            currentTopic.Teacher = txtTeacher.Text;
            currentTopic.Student = txtStudent.Text;

            try
            {
                if (currentTopic.Id == 0)
                {
                    DatabaseHelper.AddTopic(currentTopic);
                }
                else
                {
                    DatabaseHelper.UpdateTopic(currentTopic);
                }
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
