namespace backend_dev_project.Services;

public interface IRecepeService {
    Task<List<Recepe>> GetRecepes();
    Task<Recepe> GetRecepe(string recepeId);
    Task<Recepe> AddRecepe( Recepe r );
    Task<Recepe> UpdateRecepe( Recepe r );
    Task DeleteRecepe( string recepeId );
}

public class RecepeService : IRecepeService {

    private readonly IRecepeRepository _recepeRepository;

    public RecepeService(IRecepeRepository recepeRepository) {
        _recepeRepository = recepeRepository;
    }

    public async Task<List<Recepe>> GetRecepes() {
        return await _recepeRepository.GetRecepes();
    }

    public async Task<Recepe> GetRecepe(string recepeId) {
        return await _recepeRepository.GetRecepe(recepeId);
    }

    public async Task<Recepe> AddRecepe( Recepe r ) {
        return await _recepeRepository.AddRecepe(r);
    }

    public async Task<Recepe> UpdateRecepe( Recepe r ) {
        return await _recepeRepository.UpdateRecepe(r);
    }
    public async Task DeleteRecepe( string recepeId ) {
        await _recepeRepository.DeleteRecepe(recepeId);
    }
}