using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations;

/// <summary>
/// Class of objective configurations. 
/// Setting relation with employees and projects.
/// </summary>
internal class ObjectiveConfiguration : IEntityTypeConfiguration<Objective>
{
    public void Configure(EntityTypeBuilder<Objective> builder)
    {
        builder.HasKey(o => o.Id);

        builder
            .HasOne(o => o.Executor)
            .WithMany(e => e.Objectives)
            .HasForeignKey(o => o.ExecutorId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasOne(o => o.Project)
            .WithMany(p => p.Objectives)
            .HasForeignKey(o => o.ProjectId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
