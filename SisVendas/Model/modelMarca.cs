﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisVendas.Model
{
    class modelMarca
    {
        private int idMarca;
        private string nomeMarca;

        public int IdMarca { get => idMarca; set => idMarca = value; }
        public string NomeMarca { get => nomeMarca; set => nomeMarca = value; }
    }
}
