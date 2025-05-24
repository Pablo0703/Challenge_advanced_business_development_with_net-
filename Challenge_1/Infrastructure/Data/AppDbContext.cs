using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<EnderecoEntity> Enderecos { get; set; }
        public DbSet<NotaFiscalEntity> NotasFiscais { get; set; }
        public DbSet<StatusMotoEntity> StatusMotos { get; set; }
        public DbSet<StatusOperacaoEntity> StatusOperacoes { get; set; }
        public DbSet<TipoMotoEntity> TiposMoto { get; set; }
        public DbSet<FilialEntity> Filiais { get; set; }
        public DbSet<PatioEntity> Patios { get; set; }
        public DbSet<ZonaPatioEntity> ZonasPatio { get; set; }
        public DbSet<MotoEntity> Motos { get; set; }
        public DbSet<MotociclistaEntity> Motociclistas { get; set; }
        public DbSet<LocalizacaoMotoEntity> LocalizacoesMoto { get; set; }
        public DbSet<HistoricoLocalizacaoEntity> HistoricosLocalizacao { get; set; }
    }
}
