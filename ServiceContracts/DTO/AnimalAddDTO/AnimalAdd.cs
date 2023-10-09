using ServiceContracts.DTO.AnimalDTO;
using System.ComponentModel.DataAnnotations;
using ServiceContracts.DTO.AnimalUserDTO;

namespace ServiceContracts.DTO.AnimalAddDTO
{
    public class AnimalAdd
    {
        [Required(ErrorMessage = "AnimalAddRequset Can not be blank!")]
        public AnimalAddRequest? AnimalAddRequest { get; set; }

        [Required(ErrorMessage = "Zoo Trainer ID Can not be blank!")]
        public long userId { get; set; }

        [Required(ErrorMessage = "Cage ID Can not be blank!")]
        public int cageId { get; set; }
    }
}
