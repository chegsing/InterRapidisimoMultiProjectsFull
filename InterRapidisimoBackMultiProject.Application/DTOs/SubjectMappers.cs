using Domain.Entities;

namespace InterRapidisimoBackMultiProject.Application.DTOs;

/// <summary>Traduce entidades del dominio <see cref="Subject"/> a los DTOs que expone la Application.</summary>
public static class SubjectMappers
{
    public static SubjectDto ToDto(this Subject subject, Professor professor) => new(
        subject.Id.Value,
        subject.Code.Value,
        subject.Name.Value,
        subject.Credits.Value,
        professor.Id.Value,
        professor.PersonName.FullName);
}
