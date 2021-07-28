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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GestaoEconomato
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void GridBarraTitulo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Cliente_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            LoginClientes loginClientes = new LoginClientes();
            loginClientes.Show();
        }

        private void Funcionario_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            LoginFuncionario loginFuncionario = new LoginFuncionario();
            loginFuncionario.Show();
        }

        private void Chefe_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            LoginChefe loginChefe = new LoginChefe();
            loginChefe.Show();
        }
    }
}
