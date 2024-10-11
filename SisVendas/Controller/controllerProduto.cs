﻿using Npgsql;
using SisVenda.Controller;
using SisVendas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisVendas.Controller
{
    class controllerProduto
    {
        public string cadastroProduto(modelProduto mProduto)
        {
            string sql = "insert into produto(codigobarras, nomeproduto, validade, precocusto," +
            " precovenda, descricao, quantidade, idtipo, idmarca, cnpj) " +
            "values(@codigobarras, @nomeproduto, @validade, @precocusto, @precovenda, @descricao, @quantidade, @idtipo, @idmarca, @cnpj)";

            Connection conexao = new Connection();
            NpgsqlConnection conn = conexao.conectaPG();
            NpgsqlCommand comm = new NpgsqlCommand(sql, conn);

            try
            {
                comm.Parameters.AddWithValue("@codigobarras", mProduto.CodigoBarras);
                comm.Parameters.AddWithValue("@nomeproduto", mProduto.NomeProduto);
                comm.Parameters.AddWithValue("@validade", mProduto.Validade);
                comm.Parameters.AddWithValue("@precocusto", mProduto.PrecoCusto);
                comm.Parameters.AddWithValue("@precovenda", mProduto.PrecoVenda);
                comm.Parameters.AddWithValue("@descricao", mProduto.Descricao);
                comm.Parameters.AddWithValue("@quantidade", mProduto.Quantidade);
                comm.Parameters.AddWithValue("@idtipo", mProduto.IdTipo);
                comm.Parameters.AddWithValue("@idmarca", mProduto.IdMarca);
                comm.Parameters.AddWithValue("@cnpj", mProduto.Cnpj);

                comm.ExecuteNonQuery();
                return "Produto cadastrado com sucesso!";
            }
            catch (NpgsqlException erro)
            {
                return erro.ToString();
                //return "Erro ao cadastrar cliente!";
            }

        }
        public NpgsqlDataReader pesquisaProdutoNome(modelProduto mProduto)
        {
            string sql = "SELECT p.nomeproduto AS \"Produto\",  p.codigobarras AS \"Código de Barras\", p.validade AS \"Validade\",p.precocusto AS \"Preço de Custo\",p.precovenda AS \"Preço de Venda\", p.descricao AS \"Descrição\",p.quantidade AS \"Quantidade\",f.nomefornecedor AS \"Fornecedor\",tp.nometipo AS \"Tipo\",m.nomemarca AS \"Marca\" FROM produto p INNER JOIN fornecedor f ON p.cnpj = f.cnpj INNER JOIN tipo tp ON p.idtipo = tp.idtipo INNER JOIN marca m ON p.idmarca = m.idmarca WHERE p.nomeproduto like @nomeproduto";

            Connection conexao = new Connection();
            NpgsqlConnection conn = conexao.conectaPG();
            NpgsqlCommand comm = new NpgsqlCommand(sql, conn);

            try
            {
                comm.Parameters.AddWithValue("@nomeproduto", mProduto.NomeProduto);

                return comm.ExecuteReader();
            }
            catch (NpgsqlException erro)
            {
                return null;
            }
        }

        public NpgsqlDataReader pesquisaProdutoVenda(modelProduto mProduto)
        {
            string sql = "SELECT  codigobarras AS \"Codigo\", nomeproduto AS \"Produto\",precovenda AS \"Preço de Venda\",quantidade AS \"Quantidade\" FROM produto  WHERE codigobarras = codigobarras or nomeproduto like nomeproduto\r\n";

            Connection conexao = new Connection();
            NpgsqlConnection conn = conexao.conectaPG();
            NpgsqlCommand comm = new NpgsqlCommand(sql, conn);

            try
            {
                comm.Parameters.AddWithValue("@codigobarras", mProduto.CodigoBarras);
                comm.Parameters.AddWithValue("@nomeproduto", mProduto.NomeProduto);
                

                return comm.ExecuteReader();
            }
            catch (NpgsqlException erro)
            {
                return null;
            }
        }
    }
}
