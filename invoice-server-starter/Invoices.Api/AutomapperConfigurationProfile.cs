﻿/*  _____ _______         _                      _
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
using Invoices.Api.Models;
using Invoices.Data.Models;

namespace Invoices.Api;

public class AutomapperConfigurationProfile : Profile
{
    public AutomapperConfigurationProfile()
    {

        CreateMap<Invoice, InvoiceDto>()
            .ReverseMap()
            .ForMember(dest => dest.Seller, opt => opt.Ignore())
            .ForMember(dest => dest.Buyer, opt => opt.Ignore());
        CreateMap<InvoiceDto,Invoice>();
        CreateMap<Person, PersonDto>();
        CreateMap<PersonDto, Person>();
    }
}