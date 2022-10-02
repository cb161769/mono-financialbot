

namespace mono_financialbot_backend_cs_datalayer.Mappings.Profiles
{
    public class CommonProfile: Profile
    {
        public CommonProfile()
        {
            #region UserProfile
            CreateMap<User, UserDto>().ReverseMap();
            #endregion
        }
    }
}
