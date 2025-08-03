using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inova.Models;
using Microsoft.EntityFrameworkCore;

namespace Inova.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }

        public DbSet<TabelaModel> Itens { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }
    }
}