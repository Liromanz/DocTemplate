using DocTemplate.Helpers;
using DocTemplate.ViewModel.ControlPanels.Templates;
using System.Windows.Controls;

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
