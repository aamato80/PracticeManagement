using DossierManagement.Api.DTOs;
using DossierManagement.Dal.Enums;
using DossierManagement.Dal.Models;

namespace DossierManagement.Api.Services
{
    public interface ITokenService
    {
        TokenDto Generate();
    }
}
