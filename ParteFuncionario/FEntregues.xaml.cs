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
    /// Interaction logic for FEntregues.xaml
    /// </summary>
    public partial class FEntregues : Window
    {
        Gestão_EconomatoEntities context = new Gestão_EconomatoEntities();
        public FEntregues()
        {
            InitializeComponent();
            mostrarclientes();
            mostrarfornecedores();
        }
        struct clientes
        {
            public int Id { get; set; }
            public DateTime Data_Pedido { get; set; }
            public string Cliente { get; set; }
            public string Funcionario { get; set; }
        }

        public void mostrarclientes()
        {
            string connectionString = new Gestão_EconomatoEntities().Database.Connection.ConnectionString;

            List<clientes> listarclientes = new List<clientes>();

            using (SqlConnection connectionBD = new SqlConnection(connectionString))
            {
                string query = "select EncomendasCliente.id, EncomendasCliente.data_pedido, EncomendasCliente.Id_Cliente, EncomendasCliente.Id_Funcionario from EncomendasCliente where data_entregue is null and estado = 'Aceite'";
                SqlCommand sqlCommand = new SqlCommand(query, connectionBD);

                connectionBD.Open();

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listarclientes.Add(new clientes()
                        {
                            Id = reader.GetInt32(0),
                            Data_Pedido = reader.GetDateTime(1),
                            Cliente = reader.GetString(2),
                            Funcionario = reader.GetString(3)
                        });
                    }
                }
                connectionBD.Close();
            }
            clienteDataGrid.ItemsSource = listarclientes;
        }

        struct fornecedores
        {
            public int Encomenda { get; set; }
            public int Pedido { get; set; }
            public DateTime Data_Pedido { get; set; }
            public string Funcionario { get; set; }
            public int Fornecedor { get; set; }
        }
        public void mostrarfornecedores()
        {
            string connectionString = new Gestão_EconomatoEntities().Database.Connection.ConnectionString;

            List<fornecedores> listarfornecedores = new List<fornecedores>();

            using (SqlConnection connectionBD = new SqlConnection(connectionString))
            {
                string query = "select ProdutoEncomendarFornecedor.id, EncomendasFornecedor.id, EncomendasFornecedor.data_pedido, EncomendasFornecedor.Id_Fornecedor, EncomendasFornecedor.Id_Funcionario from ProdutoEncomendarFornecedor inner join EncomendasFornecedor on ProdutoEncomendarFornecedor.Id_EncomendasFornecedor = EncomendasFornecedor.id where data_recebido = '1900-01-01' or data_recebido is Null";
                SqlCommand sqlCommand = new SqlCommand(query, connectionBD);

                connectionBD.Open();

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listarfornecedores.Add(new fornecedores()
                        {
                            Encomenda = reader.GetInt32(0),
                            Pedido = reader.GetInt32(1),
                            Data_Pedido = reader.GetDateTime(2),
                            Fornecedor = reader.GetInt32(3),
                            Funcionario = reader.GetString(4),
                        });
                    }
                }
                connectionBD.Close();
            }
            fornecedorDataGrid.ItemsSource = listarfornecedores;
        }
        private void Button_Entregue_Click(object sender, RoutedEventArgs e)
        {
            int id_encomenda = Convert.ToInt32(id_encomendas_cliente_TextBox.Text);
            EncomendasCliente encomendasCliente = context.EncomendasClientes.Find(id_encomenda);
            encomendasCliente.data_entregue = (DateTime)datepicker.SelectedDate;
            context.SaveChanges();
            mostrarclientes();
        }

        private void Button_Recebido_Click(object sender, RoutedEventArgs e)
        {
            int id_pedido = Convert.ToInt32(id_pedido_fornecedores_TextBox.Text);
            int id_encomenda = Convert.ToInt32(id_encomenda_fornecedores_TextBox.Text);
            EncomendasFornecedor encomendasFornecedor = context.EncomendasFornecedors.Find(id_pedido);
            encomendasFornecedor.data_recebido = (DateTime)datepickerFornecedores.SelectedDate;
            ProdutoEncomendarFornecedor produtoEncomendarFornecedor = context.ProdutoEncomendarFornecedors.Find(id_encomenda);
            int produto = produtoEncomendarFornecedor.Id_Produto;
            Produto produto1 = context.Produtoes.Find(produto);
            produto1.Stock = produto1.Stock + produtoEncomendarFornecedor.unidades;
            context.SaveChanges();
            mostrarfornecedores();
        }
        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void GridBarraTitulo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void entregas_recebidas_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Entregues entregues = new Entregues();
            entregues.Show();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow login = new MainWindow();
            login.Show();
        }
        private void Produtos_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            FProdutos produtos = new FProdutos();
            produtos.Show();
        }

    }
}
