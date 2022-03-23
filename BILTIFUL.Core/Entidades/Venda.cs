﻿using BILTIFUL.Core.Entidades.Base;
using System;
using System.Collections.Generic;

namespace BILTIFUL.Core.Entidades
{
    public class Venda : EntidadeBase, IEntidadeDataBase<Venda>
    {
        public DateTime DataVenda { get; set; } = DateTime.Now;
        //CPF
        public string Cliente { get; set; }
        public float ValorTotal { get; set; }
        public List<ItemVenda> Itens { get; set; }

        public Venda()
        {
            Itens = new List<ItemVenda>();
        }

        public Venda(string cliente, float valorTotal)
        {
            Cliente = cliente;
            ValorTotal = valorTotal;
            Itens = new List<ItemVenda>();
        }

        public string Dados()
        {
            return $"--------------------Venda-----------------------\nId: {Id}\nCliente: {Cliente}\nValor total: {ValorTotal}\n-------------------------------------------";
        }
        public string ConverterParaDAT()
        {
            return $"{Id}{DataVenda.ToString("dd/MM/yyyy")}{Cliente.ToString().PadLeft(11, '0')}{ValorTotal}";
        }
        public string MostrarItemVenda()
        {
            return $"\n\t\t\t\t\tId = {Id}" +
                   $"\n\t\t\t\t\tCpf: {Cliente}" +
                   $"\n\t\t\t\t\tValor Total: {ValorTotal}";
        }

        public Venda ExtrairDados(string line)
        {
            if (line == null) return null;

            Id = int.Parse(line.Substring(0, 5));
            DataVenda = DateTime.Parse(line.Substring(5, 10));
            Cliente = line.Substring(15, 11);
            ValorTotal = int.Parse(line.Substring(26, 7));

            return this;
        }
    }
}
