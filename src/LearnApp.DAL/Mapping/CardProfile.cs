using AutoMapper;
using LearnApp.DAL.DTO;
using LearnApp.DAL.Models;

namespace LearnApp.DAL.Mapping
{
    /// <summary>
    /// Card profile which sereves for mapping cards.
    /// </summary>
    public class CardProfile : Profile
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public CardProfile()
        {
            CreateMap<CardDto, Card>()
               .ForMember(card => card.Definition, opt => opt.MapFrom(dto => string.Join(", ", dto.Definition)));
        }
    }
}
