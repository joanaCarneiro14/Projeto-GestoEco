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
    /// Interaction logic for Pedidos_Encomendas_Fornecedores.xaml
    /// </summary>
    public partial class Pedidos_Encomendas_Fornecedores : Window
    {
        Gestão_EconomatoEntities context = new Gestão_EconomatoEntities();
        public Pedidos_Encomendas_Fornecedores()
        {
            InitializeComponent();
            mostrarpedidosencomenda();
            mostrarprodutos();
        }

        struct PedidosEncomenda
        {
            public int Id { get; set; }
            public DateTime Data_Pedido { get; set; }
            public string Funcionario { get; set; }
            public int Fornecedor { get; set; }
        }

        public void mostrarpedidosencomenda()
        {
            string connectionString = new Gestão_EconomatoEntities().Database.Connection.ConnectionString;

            List<PedidosEncomenda> listarpedidos = new List<PedidosEncomenda>();

            using (SqlConnection connectionBD = new SqlConnection(connectionString))
            {
                string query = "select id,data_pedido,Id_Funcionario,Id_Fornecedor from EncomendasFornecedor where data_recebido is NULL";
                SqlCommand sqlCommand = new SqlCommand(query, connectionBD);

                connectionBD.Open();

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listarpedidos.Add(new PedidosEncomenda()
                        {
                            Id = reader.GetInt32(0),
                            Data_Pedido = reader.GetDateTime(1),
                            Funcionario = reader.GetString(2),
                            Fornecedor = reader.GetInt32(3)
                        });
                    }

                }
                connectionBD.Close();
            }
            pedidosDataGrid.ItemsSource = listarpedidos;
        }

        struct Produtoss
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public int Preço { get; set; }
            public string Setor { get; set; }
            public int Stock { get; set; }
            public int Fornecedor { get; set; }
        }

        public void mostrarprodutos()
        {
            string connectionString = new Gestão_EconomatoEntities().Database.Connection.ConnectionString;

            List<Produtoss> listarprodutos = new List<Produtoss>();

            using (SqlConnection connectionBD = new SqlConnection(connectionString))
            {
                string query = "select Produto.id, Produto.Nome, Produto.Preco, Produto.Setor, Produto.Stock, Produto_Fornecedor.Id_Fornecedor from Produto inner join Produto_Fornecedor on Produto_Fornecedor.Id_Produto = Produto.id";
                SqlCommand sqlCommand = new SqlCommand(query, connectionBD);

                connectionBD.Open();

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listarprodutos.Add(new Produtoss()
                        {
                            Id = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Preço = reader.GetInt32(2),
                            Setor = reader.GetString(3),
                            Stock = reader.GetInt32(4),
                            Fornecedor = reader.GetInt32(5)
                        });
                    }

                }
                connectionBD.Close();
            }
            produtosDataGrid.ItemsSource = listarprodutos;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            // Load data into the table EncomendasFornecedor. You can modify this code as needed.
            GestaoEconomato.Gestão_EconomatoDataSet gestão_EconomatoDataSet = ((GestaoEconomato.Gestão_EconomatoDataSet)(this.FindResource("gestão_EconomatoDataSet")));
            GestaoEconomato.Gestão_EconomatoDataSetTableAdapters.EncomendasFornecedorTableAdapter gestão_EconomatoDataSetEncomendasFornecedorTableAdapter = new GestaoEconomato.Gestão_EconomatoDataSetTableAdapters.EncomendasFornecedorTableAdapter();
            gestão_EconomatoDataSetEncomendasFornecedorTableAdapter.Fill(gestão_EconomatoDataSet.EncomendasFornecedor);
            System.Windows.Data.CollectionViewSource encomendasFornecedorViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("encomendasFornecedorViewSource")));
            encomendasFornecedorViewSource.View.MoveCurrentToFirst();
            // Load data into the table ProdutoEncomendarFornecedor. You can modify this code as needed.
            GestaoEconomato.Gestão_EconomatoDataSetTableAdapters.ProdutoEncomendarFornecedorTableAdapter gestão_EconomatoDataSetProdutoEncomendarFornecedorTableAdapter = new GestaoEconomato.Gestão_EconomatoDataSetTableAdapters.ProdutoEncomendarFornecedorTableAdapter();
            gestão_EconomatoDataSetProdutoEncomendarFornecedorTableAdapter.Fill(gestão_EconomatoDataSet.ProdutoEncomendarFornecedor);
            System.Windows.Data.CollectionViewSource produtoEncomendarFornecedorViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("produtoEncomendarFornecedorViewSource")));
            produtoEncomendarFornecedorViewSource.View.MoveCurrentToFirst();
        }

        private void encomendas_Fornecedor_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Pedidos_Encomendas_Fornecedores pedidos_Encomendas_Fornecedores = new Pedidos_Encomendas_Fornecedores();
            pedidos_Encomendas_Fornecedores.Show();
        }

        private void Criar_Pedido_Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime data = Convert.ToDateTime(data_pedidoDatePicker.SelectedDate);
            int fornecedor = Convert.ToInt32(id_FornecedorTextBox.Text);
            EncomendasFornecedor encomendasFornecedor = new EncomendasFornecedor();
            encomendasFornecedor.data_pedido = data;
            encomendasFornecedor.Id_Fornecedor = fornecedor;
            encomendasFornecedor.Id_Funcionario = id_FuncionarioTextBox.Text;
            context.EncomendasFornecedors.Add(encomendasFornecedor);
            context.SaveChanges();
            mostrarpedidosencomenda();
        }

        private void Criar_Encomenda_Button_Click(object sender, RoutedEventArgs e)
        {
            int preco = Convert.ToInt32(precoTextBox.Text);
            int id_pedido = Convert.ToInt32(id_EncomendasFornecedorTextBox.Text);
            int unidades = Convert.ToInt32(unidadesTextBox.Text);
            int produto = Convert.ToInt32(id_ProdutoTextBox.Text);
            ProdutoEncomendarFornecedor produtoEncomendarFornecedor = new ProdutoEncomendarFornecedor();
            produtoEncomendarFornecedor.Id_Produto = produto;
            produtoEncomendarFornecedor.unidades = unidades;
            produtoEncomendarFornecedor.preco = preco;
            produtoEncomendarFornecedor.Id_EncomendasFornecedor = id_pedido;
            context.ProdutoEncomendarFornecedors.Add(produtoEncomendarFornecedor);
            context.SaveChanges();
            MessageBox.Show("Encomenda Feita com sucesso!!");

        }
    }
}
