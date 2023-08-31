using Authentication_and_Authorization.Models;
using Authentication_and_Authorization.Request;
using AutoMapper;

namespace Authentication_and_Authorization.AuthProfiles
{
    public class Profiles:Profile
    {
        public Profiles()
        {
            CreateMap<User, AddUser>().ReverseMap();
            CreateMap<Product, AddProduct>().ReverseMap();
        }
      
    }
}
