namespace CleanArchitecture.Domain.Vehiculos;

public interface IUserRepository
{
    Task<Vehiculo?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
