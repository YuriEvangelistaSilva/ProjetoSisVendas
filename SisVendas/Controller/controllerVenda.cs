using Npgsql;
using SisVenda.Controller;
using SisVendas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SisVendas.Controller
{
    internal class controllerVenda
    {
        public NpgsqlDataReader novaVenda(modelVenda mVenda)
        {
            string sql = "insert into venda(cpfcliente, datavenda, totalvenda) values(@cpfcliente,@datavenda,@totalvenda) returning idvenda";

            Connection conexao = new Connection();
            NpgsqlConnection conn = conexao.conectaPG();
            NpgsqlCommand comm = new NpgsqlCommand(sql, conn);

            try
            {
                comm.Parameters.AddWithValue("@cpfcliente", mVenda.CpfCliente);
                comm.Parameters.AddWithValue("@datavenda", mVenda.DataVenda);
                comm.Parameters.AddWithValue("@totalvenda", mVenda.TotalVenda);
                return comm.ExecuteReader();
            }
            catch (NpgsqlException erro)
            {
                MessageBox.Show(erro.ToString());
                return null;

            }
        }

        public string atualizaTotalVenda(modelVenda mVenda)
        {
            string sql = "update venda set totalvenda = @totalvenda where idvenda = @idvenda";

            Connection conexao = new Connection();
            NpgsqlConnection conn = conexao.conectaPG();
            NpgsqlCommand comm = new NpgsqlCommand(sql, conn);

            try
            {
                comm.Parameters.AddWithValue("@idvenda", mVenda.IdVenda);
                comm.Parameters.AddWithValue("@totalvenda", mVenda.TotalVenda);
                comm.ExecuteReader();
                return "Venda finalizada";
            }
            catch (NpgsqlException erro)
            {
                MessageBox.Show("errou");
                return null;

            }
        }
        public NpgsqlDataReader pesquisaClienteCpf(modelVenda mVenda)
        {
            string sql = "select cpfCliente, datavenda, totalvenda from venda where cpfcliente = @cpfcliente ";

            Connection conexao = new Connection();
            NpgsqlConnection conn = conexao.conectaPG();
            NpgsqlCommand comm = new NpgsqlCommand(sql, conn);

            try
            {
                comm.Parameters.AddWithValue("@cpfcliente", mVenda.CpfCliente);

                return comm.ExecuteReader();
            }
            catch (NpgsqlException erro)
            {
                return null;
            }


        }
        public NpgsqlDataReader pesquisaVendaCliente(modelVenda mVenda)
        {
            string sql = "select idvenda, datavenda, totalvenda from venda inner join cliente on venda.cpfcliente = cliente.cpf where cpf = @cpfcliente";

            Connection conexao = new Connection();
            NpgsqlConnection conn = conexao.conectaPG();
            NpgsqlCommand comm = new NpgsqlCommand(sql, conn);

            try
            {
                comm.Parameters.AddWithValue("@cpfcliente", mVenda.CpfCliente);

                return comm.ExecuteReader();
            }
            catch (NpgsqlException erro)
            {
                return null;
            }

        }
        public NpgsqlDataReader listaItensVenda(modelVenda mVenda)
        {
            string sql = "select itensvenda.codigobarras, itensvenda.quantidade, itensvenda.valortotal, produto.descricao from itensvenda inner join produto on itensvenda.codigobarras = produto.codigobarras where itensvenda.idvenda = @idvenda";

            Connection conexao = new Connection();
            NpgsqlConnection conn = conexao.conectaPG();
            NpgsqlCommand comm = new NpgsqlCommand(sql, conn);

            try
            {
                comm.Parameters.AddWithValue("@idvenda", mVenda.IdVenda);

                return comm.ExecuteReader();
            }
            catch (NpgsqlException erro)
            {
                return null;
            }
        }
    }
}
