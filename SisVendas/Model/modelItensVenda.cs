using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisVendas.Model
{
    class modelItensVenda
    {
        private int idVenda;
        private string idProduto;
        private int quantidade;
        private float valorTotal;

        public int IdVenda { get => idVenda; set => idVenda = value; }
        public string IdProduto { get => idProduto; set => idProduto = value; }
        public int Quantidade { get => quantidade; set => quantidade = value; }
        public float ValorTotal { get => valorTotal; set => valorTotal = value; }
    }
}
