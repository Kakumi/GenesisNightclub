using AutoMapper.Execution;
using GenesisNightclub.Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisNightclub.Repository.Contexts
{
    public class NightclubContext : DbContext
    {
        public NightclubContext(DbContextOptions<NightclubContext> options) : base(options)
        { 
            
        }

        public DbSet<MemberDTO> Members { get; set; }
        public DbSet<IdentityCardDTO> IdentityCards { get; set; }
        public DbSet<MemberCardDTO> MemberCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MemberDTO>(entity =>
            {
                entity.ToTable("Member");
                entity.HasOne(x => x.IdentityCard)
                    .WithOne(x => x.Member)
                    .HasForeignKey<IdentityCardDTO>(x => x.MemberId);

                entity.HasOne(x => x.MemberCard)
                    .WithMany(x => x.Members)
                    .HasForeignKey(x => x.MemberCardId);
            });

            modelBuilder.Entity<IdentityCardDTO>().ToTable("IdentityCard")
                .HasOne(x => x.Member)
                .WithOne(x => x.IdentityCard)
                .HasForeignKey<MemberDTO>(x => x.IdentityCardNumber);

            modelBuilder.Entity<MemberCardDTO>().ToTable("MemberCard");
        }
    }
}
