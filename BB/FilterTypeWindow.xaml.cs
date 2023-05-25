using System.Windows;
using System.Windows.Controls;
//Подключаем библиотеки

namespace DailyPlanner
{
    public partial class FilterTypeWindow : Window //Наследуется от класса Window и предназначен для выбора типа фильтрации событий
    {
        public string SelectedType { get; private set; } //Свойство, которое представляет выбранный тип фильтрации (только для чтения)

        public FilterTypeWindow() //Инициализирует компоненты окна
        {
            InitializeComponent();
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e) //Вызывается при нажатии кнопки "Применить". Получает выбранный элемент из комбо-бокса (typeComboBox) и преобразует его в строку, сохраняя в свойство SelectedType. Затем устанавливается значение DialogResult в true, указывая, что окно было закрыто с применением фильтра
        {
            SelectedType = ((ComboBoxItem)typeComboBox.SelectedItem).Content.ToString();
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) //Вызывается при нажатии кнопки "Отмена". Устанавливает значение DialogResult в false, указывая, что окно было закрыто без применения фильтра
        {
            DialogResult = false;
        }
    }
}
