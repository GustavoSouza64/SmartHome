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
    public partial class Funcionalidades : Window
    {
        FuncionalidadesViewModel viewModel;
        public Funcionalidades()
        {

            viewModel = new FuncionalidadesViewModel(this.Resources);
            InitializeComponent();
            this.DataContext = viewModel;
            Loaded += delegate { viewModel.LoadData(); };
        }

        private void Sair_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Execute_click(object sender, RoutedEventArgs e)
        {
            viewModel.ExecuteArduino = !viewModel.ExecuteArduino;
        }

        private void BotaoDashboard(object sender, RoutedEventArgs e)
        {
            Dashboard dash = new Dashboard();
            this.Close();
            dash.ShowDialog();
            
        }
        private void BotaoRelatorio(object sender, RoutedEventArgs e)
        {
            Relatorios rel = new Relatorios();
            this.Close();
            rel.ShowDialog();
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

