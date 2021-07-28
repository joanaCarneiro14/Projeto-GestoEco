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
    /// Interaction logic for Produtos.xaml
    /// </summary>
    public partial class Produtos : Window
    {
        Gestão_EconomatoEntities context = new Gestão_EconomatoEntities();
        public Produtos()
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

        private void Button_Adicionar_Click(object sender, RoutedEventArgs e)
        {
            Produto produto = new Produto();
            int preco = Convert.ToInt32(precoTextBox.Text);
            int stock = Convert.ToInt32(stockTextBox.Text);

            produto.Nome = nomeTextBox.Text;
            produto.Setor = setorTextBox.Text;
            produto.Stock = stock;
            produto.Preco = preco;
            context.Produtoes.Add(produto);
            context.SaveChanges();
            produtoDataGrid.ItemsSource = context.Produtoes.ToList();
        }

        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            int id_produto = Convert.ToInt32(idTextBox.Text);
            int preco = Convert.ToInt32(precoTextBox.Text);
            int stock = Convert.ToInt32(stockTextBox.Text);

            Produto existente = context.Produtoes.Find(id_produto);

            existente.Nome = nomeTextBox.Text;
            existente.Setor = setorTextBox.Text;
            existente.Stock = stock;
            existente.Preco = preco;
            context.SaveChanges();
            produtoDataGrid.ItemsSource = context.Produtoes.ToList();
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            int id_produto = Convert.ToInt32(idTextBox.Text);
            Produto existente = context.Produtoes.Find(id_produto);
            context.Produtoes.Remove(existente);
            context.SaveChanges();
            produtoDataGrid.ItemsSource = context.Produtoes.ToList();
            idTextBox.Text = "";
            nomeTextBox.Text = "";
            setorTextBox.Text = "";
            stockTextBox.Text = "";
            precoTextBox.Text = "";
        }

        private void Funcionario_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AdminPrincipal adminPrincipal = new AdminPrincipal();
            adminPrincipal.Show();
        }

        private void Cliente_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Clientes clientes = new Clientes();
            clientes.Show();
        }

        private void Fornecedores_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Fornecedores fornecedores = new Fornecedores();
            fornecedores.Show();
        }
        private void Pedidos_Encomendas_Cliente_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Pedidos_Encomendas_Cliente pedidos_Encomendas_Cliente = new Pedidos_Encomendas_Cliente();
            pedidos_Encomendas_Cliente.Show();
        }

        private void Pedidos_Plafond_Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            PedidosPlafond pedidosplafond = new PedidosPlafond();
            pedidosplafond.Show();
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
