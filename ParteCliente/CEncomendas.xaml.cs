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
    /// Interaction logic for CEncomendas.xaml
    /// </summary>
    public partial class CEncomendas : Window
    {
        Gestão_EconomatoEntities context = new Gestão_EconomatoEntities();
        public CEncomendas()
        {
            InitializeComponent();
        }

        struct mostrarpedidosencomenda
        {
            public int Id { get; set; }
            public DateTime Data_Entregue { get; set; }
            public DateTime Data_Pedido { get; set; }
            public string Estado { get; set; }
            public string Funcionario { get; set; }

        }

        public void Mostrarpedidosencomendas()
        {
            SqlConnection conn = null;
            SqlDataReader reader = null;
            List<mostrarpedidosencomenda> listarpedidos = new List<mostrarpedidosencomenda>();

            try
            {
                conn = new
                    SqlConnection("Server=(local);DataBase=Gestão_Economato;Integrated Security=SSPI");
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "select EncomendasCliente.id, EncomendasCliente.data_entregue, EncomendasCliente.data_pedido, EncomendasCliente.estado, EncomendasCliente.Id_Funcionario from EncomendasCliente where EncomendasCliente.Id_Cliente=@CLIENTE and data_entregue is not null", conn);

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@CLIENTE";
                param.Value = emailTextBox.Text;

                cmd.Parameters.Add(param);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listarpedidos.Add(new mostrarpedidosencomenda()
                    {
                        Id = reader.GetInt32(0),
                        Data_Entregue = reader.GetDateTime(1),
                        Data_Pedido = reader.GetDateTime(2),
                        Estado = reader.GetString(3),
                        Funcionario = reader.GetString(4)
                    });
                }
                pedidosDataGrid.ItemsSource = listarpedidos;
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

        struct mostrarpedidos
        {
            public int Id { get; set; }
            public string Nome_Produto { get; set; }
            public int Preco { get; set; }
            public int Unidades { get; set; }
            public int Pedido { get; set; }
        }

        public void Mostrarencomendas()
        {
            SqlConnection conn = null;
            SqlDataReader reader = null;
            List<mostrarpedidos> listarencomendas = new List<mostrarpedidos>();

            try
            {
                conn = new
                    SqlConnection("Server=(local);DataBase=Gestão_Economato;Integrated Security=SSPI");
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "select ProdutoEncomendarCliente.id, Produto.Nome, ProdutoEncomendarCliente.preco, ProdutoEncomendarCliente.unidades, ProdutoEncomendarCliente.Id_EncomendasCliente from ProdutoEncomendarCliente inner join Produto on ProdutoEncomendarCliente.Id_Produto=Produto.id inner join EncomendasCliente on ProdutoEncomendarCliente.Id_EncomendasCliente=EncomendasCliente.id where EncomendasCliente.Id_Cliente =@CLIENTE order by ProdutoEncomendarCliente.Id_EncomendasCliente", conn);

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@CLIENTE";
                param.Value = emailTextBox.Text;

                cmd.Parameters.Add(param);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listarencomendas.Add(new mostrarpedidos()
                    {
                        Id = reader.GetInt32(0),
                        Nome_Produto = reader.GetString(1),
                        Preco = reader.GetInt32(2),
                        Unidades = reader.GetInt32(3),
                        Pedido = reader.GetInt32(4),

                    });
                }
                encomendaDataGrid.ItemsSource = listarencomendas;
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
        private void Button_Ver_Click(object sender, RoutedEventArgs e)
        {
            Mostrarpedidosencomendas();
            Mostrarencomendas();
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

        private void Pedidos_Plafond_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CPlafond plafond = new CPlafond();
            plafond.Show();
        }

        private void Pedidos_Encomendas_Cliente_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CEncomendas encomendas = new CEncomendas();
            encomendas.Show();
        }

        private void Produtos_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CProdutos produtos = new CProdutos();
            produtos.Show();
        }
    }
}
