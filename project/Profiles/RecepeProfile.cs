

namespace backend_dev_project.Profiles;
public class RecepeProfile : Profile
{
    public RecepeProfile() {
        CreateMap<Recepe, RecepeDTO>(); 
    }
    
}
