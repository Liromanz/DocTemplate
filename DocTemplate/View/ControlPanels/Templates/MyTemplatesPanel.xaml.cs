using System.Windows.Controls;
using DocTemplate.Helpers;
using DocTemplate.ViewModel.ControlPanels.Templates;

namespace DocTemplate.View.ControlPanels.Templates
{
    /// <summary>
    /// Логика взаимодействия для MyTemplatesPanel.xaml
    /// </summary>6
    public partial class MyTemplatesPanel : UserControl
    {
        public MyTemplatesVm ViewModel => DataContext as MyTemplatesVm;

        public MyTemplatesPanel()
        {
            Unloaded += (sender, args) => DataContainers.UserGroupsModel = ViewModel.CreateModelFromCards(ViewModel.Cards);
            InitializeComponent(); 
        }

    }
}
