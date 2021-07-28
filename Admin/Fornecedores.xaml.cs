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
    /// Interaction logic for Fornecedores.xaml
    /// </summary>
    public partial class Fornecedores : Window
    {
        Gestão_EconomatoEntities context = new Gestão_EconomatoEntities();
        public Fornecedores()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            // Load data into the table Fornecedor. You can modify this code as needed.
            GestaoEconomato.Gestão_EconomatoDataSet gestão_EconomatoDataSet = ((GestaoEconomato.Gestão_EconomatoDataSet)(this.FindResource("gestão_EconomatoDataSet")));
            GestaoEconomato.Gestão_EconomatoDataSetTableAdapters.FornecedorTableAdapter gestão_EconomatoDataSetFornecedorTableAdapter = new GestaoEconomato.Gestão_EconomatoDataSetTableAdapters.FornecedorTableAdapter();
            gestão_EconomatoDataSetFornecedorTableAdapter.Fill(gestão_EconomatoDataSet.Fornecedor);
            System.Windows.Data.CollectionViewSource fornecedorViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("fornecedorViewSource")));
            fornecedorViewSource.View.MoveCurrentToFirst();
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
            Fornecedor fornecedor = new Fornecedor();
            int telefone = Convert.ToInt32(telefoneTextBox.Text);

            fornecedor.Nome = nomeTextBox.Text;
            fornecedor.Telefone = telefone;
            fornecedor.Email = emailTextBox.Text;
            context.Fornecedors.Add(fornecedor);
            context.SaveChanges();
            fornecedorDataGrid.ItemsSource = context.Fornecedors.ToList();
        }

        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            int id_fornecedor = Convert.ToInt32(idTextBox.Text);
            int telefone = Convert.ToInt32(telefoneTextBox.Text);
            Fornecedor existente = context.Fornecedors.Find(id_fornecedor);

            existente.Nome = nomeTextBox.Text;
            existente.Telefone = telefone;
            existente.Email = emailTextBox.Text;
            context.SaveChanges();
            fornecedorDataGrid.ItemsSource = context.Fornecedors.ToList();
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            int id_fornecedor = Convert.ToInt32(idTextBox.Text);
            Fornecedor existente = context.Fornecedors.Find(id_fornecedor);
            context.Fornecedors.Remove(existente);
            context.SaveChanges();
            fornecedorDataGrid.ItemsSource = context.Fornecedors.ToList();
            emailTextBox.Text = "";
            nomeTextBox.Text = "";
            telefoneTextBox.Text = "";
            idTextBox.Text = "";
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
            AdminPrincipal adminPrincipal = new AdminPrincipal();
            adminPrincipal.Show();
        }

        private void Produtos_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Produtos produtos = new Produtos();
            produtos.Show();
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
