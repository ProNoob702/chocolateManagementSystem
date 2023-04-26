using AutoMapper;
using ChocolateManagementSystem.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ChocolateManagementSystem.Application.Features.ChocolateBars.GetAllChocolateBarsByFactory;

public class GetChocolateBarsByFactoryQuery : IRequest<GetChocolateBarsByFactoryResponse>
{
}

public class GetChocolateBarsByFactoryQueryHandler : IRequestHandler<GetChocolateBarsByFactoryQuery, GetChocolateBarsByFactoryResponse>
{
    private readonly IChocolateSystemContext _context;
    private readonly IMapper _mapper;

    public GetChocolateBarsByFactoryQueryHandler(IChocolateSystemContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetChocolateBarsByFactoryResponse> Handle(GetChocolateBarsByFactoryQuery request, CancellationToken cancellationToken)
    {
        var chocolates = await _context.ChocolateBars.AsNoTracking()
            .Include(x => x.Factory)
            .OrderBy(x => x.Name).ToListAsync();

        var dic = new Dictionary<string, List<ChocolateBarDTO>>();

        foreach (var c in chocolates)
        {
            var factoryName = c.Factory.Name;
            if (!dic.ContainsKey(factoryName))
            {
                dic[factoryName] = new List<ChocolateBarDTO>();
            }

            var dto = _mapper.Map<ChocolateBarDTO>(c);
            dic[factoryName].Add(dto);
        }
        var readOnlyDic = dic.ToDictionary(k => k.Key, v => v.Value.AsReadOnly());

        return new GetChocolateBarsByFactoryResponse()
        {
            ChocolateBarsByFactory = readOnlyDic
        };
    }
}
