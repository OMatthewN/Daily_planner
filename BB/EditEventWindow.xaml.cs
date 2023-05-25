using System.Windows;
using System.Windows.Controls;

namespace DailyPlanner
{
    public partial class EditEventWindow : Window //Наследуется от класса Window и предназначен для редактирования существующего события.
    {
        private Event editedEvent; //Приватное поле, которое представляет редактируемое событие

        public EditEventWindow(Event selectedEvent) //Инициализирует компоненты окна и принимает в качестве параметра выбранное событие (selectedEvent).
        {
            InitializeComponent();
            editedEvent = selectedEvent;

            //Создает копию события для редактирования
            DataContext = new Event(selectedEvent.Date, selectedEvent.Time, selectedEvent.Description, selectedEvent.Type);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e) //Вызывается при нажатии кнопки "Сохранить". Обновляет значения полей времени, описания и типа редактируемого события (editedEvent) на основе значений, введенных в окне. Затем устанавливается значение DialogResult в true, указывая, что окно было закрыто с сохранением изменений
        {
            editedEvent.Time = ((Event)DataContext).Time;
            editedEvent.Description = ((Event)DataContext).Description;
            editedEvent.Type = ((Event)DataContext).Type;

            DialogResult = true;

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) //Вызывается при нажатии кнопки "Отмена". Он устанавливает значение DialogResult в false, указывая, что окно было закрыто без сохранения изменений.
        {
            DialogResult = false;
        }
    }
}
