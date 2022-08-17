using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormMercado
{
    public partial class Form1 : Form
    {

        /* <Variáveis Globais> */

        public int NumVenda = 1;
        public int CodProduto = 0;
        public double ValorTotalVenda = 0;
        public int TotalItensVenda = 0;

        /* </Variáveis Globais> */

        public Form1() // Método Construtor
        {
            InitializeComponent();

            lblTotalItensVenda.Text = (dgvProdutos.RowCount - 1).ToString();
            lblValorTotalVenda.Text = ValorTotalVenda.ToString("C");
            txtCodVenda.Text = NumVenda.ToString();
        }
       

        /* <Funções> */

        public void limparCamposEntrada()
        {
            txtDescricao.Text = "";
            txtQuant.Text = "";
            txtValorUnit.Text = "";
        }

        public void limparTodosCampos()
        {
            limparCamposEntrada();
            dgvProdutos.Rows.Clear();
            this.ValorTotalVenda = 0;
            this.CodProduto = 0;
            lblValorTotalVenda.Text = this.ValorTotalVenda.ToString("C");
            lblTotalItensVenda.Text = (dgvProdutos.RowCount - 1).ToString();
            txtQuantSelecionada.Text = "";
        }
       
        public double calcularValorTotalProduto(string qntd, string valor)
        {
            return Convert.ToDouble(qntd) * Convert.ToDouble(valor);
        }

        /* </Funções> */

        /* <Eventos> */
        private void btnInserir_Click(object sender, EventArgs e)
        {
            if(txtDescricao.Text != "" && txtQuant.Text != "" && txtValorUnit.Text != "")
            {
                double valor_total_produto = calcularValorTotalProduto(txtQuant.Text, txtValorUnit.Text);
                this.CodProduto++;
                string[] row = {this.CodProduto.ToString(),txtDescricao.Text, txtQuant.Text, txtValorUnit.Text, valor_total_produto.ToString() };

                dgvProdutos.Rows.Add(row);
                lblTotalItensVenda.Text = (dgvProdutos.RowCount - 1).ToString();
                this.ValorTotalVenda += valor_total_produto;
                lblValorTotalVenda.Text = this.ValorTotalVenda.ToString("C");
                limparCamposEntrada();
            }
            else
            {
                MessageBox.Show("Digite todos os campos!");
            }
                
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(txtQuantSelecionada.Text != "")
            {
                dgvProdutos.CurrentRow.Cells["colQuantidade"].Value = txtQuantSelecionada.Text;
                double NovoValorTotal = calcularValorTotalProduto(dgvProdutos.CurrentRow.Cells["colQuantidade"].Value.ToString(), dgvProdutos.CurrentRow.Cells["colValorUnit"].Value.ToString());
                Console.WriteLine(dgvProdutos.CurrentRow.Cells["colTotalProduto"].Value);
                this.ValorTotalVenda -= Convert.ToDouble(dgvProdutos.CurrentRow.Cells["colTotalProduto"].Value);
                this.ValorTotalVenda += NovoValorTotal;
                lblValorTotalVenda.Text = this.ValorTotalVenda.ToString("C");

                dgvProdutos.CurrentRow.Cells["colTotalProduto"].Value = NovoValorTotal.ToString();
            }
           
        }
        private void btnNovaVenda_Click(object sender, EventArgs e)
        {
            limparTodosCampos();           
            this.NumVenda++;
            txtCodVenda.Text = this.NumVenda.ToString();
        }

       

        private void btnCancelarVenda_Click(object sender, EventArgs e)
        {
            limparTodosCampos();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dgvProdutos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((dgvProdutos.RowCount - 1) > 0)
                txtQuantSelecionada.Text = dgvProdutos.CurrentRow.Cells["colQuantidade"].Value.ToString();
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if((dgvProdutos.RowCount - 1) > 0)
            {
                this.ValorTotalVenda -= Convert.ToDouble(dgvProdutos.CurrentRow.Cells["colTotalProduto"].Value);
                dgvProdutos.Rows.RemoveAt(dgvProdutos.CurrentRow.Index);
                lblValorTotalVenda.Text = this.ValorTotalVenda.ToString("C");
                txtQuantSelecionada.Text = "";
                lblTotalItensVenda.Text = (dgvProdutos.RowCount - 1).ToString();
            }            
        }
    }
}
