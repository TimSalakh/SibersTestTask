using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations;

internal class ObjectiveConfiguration : IEntityTypeConfiguration<Objective>
{
    public void Configure(EntityTypeBuilder<Objective> builder)
    {
        builder.HasKey(o => o.Id);

        builder
            .HasOne(o => o.Creator)
            .WithMany()
            .HasForeignKey(o => o.CreatorId)
            .IsRequired();

        builder
            .HasOne(o => o.Executor)
            .WithMany(e => e.Objectives)
            .HasForeignKey(o => o.ExecutorId)
            .IsRequired();

        builder
            .HasOne(o => o.Project)
            .WithMany(p => p.Objectives)
            .HasForeignKey(o => o.ProjectId)
            .IsRequired();
    }
}
