using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Services;

/// <inheritdoc cref="IClassmateDirectoryService"/>
public sealed class ClassmateDirectoryService : IClassmateDirectoryService
{
    public IReadOnlyDictionary<SubjectId, IReadOnlyCollection<StudentDirectoryEntry>> GetClassmatesBySubject(
        StudentId requestingStudentId,
        IReadOnlyCollection<Enrollment> allEnrollments,
        IReadOnlyCollection<Student> allStudents)
    {
        var studentsById = allStudents.ToDictionary(s => s.Id);

        var subjectsOfRequester = allEnrollments
            .Where(e => e.StudentId.Equals(requestingStudentId))
            .Select(e => e.SubjectId)
            .ToHashSet();

        return subjectsOfRequester.ToDictionary(
            subjectId => subjectId,
            subjectId => (IReadOnlyCollection<StudentDirectoryEntry>)allEnrollments
                .Where(e => e.SubjectId.Equals(subjectId) && !e.StudentId.Equals(requestingStudentId))
                .Where(e => studentsById.ContainsKey(e.StudentId))
                .Select(e => studentsById[e.StudentId].ToDirectoryEntry())
                .ToList());
    }
}
