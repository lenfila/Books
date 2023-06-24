using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Books;

public partial class BooksDbContext : IdentityDbContext<User>
{
    public BooksDbContext()
    {
    }

    public BooksDbContext(DbContextOptions<BooksDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;DataBase=BooksDB;Username=postgres;Password=1234");
        optionsBuilder.UseSnakeCaseNamingConvention();;
    }




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("author_pkey");

            entity.ToTable("author");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Nameauthor)
                .HasColumnType("character varying")
                .HasColumnName("nameauthor");
            entity.Property(e => e.Patronimic)
                .HasColumnType("character varying")
                .HasColumnName("patronimic");
            entity.Property(e => e.Surname)
                .HasColumnType("character varying")
                .HasColumnName("surname");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("book_pkey");

            entity.ToTable("book");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.Category)
                .HasColumnType("character varying")
                .HasColumnName("category");
            entity.Property(e => e.Title)
                .HasColumnType("character varying")
                .HasColumnName("title");

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("fk_author");
        });

        modelBuilder.HasDefaultSchema("public");
        base.OnModelCreating(modelBuilder);

        //Переименование таблиц Identity к нижнему регистру
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var currentTableName = modelBuilder.Entity(entity.Name).Metadata.GetDefaultTableName();
            modelBuilder.Entity(entity.Name).ToTable(currentTableName.ToLower());
        }
        OnModelCreatingPartial(modelBuilder);
    }

 

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
