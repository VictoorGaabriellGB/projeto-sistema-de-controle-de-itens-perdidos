using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Inova.Models;
using Inova.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace Inova.Controllers
{
    public class TabelaController : Controller
    {
        private readonly ITabelaRepositorio _tabelaRepositorio;

        public TabelaController(ITabelaRepositorio tabelaRepositorio)
        {
            _tabelaRepositorio = tabelaRepositorio;
        }

           public IActionResult Tabela()
        {
          List<TabelaModel> itens = _tabelaRepositorio.BuscarTodos();
            return View(itens);
        }
        public IActionResult Criar()
        {
            return View();
        }
        public IActionResult Editar(int id)
        {
            TabelaModel item = _tabelaRepositorio.ListarPorId(id);
            return View(item);
        }

        public IActionResult Deletar(int id)
        {
            TabelaModel item = _tabelaRepositorio.ListarPorId(id);
            return View(item);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _tabelaRepositorio.Apagar(id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Item apagado com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Ops, não foi possível apagar o item!";

                }
                return RedirectToAction("Tabela");
            }
            catch (System.Exception erro)
            {

                TempData["MensagemErro"] = $"Ops, não foi possível apagar o item! Erro: {erro.Message}";
                return RedirectToAction("Tabela");
            }
        }

        [HttpPost]
        public IActionResult Criar(TabelaModel item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    item = _tabelaRepositorio.Adicionar(item);
                    TempData["MensagemSucesso"] = "Item adicionado com sucesso!";
                    return RedirectToAction("Tabela");
                }
                 return View(item);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não foi possível adicionar o item! Erro: {erro.Message}";
                return View("Tabela");
            }
        }

        [HttpPost]
        public IActionResult Alterar(TabelaModel item)
        {
            try
            {
                if (ModelState.IsValid)
            {
                TempData["MensagemSucesso"] = "Informações do item alteradas com sucesso!";
                _tabelaRepositorio.Atualizar(item);
                return RedirectToAction("Tabela");
            }
            return View("Editar",item);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não foi possível editar as informações do item! Erro: {erro.Message}";
                return View("Editar", item);
            }
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}