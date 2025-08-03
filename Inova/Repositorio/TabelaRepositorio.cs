using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inova.Data;
using Inova.Models;

namespace Inova.Repositorio
{
    public class TabelaRepositorio : ITabelaRepositorio
    {
        private readonly BancoContext _bancoContext;

        public TabelaRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public TabelaModel ListarPorId(int id)
        {
            return _bancoContext.Itens.FirstOrDefault(x => x.Id == id);
        }
        public List<TabelaModel> BuscarTodos()
        {
            return _bancoContext.Itens.ToList();
        }
        public TabelaModel Adicionar(TabelaModel item)
        {
            
            item.DataCadastro = DateTime.Now;
            _bancoContext.Itens.Add(item);
            _bancoContext.SaveChanges();
            return item;
        }
        public TabelaModel Atualizar(TabelaModel item)
        {
            TabelaModel itemDB = ListarPorId(item.Id);

            if (itemDB == null) throw new Exception("Houve um erro ao atualizar o item");
            itemDB.NomeItem = item.NomeItem;
            itemDB.Tipo = item.Tipo;
            itemDB.NomePessoa = item.NomePessoa;
            itemDB.Celular = item.Celular;
            itemDB.Email = item.Email;

            _bancoContext.Itens.Update(itemDB);
            _bancoContext.SaveChanges();

            return itemDB;
        }

        public bool Apagar(int id)
        {
            TabelaModel itemDB = ListarPorId(id);

            if (itemDB == null) throw new Exception("Houve um erro ao apagar o item");
            
            _bancoContext.Itens.Remove(itemDB);
            _bancoContext.SaveChanges();

            return true;
        }
    }
}