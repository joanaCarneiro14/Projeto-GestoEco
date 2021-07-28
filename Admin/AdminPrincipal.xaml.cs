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
    /// Interaction logic for AdminPrincipal.xaml
    /// </summary>
    public partial class AdminPrincipal : Window
    {
        Gestão_EconomatoEntities context = new Gestão_EconomatoEntities();
        //CollectionViewSource utilizadoresViewSource;
        public AdminPrincipal() 
        {
            InitializeComponent();
            //utilizadoresViewSource = ((CollectionViewSource)FindResource("utilizadoresViewSource"));
            mostrarfuncionario();
        }

        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void GridBarraTitulo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            GestaoEconomato.Gestão_EconomatoDataSet gestão_EconomatoDataSet = ((GestaoEconomato.Gestão_EconomatoDataSet)(this.FindResource("gestão_EconomatoDataSet")));
            // Load data into the table Cliente. You can modify this code as needed.
            // Load data into the table Utilizadores. You can modify this code as needed.
            //GestaoEconomato.Gestão_EconomatoDataSetTableAdapters.UtilizadoresTableAdapter gestão_EconomatoDataSetUtilizadoresTableAdapter = new GestaoEconomato.Gestão_EconomatoDataSetTableAdapters.UtilizadoresTableAdapter();
            //gestão_EconomatoDataSetUtilizadoresTableAdapter.Fill(gestão_EconomatoDataSet.Utilizadores);
            //System.Windows.Data.CollectionViewSource utilizadoresViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("utilizadoresViewSource")));
            //utilizadoresViewSource.View.MoveCurrentToFirst();
        }

        private void Button_Adicionar_Click(object sender, RoutedEventArgs e)
        {
            Utilizadore utilizador = new Utilizadore();
            Funcionario funcionario = new Funcionario();
            int password = Convert.ToInt32(passwordTextBox.Text);
            int telefone = Convert.ToInt32(telefoneTextBox.Text);
            

            utilizador.Email = emailTextBox.Text;
            funcionario.Id_Utilizador = emailTextBox.Text;
            utilizador.Password = password;
            utilizador.Nome = nomeTextBox.Text;
            utilizador.Morada = moradaTextBox.Text;
            utilizador.Telefone = telefone;
            utilizador.Tipo = "Funcionário";
            funcionario.Cargo = cargoTextBox.Text;
            context.Funcionarios.Add(funcionario);
            context.Utilizadores.Add(utilizador);
            context.SaveChanges();
            mostrarfuncionario();

        }

        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            string email_utilizador = emailTextBox.Text;
            int password = Convert.ToInt32(passwordTextBox.Text);
            int telefone = Convert.ToInt32(telefoneTextBox.Text);

            Utilizadore existente = context.Utilizadores.Find(email_utilizador);
            Funcionario existentefuncionario = context.Funcionarios.Find(email_utilizador);


            existente.Password = password;
            existente.Nome = nomeTextBox.Text;
            existente.Morada = moradaTextBox.Text;
            existente.Telefone = telefone;
            existentefuncionario.Cargo = cargoTextBox.Text;
            context.SaveChanges();
            mostrarfuncionario();
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            string email_utilizador = emailTextBox.Text;
            Utilizadore existente = context.Utilizadores.Find(email_utilizador);
            Funcionario existentefuncionario = context.Funcionarios.Find(email_utilizador);
            context.Funcionarios.Remove(existentefuncionario);
            context.Utilizadores.Remove(existente);
            context.SaveChanges();
            mostrarfuncionario();
            emailTextBox.Text = "";
            passwordTextBox.Text = "";
            nomeTextBox.Text = "";
            moradaTextBox.Text = "";
            telefoneTextBox.Text = "";
            cargoTextBox.Text = "";
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow login = new MainWindow();
            login.Show();
        }

        struct MostrarFuncionarios
        {
            public string Email { get; set; }
            public int Password { get; set; }
            public string Nome { get; set; }
            public string Morada { get; set; }
            public int Telefone { get; set; }
            public string  Cargo { get; set; }
        }

        public void mostrarfuncionario()
        {
            string connectionString = new Gestão_EconomatoEntities().Database.Connection.ConnectionString;

            List<MostrarFuncionarios> listarfuncionarios = new List<MostrarFuncionarios>();

            using (SqlConnection connectionBD = new SqlConnection(connectionString))
            {
                string query = "select Utilizadores.Email, Utilizadores.Password, Utilizadores.Nome, Utilizadores.Morada, Utilizadores.Telefone, Funcionario.Cargo from Utilizadores inner join Funcionario on Funcionario.Id_Utilizador = Utilizadores.Email where Utilizadores.Tipo = 'Funcionário'";
                SqlCommand sqlCommand = new SqlCommand(query, connectionBD);

                connectionBD.Open();

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listarfuncionarios.Add(new MostrarFuncionarios()
                        {
                            Email= reader.GetString(0),
                            Password = reader.GetInt32(1),
                            Nome = reader.GetString(2),
                            Morada = reader.GetString(3),
                            Telefone = reader.GetInt32(4),
                            Cargo = reader.GetString(5)
                        });
                    }

                }
                connectionBD.Close();
            }
            FuncionariosDataGrid.ItemsSource = listarfuncionarios;
        }

        private void Cliente_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Clientes clientes = new Clientes();
            clientes.Show();
        }

        private void Funcionario_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AdminPrincipal admin = new AdminPrincipal();
            admin.Show();
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
                PedidosPlafond pedidosPlafond = new PedidosPlafond();
                pedidosPlafond.Show();
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
