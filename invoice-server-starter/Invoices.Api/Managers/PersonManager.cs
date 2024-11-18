/*  _____ _______         _                      _
 * |_   _|__   __|       | |                    | |
 *   | |    | |_ __   ___| |___      _____  _ __| | __  ___ ____
 *   | |    | | '_ \ / _ \ __\ \ /\ / / _ \| '__| |/ / / __|_  /
 *  _| |_   | | | | |  __/ |_ \ V  V / (_) | |  |   < | (__ / /
 * |_____|  |_|_| |_|\___|\__| \_/\_/ \___/|_|  |_|\_(_)___/___|
 *
 *                      ___ ___ ___
 *                     | . |  _| . |  LICENCE
 *                     |  _|_| |___|
 *                     |_|
 *
 *    REKVALIFIKAČNÍ KURZY  <>  PROGRAMOVÁNÍ  <>  IT KARIÉRA
 *
 * Tento zdrojový kód je součástí profesionálních IT kurzů na
 * WWW.ITNETWORK.CZ
 *
 * Kód spadá pod licenci PRO obsahu a vznikl díky podpoře
 * našich členů. Je určen pouze pro osobní užití a nesmí být šířen.
 * Více informací na http://www.itnetwork.cz/licence
 */

using AutoMapper;
using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Invoices.Data.Interfaces;
using Invoices.Data.Models;
using Invoices.Data.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Invoices.Api.Managers;

public class PersonManager : BaseManager<PersonDto, Person>,IPersonManager
{
    private readonly IPersonRepository personRepository;
    private readonly IMapper mapper;


    public PersonManager(IPersonRepository personRepository, IMapper mapper)
    {
        this.personRepository = personRepository;
        this.mapper = mapper;
    }


    public IList<PersonDto> GetAll()
    {
        IList<Person> persons = personRepository.GetAllByHidden(false);
        return mapper.Map<IList<PersonDto>>(persons);
    }

    public PersonDto? Get(uint personId)
    {
        Person? person = personRepository.FindById(personId);

        if (person is null)
        {
            return null;
        }

        return mapper.Map<PersonDto>(person);
    }

    public PersonDto Add(PersonDto personDto)
    {
        Person person = mapper.Map<Person>(personDto);
        person.Id = default;
        Person addedPerson = personRepository.Insert(person);

        return mapper.Map<PersonDto>(addedPerson);
    }

    public PersonDto? Update(uint personId, PersonDto updatedPersonDto)
    {
        /*var existingPerson = personRepository.FindById(personId);

        updatedPersonDto.IdentificationNumber = existingPerson.IdentificationNumber;*/

        HidePerson(personId);

        Person person = mapper.Map<Person>(updatedPersonDto);
        person.Id = default;
        Person updatedPerson = personRepository.Insert(person);

        return mapper.Map<PersonDto> (updatedPerson);
    }

    public override PersonDto? Delete(ulong Id)
    {
        HidePerson(Id);
        return null;
    }

    private Person? HidePerson(ulong Id)
    {
        Person? person = personRepository.FindById(Id);

        if (person is null)
            return null;

        person.Hidden = true;
        return personRepository.Update(person);
    }
}