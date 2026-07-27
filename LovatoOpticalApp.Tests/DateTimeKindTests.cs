using AutoMapper;
using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.Mappings;
using LovatoOpticalApp.Core.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace LovatoOpticalApp.Tests;

public class DateTimeKindTests
{
    [Fact]
    public void MappingCustomerAndRecipeDates_NormalizesToUtcKind()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<CustomerProfile>();
        });

        using var serviceProvider = services.BuildServiceProvider();
        var mapper = serviceProvider.GetRequiredService<IMapper>();

        var birthDay = new DateTime(1990, 1, 1, 10, 0, 0, DateTimeKind.Unspecified);
        var prescriptionDate = new DateTime(2024, 5, 10, 15, 30, 0, DateTimeKind.Unspecified);

        var customerDto = new CustomerResquestDto
        {
            Name = "Ana",
            CiRuc = "123456789",
            Phone = "0999999999",
            Email = "ana@test.com",
            Address = "Av. Siempre Viva",
            BirthDay = birthDay
        };

        var recipeDto = new RecipeRequestDto
        {
            Optometrist = "Dr. Pérez",
            PrescriptionIssueDate = prescriptionDate,
            VL_OD_ESF = "-1.00",
            VL_OD_CIL = "-0.25",
            VL_OD_EJE = "180",
            VL_OI_ESF = "-1.25",
            VL_OI_CIL = "-0.50",
            VL_OI_EJE = "180",
            VC_OD_ESF = "-0.50",
            VC_OD_CIL = "0.00",
            VC_OD_EJE = "10",
            VC_OI_ESF = "-0.75",
            VC_OI_CIL = "0.25",
            VC_OI_EJE = "5",
            Adicion = "0.00"
        };

        var customer = mapper.Map<Customer>(customerDto);
        var recipe = mapper.Map<Recipe>(recipeDto);

        Assert.NotNull(customer.BirthDay);
        Assert.Equal(DateTimeKind.Utc, customer.BirthDay!.Value.Kind);

        Assert.Equal(DateTimeKind.Utc, recipe.PrescriptionIssueDate.Kind);
    }
}
