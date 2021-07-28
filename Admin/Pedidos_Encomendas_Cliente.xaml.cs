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
    /// Interaction logic for Pedidos_Encomendas_Cliente.xaml
    /// </summary>
    public partial class Pedidos_Encomendas_Cliente : Window
    {
        Gestão_EconomatoEntities context = new Gestão_EconomatoEntities();
        public Pedidos_Encomendas_Cliente()
        {
            InitializeComponent();
            mostrarpedidos();
            mostrarencomendas();
        }

        private void Button_Aceitar_Click(object sender, RoutedEventArgs e)
        {
            int id_pedido = Convert.ToInt32(idTextBox.Text);
            int id_encomenda = Convert.ToInt32(idencomendaTextBox.Text);
            EncomendasCliente encomendasCliente = context.EncomendasClientes.Find(id_pedido);
            encomendasCliente.estado = "Aceite";
            string cliente = encomendasCliente.Id_Cliente;
            Cliente clientesaldo = context.Clientes.Find(cliente);
            ProdutoEncomendarCliente encomenda = context.ProdutoEncomendarClientes.Find(id_encomenda);
            clientesaldo.Saldo = clientesaldo.Saldo - encomenda.preco;
            int id_produto = encomenda.Id_Produto;
            Produto produto = context.Produtoes.Find(id_produto);
            produto.Stock = produto.Stock - encomenda.unidades;
            // reduzir ao stock o numero de unidades do produto
            //compor a quem tira o saldo que nao esta a tirara ao cliente certo
            context.SaveChanges();
            mostrarpedidos();
            mostrarencomendas();
        }

        private void Button_Recusar_Click(object sender, RoutedEventArgs e)
        {
            int id_pedido = Convert.ToInt32(idTextBox.Text);
            EncomendasCliente encomendasCliente = context.EncomendasClientes.Find(id_pedido);
            encomendasCliente.estado = "Recusado";
            context.SaveChanges();
            mostrarpedidos();
            mostrarencomendas();
        }



        struct MostrarPedidos
        {
            public int Id { get; set; }
            public DateTime Data_Pedido { get; set; }
            public string Cliente { get; set; }
            public string Estado { get; set; }
            public string Funcionario { get; set; }
        }

        public void mostrarpedidos()
        {
            string connectionString = new Gestão_EconomatoEntities().Database.Connection.ConnectionString;

            List<MostrarPedidos> listarpedidos = new List<MostrarPedidos>();

            using (SqlConnection connectionBD = new SqlConnection(connectionString))
            {
                string query = "select EncomendasCliente.id, EncomendasCliente.data_pedido, EncomendasCliente.Id_Cliente, EncomendasCliente.estado, EncomendasCliente.Id_Funcionario from EncomendasCliente where estado = 'Pendente'";
                SqlCommand sqlCommand = new SqlCommand(query, connectionBD);

                connectionBD.Open();

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listarpedidos.Add(new MostrarPedidos()
                        {
                            Id = reader.GetInt32(0),
                            Data_Pedido = reader.GetDateTime(1),
                            Cliente = reader.GetString(2),
                            Estado = reader.GetString(3),
                            Funcionario = reader.GetString(4)
                        });
                    }

                }
                connectionBD.Close();
            }
            PedidosDataGrid.ItemsSource = listarpedidos;
        }

        struct MostrarEncomendas
        {
            public int Id { get; set; }
            public string Produto { get; set; }
            public int Unidades { get; set; }
            public int Preco { get; set; }
            public string Estado { get; set; }
            public int Id_Pedidos { get; set; }
        }

        public void mostrarencomendas()
        {
            string connectionString = new Gestão_EconomatoEntities().Database.Connection.ConnectionString;

            List<MostrarEncomendas> listarencomendas = new List<MostrarEncomendas>();

            using (SqlConnection connectionBD = new SqlConnection(connectionString))
            {
                string query = "select ProdutoEncomendarCliente.id, Produto.Nome, ProdutoEncomendarCliente.unidades, ProdutoEncomendarCliente.preco, EncomendasCliente.estado, ProdutoEncomendarCliente.Id_EncomendasCliente From ProdutoEncomendarCliente inner join EncomendasCliente on ProdutoEncomendarCliente.Id_EncomendasCliente= EncomendasCliente.id inner join Produto on ProdutoEncomendarCliente.Id_Produto= Produto.id where EncomendasCliente.estado = 'Pendente' order by Id_EncomendasCliente ASC";
                SqlCommand sqlCommand = new SqlCommand(query, connectionBD);

                connectionBD.Open();

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listarencomendas.Add(new MostrarEncomendas()
                        {
                            Id = reader.GetInt32(0),
                            Produto = reader.GetString(1),
                            Unidades = reader.GetInt32(2),
                            Preco = reader.GetInt32(3),
                            Estado = reader.GetString(4),
                            Id_Pedidos = reader.GetInt32(5)
                        });
                    }
                }
                connectionBD.Close();
            }
            encomendasDataGrid.ItemsSource = listarencomendas;
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

        private void Pedidos_Plafond_Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            PedidosPlafond pedidosPlafond = new PedidosPlafond();
            pedidosPlafond.Show();
        }

        private void encomendas_Fornecedor_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Pedidos_Encomendas_Fornecedores pedidos_Encomendas_Fornecedores = new Pedidos_Encomendas_Fornecedores();
            pedidos_Encomendas_Fornecedores.Show();
        }
    }
}
