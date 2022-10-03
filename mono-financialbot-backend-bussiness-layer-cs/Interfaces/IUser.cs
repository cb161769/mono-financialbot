namespace mono_financialbot_backend_bussiness_layer_cs.Interfaces
{
    public interface IUser
    {
        Task<ResponseBase> Register(UserRegistrationDto input);
        Task<ResponseBase> Login(LoginDto input);
    }
}
