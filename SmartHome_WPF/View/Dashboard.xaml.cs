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
    /// Lógica interna para Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        DashViewModel viewModel;
        public Dashboard()
        {
            viewModel = new DashViewModel(this.Resources);
            InitializeComponent();
            this.DataContext = viewModel;
            Loaded += delegate { viewModel.LoadData(); };
            PreencheDash();
        }

        public void PreencheDash()
        {

            List<string> valores = viewModel.HistoricoConsumo();

        chartHistoricoConsumo.DataContext = new KeyValuePair<String, double>[]{
            new KeyValuePair<String, double>("06/2020", Convert.ToDouble(valores[0])),
            new KeyValuePair<String, double>("07/2020", Convert.ToDouble(valores[1])),
            new KeyValuePair<String, double>("08/2020", Convert.ToDouble(valores[2])),
            new KeyValuePair<String, double>("09/2020", Convert.ToDouble(valores[3])),
            new KeyValuePair<String, double>("10/2020", Convert.ToDouble(valores[4])),
            new KeyValuePair<String, double>("11/2020", Convert.ToDouble(valores[5]))};

            List<string> potencia = viewModel.HistoricoPotencia();

        chartHistoricoConsumo2.DataContext = new KeyValuePair<String, double>[]{
            new KeyValuePair<String, double>("06/2020", Convert.ToDouble(potencia[0])),
            new KeyValuePair<String, double>("07/2020", Convert.ToDouble(potencia[1])),
            new KeyValuePair<String, double>("08/2020", Convert.ToDouble(potencia[2])),
            new KeyValuePair<String, double>("09/2020", Convert.ToDouble(potencia[3])),
            new KeyValuePair<String, double>("10/2020", Convert.ToDouble(potencia[4])),
            new KeyValuePair<String, double>("11/2020", Convert.ToDouble(potencia[5]))};

        }

        private void Sair_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BotaoRelatorio(object sender, RoutedEventArgs e)
        {
            Relatorios rel = new Relatorios();
            this.Close();
            rel.ShowDialog();
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
            dash.ShowDialog();
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

