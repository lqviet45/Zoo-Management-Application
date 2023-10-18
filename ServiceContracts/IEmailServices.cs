using ServiceContracts.DTO.EmailDTO;

namespace ServiceContracts
{
	public interface IEmailServices
	{
		Task SendEmail(EmailDto emailDto);
	}
}
