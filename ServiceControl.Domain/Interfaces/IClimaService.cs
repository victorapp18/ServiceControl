namespace ServiceControl.Domain.Interfaces
{
    public interface IClimaService
    {
        Task<double> ObterClimaAsync(string cidade);
    }
}
