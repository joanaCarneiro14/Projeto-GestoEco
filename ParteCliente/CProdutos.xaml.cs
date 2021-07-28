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
    /// Interaction logic for CProdutos.xaml
    /// </summary>
    public partial class CProdutos : Window
    {
        Gestão_EconomatoEntities context = new Gestão_EconomatoEntities();
        public CProdutos()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            // Load data into the table Produto. You can modify this code as needed.
            GestaoEconomato.Gestão_EconomatoDataSet gestão_EconomatoDataSet = ((GestaoEconomato.Gestão_EconomatoDataSet)(this.FindResource("gestão_EconomatoDataSet")));
            GestaoEconomato.Gestão_EconomatoDataSetTableAdapters.ProdutoTableAdapter gestão_EconomatoDataSetProdutoTableAdapter = new GestaoEconomato.Gestão_EconomatoDataSetTableAdapters.ProdutoTableAdapter();
            gestão_EconomatoDataSetProdutoTableAdapter.Fill(gestão_EconomatoDataSet.Produto);
            System.Windows.Data.CollectionViewSource produtoViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("produtoViewSource")));
            produtoViewSource.View.MoveCurrentToFirst();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow login = new MainWindow();
            login.Show();
        }
        private void GridBarraTitulo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Pedidos_Encomendas_Cliente_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CEncomendas encomendas = new CEncomendas();
            encomendas.Show();
        }

        private void Pedidos_Plafond_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CPlafond plafond = new CPlafond();
            plafond.Show();
        }

        struct mostrarpedidos
        {
            public int Id { get; set; }
            public DateTime Data_Pedido { get; set; }
            public string Funcionario { get; set; }
        }

        public void MostrarPedidos()
        {
            SqlConnection conn = null;
            SqlDataReader reader = null;
            List<mostrarpedidos> listarplafond = new List<mostrarpedidos>();

            try
            {
                conn = new
                    SqlConnection("Server=(local);DataBase=Gestão_Economato;Integrated Security=SSPI");
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "select id,data_pedido,Id_Funcionario from EncomendasCliente where estado = 'Pendente' and Id_Cliente = @CLIENTE", conn);

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@CLIENTE";
                param.Value = emailpedidoTextBox.Text;

                cmd.Parameters.Add(param);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listarplafond.Add(new mostrarpedidos()
                    {
                        Id = reader.GetInt32(0),
                        Data_Pedido = reader.GetDateTime(1),
                        Funcionario = reader.GetString(2)
                    });
                }
                pedidosDataGrid.ItemsSource = listarplafond;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        private void Button_Pedido_Click(object sender, RoutedEventArgs e)
        {
            DateTime data = Convert.ToDateTime(datapedidoDatePicker.SelectedDate);
            EncomendasCliente encomendasCliente = new EncomendasCliente();
            encomendasCliente.data_pedido = data;
            encomendasCliente.Id_Cliente = emailpedidoTextBox.Text;
            encomendasCliente.Id_Funcionario = "joana@gmail.com";
            encomendasCliente.estado = "Pendente";
            context.EncomendasClientes.Add(encomendasCliente);
            context.SaveChanges();
            MostrarPedidos();
        }

        private void Button_Encomenda_Click(object sender, RoutedEventArgs e)
        {
            int id_prodrutos = Convert.ToInt32(produto_TextBox.Text);
            int unidades = Convert.ToInt32(unidades_TextBox.Text);
            int id_pedido = Convert.ToInt32(pedido_TextBox.Text);
            int precos;
            Produto produto = context.Produtoes.Find(id_prodrutos);
            ProdutoEncomendarCliente produtoEncomendarCliente = new ProdutoEncomendarCliente();
            precos = unidades * produto.Preco;
            produtoEncomendarCliente.preco = precos;
            produtoEncomendarCliente.Id_Produto = id_prodrutos;
            produtoEncomendarCliente.unidades = unidades;
            produtoEncomendarCliente.Id_EncomendasCliente = id_pedido;
            context.ProdutoEncomendarClientes.Add(produtoEncomendarCliente);
            context.SaveChanges();
            MessageBox.Show("Encomenda realizada com sucesso");
        }

        private void Produtos_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CProdutos produtos = new CProdutos();
            produtos.Show();
        }
    }
}
