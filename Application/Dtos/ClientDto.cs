using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public record CreateClientDto(string FirstName, string LastName, string Email, string PhoneNumber);
    public record UpdateClientDto(string FirstName, string LastName, string Email, string PhoneNumber);
    public record ClientDto(long Id, string FirstName, string LastName, string Email, string PhoneNumber);

}
