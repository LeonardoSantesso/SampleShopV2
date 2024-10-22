using Application.Mappings;
using AutoMapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace SampleShopV2.Tests.Base;

public abstract class TestBase : IDisposable
{
    private readonly DbContextOptions<SampleShopV2Context> _options;

    protected TestBase()
    {
        _options = new DbContextOptionsBuilder<SampleShopV2Context>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }

    protected IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });

        return config.CreateMapper();
    }

    protected SampleShopV2Context CreateDbContext()
    {
        var context = new SampleShopV2Context(_options);

        context.Database.EnsureCreated();
        return context;
    }

    public void Dispose()
    {
        using var context = new SampleShopV2Context(_options);
        context.Database.EnsureDeleted();
    }
}


