using PracticeManagement.Api.DTOs;
using PracticeManagement.Dal.Enums;
using PracticeManagement.Dal.Models;

namespace PracticeManagement.Api.Services
{
    public interface ITokenService
    {
        TokenDto Generate();
        void Validate(string s);
    }
}
