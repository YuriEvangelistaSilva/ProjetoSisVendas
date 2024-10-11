using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SisVendas.Model;
using Npgsql;
using SisVendas.Controller;
using SisVenda.Controller;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SisVendas.Controller
{
    class controllerCliente
    {
        public string cadastroCliente(modelCliente mCliente)
        {
            string sql = "insert into cliente(cpf, nomecliente, rg, nascimento," +
            " endereco, telefone, idcidade) " +
            "values(@cpf, @nomecliente, @rg, @nascimento, @endereco, @telefone, @idcidade)";

            Connection conexao = new Connection();
            NpgsqlConnection conn = conexao.conectaPG();
            NpgsqlCommand comm = new NpgsqlCommand(sql, conn);

            try
            {
                comm.Parameters.AddWithValue("@cpf", mCliente.Cpf);
                comm.Parameters.AddWithValue("@nomecliente", mCliente.NomeCliente);
                comm.Parameters.AddWithValue("@rg", mCliente.Rg);
                comm.Parameters.AddWithValue("@nascimento", mCliente.Nascimento);
                comm.Parameters.AddWithValue("@endereco", mCliente.Endereco);
                comm.Parameters.AddWithValue("@telefone", mCliente.Telefone);
                comm.Parameters.AddWithValue("@idcidade", mCliente.IdCidade);

                comm.ExecuteNonQuery();
                return "Cliente cadastrado com sucesso!";
            }
            catch (NpgsqlException erro)
            {
                return erro.ToString();
                //return "Erro ao cadastrar cliente!";
            }
        }

        public NpgsqlDataReader pesquisaClienteNome(modelCliente mCliente)
        {
            string sql = "select c.nomecliente as \"Cliente\", c.cpf as \"CPF\", c.rg as \"Rg\", c.nascimento as \"Data Nacimento\", c.endereco as \"Endereço\", c.telefone as \"Telefone\" \r\nfrom cliente c inner join cidade cid\r\non c.idcidade = cid.idcidade\r\nwhere c.nomecliente \r\nlike @nomecliente";

            Connection conexao = new Connection();
            NpgsqlConnection conn = conexao.conectaPG();
            NpgsqlCommand comm = new NpgsqlCommand(sql, conn);

            try
            {
                comm.Parameters.AddWithValue("@nomecliente", mCliente.NomeCliente);

                return comm.ExecuteReader();
            }
            catch (NpgsqlException erro)
            {
                return null;
            }

        }

         public NpgsqlDataReader pesquisaClienteCpf(modelCliente mCliente)
    {
        string sql = "select c.nomecliente as \"Cliente\", c.cpf as \"CPF\", c.rg as \"Rg\", c.nascimento as \"Data Nacimento\", c.endereco as \"Endereço\", c.telefone as \"Telefone\" \r\nfrom cliente c inner join cidade cid\r\non c.idcidade = cid.idcidade\r\n where c.cpf = @cpf";

        Connection conexao = new Connection();
        NpgsqlConnection conn = conexao.conectaPG();
        NpgsqlCommand comm = new NpgsqlCommand(sql, conn);

        try
        {
            comm.Parameters.AddWithValue("@cpf", mCliente.Cpf);

            return comm.ExecuteReader();
        }
        catch (NpgsqlException erro)
        {
            return null;
        }
    } 
    }

   
}