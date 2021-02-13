using DatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DatabaseImplement
{
    public class KursachDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=PC;Initial Catalog=KursachDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }

       public virtual DbSet<Diagnosis> Diagnosiss { set; get; }
        public virtual DbSet<Doctor> Doctors { set; get; }
        public virtual DbSet<Service> Services { set; get; }
        public virtual DbSet<Ward> Wards { set; get; }
        public virtual DbSet<Pacient> Pacients { set; get; }
        public virtual DbSet<Healing> Healings { set; get; }
        public virtual DbSet<PacientWard> PacientWards { set; get; }
        public virtual DbSet<DiagnosisService> DiagnosisServices { set; get; }
        public virtual DbSet<HealingServise> HealingServises { set; get; }
    }
}
