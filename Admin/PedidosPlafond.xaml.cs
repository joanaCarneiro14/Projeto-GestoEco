using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
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
    /// Interaction logic for PedidosPlafond.xaml
    /// </summary>
    public partial class PedidosPlafond : Window
    {
        Gestão_EconomatoEntities context = new Gestão_EconomatoEntities();
        public PedidosPlafond()
        {
            InitializeComponent();
            mostrarpedidosplafond();
        }

        struct MostrarPedidosPlafond
        {
            public int Id { get; set; }
            public DateTime Data { get; set; }
            public int  Valor { get; set; }
            public string Estado { get; set; }
            public string Cliente { get; set; }
        }

        public void mostrarpedidosplafond()
        {
            string connectionString = new Gestão_EconomatoEntities().Database.Connection.ConnectionString;

            List<MostrarPedidosPlafond> listarpedidosplafond = new List<MostrarPedidosPlafond>();

            using (SqlConnection connectionBD = new SqlConnection(connectionString))
            {
                string query = "select Plafond.id, Plafond.data, Plafond.quantidade_pedida, Plafond.estado, Plafond.Id_Cliente from Plafond where Plafond.estado = 'Pendente'";
                SqlCommand sqlCommand = new SqlCommand(query, connectionBD);

                connectionBD.Open();

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listarpedidosplafond.Add(new MostrarPedidosPlafond()
                        {
                            Id = reader.GetInt32(0),
                            Data = reader.GetDateTime(1),
                            Valor = reader.GetInt32(2),
                            Estado = reader.GetString(3),
                            Cliente = reader.GetString(4)
                        });
                    }

                }
                connectionBD.Close();
            }
            pedidosplafondDataGrid.ItemsSource = listarpedidosplafond;
        }
        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow login = new MainWindow();
            login.Show();
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
        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void GridBarraTitulo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void Pedidos_Plafond_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            PedidosPlafond pedidosPlafond = new PedidosPlafond();
            pedidosPlafond.Show();
        }

        private void Aceitar_Button_Click(object sender, RoutedEventArgs e)
        {
            int id_pedido = Convert.ToInt32(IdTextBox.Text);
            Plafond plafond = context.Plafonds.Find(id_pedido);
            plafond.estado = "Aceite";
            string Cliente = plafond.Id_Cliente;
            Cliente clientesaldo = context.Clientes.Find(Cliente);
            clientesaldo.Saldo = clientesaldo.Saldo + plafond.quantidade_pedida;
            context.SaveChanges();
            mostrarpedidosplafond();
        }

        private void Recusar_Button_Click(object sender, RoutedEventArgs e)
        {
            int id_pedido = Convert.ToInt32(IdTextBox.Text);
            Plafond plafond = context.Plafonds.Find(id_pedido);
            plafond.estado = "Recusado";
            context.SaveChanges();
            mostrarpedidosplafond();
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
