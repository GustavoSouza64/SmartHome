using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SmartHome_WPF.ViewModel;
namespace SmartHome_WPF.View
{
    /// <summary>
    /// Lógica interna para Funcionalidades.xaml
    /// </summary>
    public partial class Relatorios : Window
    {
        RelatoriosViewModel viewModel;
        public Relatorios()
        {

            viewModel = new RelatoriosViewModel(this.Resources);
            InitializeComponent();
            this.DataContext = viewModel;
            Loaded += delegate { viewModel.LoadData(); };
        }

        private void Sair_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BotaoFuncionalidades(object sender, RoutedEventArgs e)
        {
            Funcionalidades func = new Funcionalidades();
            this.Close();
            func.ShowDialog();
        }

        private void BotaoDashboard(object sender, RoutedEventArgs e)
        {
            Dashboard dash = new Dashboard();
            this.Close();
            dash.ShowDialog();
        }

        private void BtnPesquisar(object sender, RoutedEventArgs e)
        {
            viewModel.Query(this.datePicker1.ToString(), this.datePicker2.ToString());        
        }

        private void Window_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

    }
}

