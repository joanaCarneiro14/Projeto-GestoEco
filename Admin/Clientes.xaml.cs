using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for Clientes.xaml
    /// </summary>
    public partial class Clientes : Window
    {
        Gestão_EconomatoEntities context = new Gestão_EconomatoEntities();
        public Clientes()
        {
            InitializeComponent();
            mostrarclientes();
        }
        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void GridBarraTitulo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow login = new MainWindow();
            login.Show();
        }

        struct MostrarCliente
        {
            public string Email { get; set; }
            public int Password { get; set; } 
            public string Nome { get; set; } 
            public string Morada { get; set; }
            public int Telefone { get; set; }
            public int Saldo { get; set; } 
        }

        public void mostrarclientes()
        {
            string connectionString = new Gestão_EconomatoEntities().Database.Connection.ConnectionString;

            List<MostrarCliente> listarclientes = new List<MostrarCliente>();

            using (SqlConnection connectionBD = new SqlConnection(connectionString))
            {
                string query = "select Utilizadores.Email, Utilizadores.Password, Utilizadores.Nome, Utilizadores.Morada, Utilizadores.Telefone, Cliente.Saldo from Utilizadores inner join Cliente on Cliente.Id_Utilizador = Utilizadores.Email where Utilizadores.Tipo = 'Cliente'";
                SqlCommand sqlCommand = new SqlCommand(query, connectionBD);

                connectionBD.Open();

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listarclientes.Add(new MostrarCliente()
                        {
                            Email = reader.GetString(0),
                            Password = reader.GetInt32(1),
                            Nome = reader.GetString(2),
                            Morada = reader.GetString(3),
                            Telefone = reader.GetInt32(4),
                            Saldo = reader.GetInt32(5)
                        });
                    }

                }
                connectionBD.Close();
            }
            ClientesDataGrid.ItemsSource = listarclientes;
        }

        private void Button_Adicionar_Click(object sender, RoutedEventArgs e)
        {
            Utilizadore utilizador = new Utilizadore();
            Cliente cliente = new Cliente();
            int password = Convert.ToInt32(passwordTextBox.Text);
            int telefone = Convert.ToInt32(telefoneTextBox.Text);
            int saldo = Convert.ToInt32(saldoTextBox.Text);


            utilizador.Email = emailTextBox.Text;
            cliente.Id_Utilizador = emailTextBox.Text;
            utilizador.Password = password;
            utilizador.Nome = nomeTextBox.Text;
            utilizador.Morada = moradaTextBox.Text;
            utilizador.Telefone = telefone;
            utilizador.Tipo = "Cliente";
            cliente.Saldo = saldo;
            context.Clientes.Add(cliente);
            context.Utilizadores.Add(utilizador);
            context.SaveChanges();
            mostrarclientes();
        }

        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            string email_utilizador = emailTextBox.Text;
            int password = Convert.ToInt32(passwordTextBox.Text);
            int telefone = Convert.ToInt32(telefoneTextBox.Text);
            int saldo = Convert.ToInt32(saldoTextBox.Text);

            Utilizadore existente = context.Utilizadores.Find(email_utilizador);
            Cliente existentecliente = context.Clientes.Find(email_utilizador);


            existente.Password = password;
            existente.Nome = nomeTextBox.Text;
            existente.Morada = moradaTextBox.Text;
            existente.Telefone = telefone;
            existentecliente.Saldo = saldo;
            context.SaveChanges();
            mostrarclientes();
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            string email_utilizador = emailTextBox.Text;
            Utilizadore existente = context.Utilizadores.Find(email_utilizador);
            Cliente existentecliente = context.Clientes.Find(email_utilizador);
            context.Clientes.Remove(existentecliente);
            context.Utilizadores.Remove(existente);
            context.SaveChanges();
            mostrarclientes();
            emailTextBox.Text = "";
            passwordTextBox.Text = "";
            nomeTextBox.Text = "";
            moradaTextBox.Text = "";
            telefoneTextBox.Text = "";
            saldoTextBox.Text = "";
        }

        private void Funcionario_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AdminPrincipal admin = new AdminPrincipal();
            admin.Show();
        }

        private void Cliente_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Clientes clientes = new Clientes();
            clientes.Show();
        }

        private void Produtos_Button_Click(object sender, RoutedEventArgs e)
        {
                this.Hide();
                Produtos produtos = new Produtos();
                produtos.Show();
        }

        private void Fornecedores_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Fornecedores fornecedores = new Fornecedores();
            fornecedores.Show();
        }

        private void Pedidos_Plafond_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            PedidosPlafond pedidosplafond = new PedidosPlafond();
            pedidosplafond.Show();
        }
        private void Pedidos_Encomendas_Cliente_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Pedidos_Encomendas_Cliente pedidos_Encomendas_Cliente = new Pedidos_Encomendas_Cliente();
            pedidos_Encomendas_Cliente.Show();
        }

        private void entregas_recebidas_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Entregues entregues = new Entregues();
            entregues.Show();
        }

        private void encomendas_Fornecedor_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Pedidos_Encomendas_Fornecedores pedidos_Encomendas_Fornecedores = new Pedidos_Encomendas_Fornecedores();
            pedidos_Encomendas_Fornecedores.Show();
        }
    }
}
