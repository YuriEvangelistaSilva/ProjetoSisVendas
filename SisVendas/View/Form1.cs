using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SisVendas.Model;
using SisVendas.View;
using SisVendas.Controller;
using Npgsql;


namespace SisVendas
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }
        decimal preco = 0, total = 0;
        int quant = 0, novaQuant = 0;

        private void carregaPrincipal(object sender, EventArgs e)
        {
            //evento load do form 1
            carregaComboboxCidade();
            carregaComboboxTipoProduto();
            carregaComboboxMarca();
            carregaComboboxFornecedor();
        }

        private void novoProduto(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
            abaNovoProduto.Parent = tabControl1;
            tabControl1.SelectedTab = abaNovoProduto;

            abaNovoCliente.Parent = null;
            abaNovaVenda.Parent = null;
            abaBuscaCliente.Parent = null;
            abaBuscaProduto.Parent = null;
            abaListarVendas.Parent = null;
        }

        private void novoCliente(object sender, EventArgs e)
        {
            errorProvider1.Clear(); //remove os erros para um novo cadastro 

            tabControl1.Visible = true; //deixa visível um tabControl

            abaNovoCliente.Parent = tabControl1; //vincula um tabPage a um tabControl
            tabControl1.SelectedTab = abaNovoCliente; //seleciona uma aba para uso

            abaNovoProduto.Parent = null; //desvincula um tabPage de um tabControl
            abaNovaVenda.Parent = null;
            abaBuscaCliente.Parent = null;
            abaBuscaProduto.Parent = null;
            abaListarVendas.Parent = null;
        }

        private void frmCidade(object sender, LinkLabelLinkClickedEventArgs e)
        {
            viewCidade frmCidade = new viewCidade();
            frmCidade.ShowDialog();
        }

        private void atualizaCombobox(object sender, EventArgs e)
        {
            carregaComboboxCidade();
        }

        private void novaVenda(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
            abaNovaVenda.Parent = tabControl1;
            abaBuscaCliente.Parent = tabControl1;
            abaBuscaProduto.Parent = tabControl1;
            tabControl1.SelectedTab = abaNovaVenda;

            abaNovoCliente.Parent = null;
            abaNovoProduto.Parent = null;
            abaListarVendas.Parent = null;
        }

        private void fechaNovoCliente_btnSair(object sender, EventArgs e)
        {
            abaNovoCliente.Parent = null;
            tabControl1.SelectedTab = null;
            tabControl1.Visible = false;
        }

        private void cadastrarCliente(object sender, EventArgs e)
        {
            modelCliente mCliente = new modelCliente();
            controllerCliente cCliente = new controllerCliente();

            if (validaCliente())
            {
                mCliente.Cpf = Convert.ToInt64(maskedTextBox1.Text);
                mCliente.NomeCliente = textBox1.Text;
                mCliente.Rg = textBox3.Text;
                mCliente.Endereco = textBox4.Text;
                mCliente.IdCidade = Convert.ToInt32(comboCidade_cliente.SelectedValue);
                mCliente.Nascimento = dateTimePicker1.Value;
                mCliente.Telefone = maskedTextBox2.Text;

                string res = cCliente.cadastroCliente(mCliente);
                MessageBox.Show(res);
            }


        }

        private void limparAbaCliente(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            maskedTextBox1.Text = "";
            maskedTextBox2.Text = "";
        }

        /* MÉTODOS DE CONFIGURAÇÃO DOS COMPONENTES DO FORM */

        private void carregaComboboxCidade()
        {
            controllerCidade cCidade = new controllerCidade();

            /* NpgsqlDataReader - tipo de dado que armazena o resultado
             de consultas (SELECT) no banco de dados */
            NpgsqlDataReader dados = cCidade.listaCidade();

            //DataTable - armazena dados no formato de tabela
            DataTable cidade = new DataTable();

            //preenche o dataTable com os dados do DataReader
            cidade.Load(dados);

            comboCidade_cliente.DataSource = cidade;

            //DisplayMember - define qual coluna será exibida na combobox
            comboCidade_cliente.DisplayMember = "nomecidade";

            //ValueMember - define qual coluna será usada como valor válido na combobox 
            comboCidade_cliente.ValueMember = "idcidade";
        }

        private void carregaComboboxTipoProduto()
        {
            controllerTipoProduto cTipoProduto = new controllerTipoProduto();

            /* NpgsqlDataReader - tipo de dado que armazena o resultado
             de consultas (SELECT) no banco de dados */
            NpgsqlDataReader dados = cTipoProduto.listaTipos();

            //DataTable - armazena dados no formato de tabela
            DataTable tipo = new DataTable();

            //preenche o dataTable com os dados do DataReader
            tipo.Load(dados);

            comboTipo_Produto.DataSource = tipo;

            //DisplayMember - define qual coluna será exibida na combobox
            comboTipo_Produto.DisplayMember = "nometipo";

            //ValueMember - define qual coluna será usada como valor válido na combobox 
            comboTipo_Produto.ValueMember = "idtipo";
        }

        private void carregaComboboxMarca()
        {
            controllerMarca cMarca = new controllerMarca();

            /* NpgsqlDataReader - tipo de dado que armazena o resultado
             de consultas (SELECT) no banco de dados */
            NpgsqlDataReader dados = cMarca.listaMarcas();

            //DataTable - armazena dados no formato de tabela
            DataTable marca = new DataTable();

            //preenche o dataTable com os dados do DataReader
            marca.Load(dados);

            comboMarca_Produto.DataSource = marca;

            //DisplayMember - define qual coluna será exibida na combobox
            comboMarca_Produto.DisplayMember = "nomemarca";

            //ValueMember - define qual coluna será usada como valor válido na combobox 
            comboMarca_Produto.ValueMember = "idmarca";
        }

        private void carregaComboboxFornecedor()
        {
            controllerFornecedor cFornecedor = new controllerFornecedor();

            /* NpgsqlDataReader - tipo de dado que armazena o resultado
             de consultas (SELECT) no banco de dados */
            NpgsqlDataReader dados = cFornecedor.listaFornecedores();

            //DataTable - armazena dados no formato de tabela
            DataTable fornecedor = new DataTable();

            //preenche o dataTable com os dados do DataReader
            fornecedor.Load(dados);

            comboFornecedor_Produto.DataSource = fornecedor;

            //DisplayMember - define qual coluna será exibida na combobox
            comboFornecedor_Produto.DisplayMember = "nomefornecedor";

            //ValueMember - define qual coluna será usada como valor válido na combobox 
            comboFornecedor_Produto.ValueMember = "cnpj";
        }

        private bool validaCliente()
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox1, "Preencha este campo");
                return false;
            }
            if (String.IsNullOrWhiteSpace(maskedTextBox1.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(maskedTextBox1, "Preencha este campo");
                return false;
            }
            if (String.IsNullOrWhiteSpace(textBox3.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox3, "Preencha este campo");
                return false;
            }
            if (String.IsNullOrWhiteSpace(textBox4.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox4, "Preencha este campo"); ;
                return false;
            }
            if (String.IsNullOrWhiteSpace(maskedTextBox2.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(maskedTextBox2, "Preencha este campo");
                return false;
            }
            else
            {
                errorProvider1.Clear();
                return true;
            }
        }

        private void frmTipoProduto(object sender, LinkLabelLinkClickedEventArgs e)
        {
            viewTipoProduto frmTipoProduto = new viewTipoProduto();
            frmTipoProduto.ShowDialog();
        }

        private void frmMarca(object sender, LinkLabelLinkClickedEventArgs e)
        {
            viewMarca frmMarca = new viewMarca();
            frmMarca.ShowDialog();
        }

        private void frmFornecedor(object sender, LinkLabelLinkClickedEventArgs e)
        {
            viewFornecedor frmFornecedor = new viewFornecedor();
            frmFornecedor.ShowDialog();
        }

        private void atualizaComboboxTipo(object sender, EventArgs e)
        {
            carregaComboboxTipoProduto();
        }

        private void atualizaComboboxMarca(object sender, EventArgs e)
        {
            carregaComboboxMarca();
        }

        private void atualizaComboboxFornecedor(object sender, EventArgs e)
        {
            carregaComboboxFornecedor();
        }

        private void cadastrarProduto(object sender, EventArgs e)
        {
            modelProduto mProduto = new modelProduto();
            controllerProduto cProduto = new controllerProduto();

            if (validaProduto())
            {
                mProduto.CodigoBarras = textBox2.Text;
                mProduto.NomeProduto = textBox5.Text;
                mProduto.Validade = dateTimePicker2.Value;
                mProduto.PrecoCusto = Convert.ToDecimal(textBox6.Text);
                mProduto.PrecoVenda = Convert.ToDecimal(textBox7.Text);
                mProduto.Descricao = textBox9.Text;
                mProduto.Quantidade = Convert.ToInt32(textBox8.Text);
                mProduto.IdTipo = Convert.ToInt32(comboTipo_Produto.SelectedValue);
                mProduto.IdMarca = Convert.ToInt32(comboMarca_Produto.SelectedValue);
                mProduto.Cnpj = Convert.ToString(comboFornecedor_Produto.SelectedValue);
                string res = cProduto.cadastroProduto(mProduto);
                MessageBox.Show(res);
            }

        }

        private bool validaProduto()
        {
            if (String.IsNullOrWhiteSpace(textBox2.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox2, "Preencha este campo");
                return false;
            }
            if (String.IsNullOrWhiteSpace(textBox5.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox5, "Preencha este campo");
                return false;
            }
            if (String.IsNullOrWhiteSpace(textBox6.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox6, "Preencha este campo");
                return false;
            }
            if (String.IsNullOrWhiteSpace(textBox7.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox7, "Preencha este campo"); ;
                return false;
            }
            if (String.IsNullOrWhiteSpace(textBox8.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox8, "Preencha este campo");
                return false;
            }
            if (String.IsNullOrWhiteSpace(textBox9.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox9, "Preencha este campo");
                return false;
            }
            else
            {
                errorProvider1.Clear();
                return true;
            }
        }


        private void limparAbaProduto(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            maskedTextBox2.Text = "";
        }

        private void fechaNovoProduto_btnSair(object sender, EventArgs e)
        {
            abaNovoProduto.Parent = null;
            tabControl1.SelectedTab = null;
            tabControl1.Visible = false;
        }

        private void abaNovoProduto_Click(object sender, EventArgs e)
        {

        }

        private void abaNovoCliente_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void novoFornecedor(object sender, EventArgs e)
        {
            viewFornecedor frmFornecedor = new viewFornecedor();
            frmFornecedor.ShowDialog();
        }

        private void novoCidade(object sender, EventArgs e)
        {
            viewCidade frmCidade = new viewCidade();
            frmCidade.ShowDialog();
        }

        private void novaMarca(object sender, EventArgs e)
        {
            viewMarca frmMarca = new viewMarca();
            frmMarca.ShowDialog();
        }

        private void novoTipoProduto(object sender, EventArgs e)
        {
            viewTipoProduto frmTipoProduto = new viewTipoProduto();
            frmTipoProduto.ShowDialog();
        }

        private void consultaCliente(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
            abaBuscaCliente.Parent = tabControl1;
            tabControl1.SelectedTab = abaBuscaCliente;

            abaNovoCliente.Parent = null;
            abaNovoProduto.Parent = null;
            abaNovaVenda.Parent = null;
            abaBuscaProduto.Parent = null;
            abaListarVendas.Parent = null;


        }

        private void pesquisaCliente(object sender, EventArgs e)
        {
            modelCliente mCliente = new modelCliente();
            controllerCliente cCliente = new controllerCliente();
            NpgsqlDataReader cliente;

            if (!String.IsNullOrWhiteSpace(textBoxPesquisaCliente.Text))
            {

                if (radioButtonNomeCliente.Checked)
                {

                    mCliente.NomeCliente = textBoxPesquisaCliente.Text + "%";
                    cliente = cCliente.pesquisaClienteNome(mCliente);
                    gridCliente(cliente);


                }
                else if (radioButtonCpf.Checked)
                {
                    if (textBoxPesquisaCliente.Text.Length == 11)
                    {
                        mCliente.Cpf = long.Parse(textBoxPesquisaCliente.Text);
                        cliente = cCliente.pesquisaClienteCpf(mCliente);
                        gridCliente(cliente);
                    }
                    else
                    {
                        cliente = null;
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("compo esta errado");

                }


            }

        }


        // **METODOS**
        private void gridCliente(NpgsqlDataReader dados)
        {

            {
                //apara as colunas da tabela
                dataGridView1.Columns.Clear();

                // define a quantidade de coluna da grid = DataReader
                dataGridView1.ColumnCount = dados.FieldCount;

                //definir os nomes da colunas da grid
                for (int i = 0; i < dados.FieldCount; i++)
                {
                    dataGridView1.Columns[i].Name = dados.GetName(i);
                }

                string[] linha = new string[dados.FieldCount];

                while (dados.Read())
                {
                    for (int i = 0; i < dados.FieldCount; i++)
                    {
                        linha[i] = dados.GetValue(i).ToString();
                    }
                    dataGridView1.Rows.Add(linha);

                }
            }
        }
        private void maskNomeCliente(object sender, EventArgs e)
        {
            textBoxPesquisaCliente.Mask = null;
        }

        private void maskCpfCliente(object sender, EventArgs e)
        {
            textBoxPesquisaCliente.Mask = "000,000,000,-00";
        }

        private void pesquisaProduto(object sender, EventArgs e)
        {
            modelProduto mProduto = new modelProduto();
            controllerProduto cProduto = new controllerProduto();
            NpgsqlDataReader produto;

            if (!String.IsNullOrWhiteSpace(textBoxPesquisaProduto.Text))
            {
                mProduto.NomeProduto = textBoxPesquisaProduto.Text + "%";
                produto = cProduto.pesquisaProdutoNome(mProduto);
                gridProduto(produto);

            }
        }
        private void gridProduto(NpgsqlDataReader dados)
        {
            {
                //apara as colunas da tabela
                dataGridView2.Columns.Clear();

                // define a quantidade de coluna da grid = DataReader
                dataGridView2.ColumnCount = dados.FieldCount;

                //definir os nomes da colunas da grid
                for (int i = 0; i < dados.FieldCount; i++)
                {
                    dataGridView2.Columns[i].Name = dados.GetName(i);
                }

                string[] linha = new string[dados.FieldCount];

                while (dados.Read())
                {
                    for (int i = 0; i < dados.FieldCount; i++)
                    {
                        linha[i] = dados.GetValue(i).ToString();
                    }
                    dataGridView2.Rows.Add(linha);

                }
            }
        }

        private void consultaProduto(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
            abaBuscaProduto.Parent = tabControl1;
            tabControl1.SelectedTab = abaBuscaProduto;

            abaNovoCliente.Parent = null;
            abaNovoProduto.Parent = null;
            abaNovaVenda.Parent = null;
            abaBuscaCliente.Parent = null;
            abaListarVendas.Parent = null;
        }

        private void buscaCpfCliente(object sender, KeyPressEventArgs e)
        {

            modelCliente mCliente = new modelCliente();
            controllerCliente cCliente = new controllerCliente();

            if (maskedTextBoxVendaCpf.Text.Length == 11)
            {
                if (e.KeyChar == 13)
                {
                    mCliente.Cpf = long.Parse(maskedTextBoxVendaCpf.Text);
                    NpgsqlDataReader cliente = cCliente.pesquisaClienteCpf(mCliente);

                    if (!cliente.HasRows)
                    {
                        MessageBox.Show("Cliente não encomtrado!");
                    }
                    else
                    {
                        while (cliente.Read())
                        {
                            textBoxVendaNomeCliente.Text = cliente.GetValue(0).ToString();
                        }
                    }
                }

            }

        }

        private void buscaProdutoVenda(object sender, KeyPressEventArgs e)
        {
            modelProduto mProduto = new modelProduto();
            controllerProduto cProduto = new controllerProduto();

            if (e.KeyChar == 13)
            {
                if (radioButtonVendaCodigoBarrasProduto.Checked)
                {

                    mProduto.CodigoBarras = textBoxVendaPesquisaProduto.Text;
                    mProduto.NomeProduto = "null%";

                }
                if (radioButtonVendaNomeProduto.Checked)
                {
                    mProduto.NomeProduto = textBoxVendaPesquisaProduto.Text + "%";
                    mProduto.CodigoBarras = "null";

                }

                NpgsqlDataReader produto = cProduto.pesquisaProdutoVenda(mProduto);

                if (!produto.HasRows)
                {
                    MessageBox.Show("Produto não cadastrado");
                }
                else
                {
                    gridProdutoVenda(produto);
                }
            }
        }

        private void selecionaLinha(object sender, DataGridViewCellEventArgs e)
        {
            //salva a quantidade atual de um itens selecionada
            quant = Convert.ToInt32(dataGridViewVendaItems.CurrentRow.Cells[3].Value);
        }

        private void removerItem(object sender, EventArgs e)
        {
            if (dataGridViewVendaItems.RowCount > 0)
            {
                DialogResult comfirm = MessageBox.Show("Remover item",
                    "Deseja remover esse item?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (comfirm == DialogResult.Yes)
                {
                    novaQuant = Convert.ToInt32(dataGridViewVendaItems.CurrentRow.Cells[3].Value);
                    preco = decimal.Parse(dataGridViewVendaItems.CurrentRow.Cells[2].Value.ToString());

                    total = total - (novaQuant * preco);
                    labelTotalItens.Text = total.ToString();
                    labelTotalVenda.Text = total.ToString();

                    dataGridViewVendaItems.Rows.Remove(dataGridViewVendaItems.CurrentRow);
                }

            }
        }

        private void calculaDesconto(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                total = decimal.Parse(labelTotalItens.Text);
                decimal desc = decimal.Parse(textBoxDesconto.Text) / 100;
                decimal totalVenda = total - (total * desc);
                labelTotalVenda.Text = totalVenda.ToString();
            }
        }

        private void atualizaTotal(object sender, DataGridViewCellEventArgs e)
        {
            novaQuant = Convert.ToInt32(dataGridViewVendaItems.CurrentRow.Cells[3].Value);
            preco = decimal.Parse(dataGridViewVendaItems.CurrentRow.Cells[2].Value.ToString());

            if (novaQuant > 0)
            {
                total = total + ((novaQuant - quant) * preco);
            }
            if (novaQuant < 0)
            {
                total = total + ((quant - novaQuant) * preco);
            }

            quant = novaQuant;
            labelTotalItens.Text = total.ToString();
            labelTotalVenda.Text = total.ToString();
        }

        private void salvarVenda(object sender, EventArgs e)
        {
            modelVenda mVenda = new modelVenda();
            controllerVenda cVenda = new controllerVenda();

            modelItensVenda mItens = new modelItensVenda();
            controllerItemsVenda cItens = new controllerItemsVenda();

            mVenda.CpfCliente = long.Parse(maskedTextBoxVendaCpf.Text);
            mVenda.DataVenda = DateTime.Now;

            if (!String.IsNullOrEmpty(textBoxVendaNomeCliente.Text))
            {
                if (dataGridViewVendaItems.Rows.Count > 0)
                {

                    NpgsqlDataReader venda = cVenda.novaVenda(mVenda);

                    while (venda.Read())
                    {
                        mItens.IdVenda = int.Parse(venda.GetValue(0).ToString());
                        MessageBox.Show(mItens.IdVenda.ToString());
                    }

                    for (int l = 0; l < dataGridViewVendaItems.RowCount; l++)
                    {
                        mItens.IdProduto = dataGridViewVendaItems.Rows[l].Cells[0].Value.ToString();
                        mItens.Quantidade = Convert.ToInt32(dataGridViewVendaItems.Rows[l].Cells[3].Value);

                        mItens.ValorTotal = mItens.Quantidade * float.Parse(dataGridViewVendaItems.Rows[l].Cells[2].Value.ToString());
                        MessageBox.Show(cItens.NovaItensVenda(mItens));
                    }
                    mVenda.IdVenda = mItens.IdVenda;
                    mVenda.TotalVenda = float.Parse(labelTotalVenda.Text);
                    MessageBox.Show(cVenda.atualizaTotalVenda(mVenda));
                }
            }
        }

        private void consultarVenda(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
            abaListarVendas.Parent = tabControl1;
            tabControl1.SelectedTab = abaListarVendas;

            abaNovoCliente.Parent = null;
            abaNovoProduto.Parent = null;
            abaNovaVenda.Parent = null;
            abaBuscaCliente.Parent = null;
            abaBuscaProduto.Parent = null;
            
        }

      

        private void gridProdutoVenda(NpgsqlDataReader dados)
        {
            {
                //apara as colunas da tabela
                dataGridViewVendaProduto.Columns.Clear();

                // define a quantidade de coluna da grid = DataReader
                dataGridViewVendaProduto.ColumnCount = dados.FieldCount;

                //definir os nomes da colunas da grid
                for (int i = 0; i < dados.FieldCount; i++)
                {
                    dataGridViewVendaProduto.Columns[i].Name = dados.GetName(i);
                }

                string[] linha = new string[dados.FieldCount];

                while (dados.Read())
                {
                    for (int i = 0; i < dados.FieldCount; i++)
                    {
                        linha[i] = dados.GetValue(i).ToString();
                    }
                    dataGridViewVendaProduto.Rows.Add(linha);

                }
            }
        }

      

        private void addItensVenda(object sender, DataGridViewCellEventArgs e)
        {
            //adiciona items a venda
            string[] produto = new string[4];
            produto[0] = dataGridViewVendaProduto.CurrentRow.Cells[0].Value.ToString();//codigo
            produto[1] = dataGridViewVendaProduto.CurrentRow.Cells[1].Value.ToString();//nome
            produto[2] = dataGridViewVendaProduto.CurrentRow.Cells[2].Value.ToString();//preco
            produto[3] = "1";//qtd

            /* calcula e atualiza*/

            preco = decimal.Parse(produto[2]);
            quant = Convert.ToInt32(produto[3]);
            total = decimal.Parse(labelTotalItens.Text) + ( preco * quant);

            dataGridViewVendaItems.Rows.Add(produto);
            labelTotalItens.Text = total.ToString();
            labelTotalItens.Text= total.ToString();
        }

        private void addListarVendas(object sender, DataGridViewCellEventArgs e)
        {
            modelVenda mVenda = new modelVenda();

            mVenda.CpfCliente = Convert.ToInt64(dataGridViewListarVEndasCliente.CurrentRow.Cells[1].Value.ToString()); 
           
            controllerVenda cVenda = new controllerVenda();

            NpgsqlDataReader vendas = cVenda.pesquisaVendaCliente(mVenda);
            gridVendas(vendas);
            
        }

        private void gridVendas(NpgsqlDataReader dados)
        {
            {
                //apara as colunas da tabela
                dataGridViewListarVenda.Columns.Clear();

                // define a quantidade de coluna da grid = DataReader
                dataGridViewListarVenda.ColumnCount = dados.FieldCount;

                //definir os nomes da colunas da grid
                for (int i = 0; i < dados.FieldCount; i++)
                {
                    dataGridViewListarVenda.Columns[i].Name = dados.GetName(i);
                }

                string[] linha = new string[dados.FieldCount];

                while (dados.Read())
                {
                    for (int i = 0; i < dados.FieldCount; i++)
                    {
                        linha[i] = dados.GetValue(i).ToString();
                    }
                    dataGridViewListarVenda.Rows.Add(linha);
                }
            }
        }

        private void ListarItensVenda(object sender, DataGridViewCellEventArgs e)
        {
            modelVenda mVenda = new modelVenda();

            mVenda.IdVenda = int.Parse(dataGridViewListarVenda.CurrentRow.Cells[0].Value.ToString());
            controllerVenda cVenda = new controllerVenda();

            NpgsqlDataReader itensVendas = cVenda.listaItensVenda(mVenda);
            gridItensVendas(itensVendas);
        }

        private void gridItensVendas(NpgsqlDataReader dados)
        {
            {
                //apara as colunas da tabela
                dataGridViewItensVenda.Columns.Clear();

                // define a quantidade de coluna da grid = DataReader
                dataGridViewItensVenda.ColumnCount = dados.FieldCount;

                //definir os nomes da colunas da grid
                for (int i = 0; i < dados.FieldCount; i++)
                {
                    dataGridViewItensVenda.Columns[i].Name = dados.GetName(i);
                }

                string[] linha = new string[dados.FieldCount];

                while (dados.Read())
                {
                    for (int i = 0; i < dados.FieldCount; i++)
                    {
                        linha[i] = dados.GetValue(i).ToString();
                    }
                    dataGridViewItensVenda.Rows.Add(linha);
                }
            }
        }

        private void buscaClienteListarVenda(object sender, KeyPressEventArgs e)
        {
            modelCliente mCliente = new modelCliente();
            controllerCliente cCliente = new controllerCliente();
            NpgsqlDataReader cliente;
            if (e.KeyChar == 13)
            {
                if (!String.IsNullOrWhiteSpace(maskedTextBoxVendaCliente.Text))
                {

                    if (radioButtonVendasNomeCliente.Checked)
                    {

                        mCliente.NomeCliente = textBoxPesquisaCliente.Text + "%";
                        cliente = cCliente.pesquisaClienteNome(mCliente);
                        gridLisatrVendaCliente(cliente);


                    }
                    else if (radioButtonVendasCpf.Checked)
                    {
                        if (textBoxPesquisaCliente.Text.Length == 11)
                        {
                            mCliente.Cpf = long.Parse(textBoxPesquisaCliente.Text);
                            cliente = cCliente.pesquisaClienteCpf(mCliente);
                            gridLisatrVendaCliente(cliente);
                        }
                        else
                        {
                            cliente = null;
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("compo esta errado");

                    }


                }

            }
        }

        private void gridLisatrVendaCliente(NpgsqlDataReader dados)
        {
            {
                //apara as colunas da tabela
                dataGridViewListarVEndasCliente.Columns.Clear();

                // define a quantidade de coluna da grid = DataReader
                dataGridViewListarVEndasCliente.ColumnCount = dados.FieldCount;

                //definir os nomes da colunas da grid
                for (int i = 0; i < dados.FieldCount; i++)
                {
                    dataGridViewListarVEndasCliente.Columns[i].Name = dados.GetName(i);
                }

                string[] linha = new string[dados.FieldCount];

                while (dados.Read())
                {
                    for (int i = 0; i < dados.FieldCount; i++)
                    {
                        linha[i] = dados.GetValue(i).ToString();
                    }
                    dataGridViewListarVEndasCliente.Rows.Add(linha);

                }
            }
        }

    }

        
     
        // **METODOS**
       
    }


