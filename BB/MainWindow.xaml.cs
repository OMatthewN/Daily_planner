using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
//Подключаем библиотеки

namespace DailyPlanner
{
    public partial class MainWindow : Window, INotifyPropertyChanged //Класс главное окно приложения и наследуется Window. Реализует интерфейс INotifyPropertyChanged, который обеспечивает возможность уведомления об изменении свойств объекта.
    {
        private List<Event> events; //Приватное поле типа List<Event>, которое представляет список событий
        private string dataFilePath = "Task.json"; //Приватное поле, содержащее путь к файлу данных
        private List<Event> filteredEvents; //Приватное поле типа List<Event>, которое представляет отфильтрованный список событий.
        private string filterType; //Приватное поле, содержащее тип фильтрации событий

        public List<Event> Events //Предоставляет доступ к отфильтрованным событиям и вызывает событие PropertyChanged при его изменении
        {
            get { return filteredEvents; }
            set
            {
                filteredEvents = value;
                OnPropertyChanged("Events");
            }
        }

        public string FilterType
        {
            get { return filterType; }
            set
            {
                filterType = value;
                FilterEvents();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged; //Событие, которое будет вызываться при изменении свойств объекта

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindow() //Инициализируются компоненты окна, устанавливается контекст данных, загружаются данные из файла
        {
            InitializeComponent();
            DataContext = this;
            LoadData();
        }

        private void LoadData() //Отвечает за загрузку данных из файла JSON. Считывает содержимое файла, десериализует его в список объектов типа Event, а затем фильтрует события
        {
            try
            {
                string jsonData = File.ReadAllText(dataFilePath);
                events = JsonSerializer.Deserialize<List<Event>>(jsonData);
                FilterEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при чтении файла данных: " + ex.Message);
                events = new List<Event>();
            }
        }

        private void SaveData() //Сохраняет данные в файл JSON. Он сериализует список событий в JSON-строку и записывает ее в файл
        {
            try
            {
                string jsonData = JsonSerializer.Serialize(events);
                File.WriteAllText(dataFilePath, jsonData);
                FilterEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении данных: " + ex.Message);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e) //Вызывается при нажатии кнопки "Добавить". Получает значения полей ввода (дата, время, описание, тип), создает новый объект Event, добавляет его в список событий, сохраняет данные и очищает поля ввода
        {
            DateTime date = datePicker.SelectedDate ?? DateTime.Today;
            string time = timeTextBox.Text;
            string description = descriptionTextBox.Text;
            string type = ((ComboBoxItem)typeComboBox.SelectedItem).Content.ToString();

            Event newEvent = new Event(date, time, description, type);
            events.Add(newEvent);
            SaveData();

            descriptionTextBox.Text = string.Empty;
            timeTextBox.Text = string.Empty;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e) //Вызывается при нажатии кнопки "Редактировать" для конкретного события. Открывает окно редактирования события (EditEventWindow) и сохраняет данные после закрытия окна
        {
            Event selectedEvent = (Event)((Button)sender).DataContext;

            EditEventWindow editEventWindow = new EditEventWindow(selectedEvent);
            if (editEventWindow.ShowDialog() == true)
            {
                SaveData();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e) //Вызывается при нажатии кнопки "Удалить" для конкретного события. Удаляет выбранное событие из списка и сохраняет данные
        {
            Event selectedEvent = (Event)((Button)sender).DataContext;
            events.Remove(selectedEvent);
            SaveData();
        }

        private void FilterEvents() //Фильтрует список событий в соответствии с выбранным типом фильтра и значением поиска. Если фильтр и поиск не установлены, список событий остается неизменным
        {
            if (string.IsNullOrEmpty(filterType) && string.IsNullOrEmpty(searchTextBox.Text))
            {
                Events = events;
            }
            else
            {
                SearchEvents();
                if (!string.IsNullOrEmpty(filterType))
                {
                    Events = Events.Where(ev => ev.Type == filterType).ToList();
                }
            }
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e) //Вызывает окно фильтрации (FilterTypeWindow) и устанавливает выбранный тип фильтра
        {
            FilterTypeWindow filterTypeWindow = new FilterTypeWindow();
            if (filterTypeWindow.ShowDialog() == true)
            {
                FilterType = filterTypeWindow.SelectedType;
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e) //Вызывается при изменении свойства Events. Устанавливает источник данных для представления списка событий и вызывает метод SearchEvents
        {
            if (e.PropertyName == "Events")
            {
                eventListView.ItemsSource = Events;
                SearchEvents();
            }
        }

        private void TimeTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e) //Ограничивает ввод только цифр и символа ":" в поле ввода времени
        {
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c) && c != ':')
                {
                    e.Handled = true;
                    break;
                }
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e) //Вызывается при нажатии кнопки "Поиск". Он фильтрует список событий в соответствии с введенным текстом поиска и вызывает метод FilterEvents
        {
            string searchText = searchTextBox.Text.ToLower();
            Events = events.Where(ev => ev.Description.ToLower().Contains(searchText)).ToList();
            FilterEvents();
        }

        private void SearchEvents() //Используется для поиска событий
        {
            string searchText = searchTextBox.Text.ToLower();
            Events = events.Where(ev => ev.Description.ToLower().Contains(searchText)).ToList();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e) //Используется для сброса фильтра
        {
            searchTextBox.Text = string.Empty;
            FilterType = string.Empty;
            FilterEvents();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) //Вызывает метод RefreshData, который обновляет данные, повторно загружая их из файла
        {
            RefreshData();
        }

        private void RefreshData()
        {
            LoadData();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e) //Метод вызывается при загрузке главного окна и вызывает метод RefreshData для загрузки данных
        {
            RefreshData();
        }

        private void eventListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

    public class Event //Представляет объект события и содержит свойства для даты, времени, описания и типа события
    {
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public Event(DateTime date, string time, string description, string type)
        {
            Date = date.Date;
            Time = time;
            Description = description;
            Type = type;
        }
    }
}
