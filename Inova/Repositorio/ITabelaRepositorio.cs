using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inova.Models;

namespace Inova.Repositorio
{
    public interface ITabelaRepositorio
    {
        TabelaModel ListarPorId(int id);
        List<TabelaModel> BuscarTodos();
        TabelaModel Adicionar(TabelaModel item);
        TabelaModel Atualizar(TabelaModel item);
        bool Apagar(int id);
    }
}