using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Interaction logic for CPlafond.xaml
    /// </summary>
    public partial class CPlafond : Window
    {
        Gestão_EconomatoEntities context = new Gestão_EconomatoEntities();
        public CPlafond()
        {
            InitializeComponent();
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

        private void Button_Pedir_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = Convert.ToDateTime(dataDatepicker.SelectedDate);
            int valor = Convert.ToInt32(valorTextBox.Text);
            Plafond plafond = new Plafond();
            plafond.Id_Cliente = clienteTextBox.Text;
            plafond.data = date;
            plafond.quantidade_pedida = valor;
            plafond.estado = "Pendente";
            context.Plafonds.Add(plafond);
            context.SaveChanges();
            MostrarPlafond();

        }

        struct mostrarplafond
        {
            public int Id { get; set; }
            public DateTime Data{ get; set; }
            public int Valor { get; set; }

            public string Estado { get; set; }
        }

        public void MostrarPlafond()
        {
            SqlConnection conn = null;
            SqlDataReader reader = null;
            List<mostrarplafond> listarplafond = new List<mostrarplafond>();

            try
            {
                conn = new
                    SqlConnection("Server=(local);DataBase=Gestão_Economato;Integrated Security=SSPI");
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "select Plafond.id, Plafond.data, Plafond.quantidade_pedida, Plafond.estado from Plafond where Plafond.Id_Cliente = @CLIENTE", conn);

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@CLIENTE";
                param.Value = veremailTextBox.Text;

                cmd.Parameters.Add(param);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listarplafond.Add(new mostrarplafond()
                    {
                        Id = reader.GetInt32(0),
                        Data = reader.GetDateTime(1),
                        Valor = reader.GetInt32(2),
                        Estado = reader.GetString(3)
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
        private void Button_Ver_Click(object sender, RoutedEventArgs e)
        {
            MostrarPlafond();          
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

        private void Produtos_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CProdutos produtos = new CProdutos();
            produtos.Show();
        }
    }
}
