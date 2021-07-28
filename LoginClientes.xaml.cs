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
using System.Windows.Shapes;

namespace GestaoEconomato
{
    /// <summary>
    /// Interaction logic for LoginClientes.xaml
    /// </summary>
    public partial class LoginClientes : Window
    {
        Gestão_EconomatoEntities context = new Gestão_EconomatoEntities();
        public LoginClientes()
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

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            int a = 0;
            int password = Convert.ToInt32(passwordTextBox.Text);
            foreach(var log in context.Utilizadores)
            {
                if(log.Email == emailTextBox.Text && log.Password == password)
                {
                    a = 1;
                }
                
            }
            if (a == 1)
            {
                MessageBox.Show("Login Com Sucesso!!");
                this.Hide();
                CProdutos produtos = new CProdutos();
                produtos.Show();
            }
            else
            {
                MessageBox.Show("Verifique os dados");
                emailTextBox.Text = "";
                passwordTextBox.Text = "";
            }
        }
    }
}
