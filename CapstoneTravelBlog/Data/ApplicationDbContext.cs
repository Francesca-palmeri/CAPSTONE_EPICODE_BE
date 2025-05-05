using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using CapstoneTravelBlog.Models.Account;
using CapstoneTravelBlog.Models;

namespace CapstoneTravelBlog.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string,
        IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }

        public DbSet<Viaggio> Viaggi { get; set; }
        public DbSet<Prenotazione> Prenotazioni { get; set; }
        public DbSet<GiornoViaggio> GiorniViaggio { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<FraseUtile> FrasiUtili { get; set; }
        public DbSet<Commento> Commenti { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurazione relazione many-to-many: ApplicationUserRole
            modelBuilder.Entity<ApplicationUserRole>()
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired(true);

            modelBuilder.Entity<ApplicationUserRole>()
                .HasOne(p => p.User)
                .WithMany(p => p.ApplicationUserRoles)
                .HasForeignKey(p => p.UserId)
                .IsRequired(true);

            modelBuilder.Entity<ApplicationUserRole>()
                .HasOne(p => p.Role)
                .WithMany(p => p.ApplicationUserRoles)
                .HasForeignKey(p => p.RoleId)
                .IsRequired(true);

            // Relazione: ApplicationUser (1) -> (molte) Prenotazioni
            modelBuilder.Entity<Prenotazione>()
                .HasOne(p => p.Utente)
                .WithMany(u => u.Prenotazioni)
                .HasForeignKey(p => p.UtenteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relazione: Viaggio (1) -> (molte) Prenotazioni
            modelBuilder.Entity<Prenotazione>()
                .HasOne(p => p.Viaggio)
                .WithMany(v => v.Prenotazioni)
                .HasForeignKey(p => p.ViaggioId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relazione: Viaggio (1) -> (molti) GiorniViaggio
            modelBuilder.Entity<GiornoViaggio>()
                .HasOne(g => g.Viaggio)
                .WithMany(v => v.ProgrammaGiorni)
                .HasForeignKey(g => g.ViaggioId)
                .OnDelete(DeleteBehavior.Restrict);

          modelBuilder.Entity<Viaggio>()
                .Property( v => v.Prezzo)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Commento>()
            .HasOne(c => c.Utente)
            .WithMany()
            .HasForeignKey(c => c.UtenteId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Commento>()
                .HasOne(c => c.BlogPost)
                .WithMany(p => p.Commenti)
                .HasForeignKey(c => c.BlogPostId)
                .OnDelete(DeleteBehavior.Cascade);


            // Seed dei ruoli iniziali
            var adminId = "150ed783-a65c-48e5-841f-804aec8fce7e";
            var userId = "854de5cb-7457-4473-99fd-5cce61678553";

            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole
                {
                    Id = adminId,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = adminId
                },
                new ApplicationRole
                {
                    Id = userId,
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = userId
                }
            );
        }



    }
}

