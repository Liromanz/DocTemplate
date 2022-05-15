using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DocTemplate.View.ControlPanels.Settings
{
    /// <summary>
    /// Логика взаимодействия для HelpControlPanel.xaml
    /// </summary>
    public partial class HelpControlPanel : UserControl
    {
        /// <summary>
        /// Метод для инициализации панели
        /// </summary>
        public HelpControlPanel()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Выставление всех панелей на скрытое свойство
        /// </summary>
        private void SetAllToCollapsed()
        {
            MeetingPanel.Visibility = Visibility.Collapsed;
            PublicPanel.Visibility = Visibility.Collapsed;
            GroupPanel.Visibility = Visibility.Collapsed;
            DocumentEditPanel.Visibility = Visibility.Collapsed;
            TemplateCreationPanel.Visibility = Visibility.Collapsed;
            WithoutInternetPanel.Visibility = Visibility.Collapsed;
            SettingsPanel.Visibility = Visibility.Collapsed;
        }
        /// <summary>
        /// Отображение панели "Знакомство"
        /// </summary>
        /// <param name="sender">Кнопка, вызвавшая событие</param>
        /// <param name="e">Событие</param>
        private void GreetingPanelClick(object sender, MouseButtonEventArgs e)
        {
            SetAllToCollapsed();
            MeetingPanel.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Отображение панели "Публичные шаблоны"
        /// </summary>
        /// <param name="sender">Кнопка, вызвавшая событие</param>
        /// <param name="e">Событие</param>
        private void PublicPanelClick(object sender, MouseButtonEventArgs e)
        {
            SetAllToCollapsed();
            PublicPanel.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Отображение панели "Работа с группами"
        /// </summary>
        /// <param name="sender">Кнопка, вызвавшая событие</param>
        /// <param name="e">Событие</param>
        private void GroupPanelClick(object sender, MouseButtonEventArgs e)
        {
            SetAllToCollapsed();
            GroupPanel.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Отображение панели "Редактирование документа"
        /// </summary>
        /// <param name="sender">Кнопка, вызвавшая событие</param>
        /// <param name="e">Событие</param>
        private void DocumentPanelClick(object sender, MouseButtonEventArgs e)
        {
            SetAllToCollapsed();
            DocumentEditPanel.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Отображение панели "Создание шаблона"
        /// </summary>
        /// <param name="sender">Кнопка, вызвавшая событие</param>
        /// <param name="e">Событие</param>
        private void TemplatePanelClick(object sender, MouseButtonEventArgs e)
        {
            SetAllToCollapsed();
            TemplateCreationPanel.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Отображение панели "Работа без интернета"
        /// </summary>
        /// <param name="sender">Кнопка, вызвавшая событие</param>
        /// <param name="e">Событие</param>
        private void InternetPanelClick(object sender, MouseButtonEventArgs e)
        {
            SetAllToCollapsed();
            WithoutInternetPanel.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Отображение панели "Параметры"
        /// </summary>
        /// <param name="sender">Кнопка, вызвавшая событие</param>
        /// <param name="e">Событие</param>
        private void SettingPanelClick(object sender, MouseButtonEventArgs e)
        {
            SetAllToCollapsed();
            SettingsPanel.Visibility = Visibility.Visible;
        }
    }
}
