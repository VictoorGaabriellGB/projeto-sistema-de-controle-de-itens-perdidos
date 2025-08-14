using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Inova.Data;
using Inova.Filters;
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
        private readonly BancoContext _context;

        public TabelaController(BancoContext context, ITabelaRepositorio tabelaRepositorio)
        {
            _context = context;
            _tabelaRepositorio = tabelaRepositorio;
        }
        
        [PaginaRestritaSomenteAdmin]
        public IActionResult Tabela()
        {
            List<TabelaModel> itens = _tabelaRepositorio.BuscarTodos();
            return View(itens);
        }
        [PaginaRestritaSomenteAdmin]
        public IActionResult Criar()
        {
            return View();
        }


        [PaginaParaUsuarioLogado]
        public IActionResult CriarHome()
        {
            return View();
        }
        [PaginaRestritaSomenteAdmin]
        public IActionResult Editar(int id)
        {
            TabelaModel item = _tabelaRepositorio.ListarPorId(id);
            return View(item);
        }
        [PaginaRestritaSomenteAdmin]
        public IActionResult Deletar(int id)
        {
            TabelaModel item = _tabelaRepositorio.ListarPorId(id);
            return View(item);
        }
       [PaginaRestritaSomenteAdmin]
        public IActionResult Apagar(int id)
        {
            try
            {
                var item = _tabelaRepositorio.ListarPorId(id);
                if (item != null && !string.IsNullOrEmpty(item.CaminhoImagem))
                {
                    // caminho físico
                    string caminhoFisico = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", item.CaminhoImagem.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                    if (System.IO.File.Exists(caminhoFisico))
                    {
                        System.IO.File.Delete(caminhoFisico);
                    }
                }

                bool apagado = _tabelaRepositorio.Apagar(id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Item apagado com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Não foi possível apagar o item.";
                }
                return RedirectToAction("Tabela");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao apagar: {erro.Message}";
                return RedirectToAction("Tabela");
            }
}


        [HttpPost]
        [PaginaRestritaSomenteAdmin]
        public async Task<IActionResult> Criar(TabelaModel item, IFormFile imagem)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(item);

                if (imagem != null && imagem.Length > 0)
                {
                    // Pasta wwwroot/uploads
                    string pastaUploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                    if (!Directory.Exists(pastaUploads))
                        Directory.CreateDirectory(pastaUploads);

                    // Cria nome único e obtém a extensão original
                    string nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(imagem.FileName);

                    string caminhoCompleto = Path.Combine(pastaUploads, nomeArquivo);

                    // Opcional: checar tamanho / tipo
                    // Exemplo: limitar a 5 MB
                    long maxBytes = 5 * 1024 * 1024;
                    if (imagem.Length > maxBytes)
                    {
                        TempData["MensagemErro"] = "Imagem muito grande. Max 5 MB.";
                        return View(item);
                    }

                    // Salva arquivo no disco
                    using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                    {
                        await imagem.CopyToAsync(stream);
                    }

                    // Caminho público que será salvo no banco (usado em <img src="...">)
                    item.CaminhoImagem = "/uploads/" + nomeArquivo;
                }

                item = _tabelaRepositorio.Adicionar(item);
                TempData["MensagemSucesso"] = "Item adicionado com sucesso!";
                return RedirectToAction("Tabela");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não foi possível adicionar o item! Erro: {erro.Message}";
                return View(item);
            }
        }

        [PaginaParaUsuarioLogado]
        [HttpPost]
        public IActionResult Criar2(TabelaModel item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    item = _tabelaRepositorio.Adicionar(item);
                    TempData["MensagemSucessoHome"] = "Item adicionado com sucesso! Aguarde o retorno da nossa equipe.";
                    return RedirectToAction("Index", "Home");
                }
                return View(item);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErroHome"] = $"Ops, não foi possível adicionar seu item! Erro: {erro.Message}";
                return RedirectToAction("Index", "Home");
            }
        }

        [PaginaRestritaSomenteAdmin]
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
                return View("Editar", item);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não foi possível editar as informações do item! Erro: {erro.Message}";
                return View("Editar", item);
            }
        }

        [PaginaParaUsuarioLogado]
        public IActionResult ListaPublica()
        {
            var itens = _context.Itens
                .Select(x => new TabelaModel
                {
                   Id = x.Id,
                   NomeItem = x.NomeItem,
                   Tipo = x.Tipo,
                   Local = x.Local,
                   DataAchado = x.DataAchado,
                   CaminhoImagem = x.CaminhoImagem
                })
                .ToList();

            return View(itens);
        }

        [PaginaParaUsuarioLogado]
        public IActionResult ObterImagem(int id)
        {
            var item = _context.Itens.FirstOrDefault(x => x.Id == id);
            if (item == null || item.CaminhoImagem == null)
            {
                return NotFound();


            }
            return File(item.CaminhoImagem, "image/jpeg"); // ajuste o tipo caso use PNG
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}