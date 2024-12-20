using AutoMapper;
using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Invoices.Data.Interfaces;
using Invoices.Data.Models;

namespace Invoices.Api.Managers
{
    /// <summary>
    /// Manages person-related operations, extending the BaseManager with custom logic for the `Person` entity.
    /// </summary>
    public class PersonManager : BaseManager<PersonDto, Person>, IPersonManager
    {
        private readonly IPersonRepository personRepository; // Repository for handling person data access.
        private readonly IMapper mapper; // Mapper for converting between entities and DTOs.

        /// <summary>
        /// Initializes a new instance of the PersonManager class.
        /// </summary>
        /// <param name="personRepository">The person repository instance.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public PersonManager(IPersonRepository personRepository, IMapper mapper)
        {
            this.personRepository = personRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Gets all persons that are not hidden.
        /// </summary>
        /// <returns>A list of person DTOs.</returns>
        public override IList<PersonDto> GetAll()
        {
            IList<Person> persons = personRepository.GetAllByHidden(false); // Retrieve all visible persons.
            return mapper.Map<IList<PersonDto>>(persons); // Map entities to DTOs.
        }

        /// <summary>
        /// Retrieves a person by their ID.
        /// </summary>
        /// <param name="Id">The ID of the person.</param>
        /// <returns>The person DTO or null if not found.</returns>
        public override PersonDto? Get(ulong Id)
        {
            Person? person = personRepository.FindById(Id); // Find the person by ID.

            if (person is null)
            {
                return null; // Return null if the person is not found.
            }

            return mapper.Map<PersonDto>(person); // Map the person to a DTO and return it.
        }

        /// <summary>
        /// Adds a new person.
        /// </summary>
        /// <param name="personDto">The DTO containing person details.</param>
        /// <returns>The added person DTO.</returns>
        public override PersonDto Add(PersonDto personDto)
        {
            Person person = mapper.Map<Person>(personDto); // Map DTO to entity.
            person.Id = default; // Reset the ID to ensure a new record is created.
            Person addedPerson = personRepository.Insert(person); // Insert the person into the repository.

            return mapper.Map<PersonDto>(addedPerson); // Map the added entity back to a DTO and return it.
        }

        /// <summary>
        /// Updates a person's information by creating a new version and hiding the old one.
        /// </summary>
        /// <param name="Id">The ID of the person to update.</param>
        /// <param name="updatedPersonDto">The updated person details.</param>
        /// <returns>The updated person DTO or null if the operation fails.</returns>
        public PersonDto? Update(ulong Id, PersonDto updatedPersonDto)
        {
            // Hide the existing person record.
            HidePerson(Id);

            // Map the updated DTO to a new person entity.
            Person person = mapper.Map<Person>(updatedPersonDto);
            person.Id = default; // Reset the ID for the new record.
            Person updatedPerson = personRepository.Insert(person); // Insert the new person record.

            return mapper.Map<PersonDto>(updatedPerson); // Map the updated entity back to a DTO and return it.
        }

        /// <summary>
        /// Deletes a person by marking them as hidden.
        /// </summary>
        /// <param name="Id">The ID of the person to delete.</param>
        /// <returns>Always returns null (as the person is not actually deleted).</returns>
        public override PersonDto? Delete(ulong Id)
        {
            HidePerson(Id); // Mark the person as hidden.
            return null;
        }

        /// <summary>
        /// Marks a person as hidden by setting the Hidden property to true.
        /// </summary>
        /// <param name="Id">The ID of the person to hide.</param>
        /// <returns>The updated person entity or null if the person is not found.</returns>
        private Person? HidePerson(ulong Id)
        {
            Person? person = personRepository.FindById(Id); // Find the person by ID.

            if (person is null)
                return null; // Return null if the person is not found.

            person.Hidden = true; // Mark the person as hidden.
            return personRepository.Update(person); // Update the person record.
        }

        /// <summary>
        /// Retrieves statistics for all persons, including their revenue.
        /// </summary>
        /// <returns>A list of person statistics DTOs.</returns>
        public async Task<List<PersonStatisticsDto>> GetPersonStatisticsAsync()
        {
            // Get raw statistics data from the repository.
            var rawStatistics = await personRepository.GetPersonStatisticsAsync();

            // Map the raw data to DTOs.
            var statisticsDto = rawStatistics.Select(r => new PersonStatisticsDto
            {
                PersonId = r.Id,
                PersonName = r.PersonName,
                Revenue = r.Revenue
            }).ToList();

            return statisticsDto; // Return the list of statistics DTOs.
        }
    }
}
